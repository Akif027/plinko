using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bet : MonoBehaviour
{

    private void Start()
    {
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Basket"))
        {
            gameObject.SetActive(false);
        }
    }
}
