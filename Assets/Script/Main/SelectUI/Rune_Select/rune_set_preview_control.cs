using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rune_set_preview_control : MonoBehaviour
{
    #region Public Variable
    public GameObject rune_datas;
    public List<GameObject> preview_slots;
    public List<Text> preview_titles;
    public List<Image> preview_imgs;
    #endregion

    #region Local Variable
    List<rune_set_class> rune_infos;
    List<Sprite> rune_set_imgs;
    List<string> activate_rune_sets = new List<string>();
    #endregion

    private void Awake()
    {
        rune_infos = rune_datas.GetComponent<rune_control>().runes;
        rune_set_imgs = rune_datas.GetComponent<rune_control>().rune_imgs;
    }
    public void RuneSetPreviewSetting(string type, string act)
    {
        switch(act)
        {
            case "subtract":
                // Subtract rune count
                foreach (var info in rune_infos)
                {
                    if (info.rune_data.name == type)
                    {
                        info.rune_data.rune_count = Mathf.Max(0, info.rune_data.rune_count - 1);
                        break;
                    }
                }
                SetActiveRuneSet();
                break;

            case "add":
                // Add rune count
                foreach (var info in rune_infos)
                {
                    if (info.rune_data.name == type)
                    {
                        info.rune_data.rune_count += 1;
                        break;
                    }
                }
                SetActiveRuneSet();
                break;
        }
    }

    private void SetActiveRuneSet()
    {
        // Reset Data
        activate_rune_sets.Clear();
        foreach (var slot in preview_slots)
            slot.SetActive(false);

        foreach (var title in preview_titles)
            title.text = "";

        foreach (var img in preview_imgs)
            img.sprite = null;

        // Check activate rune set count and set preview title
        foreach (var info in rune_infos)
        {
            if(info.rune_data.rune_count >= info.rune_data.number_of_actives)
            {
                int activate_rune_set_count = info.rune_data.rune_count / info.rune_data.number_of_actives;
                for (int i = 0; i < activate_rune_set_count; i++)
                {
                    activate_rune_sets.Add(info.rune_data.name);
                }
            }

            Debug.Log(info.rune_data.name + " : " + info.rune_data.rune_count);
        }

        foreach (var info in activate_rune_sets)
            Debug.Log(info);

        // Active preview slot
        for (int i = 0; i < activate_rune_sets.Count; i++)
        {
            preview_slots[i].SetActive(true);
        }

        // Set preview titles
        for (int i = 0; i < activate_rune_sets.Count; i++)
        {
            if(preview_slots[i].activeSelf)
            {
                preview_titles[i].text = activate_rune_sets[i];
            }
        }

        // Set preview imgs;
        for (int i = 0; i < activate_rune_sets.Count; i++)
        {
            foreach(var img in rune_set_imgs)
            {
                if(img.name.Contains(preview_titles[i].text.ToLower()))
                {
                    preview_imgs[i].sprite = img;
                    break;
                }
            }
        }
    }
}
