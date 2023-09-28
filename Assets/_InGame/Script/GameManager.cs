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
        fcp.color = colorBoxes[id].buttonImage.color;
        isColorEdit = true;
        colourId = id;
        colourPicker.transform.DOMoveX(ShowPosX, duration).SetEase(Ease.OutBack);
    }

    public void CloseColourPicker()
    {
        isColorEdit = false;
        colourPicker.transform.DOMoveX(HidePosX, duration);
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
