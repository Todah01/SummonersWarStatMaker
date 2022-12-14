using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : rune_set_class
{
    public override void InitSetting()
    {
        rune_data.name = "Guard";
        rune_data.rune_count = 0;
        rune_data.isAncient = true;
    }
    public override int get_set_effect(int data)
    {
        return Mathf.RoundToInt(data * 0.15f);
    }
}
