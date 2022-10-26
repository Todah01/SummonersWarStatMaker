using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rune_stat_select_control : MonoBehaviour
{
    #region Public Variable
    public string[] op_title;
    public int stat_value = 0;
    public string stat_string;
    #endregion

    #region Local Variable
    Dropdown dropdown;
    #endregion

    private void Awake()
    {
        StatCheckFunction_UI(0);
        dropdown = this.GetComponent<Dropdown>();
    }
    public void Function_Dropdown()
    {
        StatCheckFunction_UI(dropdown.value);

        stat_value = dropdown.value;
        if (dropdown.options[dropdown.value].text != "* Select a stat *")
            stat_string = dropdown.options[dropdown.value].text;
        else
            stat_string = "";
    }

    private void StatCheckFunction_UI(int stat_value)
    {
        dropdown.options.Clear();

        for (int i = 0; i < op_title.Length; i++)
        {
            Dropdown.OptionData newData = new Dropdown.OptionData();
            newData.text = op_title[i];
            dropdown.options.Add(newData);
        }

        dropdown.value = stat_value;
        dropdown.itemText.text = op_title[dropdown.value];
    }
}
