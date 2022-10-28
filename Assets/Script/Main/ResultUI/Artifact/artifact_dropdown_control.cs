using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class artifact_dropdown_control : MonoBehaviour
{
    #region Public Variable
    public GameObject artifact_manager;
    public Dropdown dropdown;
    public string[] op_title;
    public string artifacttype;
    public int stat_value = 0;
    #endregion

    #region Local Variable
    #endregion
    private void Start()
    {
        StatCheckFunction_UI(0);
    }
    public void Function_Dropdown()
    {
        StatCheckFunction_UI(dropdown.value);
        stat_value = dropdown.value;
    }

    private void StatCheckFunction_UI(int value)
    {
        dropdown.options.Clear();

        for (int i = 0; i < op_title.Length; i++)
        {
            Dropdown.OptionData newData = new Dropdown.OptionData();
            newData.text = op_title[i];
            dropdown.options.Add(newData);
        }

        dropdown.value = value;
        dropdown.itemText.text = op_title[dropdown.value];

        artifact_manager.GetComponent<artifact_manager>().AddArtifactStat(artifacttype, value);
    }
    public void ResetDropdown()
    {
        dropdown.options.Clear();

        for (int i = 0; i < op_title.Length; i++)
        {
            Dropdown.OptionData newData = new Dropdown.OptionData();
            newData.text = op_title[i];
            dropdown.options.Add(newData);
        }

        dropdown.value = 0;
        dropdown.itemText.text = op_title[dropdown.value];
    }
}
