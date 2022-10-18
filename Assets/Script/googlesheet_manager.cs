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
	public GameObject monster_input_name;
    public GameObject selected_data;
	public GoogleData GD;
	public int hp, atk, def, spd, crirate, cridmg, res, acc;
	#endregion

	#region Local Variable
	const string URL = "https://docs.google.com/spreadsheets/d/1celyrW7Bud-XAGBVKeYdhFbfPz3nUEEzhOMzZsa1i3w/export?format=tsv";
	const string URL_SCRIPT = "https://script.google.com/macros/s/AKfycbwrUlUJLqHgpCUT53HPp0EAVEA5ldVqUVqc0LCCuWcqvcaBG4A3DJIQUsVwOfEdcGZAtg/exec";
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
        Debug.Log("Start GetValue");

        monster_name = monster_input_name.GetComponent<Search_Algorithm>().CheckMonsterName;
        if (monster_name == "")
        {
            Debug.Log("Error : monster name is null");
            selected_data.GetComponent<select_data_control>().ResultWindowOpen();
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("name", monster_name);

        Debug.Log(monster_name + ": Set Form Complete");

        StartCoroutine(Post(form));
    }
    IEnumerator Post(WWWForm form)
    {
        Debug.Log("Post Start");

        using (UnityWebRequest www = UnityWebRequest.Post(URL_SCRIPT, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();

            Debug.Log("Get WebRequest");

            if (www.isDone)
            {
                Debug.Log("Wait for download");
                Response(www.downloadHandler.text);
                Debug.Log("Complete download");
            }
            else Debug.Log("Error : 데이터 저장소의 응답이 없습니다.");
        }
    }

    void Response(string json)
    {
        Debug.Log("Start Response");

        if (string.IsNullOrEmpty(json)) return;

        GD = JsonUtility.FromJson<GoogleData>(json);

        Debug.Log(GD.hp + '\n' + GD.spd);

        hp = int.Parse(GD.hp);
        atk = int.Parse(GD.atk);
        def = int.Parse(GD.def);
        spd = int.Parse(GD.spd);
        crirate = int.Parse(GD.crirate);
        cridmg = int.Parse(GD.cridmg);
        res = int.Parse(GD.res);
        acc = int.Parse(GD.acc);

        Debug.Log("End Response");

        selected_data.GetComponent<select_data_control>().ResultWindowOpen();
    }
}
