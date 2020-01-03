using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

namespace Models
{
    public class LotteryDeserializer
    {
        public List<Lottery> GenerateLotteryList(string jsonRes)
        {
            var rootObj = JsonConvert.DeserializeObject<RootObject>(jsonRes);
            List<Lottery> lotteryList = new List<Lottery>();

            foreach (var row in rootObj.documents)
            {

                Lottery lottery = new Lottery();
                List<Participant> participants = new List<Participant>();
                List<Draw> draws = new List<Draw>();

                lottery.createdDate = row.createTime;
                lottery.dateTime = row.updateTime;
                lottery.description = row.fields.description.stringValue;
                lottery.id = row.fields.id.stringValue;
                lottery.name = row.name;
                lottery.numberOfDraws = row.fields.numberOfDraws.integerValue;
                lottery.userId = row.fields.userId.stringValue;

                if (row.fields.draws != null)
                {
                    foreach (var draw in row.fields.draws.arrayValue.values)
                    {
                        Draw drawObj = new Draw
                        {
                            started = draw.mapValue.fields.started.booleanValue,
                            winner = draw.mapValue.fields.winner != null ? draw.mapValue.fields.winner.stringValue : "",
                        };
                        draws.Add(drawObj);
                    }
                }

                if(row.fields.participants != null)
                {
                    foreach (var participant in row.fields.participants.arrayValue.values)
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

                lotteryList.Add(lottery);
            }

            return lotteryList;
        }

    }


    public class Started
{
    public bool booleanValue { get; set; }
}

public class Winner
{
    public string stringValue { get; set; }
}

public class Fields2
{
    public Started started { get; set; }
    public Winner winner { get; set; }
}

public class MapValue
{
    public Fields2 fields { get; set; }
}

public class Value
{
    public MapValue mapValue { get; set; }
}

public class ArrayValue
{
    public List<Value> values { get; set; }
}

public class Draws
{
    public ArrayValue arrayValue { get; set; }
}

public class UserId
{
    public string stringValue { get; set; }
}

public class Name
{
    public string stringValue { get; set; }
}

public class Id
{
    public string stringValue { get; set; }
}

public class DateTime
{
    public string timestampValue { get; set; }
}

public class CssTop
{
    public string integerValue { get; set; }
}

public class Name2
{
    public string stringValue { get; set; }
}

public class CssLeft
{
    public string integerValue { get; set; }
}

public class NumberOfTickets
{
    public int integerValue { get; set; }
}

public class Fields3
{
    public CssTop cssTop { get; set; }
    public Name2 name { get; set; }
    public CssLeft cssLeft { get; set; }
    public NumberOfTickets numberOfTickets { get; set; }
}

public class MapValue2
{
    public Fields3 fields { get; set; }
}

public class Value2
{
    public MapValue2 mapValue { get; set; }
}

public class ArrayValue2
{
    public List<Value2> values { get; set; }
}

public class Participants
{
    public ArrayValue2 arrayValue { get; set; }
}

public class NumberOfDraws
{
    public int integerValue { get; set; }
}

public class CreatedDate
{
    public string timestampValue { get; set; }
}

public class Description
{
    public string stringValue { get; set; }
}

public class Fields
{
    public Draws draws { get; set; }
    public UserId userId { get; set; }
    public Name name { get; set; }
    public Id id { get; set; }
    public DateTime dateTime { get; set; }
    public Participants participants { get; set; }
    public NumberOfDraws numberOfDraws { get; set; }
    public CreatedDate createdDate { get; set; }
    public Description description { get; set; }
}

public class Document
{
    public string name { get; set; }
    public Fields fields { get; set; }
    public string createTime { get; set; }
    public string updateTime { get; set; }
}

public class RootObject
{
    public List<Document> documents { get; set; }
}

}
