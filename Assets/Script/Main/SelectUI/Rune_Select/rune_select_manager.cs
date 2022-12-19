using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rune_select_manager : MonoBehaviour
{
    #region Public Variable
    public GameObject rune_set_ui;
    public GameObject etc_BG;
    public GameObject[] rune_slots;
    public Image[] rune_img;
    public Text rune_number;
    public Dropdown rune_stat_dropdown;
    #endregion

    #region Local Variable
    Dictionary<int, string> cur_rune_status = new Dictionary<int, string>()
    {
        { 1, "" },
        { 2, "" },
        { 3, "" },
        { 4, "" },
        { 5, "" },
        { 6, "" }
    };
    Dictionary<string, int> cur_rune_set_status;
    int cur_rune_number = 0;
    #endregion

    public void RuneSlotClick(int number)
    {
        cur_rune_number = number;
        
        // Disable stat dropdown if rune number is odd number.
        if (cur_rune_number % 2 == 1) rune_stat_dropdown.interactable = false;
        else rune_stat_dropdown.interactable = true;

        // Open background and rune set ui.
        etc_BG.SetActive(true);
        rune_set_ui.SetActive(true);
        rune_number.text = "Rune Number " + cur_rune_number.ToString();

        // Fill rune infos in object for send info to others.
        object[] rune_infos = new object[3];
        rune_infos[0] = rune_slots[cur_rune_number - 1].GetComponent<rune_slot_control>().dropdown_value;
        rune_infos[1] = cur_rune_number;
        rune_infos[2] = rune_slots[cur_rune_number - 1].GetComponent<rune_slot_control>().rune_stat_value;

        gameObject.BroadcastMessage("SetFunction_UI", rune_infos);
    }

    // Activate when rune set is change.
    private void Rune_Set_Change(object[] parameters)
    {
        int before_dropdown_value = 0;
        Set_rune_name((string)parameters[0]);

        if ((string)parameters[0] == "* Select a rune set *")
        {
            // Current Rune Setting
            before_dropdown_value = rune_slots[cur_rune_number - 1].GetComponent<rune_slot_control>().dropdown_value;
            rune_slots[cur_rune_number - 1].GetComponent<rune_slot_control>().dropdown_value = 0;

            object[] dropdown_values = new object[2];
            dropdown_values[0] = before_dropdown_value;
            dropdown_values[1] = (int)parameters[2];

            gameObject.BroadcastMessage("Rune_Set_Setting", dropdown_values);

            // Clear rune statue.
            cur_rune_status[cur_rune_number - 1] = "";

            // Clear rune image alpha value.
            Color slot_color = rune_slots[cur_rune_number - 1].GetComponent<Image>().color;
            slot_color.a = 0f;
            rune_slots[cur_rune_number - 1].GetComponent<Image>().color = slot_color;

            // Clear rune pattern.
            Sprite slot_pattern = rune_img[cur_rune_number - 1].GetComponent<Image>().sprite;
            slot_pattern = (Sprite)parameters[1];
            rune_img[cur_rune_number - 1].GetComponent<Image>().sprite = slot_pattern;

            Color slot_pattern_color = rune_img[cur_rune_number - 1].GetComponent<Image>().color;
            slot_pattern_color.a = 0f;
            rune_img[cur_rune_number - 1].GetComponent<Image>().color = slot_pattern_color;
        }
        else
        {
            // Current Rune Setting
            if(rune_slots[cur_rune_number - 1].GetComponent<rune_slot_control>().dropdown_value != 0)
            {
                before_dropdown_value = rune_slots[cur_rune_number - 1].GetComponent<rune_slot_control>().dropdown_value;
                rune_slots[cur_rune_number - 1].GetComponent<rune_slot_control>().dropdown_value = (int)parameters[2];
               
                object[] dropdown_values = new object[2];
                dropdown_values[0] = before_dropdown_value;
                dropdown_values[1] = (int)parameters[2];

                gameObject.BroadcastMessage("Rune_Set_Setting", dropdown_values);
                // Rune Statue Setting
                cur_rune_status[cur_rune_number - 1] = (string)parameters[0];
            }
            else
            {
                rune_slots[cur_rune_number - 1].GetComponent<rune_slot_control>().dropdown_value = (int)parameters[2];
                
                object[] dropdown_values = new object[2];
                dropdown_values[0] = before_dropdown_value;
                dropdown_values[1] = (int)parameters[2];

                gameObject.BroadcastMessage("Rune_Set_Setting", dropdown_values);
                // Rune Statue Setting
                cur_rune_status[cur_rune_number - 1] = (string)parameters[0];
            }

            // Change rune image alpha.
            Color slot_color = rune_slots[cur_rune_number - 1].GetComponent<Image>().color;
            slot_color.a = 1f;
            rune_slots[cur_rune_number - 1].GetComponent<Image>().color = slot_color;

            // Change rune pattern change.
            Sprite slot_pattern = rune_img[cur_rune_number - 1].GetComponent<Image>().sprite;
            slot_pattern = (Sprite)parameters[1];
            rune_img[cur_rune_number - 1].GetComponent<Image>().sprite = slot_pattern;

            Color slot_pattern_color = rune_img[cur_rune_number - 1].GetComponent<Image>().color;
            slot_pattern_color.a = 1f;
            rune_img[cur_rune_number - 1].GetComponent<Image>().color = slot_pattern_color;
        }
    }
    private void Set_rune_name(string rune_name)
    {
        rune_slots[cur_rune_number - 1].GetComponent<rune_slot_control>().rune_info = rune_name;
    }
    private void Set_Stat_Value(int stat_value)
    {
        rune_slots[cur_rune_number - 1].GetComponent<rune_slot_control>().rune_stat_value = stat_value;
    }
    private void Set_Stat_String(string stat_string)
    {
        rune_slots[cur_rune_number - 1].GetComponent<rune_slot_control>().rune_stat_string = stat_string;
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
