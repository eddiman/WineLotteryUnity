using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models
{
    public class LotteryDeserializer
    {
        public List<Lottery> GenerateLottery(string jsonRes)
        {
            var rootObj = JsonConvert.DeserializeObject<Document>(jsonRes);
            List<Lottery> lotteryList = new List<Lottery>();

            {

                Lottery lottery = new Lottery(
                
                );
                List<Participant> participants = new List<Participant>();
                List<Draw> draws = new List<Draw>();

                lottery.createdDate = rootObj.createTime;
                lottery.dateTime = rootObj.updateTime;
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

                lottery.participants = participants;
                lottery.draws = draws;
                lottery.numberOfTickets = GetSumOfTickets(participants);

                lotteryList.Add(lottery);
            }

            return lotteryList;
        }

        private int GetSumOfTickets(List<Participant> list)
        {
            int tickets = 0;
            foreach (var participant in list)
            {
                tickets += participant.numberOfTickets;
            }

            return tickets;
        }

    }

}
