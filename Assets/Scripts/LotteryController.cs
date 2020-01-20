using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LotteryController : MonoBehaviour
{
    [SerializeField]
    public Lottery originalLottery;
    public Lottery currentLottery;
    public UnityEvent lotteryFinished;
    public UnityEvent lotteryNotFinished;
    public GameObject drawBall;
    [Header("Error Handling")]
    public UnityEvent errorOccured;
    public UnityEvent NoErrorOccured;
    public GameObject ErrorTextField;
    public string ErrorMsg;
    private ListDictionary listError;

    private void Start()
    {
        listError = new ListDictionary();

        listError.Add(0, "Noe gikk galt, kunne ikke laste inn lotteriet.");
        listError.Add(1, "Dette lotteriet har allerede blitt spilt.");
        listError.Add(2, "Dette lotteriet har ingen trekninger.");
        listError.Add(3, "Dette lotteriet har ingen deltakere");
    }

    public void SetOriginalLottery(Lottery lottery)
    {
        originalLottery = lottery;
    }
    public void SetLottery(Lottery lottery)
    {
        currentLottery = lottery;
        DetermineHowManyHasBeenDrawn();
    }

    public Lottery GetLottery()
    {
        return currentLottery;
    }

    public void ClearLottery()
    {
        currentLottery = null;
    }

    public void CheckLotteryForErrors()
    {
        StartCoroutine(checkLotteryAfterDelay());}

    private IEnumerator checkLotteryAfterDelay()
    {
        yield return new WaitForSeconds(1);
        ListDictionary listOfChecks = new ListDictionary();

        listOfChecks.Add(0, string.IsNullOrEmpty(currentLottery.id));
        //listOfChecks.Add(1, currentLottery.currentDraw == currentLottery.numberOfDraws);
        listOfChecks.Add(2, currentLottery.draws == null || currentLottery.draws.Count == 0);
        listOfChecks.Add(3, currentLottery.participants == null || currentLottery.participants.Count == 0);

        foreach (DictionaryEntry check in listOfChecks)
        {
            var errorCode = check.Key;
            var boolCheck = check.Value is bool ? (bool) check.Value : false;
            if (boolCheck)
            {
                Debug.Log("Error code: " + errorCode + " is" + boolCheck);
                ErrorTextField.GetComponent<TextMeshProUGUI>().text = listError[check.Key].ToString();
                errorOccured.Invoke();
                yield break;
            }
        }
        NoErrorOccured.Invoke();
    }

    public void ModifyLotteryObjectWhenDrawn()
    {
        drawBall = GameObject.FindWithTag("DrawBall");

        var participant = drawBall.GetComponent<BallScript>().participant;

        var draw = currentLottery.draws[currentLottery.currentDraw];

        if (!draw.started)
        {
            draw.winner = participant.name;
            draw.started = true;
            var tempInt = Int32.Parse(participant.numberOfTickets);
            tempInt--;
            participant.numberOfTickets = tempInt.ToString();
            SetNewDraw();
        }
    }

    public void DetermineIfLotteryIsFInished()
    {
        if(currentLottery.currentDraw == currentLottery.numberOfDraws)
        {

            lotteryFinished.Invoke();
        }
        else
        {
            lotteryNotFinished.Invoke();
        }
    }

    private void SetNewDraw()
    {
        currentLottery.currentDraw = currentLottery.currentDraw + 1;

    }

    private void DetermineHowManyHasBeenDrawn()
    {
        foreach (var draw in currentLottery.draws)
        {
            if (draw.started)
            {
                SetNewDraw();
            }
        }
    }


}
