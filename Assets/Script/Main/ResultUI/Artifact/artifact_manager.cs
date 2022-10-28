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
    public GameObject result_calculate_manager;
    public GameObject artifact_set_window;
    public Dropdown left_artifact_dropdown;
    public Dropdown right_artifact_dropdown;
    public Text monster_hp;
    public Text monster_atk;
    public Text monster_def;
    #endregion

    #region Local Variable
    int left_artifact_dropdown_value = 0;
    int right_artifact_dropdown_value = 0;
    int temp_hp, temp_atk, temp_def;
    #endregion

    // set sprite artifact
    public void SetSpriteArtifact()
    {
        if (left_artifact_dropdown_value != 0)
        {
            artifacts[0].GetComponent<Image>().sprite = complete_artifacts[0];
            plus_15s[0].SetActive(true);
        }
        else
        {
            artifacts[0].GetComponent<Image>().sprite = incomplete_artifacts[0];
            plus_15s[0].SetActive(false);
        }

        if (right_artifact_dropdown_value != 0)
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
        temp_atk = result_calculate_manager.GetComponent<result_calculate_manager>().divide_stats_plus[1];
        temp_def = result_calculate_manager.GetComponent<result_calculate_manager>().divide_stats_plus[2];
        temp_hp = result_calculate_manager.GetComponent<result_calculate_manager>().divide_stats_plus[0];
        
        // check artifact type
        if (dir == "left")
        {
            // clear cur artifact stat and reset plus stat
            if (left_artifact_dropdown_value == 1)
            {
                temp_atk -= 100;
            }
            else if (left_artifact_dropdown_value == 2)
            {
                temp_def -= 100;
            }
            else if (left_artifact_dropdown_value == 3)
            {
                temp_hp -= 1500;
            }

            // reset artifact stat
            left_artifact_dropdown_value = 0;

            // add artifact stat to plus stat
            if (value == 1)
            {
                temp_atk += 100;
                left_artifact_dropdown_value = 1;
            }
            else if (value == 2)
            {
                temp_def += 100;
                left_artifact_dropdown_value = 2;
            }
            else if (value == 3)
            {
                temp_hp += 1500;
                left_artifact_dropdown_value = 3;
            }
        }
        // check artifact type
        else if (dir == "right")
        {
            // clear cur artifact stat and reset plus stat
            if (right_artifact_dropdown_value == 1)
            {
                temp_atk -= 100;
            }
            else if (right_artifact_dropdown_value == 2)
            {
                temp_def -= 100;
            }
            else if (right_artifact_dropdown_value == 3)
            {
                temp_hp -= 1500;
            }

            // reset artifact stat
            right_artifact_dropdown_value = 0;

            // add artifact stat to plus stat
            if (value == 1)
            {
                temp_atk += 100;
                right_artifact_dropdown_value = 1;
            }
            else if (value == 2)
            {
                temp_def += 100;
                right_artifact_dropdown_value = 2;
            }
            else if (value == 3)
            {
                temp_hp += 1500;
                right_artifact_dropdown_value = 3;
            }
        }

        result_calculate_manager.GetComponent<result_calculate_manager>().divide_stats_plus[1] = temp_atk;
        result_calculate_manager.GetComponent<result_calculate_manager>().divide_stats_plus[2] = temp_def;
        result_calculate_manager.GetComponent<result_calculate_manager>().divide_stats_plus[0] = temp_hp;

        monster_atk.text = temp_atk.ToString();
        monster_def.text = temp_def.ToString();
        monster_hp.text = temp_hp.ToString();
    }
}
