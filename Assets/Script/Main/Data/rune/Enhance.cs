using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enhance : rune_set_class
{
    public override void InitSetting()
    {
        rune_data.name = "Enhance";
        rune_data.rune_count = 0;
        rune_data.isAncient = false;
    }
    public override int get_set_effect(int data)
    {
        return Mathf.RoundToInt(data * 0.08f);
    }
}
