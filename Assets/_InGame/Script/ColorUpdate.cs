using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorUpdate : MonoBehaviour
{
    // Start is called before the first frame update

    public FlexibleColorPicker fcp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Image>().color = fcp.color;
    }
}
