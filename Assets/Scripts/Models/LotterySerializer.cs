using System;
using System.Collections.Generic;
using Helpers;
using Newtonsoft.Json;
using UnityEngine;

namespace Models
{/***This shit is wack
  * @author Edvrd
  * */
    public class LotterySerializer
    {
        public string SerializeLotteryToJson(Lottery lottery)
        {

            var rootObj = new Document();
            rootObj.name = FirebaseSettings.DATABASE_URL_PARTIAL + "/" + lottery.id;
            rootObj.createTime = lottery.createdDate;
            rootObj.updateTime = lottery.dateTime;
            rootObj.updateTime = lottery.dateTime;

            rootObj.fields = new Fields();
            rootObj.fields.description = new Description();
            rootObj.fields.createdDate = new CreatedDate();
            rootObj.fields.dateTime = new DateTime();
            rootObj.fields.dateTime.timestampValue = lottery.dateTime;
            rootObj.fields.createdDate.timestampValue = lottery.createdDate;
            rootObj.fields.description.stringValue = lottery.description;

            rootObj.fields.id = new Id();
            rootObj.fields.id.stringValue = lottery.id;

            rootObj.fields.name = new Name();
            rootObj.fields.name.stringValue = lottery.name;

            rootObj.fields.numberOfDraws = new NumberOfDraws();
            rootObj.fields.numberOfDraws.integerValue = lottery.numberOfDraws;

            rootObj.fields.userId = new UserId();
            rootObj.fields.userId.stringValue = lottery.userId;


            if (lottery.draws != null)
            {
                rootObj.fields.draws = new Draws();
                rootObj.fields.draws.arrayValue = new ArrayValue();
                rootObj.fields.draws.arrayValue.values = new List<Value>();
                foreach (var draw in lottery.draws)
                {

                    Value drawValue = new Value();
                    drawValue.mapValue = new MapValue();
                    drawValue.mapValue.fields = new Fields2();
                    drawValue.mapValue.fields.started = new Started();
                    drawValue.mapValue.fields.started.booleanValue = draw.started;
                    drawValue.mapValue.fields.winner = new Winner();
                    drawValue.mapValue.fields.winner.stringValue = draw.winner;

                    rootObj.fields.draws.arrayValue.values.Add(drawValue);
                }
            }

            if(lottery.participants != null)
            {
                rootObj.fields.participants = new Participants();
                rootObj.fields.participants.arrayValue = new ArrayValue2();
                rootObj.fields.participants.arrayValue.values = new List<Value2>();
                foreach (var participant in lottery.participants)
                {
                    var particValue = new Value2();
                    particValue.mapValue = new MapValue2();
                    particValue.mapValue.fields = new Fields3();
                    particValue.mapValue.fields.name = new Name2();
                    particValue.mapValue.fields.cssLeft = new CssLeft();
                    particValue.mapValue.fields.cssTop = new CssTop();
                    particValue.mapValue.fields.cssLeft.integerValue = "0";
                    particValue.mapValue.fields.cssTop.integerValue = "0";
                    particValue.mapValue.fields.numberOfTickets = new NumberOfTickets();

                    particValue.mapValue.fields.name.stringValue = participant.name;
                    particValue.mapValue.fields.numberOfTickets.integerValue = participant.numberOfTickets;

                    rootObj.fields.participants.arrayValue.values.Add(particValue);
                }
            }

            string json = JsonConvert.SerializeObject(rootObj);

            return json;
        }


    }

}
