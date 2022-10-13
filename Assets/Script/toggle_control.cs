using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle_control : MonoBehaviour
{
    public GameObject label;
    public void SendMonsterName()
    {
        string monster_name = label.GetComponent<Text>().text;
        this.gameObject.SendMessageUpwards("SendMonster", monster_name);
    }
}
