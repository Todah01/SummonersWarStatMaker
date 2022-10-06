using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monster_dropdown_control : MonoBehaviour
{
    public List<string> op_title = new List<string>();
    public Dropdown dropdown;
    public InputField monster_name_str;
    public int monster_value = 0;
    public string monster_name_by_value = "";

    private void Awake()
    {
        SetDropdownList();
        StatCheckFunction_UI(0);
    }
    //private void Update()
    //{
    //    dropdown.onValueChanged.AddListener(delegate
    //    {
    //        Function_Dropdown(dropdown);
    //    });
    //}
    public void Function_Dropdown()
    {
        monster_value = dropdown.value;
        monster_name_by_value = dropdown.options[dropdown.value].text;
        StatCheckFunction_UI(dropdown.value);
    }
    public void SetDropdownList()
    {
        string search_str = monster_name_str.text;
        List<Dictionary<string, object>> monster_names = CSVReader.Read("monster_name_data");
        
        for (int i = 0; i < monster_names.Count; i++)
        {
            Debug.Log(monster_names[i].ContainsKey(search_str));
            if(monster_names[i].ContainsKey(search_str))
                op_title.Add(monster_names[i]["Name"].ToString());
        }
    }
    private void StatCheckFunction_UI(int value)
    {
        SetDropdownList();
        // dropdown.onValueChanged.RemoveAllListeners();
        dropdown.options.Clear();

        for (int i = 0; i < op_title.Count; i++)
        {
            Dropdown.OptionData newData = new Dropdown.OptionData();
            newData.text = op_title[i];
            dropdown.options.Add(newData);
        }

        //dropdown.SetValueWithoutNotify(-1);
        //dropdown.SetValueWithoutNotify(0);

        dropdown.value = value;
        dropdown.itemText.text = op_title[dropdown.value];
    }
}
