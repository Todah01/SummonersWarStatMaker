using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class search_inputfield_control_exporter : MonoBehaviour
{
    #region Public Variable
    public GameObject ContentView;
    public GameObject ContentWindow;
    public RectTransform ItemParent;
    public RectTransform ItemPrefab;
    public string CheckMonsterName;
    public float levenshteinDistance;
    #endregion

    public void OnInputValueChanged()
    {
        // If user click search field, search result content view will set visible.
        ContentView.SetActive(true);

        // Reset search result content view.
        ClearResults();

        // Fill search result in search result content view.
        FillResults(GetResults(this.GetComponent<InputField>().text));
    }
    private void SendMonster(string name)
    {
        string monster_name_remove_blank = "";
        for (int idx = 0; idx < name.Length; idx++)
        {
            if (name[idx + 1] == '(')
                break;

            monster_name_remove_blank += name[idx];
        }

        this.GetComponent<InputField>().text = monster_name_remove_blank;
        CheckMonsterName = monster_name_remove_blank;
        ContentView.SetActive(false);
    }
    private void ClearResults()
    {
        // Reverse loop since you destroy children
        for (int childIndex = ItemParent.childCount - 1; childIndex >= 0; --childIndex)
        {
            Transform child = ItemParent.GetChild(childIndex);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }
    private void FillResults(List<string> results)
    {
        RectTransform ContentWindowRect = ContentWindow.GetComponent<RectTransform>();
        RectTransform ContentViewRect = ContentView.GetComponent<RectTransform>();
        float max_height = 400f;
        float cur_height = 45f;

        // Set scroll view size by results
        if (results.Count != 0)
        {
            ContentWindowRect.sizeDelta = new Vector2(ContentWindowRect.sizeDelta.x, cur_height * results.Count);
            ContentViewRect.sizeDelta = new Vector2(ContentViewRect.sizeDelta.x, Mathf.Min(max_height, cur_height * results.Count));
        }
        else
        {
            ContentWindowRect.sizeDelta = new Vector2(ContentWindowRect.sizeDelta.x, 0f);
            ContentViewRect.sizeDelta = new Vector2(ContentViewRect.sizeDelta.x, 0f);
        }

        // Instantiate result in scroll view
        for (int resultIndex = 0; resultIndex < results.Count; resultIndex++)
        {
            Transform child = Instantiate(ItemPrefab, ItemParent);
            child.GetComponentInChildren<Text>().text = results[resultIndex];
            child.SetParent(ItemParent);
            child.localPosition = new Vector2(0f, -cur_height * resultIndex);
        }
    }
    private List<string> GetResults(string input)
    {
        // Get monster name by CSV Files.
        List<Dictionary<string, object>> monster_names = CSVReader.Read("monster_name_merge");
        List<string> mockData = new List<string>();
        List<string> resultData = new List<string>();

        for (int i = 0; i < monster_names.Count; i++)
        {
            mockData.Add(monster_names[i]["Name"].ToString());
        }

        // Get string that user write in field.
        string keywords = input.ToLower().Replace(" ", "");

        for (int idx = 0; idx< mockData.Count; idx++)
        {
            #region levenshtein logic
            //int mock_distance = LevenKit.Extend.StringExtend.LevenshteinDistance(mockData[idx].ToLower().Replace(" ", ""), keywords, caseSensitive: false);
            //levenshteinDistance = Mathf.Clamp01(levenshteinDistance);
            //bool closeEnough = (int)(levenshteinDistance * mockData[idx].Length) > mock_distance;

            //if (closeEnough)
            //{
            //    resultData.Add(mockData[idx]);
            //    mockData.RemoveAt(idx);
            //    idx--;
            //}
            #endregion

            #region lcs logic
            int lcs_value = CommonKit.Extend.StringCommon.LongestCommonSubsequence(mockData[idx].ToLower().Replace(" ", ""), keywords);
            bool closeEnough = lcs_value >= keywords.Length;

            if (closeEnough)
            {
                resultData.Add(mockData[idx]);
                mockData.RemoveAt(idx);
                idx--;
            }
            #endregion
        }

        return resultData;
    }
}
