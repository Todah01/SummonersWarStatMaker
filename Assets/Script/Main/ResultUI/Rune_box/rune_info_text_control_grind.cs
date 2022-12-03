using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class rune_info_text_control_grind : MonoBehaviour
{
    #region Public Variable
    public GameObject sub_text_1;
    public GameObject sub_text_2;
    #endregion

    #region Local Variable
    Text stat_name, stat_amount_1, stat_amount_2;
    Color color;
    #endregion

    public void RuneInfoGraphicSetting()
    {
        stat_name = this.GetComponent<Text>();
        stat_amount_1 = sub_text_1.GetComponent<Text>();
        stat_amount_2 = sub_text_2.GetComponent<Text>();

        if (stat_name.text == "SPD" || stat_name.text == "ATK" ||
            stat_name.text == "DEF" || stat_name.text == "HP" ||
            stat_name.text == "DEF+" || stat_name.text == "HP+" ||
            stat_name.text == "ATK+")
        {
            ColorUtility.TryParseHtmlString("#FFFFFF", out color);
            stat_amount_1.transform.localPosition = new Vector2(55f, 0f);
            stat_amount_1.color = color;

            ColorUtility.TryParseHtmlString("#FDAC51", out color);
            stat_amount_2.transform.localPosition = new Vector2(130f, 0f);
            stat_amount_2.color = color;
        }
        else if (stat_name.text == "Resistance")
        {
            ColorUtility.TryParseHtmlString("#FFFFFF", out color);
            stat_amount_1.transform.localPosition = new Vector2(140f, 0f);
            stat_amount_1.color = color;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#FFFFFF", out color);
            stat_amount_1.transform.localPosition = new Vector2(125f, 0f);
            stat_amount_1.color = color;
        }
    }
}
