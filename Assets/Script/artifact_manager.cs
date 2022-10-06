using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class artifact_manager : MonoBehaviour
{
    public Sprite[] incomplete_artifacts;
    public Sprite[] complete_artifacts;
    public GameObject[] plus_15s;
    public GameObject[] artifacts;
    public GameObject resultmanager;
    public GameObject artifact_set_window;

    int left_artifact_value, right_artifact_value;
    // set sprite artifact
    public void SetSpriteArtifact()
    {
        left_artifact_value = resultmanager.GetComponent<result_manager>().left_artifact_dropdown_values;
        right_artifact_value = resultmanager.GetComponent<result_manager>().right_artifact_dropdown_values;

        if (left_artifact_value != 0)
        {
            artifacts[0].GetComponent<Image>().sprite = complete_artifacts[0];
            plus_15s[0].SetActive(true);
        }
        else
        {
            artifacts[0].GetComponent<Image>().sprite = incomplete_artifacts[0];
            plus_15s[0].SetActive(false);
        }

        if (right_artifact_value != 0)
        {
            artifacts[1].GetComponent<Image>().sprite = complete_artifacts[1];
            plus_15s[1].SetActive(true);
        }
        else
        {
            artifacts[1].GetComponent<Image>().sprite = incomplete_artifacts[1];
            plus_15s[1].SetActive(false);
        }
    }
    public void OpenArtifactWindow()
    {
        artifact_set_window.SetActive(true);
    }
    public void CloseArtifactWindow()
    {
        SetSpriteArtifact();
        artifact_set_window.SetActive(false);
    }
}
