using System;
using System.Collections.Generic;


namespace Models
{
    [Serializable]
    public class Lottery
    {
        public string createdDate;

        public string dateTime;

        public string description;
        public List<Draw> draws;

        public string id;

        public string name;

        public int numberOfDraws;

        public List<Participant> participants;

        public string userId;

    }
}
