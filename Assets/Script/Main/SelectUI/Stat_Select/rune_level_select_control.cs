using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_level_select_control : MonoBehaviour
{
    public bool check_Legendary;
    private void Start()
    {
        check_Legendary = true;
    }
    public void onClickRuneIcon()
    {
        if(check_Legendary) check_Legendary = false;
        else check_Legendary = true;

        this.BroadcastMessage("rune_type_change", check_Legendary);
    }
}
