using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class result_ui_manager : MonoBehaviour
{
    #region Public Variable
    public GameObject selected_data;
    public GameObject result_calculate_manager;
    public Image monster_profile_img;
    public Sprite[] monster_imgs;
    public Text monster_name_txt;
    public Text[] base_monster_stats_txt;
    public Text[] plus_monster_stats_txt;
    public Text[] comp_monster_stats_txt;
    #endregion

    #region Local Variable
    string monster_name;
    List<int> base_monster_stats, plus_monster_stats, comp_monster_stats;
    #endregion
    public void Set_data_to_result_ui()
    {
        Debug.Log("result ui setting");

        // Get monster name and set monster name.
        monster_name = selected_data.GetComponent<select_data_control>().selected_monster.text;
        monster_name_txt.text = monster_name;

        // Set monster profile img.
        string moneter_name_tolower = monster_name.ToLower();
        for (int i = 0; i < monster_imgs.Length; i++)
        {
            if (monster_imgs[i].name.ToLower().Contains(moneter_name_tolower))
            {
                monster_profile_img.sprite = monster_imgs[i];
                break;
            }
        }

        // Get monster stats.
        base_monster_stats = result_calculate_manager.GetComponent<result_calculate_manager>().divide_stats_cur;
        plus_monster_stats = result_calculate_manager.GetComponent<result_calculate_manager>().divide_stats_plus;
        comp_monster_stats = result_calculate_manager.GetComponent<result_calculate_manager>().comp_stats;

        // Set monster stats.
        for(int idx = 0; idx<base_monster_stats_txt.Length; idx++)
            base_monster_stats_txt[idx].text = base_monster_stats[idx].ToString();

        for (int idx = 0; idx < plus_monster_stats_txt.Length; idx++)
            plus_monster_stats_txt[idx].text = plus_monster_stats[idx].ToString();

        for (int idx = 0; idx < comp_monster_stats_txt.Length; idx++)
        {
            if(idx != 1)
            {
                comp_monster_stats_txt[idx].text = comp_monster_stats[idx].ToString() + "%";
                if (comp_monster_stats[idx] >= 100)
                    comp_monster_stats_txt[idx].GetComponent<result_monster_txt_control>().SetTextColorToRed();
                else
                    comp_monster_stats_txt[idx].GetComponent<result_monster_txt_control>().SetTextColorToBase();
            }
            else
                comp_monster_stats_txt[idx].text = comp_monster_stats[idx].ToString() + "%";
        }
    }
}
