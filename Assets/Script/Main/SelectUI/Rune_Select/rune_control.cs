using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_control : MonoBehaviour
{
    #region public Variable
    public List<rune_set_class> runes;
    #endregion

    private void Start()
    {
        #region Init rune data setting
        foreach(var rune in runes)
        {
            rune.InitSetting();
        }
        #endregion
    }
    public bool CheckAncientRune(string rune_type)
    {
        bool check_ancient = false;
        foreach(var rune in runes)
        {
            if(rune.rune_data.name == rune_type)
            {
                check_ancient = rune.rune_data.isAncient;
                break;
            }
        }

        return check_ancient;
    }
}
