using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class GoogleData
{
    public string hp, atk, def, spd, crirate, cridmg, res, acc;
}

public class googlesheet_manager : MonoBehaviour
{
    #region Load Data
    const string URL = "https://docs.google.com/spreadsheets/d/1celyrW7Bud-XAGBVKeYdhFbfPz3nUEEzhOMzZsa1i3w/export?format=tsv";
    const string URL_SCRIPT = "https://script.google.com/macros/s/AKfycbwSLZ9TUgryyDvGqOhQpXTrOeCl3oORbhMmw1XARl0o1Qi2rMI3/exec";
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        //print(data);
    }
    #endregion


}
