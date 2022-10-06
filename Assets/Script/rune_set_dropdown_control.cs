using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rune_set_dropdown_control : MonoBehaviour
{
    //public Text message;
    //public Text result;
    //public Image result_img;
    #region Public Variable
    public Dropdown dropdown;
    public Sprite[] sprites;
    public string[] op_title;
    #endregion

    //public Button button;

    //void Start()
    //{
    //    SetFunction_UI();
    //}

    private void SetFunction_UI(object[] rune_numbers)
    {
        // Reset
        ResetFunction_UI((int)rune_numbers[0]);

        // button.onClick.AddListener(Function_Button);
        dropdown.onValueChanged.AddListener(delegate {
            Function_Dropdown(dropdown);
        });
    }

    //private void Function_Button()
    //{
    //    string op = dropdown.options[dropdown.value].text;
    //    //result.text = op;
    //    //result_img.sprite = dropdown.options[dropdown.value].image;
    //    Debug.LogError("Dropdown Result!\n" + op);
    //}

    private void Function_Dropdown(Dropdown select)
    {
        string op = select.options[select.value].text;
        Sprite op_sprite = select.options[select.value].image;
        int op_value = select.value;

        object[] parameters = new object[3];
        parameters[0] = op;

        parameters[1] = op_sprite;
        parameters[2] = op_value;

        gameObject.SendMessageUpwards("Rune_Set_Change", parameters);
        // Debug.Log("Dropdown Change!\n" + op);
    }
    private void ResetFunction_UI(int dropdown_value)
    {
        // Debug.Log("Clear");
        //button.onClick.RemoveAllListeners();
        dropdown.onValueChanged.RemoveAllListeners();
        dropdown.options.Clear();

        for (int i = 0; i < op_title.Length; i++)
        {
            Dropdown.OptionData newData = new Dropdown.OptionData();
            newData.text = op_title[i];
            newData.image = sprites[i];
            dropdown.options.Add(newData);
        }

        dropdown.SetValueWithoutNotify(-1);
        dropdown.SetValueWithoutNotify(0);

        dropdown.value = dropdown_value;
        dropdown.itemText.text = op_title[dropdown.value];
        dropdown.itemImage.sprite = sprites[dropdown.value];
    }
}
