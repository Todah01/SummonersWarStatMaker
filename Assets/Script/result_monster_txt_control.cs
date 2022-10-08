using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class result_monster_txt_control : MonoBehaviour
{
    Color color;

    public void SetTextColorToRed()
    {
        ColorUtility.TryParseHtmlString("#F2582D", out color);
        this.GetComponent<Text>().color = color;
    }
}
