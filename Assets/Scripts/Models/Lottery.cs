using System;
using System.Collections.Generic;
using UnityEngine;


namespace Models
{
    [Serializable]
    public class Lottery
    {
        public string createdDate;

        public string dateTime;

        public string description;
        [SerializeField]
        public List<Draw> draws;

        public string id;

        public string name;
        public bool started;

        public int currentDraw = 0;
        public int numberOfDraws;
        [SerializeField]
        public List<Participant> participants;

        public int numberOfTickets;

        public string userId;

    }
}
