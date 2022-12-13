using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Blade : rune_set_class
{
    public override int get_set_effect(int crirate)
    {
        return 12;
    }
}
class Endure : rune_set_class
{
    public override int get_set_effect(int res)
    {
        return 20;
    }
}
class Energy : rune_set_class
{
    public override int get_set_effect(int hp)
    {
        return Mathf.RoundToInt(hp * 0.15f);
    }
}
class Fatal : rune_set_class
{
    public override int get_set_effect(int atk)
    {
        return Mathf.RoundToInt(atk * 0.35f);
    }
}
class Focus : rune_set_class
{
    public override int get_set_effect(int acc)
    {
        return 20;
    }
}
class Guard : rune_set_class
{
    public override int get_set_effect(int def)
    {
        return Mathf.RoundToInt(def * 0.15f);
    }
}
class Rage : rune_set_class
{
    public override int get_set_effect(int cridmg)
    {
        return 40;
    }
}
class Violent : rune_set_class
{
    public override int get_set_effect(int data)
    {
        return 0;
    }
}
class Despair : rune_set_class
{
    public override int get_set_effect(int data)
    {
        return 0;
    }
}
class Destroy : rune_set_class
{
    public override int get_set_effect(int data)
    {
        return 0;
    }
}
class Vampire : rune_set_class
{
    public override int get_set_effect(int data)
    {
        return 0;
    }
}
class Nemesis : rune_set_class
{
    public override int get_set_effect(int data)
    {
        return 0;
    }
}
class Will : rune_set_class
{
    public override int get_set_effect(int data)
    {
        return 0;
    }
}
class Revenge : rune_set_class
{
    public override int get_set_effect(int data)
    {
        return 0;
    }
}
class Shield : rune_set_class
{
    public override int get_set_effect(int data)
    {
        return 0;
    }
}
class Fight : rune_set_class
{
    public override int get_set_effect(int atk)
    {
        return Mathf.RoundToInt(atk * 0.08f);
    }
}
class Determination : rune_set_class
{
    public override int get_set_effect(int def)
    {
        return Mathf.RoundToInt(def * 0.08f);
    }
}
class Accuracy : rune_set_class
{
    public override int get_set_effect(int acc)
    {
        return 10;
    }
}
class Tolerance : rune_set_class
{
    public override int get_set_effect(int res)
    {
        return 10;
    }
}
public struct Data
{
    public string name;
    public int rune_count;
    public bool isAncient;
}
public abstract class rune_set_class
{
    public Data rune_data;
    public abstract void InitSetting();
    public virtual int get_set_effect(int data)
    {
        return 0;
    }
}
