using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class result_calculate_manager : MonoBehaviour
{
    #region Public Variable
    public GameObject etc_window_control;
    public GameObject selected_data;
    public GameObject GoogleDataManager;
    public GameObject Rune_Data;
    public List<int> divide_stats_cur, divide_stats_plus, comp_stats;
    public List<List<int>> grinding_stat_list = new List<List<int>>();
    public Dictionary<int, string> conversion_dict = new Dictionary<int, string>();
    public List<Dictionary<string, int>> rune_stat_infos = new List<Dictionary<string, int>>();
    public List<Dictionary<string, int>> rune_stat_sum_info = new List<Dictionary<string, int>>();
    #endregion

    #region Local Variable
    List<rune_set_class> rune_datas;
    Dictionary<string, int> stat_rainforce_value = new Dictionary<string, int>()
    {
        {"SPD", 6}, {"HP", 8}, {"ATK", 8}, {"DEF", 8}, {"CRI RATE", 6}, {"CRI DMG", 7}, {"ACC", 8}, {"RES", 8}, {"HP+", 375}, {"ATK+", 20}, {"DEF+", 20}
    };
    Dictionary<string, int> ancient_stat_rainforce_value = new Dictionary<string, int>()
    {
        {"SPD", 7}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 7}, {"CRI DMG", 9}, {"ACC", 10}, {"RES", 10}, {"HP+", 435}, {"ATK+", 24}, {"DEF+", 24}
    };
    List<Dictionary<string, int>> separate_stats = new List<Dictionary<string, int>>();
    List<string> rune_name_infos;
    List<string> rune_type;
    List<string> even_rune_stat_type;
    List<string> prefer_stat_type;
    bool check_ancient;
    bool check_legendary;
    int cur_hp, cur_atk, cur_def, cur_spd, cur_crirate, cur_cridmg, cur_res, cur_acc;
    int plus_hp, plus_atk, plus_def, plus_spd, plus_crirate, plus_cridmg, plus_res, plus_acc;
    #endregion

    private void Awake()
    {
        // get rune data
        rune_datas = Rune_Data.GetComponent<rune_control>().runes;
        // set separate_stats
        Dictionary<string, int> separate_stat_1 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}, {"HP+", 10}
        };
        Dictionary<string, int> separate_stat_2 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}, {"HP+", 10}, {"ATK+", 10}, {"DEF+", 10}
        };
        Dictionary<string, int> separate_stat_3 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}, {"HP+", 10}
        };
        Dictionary<string, int> separate_stat_4 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}, {"HP+", 10}, {"ATK+", 10}, {"DEF+", 10}
        };
        Dictionary<string, int> separate_stat_5 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}, {"ATK+", 10}, {"DEF+", 10}
        };
        Dictionary<string, int> separate_stat_6 = new Dictionary<string, int>()
        {
            {"SPD", 10}, {"HP", 10}, {"ATK", 10}, {"DEF", 10}, {"CRI RATE", 10}, {"CRI DMG", 10}, {"ACC", 10}, {"RES", 10}, {"HP+", 10}, {"ATK+", 10}, {"DEF+", 10}
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
        #region Get selected data
        // get selected data
        rune_name_infos = selected_data.GetComponent<select_data_control>().rune_names;
        rune_type = selected_data.GetComponent<select_data_control>().rune_type;
        even_rune_stat_type = selected_data.GetComponent<select_data_control>().even_rune_stat_type;
        prefer_stat_type = selected_data.GetComponent<select_data_control>().prefer_stat_type;
        check_ancient = selected_data.GetComponent<select_data_control>().isAncient;
        check_legendary = selected_data.GetComponent<select_data_control>().isLegendary;
        #endregion

        #region Get stat data from DB.
        // get basic monster stat data
        cur_hp = GoogleDataManager.GetComponent<googlesheet_manager>().hp;
        cur_atk = GoogleDataManager.GetComponent<googlesheet_manager>().atk;
        cur_def = GoogleDataManager.GetComponent<googlesheet_manager>().def;
        cur_spd = GoogleDataManager.GetComponent<googlesheet_manager>().spd;
        cur_crirate = GoogleDataManager.GetComponent<googlesheet_manager>().crirate;
        cur_cridmg = GoogleDataManager.GetComponent<googlesheet_manager>().cridmg;
        cur_res = GoogleDataManager.GetComponent<googlesheet_manager>().res;
        cur_acc = GoogleDataManager.GetComponent<googlesheet_manager>().acc;
        #endregion

        #region Check rune set effect to stat.
        // add rune set effect stat
        for (int i=0; i<rune_type.Count; i++)
        {
            switch(rune_type[i])
            {
                case "Swift":
                    foreach(var data in rune_datas)
                    {
                        if(data.rune_data.name == rune_type[i])
                        {
                            plus_spd += data.get_set_effect(cur_spd);
                        }
                    }
                    break;
                case "Blade":
                    foreach (var data in rune_datas)
                    {
                        if (data.rune_data.name == rune_type[i])
                        {
                            plus_crirate += data.get_set_effect(cur_crirate);
                        }
                    }
                    break;
                case "Focus":
                    foreach (var data in rune_datas)
                    {
                        if (data.rune_data.name == rune_type[i])
                        {
                            plus_acc += data.get_set_effect(cur_acc);
                        }
                    }
                    break;
                case "Energy":
                    foreach (var data in rune_datas)
                    {
                        if (data.rune_data.name == rune_type[i])
                        {
                            plus_hp += data.get_set_effect(cur_hp);
                        }
                    }
                    break;
                case "Endure":
                    foreach (var data in rune_datas)
                    {
                        if (data.rune_data.name == rune_type[i])
                        {
                            plus_res += data.get_set_effect(cur_res);
                        }
                    }
                    break;
                case "Rage":
                    foreach (var data in rune_datas)
                    {
                        if (data.rune_data.name == rune_type[i])
                        {
                            plus_cridmg += data.get_set_effect(cur_cridmg);
                        }
                    }
                    break;
                case "Guard":
                    foreach (var data in rune_datas)
                    {
                        if (data.rune_data.name == rune_type[i])
                        {
                            plus_def += data.get_set_effect(cur_def);
                        }
                    }
                    break;
                case "Fatal":
                    foreach (var data in rune_datas)
                    {
                        if (data.rune_data.name == rune_type[i])
                        {
                            plus_atk += data.get_set_effect(cur_atk);
                        }
                    }
                    break;
            }
        }
        #endregion

        // Calculate monster stat by swsm logic.
        Cal_Stat(rune_type, even_rune_stat_type, prefer_stat_type, cur_hp, cur_atk, cur_def, cur_spd);

        #region Save stat after calculate.
        // save monster stat
        divide_stats_cur.Add(cur_hp);
        divide_stats_cur.Add(cur_atk);
        divide_stats_cur.Add(cur_def);
        divide_stats_cur.Add(cur_spd);

        divide_stats_plus.Add(plus_hp);
        divide_stats_plus.Add(plus_atk);
        divide_stats_plus.Add(plus_def);
        divide_stats_plus.Add(plus_spd);

        comp_stats.Add(Mathf.Min(100, cur_crirate + plus_crirate));
        comp_stats.Add(cur_cridmg + plus_cridmg);
        comp_stats.Add(Mathf.Min(100, cur_res + plus_res));
        comp_stats.Add(Mathf.Min(100, cur_acc + plus_acc));
        #endregion

        // Send signal to result_ui_manager.
        this.gameObject.BroadcastMessage("Set_data_to_result_ui");
    }
    // Calculate monster stat data based on swsm logic.
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
    // Check even rune stat.
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
    // Calculate stat based on selected prefer stats.
    void CalStatFromPreferStat(int number)
    {
        // Set strategy pattern. (receive rune stat)
        #region Set rune stat scoreboard
        Dictionary<string, int> stat_scoreboard = separate_stats[number - 1];
        Dictionary<string, int> stat_rainforceboard = new Dictionary<string, int>();
        Dictionary<string, int> conversion_stat_dict = new Dictionary<string, int>();
        Dictionary<string, int> grind_stat_dict = new Dictionary<string, int>();
        int rainforce_limit = 0;
        // set rune level
        if (check_legendary)
            rainforce_limit = 4;
        else
            rainforce_limit = 3;
        // get rune data from seleted data
        even_rune_stat_type = selected_data.GetComponent<select_data_control>().even_rune_stat_type;
        // check prefer stat and plus score in separte_stats
        prefer_stat_type = selected_data.GetComponent<select_data_control>().prefer_stat_type;
        int prefer_stat_add_value = 5;
        foreach(var stat in prefer_stat_type)
        {
            if(stat_scoreboard.ContainsKey(stat))
            {
                stat_scoreboard[stat] += prefer_stat_add_value;
                prefer_stat_add_value--;
            }
        }

        //Check prefer stat and set fix stat value.
        if(prefer_stat_type.Contains("HP") && prefer_stat_type.Contains("DEF"))
        {
            if(stat_scoreboard.ContainsKey("HP+"))
                stat_scoreboard["HP+"] += 1;
        }
        else if (prefer_stat_type.Contains("HP") && prefer_stat_type.Contains("RES"))
        {
            if (stat_scoreboard.ContainsKey("DEF"))
                stat_scoreboard["DEF"] += 2;

            if (stat_scoreboard.ContainsKey("HP+"))
                stat_scoreboard["HP+"] += 1;
        }

        // sort stat_scoreboard by value
        stat_scoreboard = stat_scoreboard.OrderByDescending(item => item.Value).ToDictionary(x => x.Key, x => x.Value);

        //foreach(var stat in stat_scoreboard)
        //{
        //    Debug.Log(stat.Key + ":" + stat.Value + "\n");
        //}

        // Set temporary dictionary for save rune info
        Dictionary<string, int> temp_rune_info = new Dictionary<string, int>();
        Dictionary<string, int> temp_rune_stat_sum_info = new Dictionary<string, int>();
        List<int> temp_grinding_info = new List<int>();
        #endregion

        #region Check rune info whether ancient or not.
        if(check_ancient)
        {
            string cur_rune_type = rune_name_infos[number - 1];
            // Search current rune type.
            foreach(var data in rune_datas)
            {
                // find rune type and check ancient boolean
                if(data.rune_data.name == cur_rune_type)
                {
                    if (data.rune_data.isAncient)
                    {
                        stat_rainforceboard = ancient_stat_rainforce_value;
                        conversion_stat_dict = rune_stat_data.conversion_ancient_rune;
                        grind_stat_dict = rune_stat_data.grind_ancient_rune;
                        break;
                    }
                    else
                    {
                        stat_rainforceboard = stat_rainforce_value;
                        conversion_stat_dict = rune_stat_data.conversion_normal_rune;
                        grind_stat_dict = rune_stat_data.grind_normal_rune;
                        break;
                    }
                }
            }
        }
        else
        {
            stat_rainforceboard = stat_rainforce_value;
            conversion_stat_dict = rune_stat_data.conversion_normal_rune;
            grind_stat_dict = rune_stat_data.grind_normal_rune;
        }

        // Debug.Log(conversion_stat_dict.Count + " : " + grind_stat_dict.Count);
        #endregion

        #region Variable to limit stat that don't spill over target stat.
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

        #region Set target stat based on selected prefer stats.
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
        #endregion

        #region Set pre_option stat.
        // Set percentage if rune has pre-option or not.
        int pre_option_percentage = Random.Range(1, 101);
        if (pre_option_percentage > 31) pre_option_possible = true;
        
        // Set pre-option
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
        #endregion

        #region Calculate odd number rune.
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

                int rainforce_value = 0;
                // Check ancient boolean and set base stat value.
                rainforce_value = CalRainforceValue(stat_rainforceboard[prefer_stat_type[i]]);
                
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

                    int rainforce_value = 0;
                    // Check ancient boolean and set base stat value.
                    rainforce_value = CalRainforceValue(stat_rainforceboard[key]);

                    temp_rune_info.Add(key, rainforce_value);
                }

                if (temp_rune_info.Count == rune_stat_cnt)
                    break;
            }

            Debug.Log(number + " Part 2. Calculate Ok");

            foreach(var dict in temp_rune_info)
            {
                Debug.Log(dict.Key + ":" + dict.Value + "\n");
            }

            // rainforce count variable
            int rainforce_cnt = 0;

            // rune rainforce
            while (rainforce_cnt < rainforce_limit)
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
        #endregion

        #region Calculate even number rune.
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

                int rainforce_value = 0;
                // Check ancient boolean and set base stat value.
                rainforce_value = CalRainforceValue(stat_rainforceboard[prefer_stat_type[i]]);

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

                    int rainforce_value = 0;
                    // Check ancient boolean and set base stat value.
                    rainforce_value = CalRainforceValue(stat_rainforceboard[key]);

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
            while (rainforce_cnt < rainforce_limit)
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
        #endregion

        #region Conversion rune stat after rainforce.
        // conversion rune stat
        string conversion_stat;
        if(pre_option_on) conversion_stat = CalConversionStatFromRune(temp_rune_info, pre_option_on, temp_rune_info.Keys.ToList()[0]);
        else conversion_stat = CalConversionStatFromRune(temp_rune_info, pre_option_on, "");

        if(conversion_stat != "")
        {
            int converstion_stat_value = conversion_stat_dict[conversion_stat];
            // Add conversion infomation to dict
            conversion_dict.Add(number, conversion_stat);
            temp_rune_info[conversion_stat] = converstion_stat_value;
        }
        else
        {
            conversion_dict.Add(number, null);
        }
        #endregion

        // copy rune infomation to temp_rune_stat_sum_info.
        foreach (var dict in temp_rune_info)
        {
            temp_rune_stat_sum_info.Add(dict.Key, dict.Value);
        }

        #region Grinding stats of rune.
        // grinding rune stat
        for (int i = 0; i < temp_rune_stat_sum_info.Count; i++)
        {
            string grind_stat = temp_rune_stat_sum_info.Keys.ToList()[i];
            if (grind_stat_dict.ContainsKey(grind_stat))
            {
                int grind_stat_value = grind_stat_dict[grind_stat];
                // Add grind infomation in dict
                temp_rune_stat_sum_info[grind_stat] += grind_stat_value;
                temp_grinding_info.Add(grind_stat_value);
            }
            else
            {
                temp_grinding_info.Add(0);
            }
        }
        #endregion

        #region Add calculated stat to plus stat.
        // calculate plus stat from current stat
        foreach (var dict in temp_rune_stat_sum_info)
        {
            if (dict.Key == "SPD") plus_spd += dict.Value;
            else if (dict.Key == "HP") plus_hp += Mathf.RoundToInt((float)cur_hp * (dict.Value / 100f));
            else if (dict.Key == "ATK") plus_atk += Mathf.RoundToInt((float)cur_atk * (dict.Value / 100f));
            else if (dict.Key == "DEF") plus_def += Mathf.RoundToInt((float)cur_def * (dict.Value / 100f));
            else if (dict.Key == "CRI RATE") plus_crirate += dict.Value;
            else if (dict.Key == "CRI DMG") plus_cridmg += dict.Value;
            else if (dict.Key == "RES") plus_res += dict.Value;
            else if (dict.Key == "ACC") plus_acc += dict.Value;
            else if (dict.Key == "HP+") plus_hp += dict.Value;
            else if (dict.Key == "ATK+") plus_atk += dict.Value;
            else if (dict.Key == "DEF+") plus_def += dict.Value;
        }
        #endregion

        // Add grinding stat infomation to list.
        grinding_stat_list.Add(temp_grinding_info);
        // Add rune stat infomation to dictionary.
        rune_stat_infos.Add(temp_rune_info);
        // Add rune stat sum infomation to dictionary.
        rune_stat_sum_info.Add(temp_rune_stat_sum_info);

        //foreach(var dict in temp_rune_info)
        //{
        //    Debug.Log(dict.Key + ":" + dict.Value + "\n");
        //}
    }

    // Set rainforce stat based on stat type and percentage.
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
    // Set rainforce stat value based on stat type and percentage.
    int CalRainforceValue(int rainforce_value)
    {
        int percentage = Random.Range(1, 100);
        if (percentage > 0 && percentage <= 5) return rainforce_value -= Mathf.CeilToInt(rainforce_value * 0.25f);
        else if (percentage > 5 && percentage <= 30) return rainforce_value -= Mathf.CeilToInt(rainforce_value * 0.125f);
        else return rainforce_value;
    }
    // Set conversion stat based on swsm logic.
    // Set strategy pattern. (receive rune conversion stat)
    string CalConversionStatFromRune(Dictionary<string, int> conversion_stat_dict, bool pre_option_check, string pre_option_stat)
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
            if (dict.Key == "HP+" || dict.Key == "DEF+" || dict.Key == "ATK+")
                continue;

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
    // Reset data to need calculating.
    public void ResetStat()
    {
        rune_stat_infos.Clear();
        rune_stat_sum_info.Clear();
        grinding_stat_list.Clear();
        conversion_dict.Clear();
        divide_stats_cur.Clear();
        divide_stats_plus.Clear();
        comp_stats.Clear();
        plus_spd = 0;
        plus_atk = 0;
        plus_hp = 0;
        plus_crirate = 0;
        plus_cridmg = 0;
        plus_def = 0;
        plus_res = 0;
        plus_acc = 0;
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
