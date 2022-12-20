using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_set_preview_control : MonoBehaviour
{
    #region Public Variable
    public GameObject rune_datas;
    #endregion

    #region Local Variable
    List<rune_set_class> rune_infos;
    Dictionary<string, int> activate_rune_sets;
    #endregion

    private void Awake()
    {
        rune_infos = rune_datas.GetComponent<rune_control>().runes;
    }
    public void RuneSetPreviewSetting(string type, string act)
    {
        switch(act)
        {
            case "subtract":
                // Subtract rune count
                foreach (var info in rune_infos)
                {
                    if (info.rune_data.name == type)
                    {
                        info.rune_data.rune_count -= Mathf.Max(0, info.rune_data.rune_count - 1);
                        break;
                    }
                }
                SetActiveRuneSet();
                break;

            case "add":
                // Add rune count
                foreach (var info in rune_infos)
                {
                    if (info.rune_data.name == type)
                    {
                        info.rune_data.rune_count += 1;
                        break;
                    }
                }
                SetActiveRuneSet();
                break;
        }
    }

    private void SetActiveRuneSet()
    {
        foreach(var info in rune_infos)
        {
            if(info.rune_data.rune_count >= info.rune_data.number_of_actives)
            {
                activate_rune_sets.Add(info.rune_data.name, info.rune_data.rune_count / info.rune_data.number_of_actives);
            }
        }


    }
}
