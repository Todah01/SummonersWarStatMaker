using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class etc_window_control : MonoBehaviour
{
    public GameObject etc_window;
    public GameObject etc_angel;
    public GameObject etc_start;
    public GameObject etc_bg;
    public GameObject etc_notice;
    public GameObject word_bubble_in_stats;
    public GameObject select_data;
    public GameObject loading_canvas;
    public GameObject prefer_stat_click_window;
    public Text etc_notice_description;

    Animator warrior_anim;
    Animator angelmon_anim;
    bool isetcopen = false;
    bool check_stat_click = false;
    bool isrecal_click = false;
    string cur_click_btn_name;

    #region Word Bubble
    private void Awake()
    {
        warrior_anim = etc_start.GetComponent<Animator>();
        angelmon_anim = etc_angel.GetComponent<Animator>();
    }
    public void StartRepeatingWordBubble()
    {
        InvokeRepeating("WordBubbleActive", 0f, 5f);
    }
    void WordBubbleActive()
    {
        if (check_stat_click)
        {
            CancelInvoke("WordBubbleActive");
        }
        else
        {
            word_bubble_in_stats.SetActive(true);
            StartCoroutine(WordBubbleCancle());
        }
    }
    IEnumerator WordBubbleCancle()
    {
        yield return new WaitForSeconds(2f);
        word_bubble_in_stats.SetActive(false);
    }
    #endregion Word Bubble

    public void OnClickStartBtn()
    {
        check_stat_click = true;
        warrior_anim.SetTrigger("IsMotion");
        StartCoroutine(CalculateStart());
    }
    public void EtcWindowControl(bool control)
    {
        if (!control) isetcopen = false;
        etc_window.SetActive(control);

        if(isrecal_click)
        {
            isrecal_click = false;
            prefer_stat_click_window.SetActive(control);
            etc_bg.SetActive(!control);
        }
    }
    public void EtcClick()
    {
        if (isetcopen) return;
        isetcopen = true;
        angelmon_anim.SetTrigger("IsMotion");
        StartCoroutine(EtcWindowOpen());
    }
    void OnClickRecalBtnInPrefer()
    {
        isrecal_click = true;
        prefer_stat_click_window.SetActive(true);
        etc_bg.SetActive(false);
    }
    public void RecalculateEtc()
    {
        loading_canvas.SetActive(true);
        EtcWindowControl(false);
        StartCoroutine(ReCalculateStart());
    }
    public void OnClickBtnInEtc(string name)
    {
        if(name == "reset")
        {
            cur_click_btn_name = "reset";
            etc_notice_description.text = "There is no function\nfor save your data.\n\nDo you want to initialize data ?";
        }

        if(name == "recal")
        {
            cur_click_btn_name = "recal";
            etc_notice_description.text = "Do you want to select again\nyour prefer stats?\n(Click background if you want to\ngo back to the previous screen.)";
        }

        etc_bg.SetActive(false);
        etc_notice.SetActive(true);
    }
    public void OnClickYes()
    {
        if(cur_click_btn_name == "reset")
            SceneManager.LoadScene("Loading");

        if(cur_click_btn_name == "recal")
        {
            OnClickRecalBtnInPrefer();
            etc_notice.SetActive(false);
        }  
    }
    public void OnClickNo(string name)
    {
        if (name == "reset")
        {
            EtcWindowControl(false);
            etc_bg.SetActive(true);
            etc_notice.SetActive(false);
        }
        else if (cur_click_btn_name == "reset")
        {
            EtcWindowControl(false);
            etc_bg.SetActive(true);
            etc_notice.SetActive(false);
        }
        else if(cur_click_btn_name == "recal")
        {
            EtcWindowControl(false);
            etc_bg.SetActive(true);
            etc_notice.SetActive(false);
            select_data.GetComponent<select_data_control>().Recalculate_Without_ResetPreferStat();
        }

        cur_click_btn_name = "";
    }
    public void ApplicationQuit()
    {
        Application.Quit();
    }
    IEnumerator CalculateStart()
    {
        yield return new WaitForSeconds(0.5f);
        select_data.GetComponent<select_data_control>().Cal_Start();
    }
    IEnumerator ReCalculateStart()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LoadingDelay());
    }
    IEnumerator EtcWindowOpen()
    {
        yield return new WaitForSeconds(0.5f);
        EtcWindowControl(true);
    }
    IEnumerator LoadingDelay()
    {
        yield return new WaitForSeconds(1.5f);
        loading_canvas.SetActive(false);
    }
}
