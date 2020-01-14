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

[Serializable] public class LotteryEvent : UnityEvent<List<Lottery>> {}

public class GetRequests : MonoBehaviour
{
    private readonly string BASE_PATH = FirebaseSettings.DATABASE_URL;
    public UnityEvent LoadingData;
    public LotteryEvent onLoadFinished;
    private RequestHelper currentRequest;
    [SerializeField]
    public List<Lottery> LotteryList;
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
            Debug.Log(FirebaseSettings.idToken);

            //EditorUtility.DisplayDialog("Response", response.Text, "Ok");
            LotteryListDeserializer l = new LotteryListDeserializer();

            LotteryList = l.GenerateLotteryList(response.Text);
            return LotteryList;
        }).Then(list =>
        {
            Debug.Log(FirebaseSettings.idToken);

            if (LotteryList != null)
            {
                onLoadFinished.Invoke(LotteryList);
            }
        }).Catch(err => this.LogMessage("Error", err.Message));

    }
    public void GetSingleLottery(string lotteryId){
        // We can add default request headers for all requests
       // RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + FIRESTORE_TOKEN;
       LoadingData.Invoke();
       RequestHelper requestOptions = new RequestHelper {
            Uri = BASE_PATH + "/" + lotteryId,
            Headers = new Dictionary<string, string> {
                { "Authorization", "Bearer " + FirebaseSettings.idToken }
            }
        };
        RestClient.Get(requestOptions).Then(response => {
            //EditorUtility.DisplayDialog("Response", response.Text, "Ok");
            LotteryListDeserializer l = new LotteryListDeserializer();

            LotteryList = l.GenerateLotteryList(response.Text);
            return LotteryList;
        }).Then(list =>
        {
            if (LotteryList != null)
            {
                onLoadFinished.Invoke(LotteryList);
            }
        }).Catch(err => this.LogMessage("Error", err.Message));

    }
}
