using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Models
{
    public class LotteryDeserializer
    {
        public Lottery GenerateLottery(string jsonRes)
        {
            var rootObj = JsonConvert.DeserializeObject<Document>(jsonRes);

                Lottery lottery = new Lottery(
                );
                List<Participant> participants = new List<Participant>();
                List<Draw> draws = new List<Draw>();

                lottery.createdDate = rootObj.createTime;
                lottery.dateTime = rootObj.fields.dateTime.timestampValue;
                lottery.description = rootObj.fields.description.stringValue;
                lottery.id = rootObj.fields.id.stringValue;
                lottery.name = rootObj.fields.name.stringValue;
                lottery.numberOfDraws = rootObj.fields.numberOfDraws.integerValue;
                lottery.userId = rootObj.fields.userId.stringValue;

                if (rootObj.fields.draws != null)
                {
                    foreach (var draw in rootObj.fields.draws.arrayValue.values)
                    {
                        Draw drawObj = new Draw
                        {
                            started = draw.mapValue.fields.started.booleanValue,
                            winner = draw.mapValue.fields.winner != null ? draw.mapValue.fields.winner.stringValue : "",
                        };
                        draws.Add(drawObj);
                    }
                }

                if(rootObj.fields.participants != null)
                {
                    foreach (var participant in rootObj.fields.participants.arrayValue.values)
                    {
                        Participant part = new Participant
                        {
                            name = participant.mapValue.fields.name.stringValue,
                            numberOfTickets = participant.mapValue.fields.numberOfTickets.integerValue
                        };


                        participants.Add(part);

                    }
                }




                lottery.participants = Shuffle(participants);
                lottery.draws = draws;
                lottery.numberOfTickets = Int32.Parse(GetSumOfTickets(participants));



            return lottery;
        }
        private static List<Participant> Shuffle (List<Participant>aList) {

            System.Random _random = new System.Random ();

            Participant myGO;

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
        private string GetSumOfTickets(List<Participant> list)
        {
            int tickets = 0;
            foreach (var participant in list)
            {
                tickets += Int32.Parse(participant.numberOfTickets);
            }

            return tickets.ToString();
        }

    }

}
