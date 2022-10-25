using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rune_preview_control : MonoBehaviour
{
    #region Public Variable
    public GameObject rune_set_data;
    public GameObject[] preview_slots;
    public Image[] preview_img;
    public Text[] preview_txt;
    #endregion

    #region Local Variable
    int[] preview_slot_info = new int[3] { 0, 0, 0 };
    string[] rune_set_data_txt;
    Sprite[] rune_set_data_sprite;
    List<int> four_rune_set = new List<int>() { 1, 3, 5, 6, 9, 14 };
    Dictionary<int, int> preview_counting = new Dictionary<int, int>()
    {
        { 1, 0 },
        { 2, 0 },
        { 3, 0 },
        { 4, 0 },
        { 5, 0 },
        { 6, 0 },
        { 7, 0 },
        { 8, 0 },
        { 9, 0 },
        { 10, 0 },
        { 11, 0 },
        { 12, 0 },
        { 13, 0 },
        { 14, 0 },
        { 15, 0 },
        { 16, 0 },
        { 17, 0 },
        { 18, 0 },
        { 19, 0 },
        { 20, 0 },
        { 21, 0 },
    };
    #endregion

    private void Start()
    {
        rune_set_data_sprite = rune_set_data.GetComponent<rune_set_dropdown_control>().sprites;
        rune_set_data_txt = rune_set_data.GetComponent<rune_set_dropdown_control>().op_title;
    }
    void Check_Available_Rune_Set(int dropdown_value)
    {
        int empty_cnt = 0;
        for (int i = 0; i < preview_slot_info.Length; i++)
            if (preview_slot_info[i] == dropdown_value)
                empty_cnt++;

        if(four_rune_set.Contains(dropdown_value))
        {
            if (preview_counting[dropdown_value] == 4)
            {
                if (empty_cnt != 1)
                    for (int i = 0; i < preview_slots.Length; i++)
                    {
                        if (preview_slots[i].activeSelf)
                            continue;
                        else
                        {
                            preview_slot_info[i] = dropdown_value;
                            preview_img[i].sprite = rune_set_data_sprite[dropdown_value];
                            preview_txt[i].text = rune_set_data_txt[dropdown_value];
                            preview_slots[i].SetActive(true);
                            Debug.Log(dropdown_value + " Setting complete");
                            break;
                        }
                    }
            }
            else if (preview_counting[dropdown_value] < 4)
            {
                for (int i = 0; i < preview_slots.Length; i++)
                {
                    if (preview_slot_info[i] == dropdown_value)
                    {
                        preview_slot_info[i] = 0;
                        preview_slots[i].SetActive(false);
                        break;
                    }
                }
            }
        }
        else
        {
            switch(preview_counting[dropdown_value])
            {
                case 1:
                    if (empty_cnt == 0) break;

                    for (int i = preview_slots.Length - 1; i>= 0; i--)
                    {
                        if (preview_slot_info[i] == dropdown_value)
                        {
                            preview_slot_info[i] = 0;
                            preview_slots[i].SetActive(false);
                            break;
                        }
                    }
                    break;
                case 2:
                    if (empty_cnt == 1) break;

                    for (int i = 0; i < preview_slots.Length; i++)
                    {
                        if (preview_slots[i].activeSelf)
                            continue;
                        else
                        {
                            preview_slot_info[i] = dropdown_value;
                            preview_img[i].sprite = rune_set_data_sprite[dropdown_value];
                            preview_txt[i].text = rune_set_data_txt[dropdown_value];
                            preview_slots[i].SetActive(true);
                            // Debug.Log(dropdown_value + " Setting complete");
                            break;
                        }
                    }
                    break;
                case 3:
                    if (empty_cnt == 1) break;

                    for (int i = preview_slots.Length - 1; i >= 0; i--)
                    {
                        if (preview_slot_info[i] == dropdown_value)
                        {
                            preview_slot_info[i] = 0;
                            preview_slots[i].SetActive(false);
                            break;
                        }
                    }
                    break;
                case 4:
                    if (empty_cnt == 2) break;

                    for (int i = 0; i < preview_slots.Length; i++)
                    {
                        if (preview_slots[i].activeSelf)
                            continue;
                        else
                        {
                            preview_slot_info[i] = dropdown_value;
                            preview_img[i].sprite = rune_set_data_sprite[dropdown_value];
                            preview_txt[i].text = rune_set_data_txt[dropdown_value];
                            preview_slots[i].SetActive(true);
                            // Debug.Log(dropdown_value + " Setting complete");
                            break;
                        }
                    }
                    break;
                case 5:
                    if (empty_cnt == 2) break;

                    for (int i = preview_slots.Length - 1; i >= 0; i--)
                    {
                        if (preview_slot_info[i] == dropdown_value)
                        {
                            preview_slot_info[i] = 0;
                            preview_slots[i].SetActive(false);
                            break;
                        }
                    }
                    break;
                case 6:
                    for (int i = 0; i < preview_slots.Length; i++)
                    {
                        if (preview_slots[i].activeSelf)
                            continue;
                        else
                        {
                            preview_slot_info[i] = dropdown_value;
                            preview_img[i].sprite = rune_set_data_sprite[dropdown_value];
                            preview_txt[i].text = rune_set_data_txt[dropdown_value];
                            preview_slots[i].SetActive(true);
                            // Debug.Log(dropdown_value + " Setting complete");
                            break;
                        }
                    }
                    break;
            }
        }
    }
    private void Rune_Set_Setting(object[] parameters)
    {
        int before_dropdown_value = (int)parameters[0];
        int cur_dropdown_value = (int)parameters[1];

        if(before_dropdown_value != 0 && preview_counting[before_dropdown_value] > 0)
        {
            preview_counting[before_dropdown_value] -= 1;
            Check_Available_Rune_Set(before_dropdown_value);
        }

        if(cur_dropdown_value != 0 && preview_counting[cur_dropdown_value] < 6)
        {
            preview_counting[cur_dropdown_value] += 1;
            Check_Available_Rune_Set(cur_dropdown_value);
        }
    }
}
