using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class LotteryController : MonoBehaviour
{

    public Lottery currentLottery;

    public void setLottery(Lottery lottery)
    {
        currentLottery = lottery;
    }

    public  Lottery getLottery()
    {
        return currentLottery;
    }
}
