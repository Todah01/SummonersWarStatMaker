using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class rune_box_control : MonoBehaviour
{
    #region Public Variable
    public GameObject selected_data;
    public GameObject rune_datas;
    public Image[] rune_imgs;
    public Image[] rune_seteffects;
    #endregion

    #region Local Variable
    Sprite[] rune_sprites;
    string[] rune_names;
    #endregion
    private void Awake()
    {
        rune_sprites = rune_datas.GetComponent<rune_set_dropdown_control>().sprites;
        rune_names = rune_datas.GetComponent<rune_set_dropdown_control>().op_title;
    }
    private void Start()
    {
        List<int> rune_dropdown_values = selected_data.GetComponent<select_data_control>().rune_dropdown_values;
        List<string> rune_seteffects_titles = selected_data.GetComponent<select_data_control>().rune_type;
        
        for(int i=0; i<rune_imgs.Length; i++)
        {
            rune_imgs[i].sprite = rune_sprites[rune_dropdown_values[i]];
            Color rune_img_color = rune_imgs[i].GetComponent<Image>().color;
            rune_img_color.a = 1f;
            rune_imgs[i].GetComponent<Image>().color = rune_img_color;
        }

        for(int i=0; i< rune_seteffects_titles.Count; i++)
        {
            int rune_seteffect = Array.IndexOf(rune_names, rune_seteffects_titles[i]);
            rune_seteffects[i].sprite = rune_sprites[rune_seteffect];
            Color rune_set_color = rune_seteffects[i].GetComponent<Image>().color;
            rune_set_color.a = 1f;
            rune_seteffects[i].GetComponent<Image>().color = rune_set_color;
        }
    }
}
