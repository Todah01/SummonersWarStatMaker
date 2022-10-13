using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monster_dropdown_control : MonoBehaviour
{
    public List<string> op_title = new List<string>();
    public Dropdown dropdown;
    public InputField monster_name_str;
    public int monster_value;
    public string monster_name_by_value;

    private void Awake()
    {
        SetDropdownList();
        SetDropdown(0);
    }
    public void OnValueChangeOfDropdown()
    {
        monster_value = dropdown.value;
        monster_name_by_value = "";
        for (int idx = 0; idx < dropdown.options[dropdown.value].text.Length; idx++)
        {
            if (dropdown.options[dropdown.value].text[idx + 1] == '(' && dropdown.options[dropdown.value].text[idx] == ' ')
                break;
            monster_name_by_value += dropdown.options[dropdown.value].text[idx];
        }
        SetDropdown(dropdown.value);
    }
    public void SetDropdownList()
    {
        dropdown.options.Clear();
        // string search_str = monster_name_str.text;
        List<Dictionary<string, object>> monster_names = CSVReader.Read("monster_name_merge");

        for (int i = 0; i < monster_names.Count; i++)
        {
            op_title.Add(monster_names[i]["Name"].ToString());
            //if (monster_names[i].ContainsKey(search_str))
            //    op_title.Add(monster_names[i]["Name"].ToString());
        }

        for (int i = 0; i < op_title.Count; i++)
        {
            Dropdown.OptionData newData = new Dropdown.OptionData();
            newData.text = op_title[i];
            dropdown.options.Add(newData);
        }
    }
    void SetDropdown(int value)
    {
        dropdown.value = value;
        dropdown.itemText.text = op_title[dropdown.value];
    }
}
