using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class loading_manager : MonoBehaviour
{
    public GameObject irene_animation;
    public GameObject profile_animation;

    Animator irene_anim_controllor;
    bool isStart = false;
    bool isApply = false;
    async void Start()
    {
        if (googleplay_manager.Instance != null)
            await googleplay_manager.Instance.UpdateApp();
    }
    private void Awake()
    {
        irene_anim_controllor = irene_animation.GetComponent<Animator>();
    }
    public void OnClickIrene()
    {
        if (!isApply) return;

        if (isStart) return;

        isStart = true;
        irene_anim_controllor.SetTrigger("IsMotion");
        StartCoroutine(GoToMain());
    }
    public void OffProfile()
    {
        profile_animation.SetActive(false);
    }
    public void OnApply()
    {
        isApply = true;
    }
    IEnumerator GoToMain()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Select");
    }
}
