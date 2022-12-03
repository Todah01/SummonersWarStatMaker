using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class rune_info_box_control : MonoBehaviour
{
    #region Public Variable
    public GameObject selected_data;
    public GameObject result_calculate_manager;
    public GameObject rune_info;
    public GameObject rune_img_slot;
    public GameObject grinding_on_window;
    public GameObject grinding_off_window;
    public GameObject fengyanimg;
    public GameObject word_bubble;
    public GameObject[] rune_info_datas;
    public Text rune_stat_name;
    public Text pre_option_stat;
    public Text first_rune_stat_name;
    public Text first_rune_stat_name_grind_off;
    public Text first_rune_stat_amount;
    public Text first_rune_stat_amount_1;
    public Text first_rune_stat_amount_2;
    public GameObject first_rune_conversion_icon;
    public GameObject first_rune_conversion_icon_grinding_on;
    public GameObject first_rune_conversion_icon_grinding_off;
    public Text second_rune_stat_name;
    public Text second_rune_stat_name_grind_off;
    public Text second_rune_stat_amount;
    public Text second_rune_stat_amount_1;
    public Text second_rune_stat_amount_2;
    public GameObject second_rune_conversion_icon;
    public GameObject second_rune_conversion_icon_grinding_on;
    public GameObject second_rune_conversion_icon_grinding_off;
    public Text third_rune_stat_name;
    public Text third_rune_stat_name_grind_off;
    public Text third_rune_stat_amount;
    public Text third_rune_stat_amount_1;
    public Text third_rune_stat_amount_2;
    public GameObject third_rune_conversion_icon;
    public GameObject third_rune_conversion_icon_grinding_on;
    public GameObject third_rune_conversion_icon_grinding_off;
    public Text fourth_rune_stat_name;
    public Text fourth_rune_stat_name_grind_off;
    public Text fourth_rune_stat_amount;
    public Text fourth_rune_stat_amount_1;
    public Text fourth_rune_stat_amount_2;
    public GameObject fourth_rune_conversion_icon;
    public GameObject fourth_rune_conversion_icon_grinding_on;
    public GameObject fourth_rune_conversion_icon_grinding_off;
    public bool isfengyan = false;
    #endregion

    #region Local Variable
    List<string> even_rune_stat_type;
    List<List<int>> grinding_stat_infos;
    List<Dictionary<string, int>> rune_stat_infos, rune_stat_sum_infos;
    Dictionary<int, string> conversion_dict;
    Animator fengyan_anim;
    bool fengyan_isanim = false;
    #endregion
    private void Awake()
    {
        // Get feng yan animator controller.
        fengyan_anim = fengyanimg.GetComponent<Animator>();
    }

    // Open rune window
    public void OnRuneClick(int i)
    {
        // Get rune data from seleted data.
        even_rune_stat_type = selected_data.GetComponent<select_data_control>().even_rune_stat_type;
        rune_stat_infos = result_calculate_manager.GetComponent<result_calculate_manager>().rune_stat_infos;
        grinding_stat_infos = result_calculate_manager.GetComponent<result_calculate_manager>().grinding_stat_list;
        rune_stat_sum_infos = result_calculate_manager.GetComponent<result_calculate_manager>().rune_stat_sum_info;
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
        int grind_number = 0;
        int order_number_grind_off = 1;
        bool ispreoption = false;
        pre_option_stat.gameObject.SetActive(false);

        // check pre-option icon
        if (rune_stat_infos[rune_number].Count == 5)
        {
            ispreoption = true;
            grind_number += 1;
            pre_option_stat.gameObject.SetActive(true);
        }

        #region Set visible grinding stat.
        // check stat of rune number and set stat in info box
        foreach (var dict in rune_stat_infos[rune_number])
        {
            string percentage = "";

            Debug.Log(dict.Key + " : " + dict.Value);

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

                // Check stat type.
                if (dict.Key == "ACC" || dict.Key == "RES" || dict.Key == "CRI RATE" || dict.Key == "CRI DMG")
                    first_rune_stat_amount_2.gameObject.SetActive(false);

                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                // Check conversion.
                if (conversion_dict[rune_number + 1] == dict.Key)
                {
                    if (dict.Key == "ACC" || dict.Key == "RES" || dict.Key == "CRI RATE" || dict.Key == "CRI DMG")
                        first_rune_conversion_icon.SetActive(true);
                    first_rune_conversion_icon_grinding_on.SetActive(true);
                }

                first_rune_stat_name.text = description_key;
                if (dict.Key != "SPD" && dict.Key != "HP+" &&
                    dict.Key != "DEF+" && dict.Key != "ATK+")
                    percentage += "%";
                
                first_rune_stat_amount_1.text = " + " + dict.Value.ToString() + percentage;
                
                if (grinding_stat_infos[rune_number][grind_number] != 0)
                    first_rune_stat_amount_2.text = " + " + grinding_stat_infos[rune_number][grind_number].ToString() + percentage;
            }
            // set second option
            else if (order_number == 2)
            {
                string description_key = dict.Key;

                if (dict.Key == "ACC" || dict.Key == "RES" || dict.Key == "CRI RATE" || dict.Key == "CRI DMG")
                    second_rune_stat_amount_2.gameObject.SetActive(false);

                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                if (conversion_dict[rune_number + 1] == dict.Key)
                {
                    if (dict.Key == "ACC" || dict.Key == "RES" || dict.Key == "CRI RATE" || dict.Key == "CRI DMG")
                        second_rune_conversion_icon.SetActive(true);
                    second_rune_conversion_icon_grinding_on.SetActive(true);
                }

                second_rune_stat_name.text = description_key;
                if (dict.Key != "SPD" && dict.Key != "HP+" &&
                    dict.Key != "DEF+" && dict.Key != "ATK+")
                    percentage += "%";
                
                second_rune_stat_amount_1.text = " + " + dict.Value.ToString() + percentage;
                
                if (grinding_stat_infos[rune_number][grind_number] != 0)
                    second_rune_stat_amount_2.text = " + " + grinding_stat_infos[rune_number][grind_number].ToString() + percentage;
            }
            // set third option
            else if (order_number == 3)
            {
                string description_key = dict.Key;

                if (dict.Key == "ACC" || dict.Key == "RES" || dict.Key == "CRI RATE" || dict.Key == "CRI DMG")
                    third_rune_stat_amount_2.gameObject.SetActive(false);

                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                if (conversion_dict[rune_number + 1] == dict.Key)
                {
                    if (dict.Key == "ACC" || dict.Key == "RES" || dict.Key == "CRI RATE" || dict.Key == "CRI DMG")
                        third_rune_conversion_icon.SetActive(true);
                    third_rune_conversion_icon_grinding_on.SetActive(true);
                }

                third_rune_stat_name.text = description_key;
                if (dict.Key != "SPD" && dict.Key != "HP+" &&
                    dict.Key != "DEF+" && dict.Key != "ATK+")
                    percentage += "%";

                third_rune_stat_amount_1.text = " + " + dict.Value.ToString() + percentage;
                
                if (grinding_stat_infos[rune_number][grind_number] != 0)
                    third_rune_stat_amount_2.text = " + " + grinding_stat_infos[rune_number][grind_number].ToString() + percentage;
            }
            // set fourth option
            else if (order_number == 4)
            {
                string description_key = dict.Key;

                if (dict.Key == "ACC" || dict.Key == "RES" || dict.Key == "CRI RATE" || dict.Key == "CRI DMG")
                    fourth_rune_stat_amount_2.gameObject.SetActive(false);

                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                if (conversion_dict[rune_number + 1] == dict.Key)
                {
                    if (dict.Key == "ACC" || dict.Key == "RES" || dict.Key == "CRI RATE" || dict.Key == "CRI DMG")
                        fourth_rune_conversion_icon.SetActive(true);
                    fourth_rune_conversion_icon_grinding_on.SetActive(true);
                }

                fourth_rune_stat_name.text = description_key;
                if (dict.Key != "SPD" && dict.Key != "HP+" &&
                    dict.Key != "DEF+" && dict.Key != "ATK+")
                    percentage += "%";

                fourth_rune_stat_amount_1.text = " + " + dict.Value.ToString() + percentage;

                if (grinding_stat_infos[rune_number][grind_number] != 0)
                    fourth_rune_stat_amount_2.text = " + " + grinding_stat_infos[rune_number][grind_number].ToString() + percentage;
            }

            order_number++;
            grind_number++;
        }
        #endregion

        // check pre-option icon
        if (rune_stat_infos[rune_number].Count == 5)
        {
            ispreoption = true;
        }

        #region Set disable grinding stat.
        // check stat of rune number and set stat in info box
        foreach (var dict in rune_stat_sum_infos[rune_number])
        {
            string percentage = "";

            if (ispreoption)
            {
                ispreoption = false;
                continue;
            }

            // set first option
            if (order_number_grind_off == 1)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                if (conversion_dict[rune_number + 1] == dict.Key)
                    first_rune_conversion_icon_grinding_off.SetActive(true);

                first_rune_stat_name_grind_off.text = description_key;
                if (dict.Key != "SPD" && dict.Key != "HP+" &&
                    dict.Key != "DEF+" && dict.Key != "ATK+")
                    percentage += "%";

                first_rune_stat_amount.text = " + " + dict.Value.ToString() + percentage;
            }
            // set second option
            else if (order_number_grind_off == 2)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                if (conversion_dict[rune_number + 1] == dict.Key)
                    second_rune_conversion_icon_grinding_off.SetActive(true);

                second_rune_stat_name_grind_off.text = description_key;
                if (dict.Key != "SPD" && dict.Key != "HP+" &&
                    dict.Key != "DEF+" && dict.Key != "ATK+")
                    percentage += "%";

                second_rune_stat_amount.text = " + " + dict.Value.ToString() + percentage;
            }
            // set third option
            else if (order_number_grind_off == 3)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                if (conversion_dict[rune_number + 1] == dict.Key)
                    third_rune_conversion_icon_grinding_off.SetActive(true);

                third_rune_stat_name_grind_off.text = description_key;
                if (dict.Key != "SPD" && dict.Key != "HP+" &&
                    dict.Key != "DEF+" && dict.Key != "ATK+")
                    percentage += "%";

                third_rune_stat_amount.text = " + " + dict.Value.ToString() + percentage;
            }
            // set fourth option
            else if (order_number_grind_off == 4)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                if (conversion_dict[rune_number + 1] == dict.Key)
                    fourth_rune_conversion_icon_grinding_off.SetActive(true);

                fourth_rune_stat_name_grind_off.text = description_key;
                if (dict.Key != "SPD" && dict.Key != "HP+" &&
                    dict.Key != "DEF+" && dict.Key != "ATK+")
                    percentage += "%";

                fourth_rune_stat_amount.text = " + " + dict.Value.ToString() + percentage;
            }

            order_number_grind_off++;
        }
        #endregion
    }
    // Set visible / disable grinding stat.
    public void GrindingStatWindowChange()
    {
        if (fengyan_isanim) return;

        fengyan_isanim = true;
        word_bubble.SetActive(false);
        fengyan_anim.SetTrigger("IsMotion");
        StartCoroutine(WaitForFengYan());
    }
    IEnumerator WaitForFengYan()
    {
        yield return new WaitForSeconds(1f);

        if (!isfengyan)
        {
            isfengyan = true;
            grinding_on_window.SetActive(true);
            grinding_off_window.SetActive(false);
        }
        else
        {
            isfengyan = false;
            grinding_on_window.SetActive(false);
            grinding_off_window.SetActive(true);
        }

        word_bubble.SetActive(true);
        fengyan_isanim = false;
    }
    // Close rune window.
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
        fengyan_isanim = false;
        word_bubble.SetActive(true);
        first_rune_conversion_icon.SetActive(false);
        first_rune_conversion_icon_grinding_on.SetActive(false);
        first_rune_conversion_icon_grinding_off.SetActive(false);
        second_rune_conversion_icon.SetActive(false);
        second_rune_conversion_icon_grinding_on.SetActive(false);
        second_rune_conversion_icon_grinding_off.SetActive(false);
        third_rune_conversion_icon.SetActive(false);
        third_rune_conversion_icon_grinding_on.SetActive(false);
        third_rune_conversion_icon_grinding_off.SetActive(false);
        fourth_rune_conversion_icon.SetActive(false);
        fourth_rune_conversion_icon_grinding_on.SetActive(false);
        fourth_rune_conversion_icon_grinding_off.SetActive(false);
        first_rune_stat_amount_2.gameObject.SetActive(true);
        second_rune_stat_amount_2.gameObject.SetActive(true);
        third_rune_stat_amount_2.gameObject.SetActive(true);
        fourth_rune_stat_amount_2.gameObject.SetActive(true);
    }
}
