using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle_control : MonoBehaviour
{
    public GameObject label;

    // If user click toggle, toggle_control will send message to set monster name in inputfield to search_algorithm.
    public void SendMonsterName()
    {
        string monster_name = label.GetComponent<Text>().text;
        this.gameObject.SendMessageUpwards("SendMonster", monster_name);
    }
}
