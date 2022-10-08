using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class GoogleData
{
	public string hp, atk, def, spd, crirate, cridmg, res, acc;
}

public class googlesheet_manager : MonoBehaviour
{
	#region Public Variable
	public GameObject monster_dropdown;
    public GameObject selected_data;
	public GoogleData GD;
	public int hp, atk, def, spd, crirate, cridmg, res, acc;
	#endregion

	#region Local Variable
	const string URL = "https://docs.google.com/spreadsheets/d/1celyrW7Bud-XAGBVKeYdhFbfPz3nUEEzhOMzZsa1i3w/export?format=tsv";
	const string URL_SCRIPT = "https://script.google.com/macros/s/AKfycbxK6jKGB9_RVj7NtLDsuNKL_bsgLScSRWV2rIanTDC-iCOnsWGAoAFJ0_L7xvIrS4nNFQ/exec";
	string monster_name;
    #endregion

    #region Load data from google sheet
    //IEnumerator Start()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("name", "Abellio");
    //    UnityWebRequest www = UnityWebRequest.Post(URL_SCRIPT, form);
    //    yield return www.SendWebRequest();

    //    string data = www.downloadHandler.text;
    //    print(data);
    //}
    #endregion

    public void GetValue()
    {
        monster_name = monster_dropdown.GetComponent<monster_dropdown_control>().monster_name_by_value;
        WWWForm form = new WWWForm();
        form.AddField("name", monster_name);

        StartCoroutine(Post(form));
    }
    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL_SCRIPT, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();

            if (www.isDone) Response(www.downloadHandler.text);
            else Debug.Log("데이터 저장소의 응답이 없습니다.");
        }
    }

    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        GD = JsonUtility.FromJson<GoogleData>(json);

        hp = int.Parse(GD.hp);
        atk = int.Parse(GD.atk);
        def = int.Parse(GD.def);
        spd = int.Parse(GD.spd);
        crirate = int.Parse(GD.crirate);
        cridmg = int.Parse(GD.cridmg);
        res = int.Parse(GD.res);
        acc = int.Parse(GD.acc);

        selected_data.GetComponent<select_data_control>().ResultWindowOpen();
    }
}
