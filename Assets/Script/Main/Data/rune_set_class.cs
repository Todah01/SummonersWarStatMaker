using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Data
{
    public string name;
    public int rune_count;
    public bool isAncient;
}
public abstract class rune_set_class : MonoBehaviour
{
    public Data rune_data;
    public abstract void InitSetting();
    public virtual int get_set_effect(int data)
    {
        return 0;
    }
}
