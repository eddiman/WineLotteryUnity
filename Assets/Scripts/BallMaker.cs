﻿using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.Events;

public class BallMaker : MonoBehaviour
{
    public GameObject ball;

    public int maxAmount;

    public float frequency = 1f;
    public int amountCounter = 0;
    public GameObject LotteryController;
    public bool instantiateAmount = false;
    public UnityEvent onStart;
    public UnityEvent onBallMaxAmount;

    private int _participatorCounter = 0;
    private int _participatorTicketCounter = 0;
    private Lottery _lottery;
    // Start is called before the first frame update

    public void StartBallMaking()
    {
        _lottery = LotteryController.GetComponent<LotteryController>().getLottery();
        onStart.Invoke();
        if (instantiateAmount)
        {
            InvokeRepeating("Generate", 1f, frequency);
        }
        else
        {
            maxAmount = _lottery.numberOfTickets;
            InvokeRepeating("GenerateParticipatingBalls", 1f, frequency);
        }
    }


    private void GenerateParticipatingBalls()
    {
        var currentParticipant = _lottery.participants[_participatorCounter];
        if (currentParticipant.numberOfTickets == _participatorTicketCounter)
        {
            _participatorCounter += 1;;
            _participatorTicketCounter = 0;
        }
        if (amountCounter >= maxAmount)
        {
            onBallMaxAmount.Invoke();
            CancelInvoke();
        }
        else
        {
            amountCounter += 1;
            _participatorTicketCounter += 1;
            var position = transform.position;
            var obj = Instantiate(ball, new Vector3(position.x, position.y, position.z), transform.rotation);
            obj.GetComponent<BallScript>().SetParticipant(currentParticipant);
            obj.transform.parent = transform;
        }
    }
    private void Generate()
    {
        if (amountCounter >= maxAmount)
        {
            onBallMaxAmount.Invoke();
            CancelInvoke();
        }
        else
        {
            var position = transform.position;
            var obj = Instantiate(ball, new Vector3(position.x, position.y, position.z), transform.rotation);
            ;
            obj.transform.parent = transform;
            amountCounter++;
        }
    }
}