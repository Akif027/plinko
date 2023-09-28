using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

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
