using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class splash_manager : MonoBehaviour
{
    #region Public Variable
    public GameObject irene_animation;
    public GameObject profile_animation;
    #endregion

    #region Local Variable
    Animator irene_anim_controllor;
    bool isStart = false;
    bool isApply = false;
    #endregion

    private void Awake()
    {
        // Get irene animation controllor from irene
        irene_anim_controllor = irene_animation.GetComponent<Animator>();
    }
    async void Start()
    {
        // check app update from google play store
        if (googleplay_manager.Instance != null)
            await googleplay_manager.Instance.UpdateApp();
    }
    // Set irene animation time
    public void OnClickIrene()
    {
        if (!isApply) return;

        if (isStart) return;

        isStart = true;
        irene_anim_controllor.SetTrigger("IsMotion");
        StartCoroutine(GoToMain());
    }
    IEnumerator GoToMain()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main");
    }

    #region Splash image Control
    public void OffProfile()
    {
        profile_animation.SetActive(false);
    }
    public void OnApply()
    {
        isApply = true;
    }
    #endregion
}
