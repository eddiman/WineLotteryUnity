using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class LotteryController : MonoBehaviour
{
    [SerializeField]
    public Lottery currentLottery;

    public void SetLottery(Lottery lottery)
    {
        currentLottery = lottery;
    }

    public  Lottery GetLottery()
    {
        return currentLottery;
    }
}
