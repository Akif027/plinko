using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    public ShapeGenrator sg;

    [Header("Buttons")]
    public Button[] buttons;
    public TMP_Dropdown[] dropdowns;
    public TMP_InputField[] inputFields;

    [Header("Win Objects")]
    public GameObject winnerPanel;
    public GameObject winnerWindows;
    public Vector3 punchScaleAmount = new Vector3(0.1f, 0.1f, 0.1f); // Amount to "punch" the scale by
    public float winDuration = 0.5f; // Duration of the punch animation
    public GameObject mainCamera;

    [Header("ButtonClick")]
    public Vector3 _intialScale;
    public float clickDuration = 0.1f;
    public float clickScale = 0.9f;

    [Header("Entry")]
    public GameObject entryPanel;
    public float entryPanelShowPosX;
    public float entryPanelHidePosX;
    public float entryPanelDuration;

    [Header("Color")]
    int colourId;
    public FlexibleColorPicker fcp;
    public GameObject colourPicker;
    public ColorBox[] colorBoxes;
    public float ShowPosX = -700f;
    public float HidePosX = -1200f;
    public float duration = 0.5f;
    public bool isColorEdit;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        mainCamera.SetActive(true);
        sg.Generate();
    }

    // Update is called once per frame
    void Update()
    {
        ColourUpdate();
    }

    void ColourUpdate()
    {
        if (isColorEdit)
        {
            colorBoxes[colourId].SetColour(fcp.color);
        }
    }

    public void SetColour(int id)
    {
        colourId = id;
        fcp.color = colorBoxes[id].buttonImage.color;
        entryPanel.transform.DOMoveX(entryPanelHidePosX, entryPanelDuration);
        colourPicker.transform.DOMoveX(ShowPosX, duration).SetEase(Ease.OutBack);
        isColorEdit = true;
    }

    public void CloseColourPicker()
    {
        isColorEdit = false;
        entryPanel.transform.DOMoveX(entryPanelShowPosX, entryPanelDuration);
        colourPicker.transform.DOMoveX(HidePosX, duration);
    }

    public void Click(GameObject obj)
    {
        obj.transform.DOScale(_intialScale * clickScale, clickDuration).OnComplete(() =>
        {
            obj.transform.DOScale(_intialScale, clickDuration);
        });
    }

    public void ShowWin()
    {
        mainCamera.SetActive(false);
        winnerPanel.SetActive(true);
        winnerWindows.transform.DOPunchScale(punchScaleAmount, winDuration);
    }

    public void HideWin()
    {
        winnerPanel.SetActive(false);
        mainCamera.SetActive(true);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DisableComponents()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            dropdowns[i].interactable = false;
            inputFields[i].interactable = false;
        }
    }

    public void EnableComponents()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
            dropdowns[i].interactable = true;
            inputFields[i].interactable = true;
        }
    }
}

[Serializable]
public class ColorBox
{
    public int id;
    public Image buttonImage;
    public SpriteRenderer boxL;
    public SpriteRenderer boxR;

    public void SetColour(Color colour)
    {
        buttonImage.color = colour;
        boxL.color = colour;
        boxR.color = colour;
    }
}
