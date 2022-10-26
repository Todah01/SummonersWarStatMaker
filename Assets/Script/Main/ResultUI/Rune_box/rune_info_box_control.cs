using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rune_info_box_control : MonoBehaviour
{
    #region Public Variable
    public GameObject selected_data;
    public GameObject result_calculate_manager;
    public GameObject rune_info;
    public GameObject rune_img_slot;
    public GameObject[] rune_info_datas;
    public Text rune_stat_name;
    public Text pre_option_stat;
    public Text first_rune_stat_name;
    public Text first_rune_stat_amount;
    public GameObject first_rune_conversion_icon;
    public Text second_rune_stat_name;
    public Text second_rune_stat_amount;
    public GameObject second_rune_conversion_icon;
    public Text third_rune_stat_name;
    public Text third_rune_stat_amount;
    public GameObject third_rune_conversion_icon;
    public Text fourth_rune_stat_name;
    public Text fourth_rune_stat_amount;
    public GameObject fourth_rune_conversion_icon;
    #endregion

    #region Local Variable
    List<string> even_rune_stat_type;
    List<Dictionary<string, int>> rune_stat_infos;
    Dictionary<int, string> conversion_dict;
    #endregion
    // open rune window
    public void OnRuneClick(int i)
    {
        // get rune data from seleted data.
        even_rune_stat_type = selected_data.GetComponent<select_data_control>().even_rune_stat_type;
        rune_stat_infos = result_calculate_manager.GetComponent<result_calculate_manager>().rune_stat_infos;
        conversion_dict = result_calculate_manager.GetComponent<result_calculate_manager>().conversion_dict;

        // Set rune data and img from result.
        switch (i)
        {
            case 0:
                rune_stat_name.text = "ATK + 160";
                SettingRuneStat(i);
                break;
            case 1:
                if (even_rune_stat_type[0] == "SPD")
                {
                    rune_stat_name.text = even_rune_stat_type[0] + " + 42";
                }
                else
                {
                    rune_stat_name.text = even_rune_stat_type[0] + " + 63%";
                }
                SettingRuneStat(i);
                break;
            case 2:
                rune_stat_name.text = "DEF + 160";
                SettingRuneStat(i);
                break;
            case 3:
                if (even_rune_stat_type[1] == "CRI RATE")
                {
                    rune_stat_name.text = even_rune_stat_type[1] + " + 58%";
                }
                else if (even_rune_stat_type[1] == "CRI DMG")
                {
                    rune_stat_name.text = even_rune_stat_type[1] + " + 80%";
                }
                else
                {
                    rune_stat_name.text = even_rune_stat_type[1] + " + 63%";
                }
                SettingRuneStat(i);
                break;
            case 4:
                rune_stat_name.text = "HP + 2448";
                SettingRuneStat(i);
                break;
            case 5:
                if (even_rune_stat_type[2] == "RES")
                {
                    rune_stat_name.text = even_rune_stat_type[2] + " + 64%";
                }
                else if (even_rune_stat_type[1] == "ACC")
                {
                    rune_stat_name.text = even_rune_stat_type[2] + " + 64%";
                }
                else
                {
                    rune_stat_name.text = even_rune_stat_type[2] + " + 63%";
                }
                SettingRuneStat(i);
                break;
        }
        // instantiate rune_img
        Transform temp = Instantiate(rune_info_datas[i].transform.Find($"rune ({i + 1})"), rune_img_slot.transform.position, rune_img_slot.transform.rotation);
        temp.transform.SetParent(rune_img_slot.transform);
        temp.localScale = rune_img_slot.transform.localScale;
        rune_info.SetActive(true);
    }

    void SettingRuneStat(int rune_number)
    {
        int order_number = 1;
        bool ispreoption = false;
        pre_option_stat.gameObject.SetActive(false);

        // check pre-option icon
        if (rune_stat_infos[rune_number].Count == 5)
        {
            ispreoption = true;
            pre_option_stat.gameObject.SetActive(true);
        }

        // check stat of rune number and set stat in info box
        foreach (var dict in rune_stat_infos[rune_number])
        {
            string percentage = "";

            // check pre option
            if (ispreoption)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                pre_option_stat.text = description_key + " + " + dict.Value.ToString() + "%";
                ispreoption = false;
                continue;
            }

            // set first option
            if (order_number == 1)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                if (conversion_dict[rune_number + 1] == dict.Key)
                    first_rune_conversion_icon.SetActive(true);

                first_rune_stat_name.text = description_key;
                if (dict.Key != "SPD") percentage += "%";
                first_rune_stat_amount.text = " + " + dict.Value.ToString() + percentage;

            }
            // set second option
            else if (order_number == 2)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                if (conversion_dict[rune_number + 1] == dict.Key)
                    second_rune_conversion_icon.SetActive(true);

                second_rune_stat_name.text = description_key;
                if (dict.Key != "SPD") percentage += "%";
                second_rune_stat_amount.text = " + " + dict.Value.ToString() + percentage;
            }
            // set third option
            else if (order_number == 3)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                if (conversion_dict[rune_number + 1] == dict.Key)
                    third_rune_conversion_icon.SetActive(true);

                third_rune_stat_name.text = description_key;
                if (dict.Key != "SPD") percentage += "%";
                third_rune_stat_amount.text = " + " + dict.Value.ToString() + percentage;
            }
            // set fourth option
            else if (order_number == 4)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                if (conversion_dict[rune_number + 1] == dict.Key)
                    fourth_rune_conversion_icon.SetActive(true);

                fourth_rune_stat_name.text = description_key;
                if (dict.Key != "SPD") percentage += "%";
                fourth_rune_stat_amount.text = " + " + dict.Value.ToString() + percentage;
            }

            order_number++;
        }
    }

    // close rune window
    public void OnRuneClose()
    {
        rune_info.SetActive(false);

        // delete child
        Transform[] child = rune_img_slot.GetComponentsInChildren<Transform>();
        if (child != null)
        {
            for (int i = 1; i < child.Length; i++)
            {
                if (child[i] != rune_img_slot.transform)
                    Destroy(child[i].gameObject);
            }
        }

        //disable conversion icon
        first_rune_conversion_icon.SetActive(false);
        second_rune_conversion_icon.SetActive(false);
        third_rune_conversion_icon.SetActive(false);
        fourth_rune_conversion_icon.SetActive(false);
    }
}
