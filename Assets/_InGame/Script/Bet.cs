using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Bet : MonoBehaviour
{
    private int ColCount =0;
    private void Start()
    {
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Basket"))
        {
            ColCount++;
            gameObject.SetActive(false);
            if (ColCount <=1)
            {
                GetTheName(collision.gameObject.name);
                if (AmountManager.instance.CurrentAmount <= 0)
                {
                    AmountManager.instance.Broke = true;
                }
                AmountManager.instance.BetAmount = 100;
              
            }
            ColCount = 0;
        }
    }

  /*  IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        ColCount = 0;
    }*/
    private void GetTheName(string Name)
    {
        Debug.Log(Name);
        switch (Name)
        {
            case "5.6": AmountManager.instance.BetAmount *= 5.6f;
                AmountManager.instance.CurrentAmount += AmountManager.instance.BetAmount;
                AmountManager.instance.Profit = AmountManager.instance.BetAmount;

                break;
            case "2.1":
                AmountManager.instance.BetAmount *= 2.1f;
                AmountManager.instance.CurrentAmount += AmountManager.instance.BetAmount;
                AmountManager.instance.Profit = AmountManager.instance.BetAmount;
               
                break;
            case "1.1":
                AmountManager.instance.BetAmount *= 1.1f;
                AmountManager.instance.CurrentAmount += AmountManager.instance.BetAmount;
                AmountManager.instance.Profit = AmountManager.instance.BetAmount;
               
                break;
            case "1":
                AmountManager.instance.BetAmount *= 1f;
                AmountManager.instance.CurrentAmount += AmountManager.instance.BetAmount;
                AmountManager.instance.Profit = AmountManager.instance.BetAmount;
            
                break;
            case "0.5":
                AmountManager.instance.BetAmount *= 0.5f;
                AmountManager.instance.CurrentAmount += AmountManager.instance.BetAmount;
                        AmountManager.instance.Profit = 0;
             
                break;
            default:Debug.Log( AmountManager.instance.BetAmount);
                break;
        }
    }
}
