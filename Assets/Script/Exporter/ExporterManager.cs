using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExporterManager : MonoBehaviour
{
    public int width = 1024;
    public int height = 1024;
    public Camera captureCamera;
    public Texture2D resultTexture;
    public Image curImg;
    public Image resultImage;
    public string curMonsterName;
    public string totalMonstersName;
    public string resultSpriteSavePath;
    public Sprite[] monster_imgs;
    public GameObject inputField;
    public GameObject resultImgViewer;
    
    Sprite mySprite;

    public void imgSelector(Image img)
    {
        curImg = img;
    }

    public void SetImg()
    {
        curMonsterName = inputField.GetComponent<search_inputfield_control>().CheckMonsterName;
        // Set monster profile img.
        string moneter_name_tolower = curMonsterName.ToLower();
        for (int i = 0; i < monster_imgs.Length; i++)
        {

            if (monster_imgs[i].name.ToLower().Contains(moneter_name_tolower))
            {
                Debug.Log(moneter_name_tolower);
                curImg.sprite = monster_imgs[i];
                break;
            }
        }
    }

    public void ExportImg()
    {
        // Create a new RenderTexture with the desired dimensions
        RenderTexture rt = new RenderTexture(width, height, 24);

        // Set the capture camera's target texture to be the new RenderTexture
        captureCamera.targetTexture = rt;

        // Render the scene to the new RenderTexture
        captureCamera.Render();

        // Activate the RenderTexture
        RenderTexture.active = rt;

        // Create a new Texture2D and read the pixels from the RenderTexture into it
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        // Apply the changes to the Texture2D
        texture.Apply();

        // Set the resultTexture to the new Texture2D
        resultTexture = texture;

        // Clean up
        RenderTexture.active = null;
        captureCamera.targetTexture = null;

        FooBar(texture);

        resultImgViewer.SetActive(true);
    }

    public void ExportSprite()
    {
        ExportTexture2D(resultTexture, resultSpriteSavePath);
        Debug.Log("Download Complete!");
    }

    void ExportTexture2D(Texture2D tex, string outputPath)
    {
        byte[] bytes = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes(outputPath, bytes);
    }

    public void FooBar(Texture2D myTexture2D)
    {
        // Texture2D 객체를 Sprite 객체로 변환
        mySprite = Sprite.Create(myTexture2D, new Rect(0.0f, 0.0f, myTexture2D.width, myTexture2D.height), new Vector2(0.5f, 0.5f));

        // Sprite를 Image에 적용하여 화면에 표시
        resultImage.sprite = mySprite;
    }
}
