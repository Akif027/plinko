using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB; // Using StandaloneFileBrowser for getting images
using UnityEngine.UI;
using TMPro; // Using TextMeshPro for better text in Unity

public class uiManager : MonoBehaviour
{
    // UI Components
    public TMP_InputField Text;
    public Image Image;

    // Data list
    private List<string> NameData = new List<string>();

    // Serialized game objects to be set from the inspector
    [SerializeField] private GameObject BetBasketL;
    [SerializeField] private GameObject BetBasketR;

    private void Start()
    {
        // Get children objects and their components
        Transform child = transform.GetChild(1);
        Text = child.GetChild(0).GetComponent<TMP_InputField>();
        Image = child.GetChild(1).GetChild(0).GetComponent<Image>();
        BetBasketL.GetComponent<Basket>().choice = 't';
        BetBasketR.GetComponent<Basket>().choice = 't';

        // Initially set the image to inactive
        Image.gameObject.SetActive(false);
    }

    // Handles adding image and text
    public void handleData(int val)
    {
        switch (val)
        {
            case 0:
                BetBasketL.GetComponent<Basket>().choice = 't';
                BetBasketR.GetComponent<Basket>().choice = 't';
                HandleText();
                break;
            case 1:
                BetBasketL.GetComponent<Basket>().choice = 'i';
                BetBasketR.GetComponent<Basket>().choice = 'i';
                HandleImage();
                Debug.Log("image select");
                break;
            default:
                Debug.Log("Select again");
                break;
        }
    }

    // Saves the input field's data to game objects
    public void SaveInputFieldData()
    {
        BetBasketL.gameObject.name = Text.text;
        BetBasketR.gameObject.name = Text.text;
        BetBasketL.GetComponent<Basket>().txt = Text.text;
        BetBasketR.GetComponent<Basket>().txt = Text.text;
    }

    // Display text and hide image
    private void HandleText()
    {
        Image.transform.parent.gameObject.SetActive(true);
        Text.gameObject.SetActive(true);
    }

    // Display image and hide text
    private void HandleImage()
    {
        Image.transform.parent.gameObject.SetActive(true);
        Text.gameObject.SetActive(false);
        SelectImage();
    }

    // Initiates image selection
    public void SelectImage()
    {
#if !UNITY_EDITOR
        GetImage.GetImageFromUserAsync(gameObject.name, "ReceiveImage");
#endif
    }

    // Prefix for data URLs
    static string s_dataUrlPrefix = "data:image/png;base64,";

    // Receives and processes the selected image
    public void ReceiveImage(string dataUrl)
    {
        if (dataUrl.StartsWith(s_dataUrlPrefix))
        {
            byte[] pngData = System.Convert.FromBase64String(dataUrl.Substring(s_dataUrlPrefix.Length));
            Texture2D tex = new Texture2D(1, 1);

            if (tex.LoadImage(pngData))
            {
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                Image.sprite = sprite;
                BetBasketL.GetComponent<Basket>().img = sprite;
                BetBasketR.GetComponent<Basket>().img = sprite;
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
