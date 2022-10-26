using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class artifact_manager : MonoBehaviour
{
    #region Public Variable
    public Sprite[] incomplete_artifacts;
    public Sprite[] complete_artifacts;
    public GameObject[] plus_15s;
    public GameObject[] artifacts;
    public GameObject resultmanager;
    public GameObject artifact_set_window;
    public Dropdown left_artifact_dropdown;
    public Dropdown right_artifact_dropdown;
    #endregion

    #region Local Variable
    int left_artifact_value, right_artifact_value;
    int left_artifact_dropdown_value;
    int right_artifact_dropdown_value;
    #endregion
    // set sprite artifact
    public void SetSpriteArtifact()
    {
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
    // add artifact stat func
    public void AddArtifactStat(string dir, int value)
    {
        // check artifact type
        if (dir == "left")
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
}
