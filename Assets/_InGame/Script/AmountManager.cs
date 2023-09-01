using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AmountManager : MonoBehaviour
{
    public static AmountManager instance;

    private float startTime;
    private bool isRunning = false;


    public float Profit = 0;
    public float BetAmount = 0;
    public float CurrentAmount = 0;

    [SerializeField] TMP_Text BetAmountTxt;
    [SerializeField] TMP_Text BetisTxt;
    [SerializeField] TMP_Text BalanceText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text ProfitText;
    [SerializeField] TMP_Text BonusText;

    public bool Canplay = false;
    [HideInInspector]
    public bool Broke = false; //if the player is broke

    
    private void Start()
    {
        instance = this;

        BetAmount = 100;
        timerText.text = " 00:00";
      

    }
    private void Update()
    {
       
       FormatBalance(BetAmountTxt,BetAmount, "Bet ");
        FormatBalance(ProfitText, Profit, " ");

        if (CurrentAmount>=0) {
            FormatBalance(BalanceText, CurrentAmount, "Balance ");
            Canplay = true;
            Broke = false;
        }
        else 
        {
          
            Canplay = false;
        }

        if (Broke)
        {
            PopText();
            BonusText.text = "Bonus of 100";
            CurrentAmount = 100;
        }

        if (isRunning)
        {
            float elapsedTime = Time.time - startTime;
            int minutes = (int)(elapsedTime / 60);
            int seconds = (int)(elapsedTime % 60);
            timerText.text = string.Format(" {0:00}:{1:00}", minutes, seconds);
        }
      
    }

    public void IncreamentBet()
    {
        if (BetAmount<CurrentAmount-99)
        {
            BetAmount += 100;
        }
       
    }
    public void DecrementBet()
    {
        if (BetAmount!=100)
        {
            BetAmount -= 100;
        }
      
    }

    public void SetBetAmount()
    {
        if (Canplay)
        {
            BetisTxt.text = BetAmount.ToString();
            CurrentAmount -= BetAmount;
            FormatBalance(BetisTxt, BetAmount, "Bet ");
        }
   

    }

     public void HighRisk(Toggle t)
     {
        BetAmount = 100;
        if (t.isOn && BetAmount<CurrentAmount * 0.7)
        {
            BetAmount += CurrentAmount * 0.7f;
        }
    
     }
    public void MediumRisk(Toggle t)
    {

        BetAmount = 100;
        if (t.isOn && BetAmount < CurrentAmount * 0.5)
        {
            BetAmount += CurrentAmount * 0.5f;
        }

    }

    public void LowRisk(Toggle t)
    {

        BetAmount = 100;
        if (t.isOn && BetAmount < CurrentAmount * 0.2)
        {
            BetAmount += CurrentAmount * 0.2f;
        }

    }
    private void FormatBalance(TMP_Text Text,float balance, string StartText)
    {
        if (balance >= 1000000)
        {
            // If the balance is a million or more, display it in millions (M)
            Text.text = StartText + (balance / 1000000f).ToString("0.##") + "M" ;
        }
        else if (balance >= 1000)
        {
            // If the balance is a thousand or more, display it in thousands (K)
            Text.text = StartText + (balance / 1000f).ToString("0.##") + "K" ;
        }
        else
        {
            // If the balance is less than a thousand, display it as is
            Text.text = StartText + balance.ToString("0");
        }
    }
 
   public void StartTimer()
    {
        if (Canplay)
        {
            isRunning = true;
            startTime = Time.time;
        }
       
    }



    private void PopText()
    {
        // Scale up the text
       BonusText.transform.DOScale(Vector3.one * 1.5f, 0.3f)
            .OnComplete(() =>
            {
                // Scale it back down to its original size
                BonusText.transform.DOScale(Vector3.one, 0.3f)
                    .OnComplete(() =>
                    {
                        BonusText.transform.localScale =Vector3.zero;
                    });
            });
    }


 

}
