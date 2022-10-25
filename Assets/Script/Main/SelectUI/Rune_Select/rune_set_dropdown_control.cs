using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rune_set_dropdown_control : MonoBehaviour
{
    #region Public Variable
    public Sprite[] sprites;
    public string[] op_title;
    #endregion

    #region Local Variable
    Dropdown dropdown;
    #endregion

    private void Awake()
    {
        dropdown = this.GetComponent<Dropdown>();
        Reconstruction_dropdown(0);
    }

    private void SetFunction_UI(object[] rune_infos)
    {
        Reconstruction_dropdown((int)rune_infos[0]);
    }

    public void Function_Dropdown()
    {
        string op = dropdown.options[dropdown.value].text;
        Sprite op_sprite = dropdown.options[dropdown.value].image;
        int op_value = dropdown.value;

        object[] parameters = new object[3];
        parameters[0] = op;
        parameters[1] = op_sprite;
        parameters[2] = op_value;

        gameObject.SendMessageUpwards("Rune_Set_Change", parameters);
    }
    private void Reconstruction_dropdown(int dropdown_value)
    {
        dropdown.options.Clear();

        for (int i = 0; i < op_title.Length; i++)
        {
            Dropdown.OptionData newData = new Dropdown.OptionData();
            newData.text = op_title[i];
            newData.image = sprites[i];
            dropdown.options.Add(newData);
        }

        dropdown.value = dropdown_value;
        dropdown.itemText.text = op_title[dropdown.value];
        dropdown.itemImage.sprite = sprites[dropdown.value];
    }
}