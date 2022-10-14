using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputfield_test : MonoBehaviour
{
    public GameObject ContentView;
    public GameObject ContentWindow;
    public RectTransform ItemParent;
    public RectTransform ItemPrefab;
    public string CheckMonsterName;
    public float levenshteinDistance;

    public void OnInputValueChanged()
    {
        ContentView.SetActive(true);
        ClearResults();
        FillResults(GetResults(this.GetComponent<InputField>().text));
    }
    private void SendMonster(string name)
    {
        string monster_name_remove = "";
        for (int idx = 0; idx < name.Length; idx++)
        {
            if (name[idx + 1] == '(' && name[idx] == ' ')
                break;
            monster_name_remove += name[idx];
        }

        this.GetComponent<InputField>().text = monster_name_remove;
        CheckMonsterName = monster_name_remove;
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

        if(results.Count != 0)
        {
            ContentWindowRect.sizeDelta = new Vector2(ContentWindowRect.sizeDelta.x, Mathf.Min(max_height, cur_height * results.Count));
            ContentViewRect.sizeDelta = new Vector2(ContentViewRect.sizeDelta.x, Mathf.Min(max_height, cur_height * results.Count));
        }

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
        List<Dictionary<string, object>> monster_names = CSVReader.Read("monster_name_merge");
        List<string> mockData = new List<string>();
        List<string> resultData = new List<string>();

        for (int i = 0; i < monster_names.Count; i++)
        {
            mockData.Add(monster_names[i]["Name"].ToString());
            // awakenData.Add(monster_names[i]["Awaken_Name"].ToString());
        }

        levenshteinDistance = Mathf.Clamp01(levenshteinDistance);
        string keywords = input.ToLower();

        for (int idx = 0; idx< mockData.Count; idx++)
        {
            int mock_distance = Kit.Extend.StringExtend.LevenshteinDistance(mockData[idx].ToLower(), keywords, caseSensitive: false);
            // int awaken_distance = Kit.Extend.StringExtend.LevenshteinDistance(awakenData[idx].ToLower(), keywords, caseSensitive: false);

            // int distance = Math.Min(mock_distance, awaken_distance);

            bool closeEnough = (int)(levenshteinDistance * mockData[idx].Length) > mock_distance;

            //if(distance < mockData[idx].Length)
            //{
            //    resultData.Add(mockData[idx]);
            //    mockData.RemoveAt(idx);
            //    idx--;
            //}

            if (closeEnough)
            {
                resultData.Add(mockData[idx]);
                mockData.RemoveAt(idx);
                idx--;
            }
        }

        return resultData;
    }
}
