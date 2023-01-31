using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rune_select_manager : MonoBehaviour
{
    #region Public Variable
    public GameObject selected_data;
    public GameObject rune_set_ui;
    public GameObject etc_BG;
    public GameObject preview_manager;
    public GameObject[] rune_slots;
    public Image[] rune_img;
    public Text rune_number;
    public Dropdown rune_stat_dropdown;
    #endregion

    #region Local Variable
    int cur_rune_number = 0;
    rune_slot_control rune_slot_manager;
    GameObject rune_pattern_img;
    Image rune_slot_img_manager;
    Image rune_pattern_manager;
    #endregion

    public void RuneSlotClick(int number)
    {
        // Set current rune infomation
        cur_rune_number = number;
        rune_slot_manager = rune_slots[cur_rune_number - 1].GetComponent<rune_slot_control>();
        rune_slot_img_manager = rune_slots[cur_rune_number - 1].GetComponent<Image>();
        rune_pattern_manager = rune_img[cur_rune_number - 1].GetComponent<Image>();

        // Disable stat dropdown if rune number is odd number.
        if (cur_rune_number % 2 == 1) rune_stat_dropdown.interactable = false;
        else rune_stat_dropdown.interactable = true;

        // Open background and rune set ui.
        etc_BG.SetActive(true);
        rune_set_ui.SetActive(true);
        rune_number.text = "Rune Number " + cur_rune_number.ToString();

        // Fill rune infos in object for send info to others.
        object[] rune_infos = new object[3];
        rune_infos[0] = rune_slot_manager.dropdown_value;
        rune_infos[1] = cur_rune_number;
        rune_infos[2] = rune_slot_manager.rune_stat_value;

        gameObject.BroadcastMessage("SetFunction_UI", rune_infos);
    }

    // Activate when rune set is change.
    private void Rune_Set_Change(object[] parameters)
    {
        string before_rune_set_name = rune_slot_manager.rune_info;

        if ((string)parameters[0] == "* Select a rune set *")
        {
            // Debug.Log("First");
            preview_manager.GetComponent<rune_set_preview_control>().
                RuneSetPreviewSetting(before_rune_set_name, "subtract");

            // Current Rune Setting
            Set_rune_name((string)parameters[0]);
            Set_rune_dropdown_value(0);

            // Clear rune image alpha value.
            Color slot_color = rune_slot_img_manager.color;
            slot_color.a = 0f;
            rune_slot_img_manager.color = slot_color;

            // Clear rune pattern.
            Sprite slot_pattern = rune_pattern_manager.sprite;
            slot_pattern = (Sprite)parameters[1];
            rune_pattern_manager.sprite = slot_pattern;

            Color slot_pattern_color = rune_pattern_manager.color;
            slot_pattern_color.a = 0f;
            rune_pattern_manager.color = slot_pattern_color;
            
            rune_pattern_manager.enabled = false;
        }
        else
        {
            // Current Rune Setting
            if(rune_slot_manager.dropdown_value == 0)
            {
                // Debug.Log("Second");
                Set_rune_name((string)parameters[0]);
                Set_rune_dropdown_value((int)parameters[2]);

                preview_manager.GetComponent<rune_set_preview_control>().
                    RuneSetPreviewSetting((string)parameters[0], "add");
            }
            else
            {
                // Debug.Log("Third");
                Set_rune_name((string)parameters[0]);
                Set_rune_dropdown_value((int)parameters[2]);

                preview_manager.GetComponent<rune_set_preview_control>().
                    RuneSetPreviewSetting(before_rune_set_name, "subtract");
                preview_manager.GetComponent<rune_set_preview_control>().
                    RuneSetPreviewSetting((string)parameters[0], "add");
            }

            // Set rune image with check ancient
            rune_slot_manager.RuneSlotImgChange(selected_data.
                GetComponent<select_data_control>().isAncient);

            // Change rune image alpha.
            Color slot_color = rune_slot_img_manager.color;
            slot_color.a = 1f;
            rune_slot_img_manager.color = slot_color;

            // Change rune pattern change.
            Sprite slot_pattern = rune_pattern_manager.sprite;
            slot_pattern = (Sprite)parameters[1];
            rune_pattern_manager.sprite = slot_pattern;

            Color slot_pattern_color = rune_pattern_manager.color;
            slot_pattern_color.a = 1f;
            rune_pattern_manager.color = slot_pattern_color;

            rune_pattern_manager.enabled = true;
        }
    }
    private void Set_rune_name(string rune_name)
    {
        rune_slot_manager.rune_info = rune_name;
    }
    private void Set_rune_dropdown_value(int value)
    {
        rune_slot_manager.dropdown_value = value;
    }
    private void Set_Stat_Value(int stat_value)
    {
        rune_slot_manager.rune_stat_value = stat_value;
    }
    private void Set_Stat_String(string stat_string)
    {
        rune_slot_manager.rune_stat_string = stat_string;
    }
    private void RuneSlotImgChange(bool isAncient)
    {
        foreach(var rune_slot in rune_slots)
        {
            rune_slot.GetComponent<rune_slot_control>().RuneSlotImgChange(isAncient);
        }
    }
    public void RuneSlotClose()
    {
        cur_rune_number = 0;
        rune_set_ui.SetActive(false);
        etc_BG.SetActive(false);
    }
}
