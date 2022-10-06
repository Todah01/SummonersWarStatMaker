using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class artifact_dropdown_control : MonoBehaviour
{
    public GameObject resultmanager;
    public Dropdown dropdown;
    public string[] op_title;
    public string artifacttype;
    public int stat_value = 0;

    private void Start()
    {
        StatCheckFunction_UI(0);
    }
    private void Update()
    {
        dropdown.onValueChanged.AddListener(delegate
        {
            Function_Dropdown(dropdown);
        });
    }
    private void Function_Dropdown(Dropdown select)
    {
        StatCheckFunction_UI(dropdown.value);
        stat_value = dropdown.value;
        //string op = select.options[select.value].text;
        //Debug.Log("Dropdown Change!\n" + op);
    }

    private void StatCheckFunction_UI(int value)
    {
        dropdown.onValueChanged.RemoveAllListeners();
        dropdown.options.Clear();

        for (int i = 0; i < op_title.Length; i++)
        {
            Dropdown.OptionData newData = new Dropdown.OptionData();
            newData.text = op_title[i];
            dropdown.options.Add(newData);
        }
        dropdown.SetValueWithoutNotify(-1);
        dropdown.SetValueWithoutNotify(0);

        dropdown.value = value;
        dropdown.itemText.text = op_title[dropdown.value];

        resultmanager.GetComponent<result_manager>().AddArtifactStat(artifacttype, value);
    }
    public void ResetDropdown()
    {
        dropdown.onValueChanged.RemoveAllListeners();
        dropdown.options.Clear();

        for (int i = 0; i < op_title.Length; i++)
        {
            Dropdown.OptionData newData = new Dropdown.OptionData();
            newData.text = op_title[i];
            dropdown.options.Add(newData);
        }
        dropdown.SetValueWithoutNotify(-1);
        dropdown.SetValueWithoutNotify(0);

        dropdown.value = 0;
        dropdown.itemText.text = op_title[dropdown.value];
    }
}
