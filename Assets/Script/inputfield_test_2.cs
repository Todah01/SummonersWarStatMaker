﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public sealed class EditorExtend
{
    #region Text AutoComplete
    /// <summary>The internal struct used for AutoComplete (Editor)</summary>
    private struct EditorAutoCompleteParams
    {
        public const string FieldTag = "AutoCompleteField";
        public static readonly Color FancyColor = new Color(.6f, .6f, .7f);
        public static readonly float optionHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        public const int fuzzyMatchBias = 3; // input length smaller then this letter, will not trigger fuzzy checking.
        public static List<string> CacheCheckList = null;
        public static string lastInput;
        public static string focusTag = "";
        public static string lastTag = ""; // Never null, optimize for length check.
        public static int selectedOption = -1; // record current selected option.
        public static Vector2 mouseDown;

        public static void CleanUpAndBlur()
        {
            selectedOption = -1;
            GUI.FocusControl("");
        }
    }

    /// <summary>A textField to popup a matching popup, based on developers input values.</summary>
    /// <param name="input">string input.</param>
    /// <param name="source">the data of all possible values (string).</param>
    /// <param name="maxShownCount">the amount to display result.</param>
    /// <param name="levenshteinDistance">
    /// value between 0f ~ 1f, (percent)
    /// - more then 0f will enable the fuzzy matching
    /// - 1f = 100% error threshold = anything thing is okay.
    /// - 0f = 000% error threshold = require full match to the reference
    /// - recommend 0.4f ~ 0.7f
    /// </param>
    /// <returns>output string.</returns>
    public static string TextFieldAutoComplete(string input, string[] source, int maxShownCount = 5, float levenshteinDistance = 0.5f)
    {
        return TextFieldAutoComplete(EditorGUILayout.GetControlRect(), input, source, maxShownCount, levenshteinDistance);
    }

    /// <summary>A textField to popup a matching popup, based on developers input values.</summary>
    /// <param name="position">EditorGUI position</param>
    /// <param name="input">string input.</param>
    /// <param name="source">the data of all possible values (string).</param>
    /// <param name="maxShownCount">the amount to display result.</param>
    /// <param name="levenshteinDistance">
    /// value between 0f ~ 1f, (percent)
    /// - more then 0f will enable the fuzzy matching
    /// - 1f = 100% error threshold = everything is okay.
    /// - 0f = 000% error threshold = require full match to the reference
    /// - recommend 0.4f ~ 0.7f
    /// </param>
    /// <returns>output string.</returns>
    public static string TextFieldAutoComplete(Rect position, string input, string[] source, int maxShownCount = 5, float levenshteinDistance = 0.5f)
    {
        // Text field
        int controlId = GUIUtility.GetControlID(FocusType.Passive);
        string tag = EditorAutoCompleteParams.FieldTag + controlId;
        GUI.SetNextControlName(tag);
        string rst = EditorGUI.TextField(position, input, EditorStyles.popup);

        // Matching with giving source
        if (input.Length > 0 && // have input
            (EditorAutoCompleteParams.lastTag.Length == 0 || EditorAutoCompleteParams.lastTag == tag) && // one frame delay for process click event.
            GUI.GetNameOfFocusedControl() == tag) // focus this control
        {
            // Matching
            if (EditorAutoCompleteParams.lastInput != input || // input changed
                EditorAutoCompleteParams.focusTag != tag) // switch focus from another field.
            {
                // Update cache
                EditorAutoCompleteParams.focusTag = tag;
                EditorAutoCompleteParams.lastInput = input;

                List<string> uniqueSrc = new List<string>(new HashSet<string>(source)); // remove duplicate
                int srcCnt = uniqueSrc.Count;
                EditorAutoCompleteParams.CacheCheckList = new List<string>(System.Math.Min(maxShownCount, srcCnt)); // optimize memory alloc
                // Start with - slow
                for (int i = 0; i < srcCnt && EditorAutoCompleteParams.CacheCheckList.Count < maxShownCount; i++)
                {
                    if (uniqueSrc[i].ToLower().StartsWith(input.ToLower()))
                    {
                        EditorAutoCompleteParams.CacheCheckList.Add(uniqueSrc[i]);
                        uniqueSrc.RemoveAt(i);
                        srcCnt--;
                        i--;
                    }
                }

                // Contains - very slow
                if (EditorAutoCompleteParams.CacheCheckList.Count == 0)
                {
                    for (int i = 0; i < srcCnt && EditorAutoCompleteParams.CacheCheckList.Count < maxShownCount; i++)
                    {
                        if (uniqueSrc[i].ToLower().Contains(input.ToLower()))
                        {
                            EditorAutoCompleteParams.CacheCheckList.Add(uniqueSrc[i]);
                            uniqueSrc.RemoveAt(i);
                            srcCnt--;
                            i--;
                        }
                    }
                }

                // Levenshtein Distance - very very slow.
                if (levenshteinDistance > 0f && // only developer request
                    input.Length > EditorAutoCompleteParams.fuzzyMatchBias && // bias on input, hidden value to avoid doing it too early.
                    EditorAutoCompleteParams.CacheCheckList.Count < maxShownCount) // have some empty space for matching.
                {
                    levenshteinDistance = Mathf.Clamp01(levenshteinDistance);
                    string keywords = input.ToLower();
                    for (int i = 0; i < srcCnt && EditorAutoCompleteParams.CacheCheckList.Count < maxShownCount; i++)
                    {
                        int distance = Kit.Extend.StringExtend.LevenshteinDistance(uniqueSrc[i], keywords, caseSensitive: false);
                        bool closeEnough = (int)(levenshteinDistance * uniqueSrc[i].Length) > distance;
                        if (closeEnough)
                        {
                            EditorAutoCompleteParams.CacheCheckList.Add(uniqueSrc[i]);
                            uniqueSrc.RemoveAt(i);
                            srcCnt--;
                            i--;
                        }
                    }
                }
            }

            // Draw recommend keyward(s)
            if (EditorAutoCompleteParams.CacheCheckList.Count > 0)
            {
                Event evt = Event.current;
                int cnt = EditorAutoCompleteParams.CacheCheckList.Count;
                float height = cnt * EditorAutoCompleteParams.optionHeight;
                Rect area = new Rect(position.x, position.y - height, position.width, height);

                // Fancy color UI
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.DrawRect(area, EditorAutoCompleteParams.FancyColor);
                GUI.Label(area, GUIContent.none, GUI.skin.button);
                EditorGUI.EndDisabledGroup();

                // Click event hack - part 1
                // cached data for click event hack.
                if (evt.type == EventType.Repaint)
                {
                    // Draw option(s), if we have one.
                    // in repaint cycle, we only handle display.
                    Rect line = new Rect(area.x, area.y, area.width, EditorAutoCompleteParams.optionHeight);
                    for (int i = 0; i < cnt; i++)
                    {
                        EditorGUI.ToggleLeft(line, GUIContent.none, (input == EditorAutoCompleteParams.CacheCheckList[i]));
                        Rect option = EditorGUI.IndentedRect(line);
                        if (line.Contains(evt.mousePosition))
                        {
                            // hover style
                            EditorGUI.LabelField(option, EditorAutoCompleteParams.CacheCheckList[i], GUI.skin.textArea);
                            EditorAutoCompleteParams.selectedOption = i;

                            GUIUtility.hotControl = controlId; // required for Cursor skin. (AddCursorRect)
                            EditorGUIUtility.AddCursorRect(area, MouseCursor.ArrowPlus);
                        }
                        else if (EditorAutoCompleteParams.selectedOption == i)
                        {
                            // hover style
                            EditorGUI.LabelField(option, EditorAutoCompleteParams.CacheCheckList[i], GUI.skin.textArea);
                        }
                        else
                        {
                            EditorGUI.LabelField(option, EditorAutoCompleteParams.CacheCheckList[i], EditorStyles.label);
                        }
                        line.y += line.height;
                    }

                    // when hover popup, record this as the last usein tag.
                    if (area.Contains(evt.mousePosition) && EditorAutoCompleteParams.lastTag != tag)
                    {
                        // Debug.Log("->" + tag + " Enter popup: " + area);
                        // used to trigger the clicked checking later.
                        EditorAutoCompleteParams.lastTag = tag;
                    }
                }
                else if (evt.type == EventType.MouseDown)
                {
                    if (area.Contains(evt.mousePosition) || position.Contains(evt.mousePosition))
                    {
                        EditorAutoCompleteParams.mouseDown = evt.mousePosition;
                    }
                    else
                    {
                        // click outside popup area, deselected - blur.
                        EditorAutoCompleteParams.CleanUpAndBlur();
                    }
                }
                else if (evt.type == EventType.MouseUp)
                {
                    if (position.Contains(evt.mousePosition))
                    {
                        // common case click on textfield.
                        return rst;
                    }
                    else if (area.Contains(evt.mousePosition))
                    {
                        if (Vector2.Distance(EditorAutoCompleteParams.mouseDown, evt.mousePosition) >= 3f)
                        {
                            // Debug.Log("Click and drag out the area.");
                            return rst;
                        }
                        else
                        {
                            // Click event hack - part 3
                            // for some reason, this session only run when popup display on inspector empty space.
                            // when any selectable field behind of the popup list, Unity3D can't reaching this session.
                            _AutoCompleteClickhandle(position, ref rst);
                            EditorAutoCompleteParams.focusTag = string.Empty; // Clean up
                            EditorAutoCompleteParams.lastTag = string.Empty; // Clean up
                        }
                    }
                    else
                    {
                        // click outside popup area, deselected - blur.
                        EditorAutoCompleteParams.CleanUpAndBlur();
                    }
                    return rst;
                }
                else if (evt.isKey && evt.type == EventType.KeyUp)
                {
                    switch (evt.keyCode)
                    {
                        case KeyCode.PageUp:
                        case KeyCode.UpArrow:
                            EditorAutoCompleteParams.selectedOption--;
                            if (EditorAutoCompleteParams.selectedOption < 0)
                                EditorAutoCompleteParams.selectedOption = EditorAutoCompleteParams.CacheCheckList.Count - 1;
                            break;
                        case KeyCode.PageDown:
                        case KeyCode.DownArrow:
                            EditorAutoCompleteParams.selectedOption++;
                            if (EditorAutoCompleteParams.selectedOption >= EditorAutoCompleteParams.CacheCheckList.Count)
                                EditorAutoCompleteParams.selectedOption = 0;
                            break;

                        case KeyCode.KeypadEnter:
                        case KeyCode.Return:
                            if (EditorAutoCompleteParams.selectedOption != -1)
                            {
                                _AutoCompleteClickhandle(position, ref rst);
                                EditorAutoCompleteParams.focusTag = string.Empty; // Clean up
                                EditorAutoCompleteParams.lastTag = string.Empty; // Clean up
                            }
                            else
                            {
                                EditorAutoCompleteParams.CleanUpAndBlur();
                            }
                            break;

                        case KeyCode.Escape:
                            EditorAutoCompleteParams.CleanUpAndBlur();
                            break;

                        default:
                            // hit any other key(s), assume typing, avoid override by Enter;
                            EditorAutoCompleteParams.selectedOption = -1;
                            break;
                    }
                }
            }
        }
        else if (EditorAutoCompleteParams.lastTag == tag &&
            GUI.GetNameOfFocusedControl() != tag)
        {
            // Click event hack - part 2
            // catching mouse click on blur
            _AutoCompleteClickhandle(position, ref rst);
            EditorAutoCompleteParams.lastTag = string.Empty; // reset
        }

        return rst;
    }

    /// <summary>calculate auto complete select option location, and select it.
    /// within area, and we display option in "Vertical" style.
    /// which line is what we care.
    /// </summary>
    /// <param name="rst">input string, may overrided</param>
    /// <param name="cnt"></param>
    /// <param name="area"></param>
    /// <param name="mouseY"></param>
    private static void _AutoCompleteClickhandle(Rect position, ref string rst)
    {
        int index = EditorAutoCompleteParams.selectedOption;
        Vector2 pos = EditorAutoCompleteParams.mouseDown; // hack: assume mouse are stay in click position (1 frame behind).

        if (0 <= index && index < EditorAutoCompleteParams.CacheCheckList.Count)
        {
            rst = EditorAutoCompleteParams.CacheCheckList[index];
            GUI.changed = true;
            // Debug.Log("Selecting index (" + EditorAutoCompleteParams.selectedOption + ") "+ rst);
        }
        else
        {
            // Fail safe, when selectedOption failure

            int cnt = EditorAutoCompleteParams.CacheCheckList.Count;
            float height = cnt * EditorAutoCompleteParams.optionHeight;
            Rect area = new Rect(position.x, position.y - height, position.width, height);
            if (!area.Contains(pos))
                return; // return early.

            float lineY = area.y;
            for (int i = 0; i < cnt; i++)
            {
                if (lineY < pos.y && pos.y < lineY + EditorAutoCompleteParams.optionHeight)
                {
                    rst = EditorAutoCompleteParams.CacheCheckList[i];
                    Debug.LogError("Fail to select on \"" + EditorAutoCompleteParams.lastTag + "\" selected = " + rst + "\ncalculate by mouse position.");
                    GUI.changed = true;
                    break;
                }
                lineY += EditorAutoCompleteParams.optionHeight;
            }
        }

        EditorAutoCompleteParams.CleanUpAndBlur();
    }
    #endregion
}