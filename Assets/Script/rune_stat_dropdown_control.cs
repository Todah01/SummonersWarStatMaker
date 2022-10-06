using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rune_stat_dropdown_control : MonoBehaviour
{
    //public Text message;
    //public Text result;
    //public Image result_img;
    public Dropdown dropdown;
    public string[] op_title;
    //public Button button;

    private void SetFunction_UI(object[] rune_numbers)
    {
        //Reset
        ResetFunction_UI((int)rune_numbers[1], (int)rune_numbers[2]);

        //button.onClick.AddListener(Function_Button);
        dropdown.onValueChanged.AddListener(delegate {
            Function_Dropdown(dropdown);
        });
    }

    private void Function_Dropdown(Dropdown select)
    {
        string op = select.options[select.value].text;
        //message.text = op;
        gameObject.SendMessageUpwards("Set_Stat_Value", dropdown.value);
        gameObject.SendMessageUpwards("Set_Stat_String", op_title[dropdown.value]);
        // Debug.Log("Dropdown Change!\n" + op);
    }
    void Set_Options(int rune_number)
    {
        switch(rune_number)
        {
            case 2:
                op_title = new string[] { "* Select a rune set *", "SPD", "HP", "DEF", "ATK" };
                break;
            case 4:
                op_title = new string[] { "* Select a rune set *", "CRI RATE", "CRI DMG", "HP", "DEF", "ATK" };
                break;
            case 6:
                op_title = new string[] { "* Select a rune set *", "ACC", "RES", "HP", "DEF", "ATK" };
                break;
        }
    }
    private void ResetFunction_UI(int rune_number, int stat_number)
    {
        if(rune_number % 2 == 0) 
        {
            //button.onClick.RemoveAllListeners();
            dropdown.onValueChanged.RemoveAllListeners();
            dropdown.options.Clear();

            Set_Options(rune_number);

            for (int i = 0; i < op_title.Length; i++)
            {
                Dropdown.OptionData newData = new Dropdown.OptionData();
                newData.text = op_title[i];
                dropdown.options.Add(newData);
            }
            dropdown.SetValueWithoutNotify(-1);
            dropdown.SetValueWithoutNotify(0);

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
