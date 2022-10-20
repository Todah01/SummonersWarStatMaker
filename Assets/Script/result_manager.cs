using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class result_manager : MonoBehaviour
{
    #region Public Variable
    public GameObject etc_window_control;
    public GameObject selected_data;
    public GameObject rune_info;
    public GameObject[] rune_info_datas;
    public GameObject GoogleDataManager;
    public GameObject txt_crirate;
    public GameObject txt_res;
    public GameObject txt_acc;
    public Image rune_img_slot;
    public Image rune_img_pattern;
    public Image monster_profile;
    public Text monster_name_txt;
    public Text rune_stat_name;
    public Text pre_option_stat;
    public Text first_rune_stat_name;
    public Text first_rune_stat_amount;
    public Text second_rune_stat_name;
    public Text second_rune_stat_amount;
    public Text third_rune_stat_name;
    public Text third_rune_stat_amount;
    public Text fourth_rune_stat_name;
    public Text fourth_rune_stat_amount;
    public Text[] monster_stats_divide;
    public Text[] monster_plus_stats_divide;
    public Text[] monster_stats_combine;
    public Sprite[] monster_profiles;
    public int left_artifact_dropdown_values = 0, right_artifact_dropdown_values = 0;
    public int cur_hp, cur_atk, cur_def, cur_spd, cur_crirate, cur_cridmg, cur_res, cur_acc;
    #endregion

    #region Local Variable
    Dictionary<string, int> stat_rainforce_value = new Dictionary<string, int>()
    {
        {"SPD", 6}, {"HP", 8}, {"ATK", 8}, {"DEF", 8}, {"CRI RATE", 6}, {"CRI DMG", 7}, {"ACC", 8}, {"RES", 8}
    };
    List<Dictionary<string, int>> separate_stats = new List<Dictionary<string, int>>();
    List<Dictionary<string, int>> rune_stat_infos = new List<Dictionary<string, int>>();
    List<string> rune_type;
    List<string> even_rune_stat_type;
    List<string> prefer_stat_type;
    int plus_hp, plus_atk, plus_def, plus_spd, plus_crirate, plus_cridmg, plus_res, plus_acc;
    string monster_name = "";
    #endregion
    private void Start()
    {
        // set separate_stats
        Dictionary<string, int> separate_stat_1 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}
        };
        Dictionary<string, int> separate_stat_2 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}
        };
        Dictionary<string, int> separate_stat_3 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}
        };
        Dictionary<string, int> separate_stat_4 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}
        };
        Dictionary<string, int> separate_stat_5 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}
        };
        Dictionary<string, int> separate_stat_6 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}
        };
        separate_stats.Add(separate_stat_1);
        separate_stats.Add(separate_stat_2);
        separate_stats.Add(separate_stat_3);
        separate_stats.Add(separate_stat_4);
        separate_stats.Add(separate_stat_5);
        separate_stats.Add(separate_stat_6);
    }
    public void Start_StatSetting()
    {
        // get selected data
        monster_name = selected_data.GetComponent<select_data_control>().selected_monster.text;
        rune_type = selected_data.GetComponent<select_data_control>().rune_type;
        even_rune_stat_type = selected_data.GetComponent<select_data_control>().even_rune_stat_type;
        prefer_stat_type = selected_data.GetComponent<select_data_control>().prefer_stat_type;

        // get basic monster stat data
        monster_name_txt.text = monster_name;
        cur_hp = GoogleDataManager.GetComponent<googlesheet_manager>().hp;
        cur_atk = GoogleDataManager.GetComponent<googlesheet_manager>().atk;
        cur_def = GoogleDataManager.GetComponent<googlesheet_manager>().def;
        cur_spd = GoogleDataManager.GetComponent<googlesheet_manager>().spd;
        cur_crirate = GoogleDataManager.GetComponent<googlesheet_manager>().crirate;
        cur_cridmg = GoogleDataManager.GetComponent<googlesheet_manager>().cridmg;
        cur_res = GoogleDataManager.GetComponent<googlesheet_manager>().res;
        cur_acc = GoogleDataManager.GetComponent<googlesheet_manager>().acc;

        // add rune set effect stat
        for(int i=0; i<rune_type.Count; i++)
        {
            if (rune_type[i] == "Swift") plus_spd += Mathf.RoundToInt(cur_spd * 0.25f);
            else if (rune_type[i] == "Blade") plus_crirate += 12;
            else if (rune_type[i] == "Endure") plus_res += 20;
            else if (rune_type[i] == "Energy") plus_hp += Mathf.RoundToInt(cur_hp * 0.15f);
            else if (rune_type[i] == "Fatal") plus_atk += Mathf.RoundToInt(cur_atk * 0.35f);
            else if (rune_type[i] == "Focus") plus_acc += 20;
            else if (rune_type[i] == "Guard") plus_def += Mathf.RoundToInt(cur_def * 0.15f);
            else if (rune_type[i] == "Rage") plus_cridmg += 40;
        }

        // cal monster stat
        Cal_Stat(rune_type, even_rune_stat_type, prefer_stat_type, cur_hp, cur_atk, cur_def, cur_spd);

        // set monster profile
        string moneter_name_tolower = monster_name.ToLower();
        for (int i=0; i<monster_profiles.Length; i++)
        {
            if (monster_profiles[i].name.ToLower().Contains(moneter_name_tolower))
            {
                monster_profile.sprite = monster_profiles[i];
                break;
            }
        }

        // set monster stat
        monster_stats_divide[0].text = cur_hp.ToString();
        monster_stats_divide[1].text = cur_atk.ToString();
        monster_stats_divide[2].text = cur_def.ToString();
        monster_stats_divide[3].text = cur_spd.ToString();

        monster_plus_stats_divide[0].text = plus_hp.ToString();
        monster_plus_stats_divide[1].text = plus_atk.ToString();
        monster_plus_stats_divide[2].text = plus_def.ToString();
        monster_plus_stats_divide[3].text = plus_spd.ToString();

        monster_stats_combine[0].text = (Mathf.Min(100, cur_crirate + plus_crirate)).ToString() + "%";
        if (cur_crirate + plus_crirate >= 100)
            txt_crirate.GetComponent<result_monster_txt_control>().SetTextColorToRed();
        else
            txt_crirate.GetComponent<result_monster_txt_control>().SetTextColorToBase();
        monster_stats_combine[1].text = (cur_cridmg + plus_cridmg).ToString() + "%";
        monster_stats_combine[2].text = (Mathf.Min(100, cur_res + plus_res)).ToString() + "%";
        if (cur_res + plus_res >= 100)
            txt_res.GetComponent<result_monster_txt_control>().SetTextColorToRed();
        else
            txt_res.GetComponent<result_monster_txt_control>().SetTextColorToBase();
        monster_stats_combine[3].text = (Mathf.Min(100, cur_acc + plus_acc)).ToString() + "%";
        if (cur_acc + plus_acc >= 100)
            txt_acc.GetComponent<result_monster_txt_control>().SetTextColorToRed();
        else
            txt_acc.GetComponent<result_monster_txt_control>().SetTextColorToBase();
    }
    void Cal_Stat(List<string> rune_type, List<string> even_rune_stat_type, List<string> prefer_stat_type, int hp, int atk, int def, int spd)
    {
        // repeat 6 times
        for (int rune_number = 1; rune_number < 7; rune_number++)
        {
            switch (rune_number)
            {
                case 1:
                    plus_atk += 160;
                    CalStatFromPreferStat(rune_number);
                    break;
                case 2:
                    CheckEvenRuneStat(rune_number);
                    break;
                case 3:
                    plus_def += 160;
                    CalStatFromPreferStat(rune_number);
                    break;
                case 4:
                    CheckEvenRuneStat(rune_number);
                    break;
                case 5:
                    plus_hp += 2448;
                    CalStatFromPreferStat(rune_number);
                    break;
                case 6:
                    CheckEvenRuneStat(rune_number);
                    break;
            }
        }
    }

    void CheckEvenRuneStat(int number)
    {
        if (number == 2)
        {
            if (even_rune_stat_type[0] == "SPD") plus_spd += 42;
            else if (even_rune_stat_type[0] == "HP") plus_hp += Mathf.RoundToInt((float)cur_hp * 0.63f);
            else if (even_rune_stat_type[0] == "ATK") plus_atk += Mathf.RoundToInt((float)cur_atk * 0.63f);
            else if (even_rune_stat_type[0] == "DEF") plus_def += Mathf.RoundToInt((float)cur_def * 0.63f);

            CalStatFromPreferStat(number);
        }
        else if (number == 4)
        {
            if (even_rune_stat_type[1] == "HP") plus_hp += Mathf.RoundToInt((float)cur_hp * 0.63f);
            else if (even_rune_stat_type[1] == "ATK") plus_atk += Mathf.RoundToInt((float)cur_atk * 0.63f);
            else if (even_rune_stat_type[1] == "DEF") plus_def += Mathf.RoundToInt((float)cur_def * 0.63f);
            else if (even_rune_stat_type[1] == "CRI RATE") plus_crirate += 58;
            else if (even_rune_stat_type[1] == "CRI DMG") plus_cridmg += 80;
            CalStatFromPreferStat(number);
        }
        else if (number == 6)
        {
            if (even_rune_stat_type[2] == "HP") plus_hp += Mathf.RoundToInt((float)cur_hp * 0.63f);
            else if (even_rune_stat_type[2] == "ATK") plus_atk += Mathf.RoundToInt((float)cur_atk * 0.63f);
            else if (even_rune_stat_type[2] == "DEF") plus_def += Mathf.RoundToInt((float)cur_def * 0.63f);
            else if (even_rune_stat_type[2] == "RES") plus_res += 64;
            else if (even_rune_stat_type[2] == "ACC") plus_acc += 64;
            
            CalStatFromPreferStat(number);
        }
    }

    void CalStatFromPreferStat(int number)
    {
        #region set scoreboard
        Dictionary<string, int> stat_scoreboard = separate_stats[number - 1];
        // get rune data from seleted data
        even_rune_stat_type = selected_data.GetComponent<select_data_control>().even_rune_stat_type;
        // check prefer stat and plus score in separte_stats
        prefer_stat_type = selected_data.GetComponent<select_data_control>().prefer_stat_type;
        for(int i=0; i<prefer_stat_type.Count; i++)
        {
            if(i == 0 && stat_scoreboard.ContainsKey(prefer_stat_type[i])) stat_scoreboard[prefer_stat_type[i]] += 4;
            else if (i == 1 && stat_scoreboard.ContainsKey(prefer_stat_type[i])) stat_scoreboard[prefer_stat_type[i]] += 3;
            else if (i == 2 && stat_scoreboard.ContainsKey(prefer_stat_type[i])) stat_scoreboard[prefer_stat_type[i]] += 2;
            else if (i == 3 && stat_scoreboard.ContainsKey(prefer_stat_type[i])) stat_scoreboard[prefer_stat_type[i]] += 1;
        }

        // sort stat_scoreboard by value
        stat_scoreboard.OrderByDescending(item => item.Key).ToDictionary(x => x.Key, x => x.Value);
        #endregion

        #region min_stat_demand
        int min_crirate = 0;
        int min_acc = 0;
        int min_res = 0;
        int rune_stat_cnt = 4;
        bool pre_option_possible = false;
        bool pre_option_on = false;
        bool prefer_crirate = false;
        bool prefer_acc = false;
        bool prefer_res = false;
        #endregion

        // check prefer stat and set minimum stat
        for (int i = 0; i < prefer_stat_type.Count; i++)
        {
            switch (prefer_stat_type[i])
            {
                case "CRI RATE":
                    if (i == 0) min_crirate = 100;
                    else if (i == 1) min_crirate = 90;
                    else if (i == 2) min_crirate = 80;
                    else if (i == 3) min_crirate = 70;
                    prefer_crirate = true;
                    break;
                case "ACC":
                    if (i == 0) min_acc = 85;
                    else if (i == 1) min_acc = 70;
                    else if (i == 2) min_acc = 55;
                    else if (i == 3) min_acc = 40;
                    prefer_acc = true;
                    break;
                case "RES":
                    min_res = 100;
                    prefer_res = true;
                    break;
            }
        }

        // temp dict for save rune info
        Dictionary<string, int> temp_rune_info = new Dictionary<string, int>();

        int pre_option_percentage = Random.Range(1, 101);
        if (pre_option_percentage > 31) pre_option_possible = true;

        // set pre-option
        if (number != 2 && prefer_acc && pre_option_possible)
        {
            if (!temp_rune_info.ContainsKey("ACC"))
            {
                if(cur_acc + plus_acc < min_acc)
                {
                    pre_option_on = true;
                    rune_stat_cnt += 1;
                    int pre_option_value = CalRainforceValue(stat_rainforce_value["ACC"]);
                    temp_rune_info.Add("ACC", pre_option_value);
                }
            }
        }
        else if (number != 2 && prefer_res && pre_option_possible)
        {
            if (!temp_rune_info.ContainsKey("RES"))
            {
                if (cur_res + plus_res < min_res)
                {
                    pre_option_on = true;
                    rune_stat_cnt += 1;
                    int pre_option_value = CalRainforceValue(stat_rainforce_value["RES"]);
                    temp_rune_info.Add("RES", pre_option_value);
                }
            }
        }

        // if rune number is odd number, just add stat to rune.
        if (number % 2 == 1)
        {
            // set prefer basic stat to rune
            for (int i = 0; i < prefer_stat_type.Count; i++)
            {
                // check prefer stat and plus score in separte_stats
                if (!stat_scoreboard.ContainsKey(prefer_stat_type[i]) || temp_rune_info.ContainsKey(prefer_stat_type[i]))
                    continue;

                if (prefer_crirate && prefer_stat_type[i] == "CRI RATE" && cur_crirate + plus_crirate >= min_crirate ||
                    prefer_acc && prefer_stat_type[i] == "ACC" && cur_acc + plus_acc >= min_acc ||
                    prefer_res && prefer_stat_type[i] == "RES" && cur_res + plus_res >= min_res)
                    continue;

                int rainforce_value = CalRainforceValue(stat_rainforce_value[prefer_stat_type[i]]);
                temp_rune_info.Add(prefer_stat_type[i], rainforce_value);
            }

            Debug.Log(number + " Part 1. Calculate Ok");

            // set prefer extra basic stat to rune
            foreach (string key in stat_scoreboard.Keys)
            {
                if (prefer_crirate && key == "CRI RATE" && cur_crirate + plus_crirate >= min_crirate ||
                    prefer_acc && key == "ACC" && cur_acc + plus_acc >= min_acc ||
                    prefer_res && key == "RES" && cur_res + plus_res >= min_res)
                    continue;

                if (!temp_rune_info.ContainsKey(key))
                {
                    if (temp_rune_info.Count == rune_stat_cnt)
                        break;

                    int rainforce_value = CalRainforceValue(stat_rainforce_value[key]);
                    temp_rune_info.Add(key, rainforce_value);
                }

                if (temp_rune_info.Count == rune_stat_cnt)
                    break;
            }

            Debug.Log(number + " Part 2. Calculate Ok");

            // rainforce count variable
            int rainforce_cnt = 0;

            // rune rainforce
            while (rainforce_cnt < 4)
            {
                string rainforce_stat = CalRainforceStatNumber(temp_rune_info, pre_option_on);
                int rainforce_value = CalRainforceValue(stat_rainforce_value[rainforce_stat]);

                if (prefer_crirate && rainforce_stat == "CRI RATE" && cur_crirate + plus_crirate + temp_rune_info[rainforce_stat] < min_crirate)
                    // && cur_crirate + plus_crirate + temp_rune_info[rainforce_stat] + rainforce_value >= min_crirate)
                {
                    temp_rune_info[rainforce_stat] += rainforce_value;
                    rainforce_cnt += 1;
                }
                else if (prefer_acc && rainforce_stat == "ACC" && cur_acc + plus_acc + temp_rune_info[rainforce_stat] < min_acc)
                    // && cur_acc + plus_acc + temp_rune_info[rainforce_stat] + rainforce_value >= min_acc)
                {
                    temp_rune_info[rainforce_stat] += rainforce_value;
                    rainforce_cnt += 1;
                }
                else if (prefer_res && rainforce_stat == "RES" && cur_res + plus_res + temp_rune_info[rainforce_stat] < min_res)
                    // && cur_res + plus_res + temp_rune_info[rainforce_stat] + rainforce_value >= min_res)
                {
                    temp_rune_info[rainforce_stat] += rainforce_value;
                    rainforce_cnt += 1;
                }
                else if (prefer_crirate && rainforce_stat == "CRI RATE" && cur_crirate + plus_crirate + temp_rune_info[rainforce_stat] >= min_crirate ||
                    prefer_acc && rainforce_stat == "ACC" && cur_acc + plus_acc + temp_rune_info[rainforce_stat] >= min_acc ||
                    prefer_res && rainforce_stat == "RES" && cur_res + plus_res + temp_rune_info[rainforce_stat] >= min_res)
                    continue;
                else
                {
                    temp_rune_info[rainforce_stat] += rainforce_value;
                    rainforce_cnt += 1;
                }
            }

            Debug.Log(number + " Part 3. Calculate Ok");

        }
        // if rune number is even number, check even number main stat before add stat to rune.
        else
        {
            // get even rune stat
            string even_stat_type = "";
            if (number == 2) even_stat_type = even_rune_stat_type[0];
            else if (number == 4) even_stat_type = even_rune_stat_type[1];
            else if (number == 6) even_stat_type = even_rune_stat_type[2];

            // set prefer basic stat to rune
            for (int i = 0; i < prefer_stat_type.Count; i++)
            {
                // check stat between even rune stat and prefer stat
                if (prefer_stat_type[i] == even_stat_type)
                    continue;

                // check prefer stat and plus score in separte_stats
                if (!stat_scoreboard.ContainsKey(prefer_stat_type[i]) || temp_rune_info.ContainsKey(prefer_stat_type[i]))
                    continue;

                if (prefer_crirate && prefer_stat_type[i] == "CRI RATE" && cur_crirate + plus_crirate >= min_crirate ||
                    prefer_acc && prefer_stat_type[i] == "ACC" && cur_acc + plus_acc >= min_acc ||
                    prefer_res && prefer_stat_type[i] == "RES" && cur_res + plus_res >= min_res)
                    continue;

                int rainforce_value = CalRainforceValue(stat_rainforce_value[prefer_stat_type[i]]);
                temp_rune_info.Add(prefer_stat_type[i], rainforce_value);
            }

            Debug.Log(number + " Part 1. Calculate Ok");

            // set prefer extra basic stat to rune
            foreach (string key in stat_scoreboard.Keys)
            {
                if (prefer_crirate && key == "CRI RATE" && cur_crirate + plus_crirate >= min_crirate ||
                    prefer_acc && key == "ACC" && cur_acc + plus_acc >= min_acc ||
                    prefer_res && key == "RES" && cur_res + plus_res >= min_res)
                    continue;

                if (!temp_rune_info.ContainsKey(key) && key != even_stat_type)
                {
                    if (temp_rune_info.Count == rune_stat_cnt)
                        break;

                    int rainforce_value = CalRainforceValue(stat_rainforce_value[key]);
                    temp_rune_info.Add(key, rainforce_value);
                }

                if (temp_rune_info.Count == rune_stat_cnt)
                    break;
            }

            Debug.Log(number + " Part 2. Calculate Ok");

            //if(number == 2)
            //{
            //    foreach (var obj in temp_rune_info)
            //        Debug.Log(obj.Key + " : " + obj.Value);
            //}

            // rainforce count variable
            int rainforce_cnt = 0;

            // rune rainforce
            while (rainforce_cnt < 4)
            {
                string rainforce_stat = "";
                if (even_stat_type == "SPD")
                {
                    if (prefer_res && temp_rune_info.ContainsKey("RES"))
                    {
                        if (cur_res + plus_res + temp_rune_info["RES"] < min_res)
                            rainforce_stat = "RES";
                        else
                            rainforce_stat = CalRainforceStatNumber(temp_rune_info, pre_option_on);
                    }
                    else if (prefer_crirate && temp_rune_info.ContainsKey("CRI RATE"))
                    {
                        if (cur_crirate + plus_crirate + temp_rune_info["CRI RATE"] < min_crirate)
                            rainforce_stat = "CRI RATE";
                        else
                            rainforce_stat = CalRainforceStatNumber(temp_rune_info, pre_option_on);
                    }
                    else if (prefer_acc && temp_rune_info.ContainsKey("ACC"))
                    {
                        if (cur_acc + plus_acc + temp_rune_info["ACC"] < min_acc)
                            rainforce_stat = "ACC";
                        else
                            rainforce_stat = CalRainforceStatNumber(temp_rune_info, pre_option_on);
                    }
                    else rainforce_stat = CalRainforceStatNumber(temp_rune_info, pre_option_on);
                }
                else rainforce_stat = CalRainforceStatNumber(temp_rune_info, pre_option_on);

                int rainforce_value = CalRainforceValue(stat_rainforce_value[rainforce_stat]);

                //temp_rune_info[rainforce_stat] += rainforce_value;
                //rainforce_cnt += 1;

                if (prefer_crirate && rainforce_stat == "CRI RATE" && cur_crirate + plus_crirate + temp_rune_info[rainforce_stat] < min_crirate)
                // && cur_crirate + plus_crirate + temp_rune_info[rainforce_stat] + rainforce_value >= min_crirate)
                {
                    temp_rune_info[rainforce_stat] += rainforce_value;
                    rainforce_cnt += 1;
                }
                else if (prefer_acc && rainforce_stat == "ACC" && cur_acc + plus_acc + temp_rune_info[rainforce_stat] < min_acc)
                // && cur_acc + plus_acc + temp_rune_info[rainforce_stat] + rainforce_value >= min_acc)
                {
                    temp_rune_info[rainforce_stat] += rainforce_value;
                    rainforce_cnt += 1;
                }
                else if (prefer_res && rainforce_stat == "RES" && cur_res + plus_res + temp_rune_info[rainforce_stat] < min_res)
                // && cur_res + plus_res + temp_rune_info[rainforce_stat] + rainforce_value >= min_res)
                {
                    temp_rune_info[rainforce_stat] += rainforce_value;
                    rainforce_cnt += 1;
                }
                else if (prefer_crirate && rainforce_stat == "CRI RATE" && cur_crirate + plus_crirate + temp_rune_info[rainforce_stat] >= min_crirate ||
                    prefer_acc && rainforce_stat == "ACC" && cur_acc + plus_acc + temp_rune_info[rainforce_stat] >= min_acc ||
                    prefer_res && rainforce_stat == "RES" && cur_res + plus_res + temp_rune_info[rainforce_stat] >= min_res)
                    continue;
                else
                {
                    temp_rune_info[rainforce_stat] += rainforce_value;
                    rainforce_cnt += 1;
                }
            }

            Debug.Log(number + " Part 3. Calculate Ok");
        }

        // conversion rune stat
        string converstion_stat;
        if(pre_option_on) converstion_stat = CalConverstionStatFromRune(temp_rune_info, pre_option_on, temp_rune_info.Keys.ToList()[0]);
        else converstion_stat = CalConverstionStatFromRune(temp_rune_info, pre_option_on, "");

        int converstion_stat_value = CalConversionStatValue(converstion_stat);
        temp_rune_info[converstion_stat] = converstion_stat_value;

        // grinding rune stat
        for (int i = 0; i < temp_rune_info.Count; i++)
        {
            if (temp_rune_info.Keys.ToList()[i] == "HP" || temp_rune_info.Keys.ToList()[i] == "ATK" || temp_rune_info.Keys.ToList()[i] == "DEF")
            {
                temp_rune_info[temp_rune_info.Keys.ToList()[i]] += 10;
            }
            else if (temp_rune_info.Keys.ToList()[i] == "SPD")
            {
                temp_rune_info[temp_rune_info.Keys.ToList()[i]] += 5;
            }
        }

        // calculate plus stat from current stat
        foreach (var dict in temp_rune_info)
        {
            if (dict.Key == "SPD") plus_spd += dict.Value;
            else if (dict.Key == "HP") plus_hp += Mathf.RoundToInt((float)cur_hp * (dict.Value / 100f));
            else if (dict.Key == "ATK") plus_atk += Mathf.RoundToInt((float)cur_atk * (dict.Value / 100f));
            else if (dict.Key == "DEF") plus_def += Mathf.RoundToInt((float)cur_def * (dict.Value / 100f));
            else if (dict.Key == "CRI RATE") plus_crirate += dict.Value;
            else if (dict.Key == "CRI DMG") plus_cridmg += dict.Value;
            else if (dict.Key == "RES") plus_res += dict.Value;
            else if (dict.Key == "ACC") plus_acc += dict.Value;
        }

        //Debug.Log("rune " + number);
        //foreach (var dict in temp_rune_info)
        //{
        //    Debug.Log(dict.Key + " : " + dict.Value);
        //}

        // add stat to rune_stat_infos
        rune_stat_infos.Add(temp_rune_info);
    }
    int CalRainforceValue(int rainforce_value)
    {
        int percentage = Random.Range(1, 100);
        if (percentage > 0 && percentage <= 5) return rainforce_value -= 2;
        else if (percentage > 5 && percentage <= 30) return rainforce_value -= 1;
        else return rainforce_value;
    }
    string CalRainforceStatNumber(Dictionary<string, int> rainforce_stat_dict, bool pre_option_check)
    {
        List<string> temp = new List<string>(rainforce_stat_dict.Keys);

        int percentage = Random.Range(1, 100);

        if (pre_option_check)
        {
            if (percentage > 0 && percentage <= 5) return temp[4];
            else if (percentage > 5 && percentage <= 10) return temp[3];
            else if (percentage > 10 && percentage <= 20) return temp[2];
            else return temp[1];
        }
        else
        {
            if (percentage > 0 && percentage <= 5) return temp[3];
            else if (percentage > 5 && percentage <= 10) return temp[2];
            else if (percentage > 10 && percentage <= 20) return temp[1];
            else return temp[0];
        }
    }
    string CalConverstionStatFromRune(Dictionary<string, int> conversion_stat_dict, bool pre_option_check, string pre_option_stat)
    {
        string check_stat = "";
        int check_max_value = -1;

        foreach (var dict in conversion_stat_dict)
        {
            // excluding pre-option stat
            if (pre_option_check && dict.Key == pre_option_stat)
                continue;

            // excluding stat that do not require conversion
            if (dict.Value > stat_rainforce_value[dict.Key])
                continue;

            // excluding stat that is inefficient to use grinding stone.

            // get difference from check value
            int check_value = stat_rainforce_value[dict.Key] - dict.Value;
            if (check_value > check_max_value)
            {
                check_stat = dict.Key;
                check_max_value = check_value;
            }
            else if (check_value == check_max_value)
            {
                if (prefer_stat_type.Contains(dict.Key) && prefer_stat_type.Contains(check_stat))
                {
                    // check stat priority from prefer stat list
                    int difference = prefer_stat_type.IndexOf(dict.Key) - prefer_stat_type.IndexOf(check_stat);
                    if (difference < 0)
                    {
                        check_stat = dict.Key;
                        check_max_value = check_value;
                    }
                }
                else if (prefer_stat_type.Contains(dict.Key) && !prefer_stat_type.Contains(check_stat))
                {
                    check_stat = dict.Key;
                    check_max_value = check_value;
                }
            }
        }
        return check_stat;
    }
    int CalConversionStatValue(string conversion_stat)
    {
        int conversion_value = 0;
        if (conversion_stat == "HP" || conversion_stat == "ATK" || conversion_stat == "DEF")
            conversion_value = 13;
        else if (conversion_stat == "RES" || conversion_stat == "ACC")
            conversion_value = 11;
        else if (conversion_stat == "SPD" || conversion_stat == "CRI DMG")
            conversion_value = 10;
        else if (conversion_stat == "CRI RATE")
            conversion_value = 9;

        return conversion_value;
    }

    // open rune window
    public void OnRuneClick(int i)
    {
        // get rune data from seleted data.
        even_rune_stat_type = selected_data.GetComponent<select_data_control>().even_rune_stat_type;
        // get rune img from rune box.
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

        if (rune_stat_infos[rune_number].Count == 5)
        {
            ispreoption = true;
            pre_option_stat.gameObject.SetActive(true);
        }

        foreach (var dict in rune_stat_infos[rune_number])
        {
            string percentage = "";

            if (ispreoption)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                pre_option_stat.text = description_key + " + " + dict.Value.ToString() + "%";
                ispreoption = false;
                continue;
            }

            if (order_number == 1)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                first_rune_stat_name.text = description_key;
                if (dict.Key != "SPD") percentage += "%";
                first_rune_stat_amount.text = " + " + dict.Value.ToString() + percentage;
            }
            else if(order_number == 2)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                second_rune_stat_name.text = description_key;
                if (dict.Key != "SPD") percentage += "%";
                second_rune_stat_amount.text = " + " + dict.Value.ToString() + percentage;
            }
            else if(order_number == 3)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

                third_rune_stat_name.text = description_key;
                if (dict.Key != "SPD") percentage += "%";
                third_rune_stat_amount.text = " + " + dict.Value.ToString() + percentage;
            }
            else if(order_number == 4)
            {
                string description_key = dict.Key;
                if (dict.Key == "ACC") description_key = "Accuracy";
                else if (dict.Key == "RES") description_key = "Resistance";

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
    }
    // add artifact stat func
    public void AddArtifactStat(string dir, int value)
    {
        // check artifact type
        if(dir == "left")
        {
            // clear cur artifact stat and reset plus stat
            if (left_artifact_dropdown_values == 1)
            {
                plus_atk -= 100;
            }
            else if (left_artifact_dropdown_values == 2)
            { 
                plus_def -= 100;
            }
            else if (left_artifact_dropdown_values == 3)
            {
                plus_hp -= 1500;
            }

            // reset artifact stat
            left_artifact_dropdown_values = 0;

            // add artifact stat to plus stat
            if (value == 1)
            {
                plus_atk += 100;
                left_artifact_dropdown_values = 1;
            }
            else if (value == 2)
            {
                plus_def += 100;
                left_artifact_dropdown_values = 2;
            }
            else if (value == 3)
            {
                plus_hp += 1500;
                left_artifact_dropdown_values = 3;
            }
        }
        // check artifact type
        else if (dir == "right")
        {
            // clear cur artifact stat and reset plus stat
            if (right_artifact_dropdown_values == 1)
            {
                plus_atk -= 100;
            }
            else if (right_artifact_dropdown_values == 2)
            {
                plus_def -= 100;
            }
            else if (right_artifact_dropdown_values == 3)
            {
                plus_hp -= 1500;
            }

            // reset artifact stat
            right_artifact_dropdown_values = 0;

            // add artifact stat to plus stat
            if (value == 1)
            {
                plus_atk += 100;
                right_artifact_dropdown_values = 1;
            }
            else if (value == 2)
            {
                plus_def += 100;
                right_artifact_dropdown_values = 2;
            }
            else if (value == 3)
            {
                plus_hp += 1500;
                right_artifact_dropdown_values = 3;
            }
        }

        monster_plus_stats_divide[1].text = plus_atk.ToString();
        monster_plus_stats_divide[2].text = plus_def.ToString();
        monster_plus_stats_divide[0].text = plus_hp.ToString();
    }
    // reset plus stat and artifact stat
    public void ResetStat()
    {
        rune_stat_infos.Clear();
        plus_spd = 0;
        plus_atk = 0;
        plus_hp = 0;
        plus_crirate = 0;
        plus_cridmg = 0;
        plus_def = 0;
        plus_res = 0;
        plus_acc = 0;
        right_artifact_dropdown_values = 0;
        left_artifact_dropdown_values = 0;
    }
    // Recalculate Stat
    public void OnClickReCalculateBtn()
    {
        ResetStat();
        // Restart Stat Calculate
        Start_StatSetting();
        // Close Etc Window
        etc_window_control.GetComponent<etc_window_control>().RecalculateEtc();
    }
}
