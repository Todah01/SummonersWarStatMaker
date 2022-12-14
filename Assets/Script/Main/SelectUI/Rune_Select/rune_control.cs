using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_control : MonoBehaviour
{
    #region Get rune data
    public rune_set_class Swift_rune;
    public rune_set_class Blade_rune;
    public rune_set_class Tolerance_rune;
    public rune_set_class Violent_rune;
    public rune_set_class Will_rune;
    public rune_set_class Despair_rune;
    public rune_set_class Destroy_rune;
    public rune_set_class Vampire_rune;
    public rune_set_class Accuracy_rune;
    public rune_set_class Endure_rune;
    public rune_set_class Enhance_rune;
    public rune_set_class Determination_rune;
    public rune_set_class Energy_rune;
    public rune_set_class Guard_rune;
    public rune_set_class Shield_rune;
    public rune_set_class Revenge_rune;
    public rune_set_class Nemesis_rune;
    public rune_set_class Fatal_rune;
    public rune_set_class Fight_rune;
    public rune_set_class Rage_rune;
    #endregion

    private void Start()
    {
        #region Init rune data setting
        Swift_rune.InitSetting();
        Blade_rune.InitSetting();
        Tolerance_rune.InitSetting();
        Violent_rune.InitSetting();
        Will_rune.InitSetting();
        Despair_rune.InitSetting();
        Destroy_rune.InitSetting();
        Vampire_rune.InitSetting();
        Accuracy_rune.InitSetting();
        Endure_rune.InitSetting();
        Enhance_rune.InitSetting();
        Determination_rune.InitSetting();
        Energy_rune.InitSetting();
        Guard_rune.InitSetting();
        Shield_rune.InitSetting();
        Revenge_rune.InitSetting();
        Nemesis_rune.InitSetting();
        Fatal_rune.InitSetting();
        Fight_rune.InitSetting();
        Rage_rune.InitSetting();
        #endregion
    }
}
