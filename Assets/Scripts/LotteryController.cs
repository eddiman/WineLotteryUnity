using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.Events;

public class LotteryController : MonoBehaviour
{
    [SerializeField]
    public Lottery originalLottery;
    public Lottery currentLottery;
    public UnityEvent LotteryFinished;
    public GameObject drawBall;

    //TODO: Make Draw counter
    public void SetOriginalLottery(Lottery lottery)
    {
        originalLottery = lottery;
    }
    public void SetLottery(Lottery lottery)
    {
        currentLottery = lottery;
    }

    public  Lottery GetLottery()
    {
        return currentLottery;
    }

    public void ClearLottery()
    {
        currentLottery = null;
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


    private void SetNewDraw()
    {
        currentLottery.currentDraw = currentLottery.currentDraw + 1;

    }
}
