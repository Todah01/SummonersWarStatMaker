using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class select_data_control : MonoBehaviour
{
    #region Public Variable
    public GameObject[] rune_slots;
    public GameObject[] rune_previews;
    public GameObject[] rune_stats_prefer;
    public GameObject[] rune_stats_prefer_recheck;
    public GameObject[] even_runes;
    public GameObject resultmanager;
    public GameObject btn_cal_start;
    public GameObject etc_bg;
    public GameObject rune_check_window;
    public GameObject loading_canvas;
    public GameObject result_ui;
    public GameObject selected_monster_bg;
    public GameObject selected_rune_bg;
    public GameObject selected_prefer_stat_bg;
    public GameObject pirate;
    public GameObject angelmon_yellow;
    public GameObject word_bubble;
    public GameObject monster_name_drop;
    public GameObject word_bubble_in_angel;
    public GameObject angel_mon;
    public GameObject artifact_boxes;
    public GameObject left_artifact_dropdown;
    public GameObject right_artifact_dropdown;
    public Text selected_monster;
    public Text selected_rune_set;
    public Text selected_rune_set_stat;
    public Text selected_stat;
    public Text msg_error;

    public List<int> rune_dropdown_values;
    public List<string> rune_names;
    public List<string> rune_type;
    public List<string> even_rune_stat_type;
    public List<string> prefer_stat_type;
    public bool isAncient;
    #endregion

    #region Local Variable
    Animator pirate_anim;
    Animator angelmon_yellow_anim;
    int rune_cnt = 6;
    int even_rune_stat_cnt = 3;
    int prefer_stat_cnt = 4;
    bool ispirateon = false;
    bool isangelyellow = false;
    bool check_monster = false;
    bool check_rune = false;
    bool check_even_rune_stat = false;
    bool check_prefer_stat = false;
    #endregion
    private void Awake()
    {
        pirate_anim = pirate.GetComponent<Animator>();
        angelmon_yellow_anim = angelmon_yellow.GetComponent<Animator>();
    }
    public void Cal_Start()
    {
        string monster_name = monster_name_drop.GetComponent<search_inputfield_control>().CheckMonsterName.Trim();

        if (monster_name != "") check_monster = true;

        foreach (var obj in rune_slots)
        {
            if (obj.GetComponent<rune_slot_control>().dropdown_value != 0)
            {
                rune_dropdown_values.Add(obj.GetComponent<rune_slot_control>().dropdown_value);
                rune_names.Add(obj.GetComponent<rune_slot_control>().rune_info);
            }
        }

        if (rune_dropdown_values.Count == rune_cnt)
            check_rune = true;

        foreach (var obj in rune_previews)
        {
            string cur_rune_set = "";

            if (obj.activeSelf)
            {
                cur_rune_set = obj.transform.Find("rune_preview_text").GetComponent<Text>().text;
                rune_type.Add(cur_rune_set);
            }
        }

        foreach (var obj in even_runes)
        {
            string cur_even_rune_stat = obj.GetComponent<rune_slot_control>().rune_stat_string;
            if (cur_even_rune_stat != "")
                even_rune_stat_type.Add(cur_even_rune_stat);
        }

        if (even_rune_stat_type.Count == even_rune_stat_cnt)
            check_even_rune_stat = true;

        foreach (var obj in rune_stats_prefer)
        {
            string cur_prefer_stat = obj.GetComponent<rune_stat_select_control>().stat_string;
            if (cur_prefer_stat != "")
                prefer_stat_type.Add(cur_prefer_stat);
        }

        if (prefer_stat_type.Count == prefer_stat_cnt)
            check_prefer_stat = true;

        // Debug.Log(rune_dropdown_values.Count + " " + even_rune_stat_type.Count + " " + prefer_stat_type.Count);

        selected_monster.text = monster_name;
        selected_rune_set.text = string.Join("\n\n", rune_type);
        selected_rune_set_stat.text = string.Join("\n\n", even_rune_stat_type);
        selected_stat.text = string.Join(", ", prefer_stat_type);

        etc_bg.SetActive(true);
        rune_check_window.SetActive(true);
    }

    public void Cal_Reset()
    {
        rune_type.Clear();
        rune_dropdown_values.Clear();
        rune_names.Clear();

        even_rune_stat_type.Clear();
        prefer_stat_type.Clear();

        btn_cal_start.GetComponent<Button>().interactable = true;
        selected_monster_bg.GetComponent<Image>().DOKill();
        selected_rune_bg.GetComponent<Image>().DOKill();
        selected_prefer_stat_bg.GetComponent<Image>().DOKill();
        msg_error.text = "";
        word_bubble.SetActive(false);
        pirate.SetActive(false);
        etc_bg.SetActive(false);
        rune_check_window.SetActive(false);
    }
    public void AngelMonYellowControl()
    {
        if (isangelyellow) return;
        isangelyellow = true;
        angelmon_yellow_anim.SetTrigger("IsMotion", () =>
        {
            isangelyellow = false;
        });
        // Set Ancient Rune Effect
        if (isAncient == false)
        {
            isAncient = true;
        }
        else
        {
            isAncient = false;
        }
    }
    public void ResetPreferStat()
    {
        prefer_stat_type.Clear();

        foreach (var obj in rune_stats_prefer_recheck)
        {
            string cur_prefer_stat = obj.GetComponent<rune_stat_select_control>().stat_string;
            if (cur_prefer_stat != "")
                prefer_stat_type.Add(cur_prefer_stat);
        }

        if (prefer_stat_type.Count == prefer_stat_cnt)
            check_prefer_stat = true;

        resultmanager.GetComponent<result_calculate_manager>().OnClickReCalculateBtn();
    }
    public void Recalculate_Without_ResetPreferStat()
    {
        left_artifact_dropdown.GetComponent<artifact_dropdown_control>().ResetDropdown();
        right_artifact_dropdown.GetComponent<artifact_dropdown_control>().ResetDropdown();
        artifact_boxes.GetComponent<artifact_manager>().SetSpriteArtifact();
        resultmanager.GetComponent<result_calculate_manager>().OnClickReCalculateBtn();
    }
    public void OnClickPirate()
    {
        if (ispirateon) return;

        ispirateon = true;
        word_bubble.SetActive(true);
        pirate_anim.SetTrigger("IsMotion", () =>
        {
            ispirateon = false;
        });
        Invoke("WordBubbleClose", 2f);
    }
    void WordBubbleClose()
    {
        word_bubble.SetActive(false);
    }
    public void loadingOn(bool check)
    {
        loading_canvas.SetActive(check);
    }
    public void ResultWindowOpen()
    {
        if (check_monster && check_rune && check_even_rune_stat && check_prefer_stat)
        {
            resultmanager.GetComponent<result_calculate_manager>().Start_StatSetting();
            StartCoroutine(OpenResultWindow());
        }
        else
        {
            btn_cal_start.GetComponent<Button>().interactable = false;
            btn_cal_start.transform.DOPunchPosition(new Vector3(10f, 0, 0), 0.75f, 50, 0f);
            StartCoroutine(ShakeEffect());
        }
    }
    IEnumerator OpenResultWindow()
    {
        yield return new WaitForSeconds(2f);
        loadingOn(false);
        result_ui.SetActive(true);

        yield return new WaitForSeconds(1f);
        word_bubble_in_angel.SetActive(true);
        angel_mon.SetActive(true);

        yield return new WaitForSeconds(3f);
        word_bubble_in_angel.SetActive(false);
    }
    IEnumerator ShakeEffect()
    {
        yield return new WaitForSeconds(1f);

        loadingOn(false);
        pirate.SetActive(true);

        if(!check_monster)
        {
            msg_error.text = "selected data";
            selected_monster_bg.GetComponent<Text>()
                .DOFade(0.2f, 0.25f)
                .SetEase(Ease.OutSine)
                .SetLoops(4, LoopType.Yoyo);
        }

        if (!check_rune || !check_even_rune_stat)
        {
            msg_error.text = "selected data";
            selected_rune_bg.GetComponent<Text>()
                .DOFade(0.2f, 0.25f)
                .SetEase(Ease.OutSine)
                .SetLoops(4, LoopType.Yoyo);
        }
            
        if (!check_prefer_stat)
        {
            msg_error.text = "selected data";
            selected_prefer_stat_bg.GetComponent<Text>()
                .DOFade(0.2f, 0.25f)
                .SetEase(Ease.OutSine)
                .SetLoops(4, LoopType.Yoyo);
        }
    }
}
