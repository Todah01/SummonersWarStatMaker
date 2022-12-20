using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : rune_set_class
{
    public override void InitSetting()
    {
        rune_data.name = "Fight";
        rune_data.rune_count = 0;
        rune_data.number_of_actives = 2;
        rune_data.isAncient = true;
    }
    public override int get_set_effect(int data)
    {
        return Mathf.RoundToInt(data * 0.08f);
    }
}
