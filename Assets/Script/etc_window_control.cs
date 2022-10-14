using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class etc_window_control : MonoBehaviour
{
    public GameObject etc_window;
    public GameObject etc_angel;
    public GameObject etc_start;
    public GameObject etc_bg;
    public GameObject etc_notice;
    public GameObject word_bubble_in_stats;
    public GameObject word_bubble_in_angel;
    public GameObject select_data;
    public GameObject loading_canvas;

    Animator warrior_anim;
    Animator angelmon_anim;
    bool isetcopen = false;
    bool check_stat_click = false;
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
    }
    public void EtcClick()
    {
        if (isetcopen) return;
        isetcopen = true;
        angelmon_anim.SetTrigger("IsMotion");
        StartCoroutine(EtcWindowOpen());
    }
    public void RecalculateEtc()
    {
        loading_canvas.SetActive(true);
        EtcWindowControl(false);
        StartCoroutine(ReCalculateStart());
    }
    public void OnClickResetBtn()
    {
        etc_bg.SetActive(false);
        etc_notice.SetActive(true);
    }
    public void OnClickYes()
    {
        SceneManager.LoadScene("Loading");
    }
    public void OnClickNo()
    {
        EtcWindowControl(false);
        etc_bg.SetActive(true);
        etc_notice.SetActive(false);
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
