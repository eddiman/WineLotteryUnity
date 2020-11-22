using System;
using System.Collections;
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
    public int _participatorTicketCounter = 0;

    private Lottery _lottery;

    private List<Ticket> allTickets;
    // Start is called before the first frame update

    public void setNewBallType(Transform newBallPrefab)
    {
        ball = newBallPrefab.gameObject;
    }
    public void StartBallMaking()
    {
        _lottery = LotteryController.GetComponent<LotteryController>().GetLottery();
        onStart.Invoke();
        if (instantiateAmount)
        {
            InvokeRepeating("Generate", 1f, frequency);
        }
        else
        {
            allTickets = new List<Ticket>();
            GenerateAllTickets();
            Debug.Log(allTickets);

            maxAmount = _lottery.numberOfTickets;
            InvokeRepeating("GenerateParticipatingBalls", 1f, frequency);
        }
    }


    private void GenerateParticipatingBalls()
    {
        if (amountCounter >= maxAmount)
        {
            onBallMaxAmount.Invoke();
            CancelInvoke();
        }
        else
        {
            var currentTicket = allTickets[_participatorTicketCounter];
            amountCounter += 1;
            _participatorTicketCounter += 1;
            var position = transform.position;
            var obj = Instantiate(ball, new Vector3(position.x, position.y, position.z), transform.rotation, transform);
            obj.name = currentTicket.participant.name + "-" + _participatorTicketCounter;
            obj.GetComponent<BallScript>().SetParticipant(currentTicket.participant);


        }
    }    private void GenerateParticipatingBallsOLD()
    {
        var currentParticipant = _lottery.participants[_participatorCounter];
        if (amountCounter >= maxAmount)
        {
            onBallMaxAmount.Invoke();
            CancelInvoke();
        } else if (Int32.Parse(currentParticipant.numberOfTickets) == _participatorTicketCounter)
        {
            _participatorCounter += 1;
            _participatorTicketCounter = 0;
        }
        else
        {
            amountCounter += 1;
            _participatorTicketCounter += 1;
            var position = transform.position;
            var obj = Instantiate(ball, new Vector3(position.x, position.y, position.z), transform.rotation, transform);
            obj.name = currentParticipant.name + "-" + _participatorTicketCounter;
            obj.GetComponent<BallScript>().SetParticipant(currentParticipant);


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
            var obj = Instantiate(ball, new Vector3(position.x, position.y, position.z), transform.rotation,transform );
            amountCounter++;
        }
    }


    private void GenerateAllTickets()
    {
        foreach (var participant in _lottery.participants)
        {
            for (var i = 0; i < int.Parse(participant.numberOfTickets); i++)
            {
                var ticket = new Ticket {participant = participant};
                allTickets.Add(ticket);
            }
        }

        allTickets = Shuffle(allTickets);
    }

    private static List<Ticket> Shuffle (List<Ticket>aList) {

        System.Random _random = new System.Random ();

        Ticket myGO;

        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }

        return aList;
    }
}
