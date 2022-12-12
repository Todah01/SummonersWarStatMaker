using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_stat_data : MonoBehaviour
{
    public static Dictionary<string, int> conversion_ancient_rune = new Dictionary<string, int>()
        { {"HP", 15}, {"ATK", 15}, {"DEF", 15},
        {"SPD", 11}, {"ACC", 13}, {"RES", 13},
        {"CRI RATE", 10}, {"CRI DMG", 12},
        {"HP+", 640}, {"ATK+", 44}, {"DEF+", 44}};
    public static Dictionary<string, int> conversion_normal_rune = new Dictionary<string, int>()
    { {"HP", 13}, {"ATK", 13}, {"DEF", 13},
        {"SPD", 10}, {"ACC", 11}, {"RES", 11},
        {"CRI RATE", 9}, {"CRI DMG", 10},
        {"HP+", 580}, {"ATK+", 40}, {"DEF+", 40}};
    public static Dictionary<string, int> grind_ancient_rune = new Dictionary<string, int>()
        { {"HP", 12}, {"ATK", 12}, {"DEF", 12},
        {"SPD", 6}, {"HP+", 610}, {"ATK+", 34}, {"DEF+", 34}};
    public static Dictionary<string, int> grind_normal_rune = new Dictionary<string, int>()
        { {"HP", 10}, {"ATK", 10}, {"DEF", 10},
        {"SPD", 5}, {"HP+", 550}, {"ATK+", 30}, {"DEF+", 30}};
}
