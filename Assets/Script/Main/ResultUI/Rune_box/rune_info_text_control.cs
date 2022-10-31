using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rune_info_text_control : MonoBehaviour
{
    #region Public Variable
    public GameObject sub_text;
    #endregion

    #region Local Variable
    Text stat_name, stat_amount;
    Color color;
    #endregion

    public void RuneInfoGraphicSetting()
    {
        stat_name = this.GetComponent<Text>();
        stat_amount = sub_text.GetComponent<Text>();

        if (stat_name.text == "SPD" || stat_name.text == "ATK" ||
            stat_name.text == "DEF" || stat_name.text == "HP")
        {
            ColorUtility.TryParseHtmlString("#FDAC51", out color);
            stat_amount.transform.localPosition = new Vector3(55f, 0f, this.transform.localPosition.z);
            stat_amount.color = color;
        }
        else if (stat_name.text == "Resistance")
        {
            ColorUtility.TryParseHtmlString("#FFFFFF", out color);
            stat_amount.transform.localPosition = new Vector3(140f, 0f, this.transform.localPosition.z);
            stat_amount.color = color;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#FFFFFF", out color);
            stat_amount.transform.localPosition = new Vector3(125f, 0f, this.transform.localPosition.z);
            stat_amount.color = color;
        }
    }
}
