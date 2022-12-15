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
}
