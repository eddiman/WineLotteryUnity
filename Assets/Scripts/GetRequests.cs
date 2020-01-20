using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Models;
using Newtonsoft.Json;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class LotteryListEvent : UnityEvent<List<Lottery>> {}
[Serializable] public class LotteryEvent : UnityEvent<Lottery> {}

public class GetRequests : MonoBehaviour
{
    private readonly string BASE_PATH = FirebaseSettings.DATABASE_URL;
    public UnityEvent LoadingData;
    public LotteryListEvent onListLoadFinished;
    public LotteryEvent onLotteryLoadFinished;
    private RequestHelper currentRequest;
    [SerializeField]
    public List<Lottery> lotteryList;
    public Lottery lottery;
    private readonly string API_KEY = FirebaseSettings.API_KEY;

    private void LogMessage(string title, string message) {
#if UNITY_EDITOR
        Debug.Log(message);
#else
		Debug.Log(message);
#endif
    }

    public void GetAllLotteries(){
        // We can add default request headers for all requests
        // RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + FIRESTORE_TOKEN;

        LoadingData.Invoke();
        RequestHelper requestOptions = new RequestHelper {
            Uri = BASE_PATH,
            Headers = new Dictionary<string, string> {
                { "Authorization", "Bearer " + FirebaseSettings.idToken }
            }
        };
        RestClient.Get(requestOptions).Then(response => {

            //EditorUtility.DisplayDialog("Response", response.Text, "Ok");
            LotteryListDeserializer l = new LotteryListDeserializer();

            lotteryList = l.GenerateLotteryList(response.Text);
            return lotteryList;
        }).Then(list =>
        {

            if (lotteryList != null)
            {
                onListLoadFinished.Invoke(lotteryList);
            }
        }).Catch(err => this.LogMessage("Error", err.Message));

    }
    public void GetSingleLottery(string lotteryId){
        // We can add default request headers for all requests
        LoadingData.Invoke();
        var entireUrl = BASE_PATH + "/" + lotteryId;
        RequestHelper requestOptions = new RequestHelper {
            Uri = entireUrl,
            Method = "GET",
            Headers = new Dictionary<string, string> {
                { "Authorization", "Bearer " + FirebaseSettings.idToken }
            }
        };
        Debug.Log(requestOptions);

        RestClient.Request(requestOptions).Then(response => {

            LotteryDeserializer l = new LotteryDeserializer();

            lottery = l.GenerateLottery(response.Text);
            return lottery;
        }).Then(lott =>
        { ;
            if (lott != null)
            {
                onLotteryLoadFinished.Invoke(lott);
            }
        }).Catch(err => this.LogMessage("Error", err.Message));

    }
}
