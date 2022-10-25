using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rune_stat_dropdown_control : MonoBehaviour
{
    #region Public Variable
    public string[] op_title;
    #endregion

    #region Local Variable
    Dropdown dropdown;
    #endregion
    private void Awake()
    {
        dropdown = this.GetComponent<Dropdown>();
        Reconstruction_dropdown(2, 0);
    }

    private void SetFunction_UI(object[] rune_numbers)
    {
        Reconstruction_dropdown((int)rune_numbers[1], (int)rune_numbers[2]);
    }

    public void Function_Dropdown()
    {
        gameObject.SendMessageUpwards("Set_Stat_Value", dropdown.value);
        gameObject.SendMessageUpwards("Set_Stat_String", op_title[dropdown.value]);
    }
    void Set_Options(int rune_number)
    {
        switch(rune_number)
        {
            case 2:
                op_title = new string[] { "* Select a stat *", "SPD", "HP", "DEF", "ATK" };
                break;
            case 4:
                op_title = new string[] { "* Select a stat *", "CRI RATE", "CRI DMG", "HP", "DEF", "ATK" };
                break;
            case 6:
                op_title = new string[] { "* Select a stat *", "ACC", "RES", "HP", "DEF", "ATK" };
                break;
        }
    }
    private void Reconstruction_dropdown(int rune_number, int stat_number)
    {
        if(rune_number % 2 == 0) 
        {
            dropdown.options.Clear();

            Set_Options(rune_number);

            for (int i = 0; i < op_title.Length; i++)
            {
                Dropdown.OptionData newData = new Dropdown.OptionData();
                newData.text = op_title[i];
                dropdown.options.Add(newData);
            }

            dropdown.value = stat_number;
            dropdown.itemText.text = op_title[dropdown.value];
        }
        else
        {
            dropdown.value = 0;
            dropdown.itemText.text = "-";
        }
    }
}
