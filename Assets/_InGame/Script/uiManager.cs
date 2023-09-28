using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using UnityEngine.UI;
using TMPro;

public class uiManager : MonoBehaviour
{
    private TMP_InputField Text;
   private Image Image;

    private List<string> NameData = new List<string>();

    [SerializeField] GameObject BetBasket;


    private void Start()
    {
      
       Transform child;
        child = transform.GetChild(1);
        Text = child.GetChild(0).GetComponent<TMP_InputField>();
        Image = child.GetChild(1).GetComponent<Image>();
        Image.gameObject.SetActive(false);
    }
    //Add image and text 
    public void handleData(int val)
    {
       
        switch (val)
        {
            case 0:
                HandleText();
                break;
                case 1:
                HandleImage();
                Debug.Log("image select");
                break;
            default:
                Debug.Log("Select again");
                break;
        }
    }
 
    public void SaveInputFieldData()
    {
       
        BetBasket.gameObject.name = Text.text;  

        
    }

    private void HandleText()
    {
        Image.gameObject.SetActive(false);
        Text.gameObject.SetActive(true);
       
    }

    private void HandleImage()
    {
        Image.gameObject.SetActive(true);
        Text.gameObject.SetActive(false);
        SelectImage();
    }


    public void SelectImage()
    {

#if !UNITY_EDITOR
        GetImage.GetImageFromUserAsync(gameObject.name, "ReceiveImage");
#endif
    }
    static string s_dataUrlPrefix = "data:image/png;base64,";
    public void ReceiveImage(string dataUrl)
    {
        if (dataUrl.StartsWith(s_dataUrlPrefix))
        {
            byte[] pngData = System.Convert.FromBase64String(dataUrl.Substring(s_dataUrlPrefix.Length));

            // Create a new Texture (or use some old one?)
            Texture2D tex = new Texture2D(1, 1); // does the size matter?
            if (tex.LoadImage(pngData))
            {
                // Get the Image component
                // Image image = GetComponent<Image>();

                // Create a Sprite from the loaded Texture2D
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);

                // Set the Sprite to the Image component

                Image.sprite = sprite;  
            }
            else
            {
                Debug.LogError("could not decode image");
            }
        }
        else
        {
            Debug.LogError("Error getting image:" + dataUrl);
        }

    }
}
