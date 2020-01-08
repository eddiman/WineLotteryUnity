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
    private readonly string FIRESTORE_TOKEN = FirebaseSettings.TOKEN;
    private readonly string API_KEY = FirebaseSettings.API_KEY;


    private void LogMessage(string title, string message) {
#if UNITY_EDITOR
        EditorUtility.DisplayDialog (title, message, "Ok");
#else
		Debug.Log(message);
#endif
    }

    public void Post(){

        // We can add default query string params for all requests
        RestClient.DefaultRequestParams["param1"] = "My first param";
        RestClient.DefaultRequestParams["param3"] = "My other param";

        currentRequest = new RequestHelper {
            Uri = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithCustomToken?key=" + API_KEY,
            Params = new Dictionary<string, string> {
                { "param1", "value 1" },
                { "param2", "value 2" }
            },
            Body = new Post {
                title = "foo",
                body = "bar",
                userId = 1
            },
            EnableDebug = true
        };
        RestClient.Post<Post>(currentRequest)
            .Then(res => {

                // And later we can clear the default query string params for all requests
                RestClient.ClearDefaultParams();

                this.LogMessage("Success", JsonUtility.ToJson(res, true));
            })
            .Catch(err => this.LogMessage("Error", err.Message));
    }

    public void GetAllLotteries(){
        // We can add default request headers for all requests
       // RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + FIRESTORE_TOKEN;
       LoadingData.Invoke();
       RequestHelper requestOptions = new RequestHelper {
            Uri = BASE_PATH,
            Headers = new Dictionary<string, string> {
                { "Authorization", "Bearer " + FIRESTORE_TOKEN }
            }
        };
        RestClient.Get(BASE_PATH).Then(response => {
            //EditorUtility.DisplayDialog("Response", response.Text, "Ok");
            LotteryDeserializer l = new LotteryDeserializer();

            LotteryList = l.GenerateLotteryList(response.Text);
            return LotteryList;
        }).Then(list =>
        {
            if (LotteryList != null)
            {
                onLoadFinished.Invoke(LotteryList);
            }
        }).Catch(err => this.LogMessage("Error", err.Message));

        /*
        RestClient.GetArray<Lottery>(requestOptions).Then(res => {
            LogMessage("Posts", JsonHelper.ArrayToJsonString(res, true));

            return RestClient.GetArray<Todo>(requestOptions);
        }).Then(res => {
            this.LogMessage("Header", requestOptions.GetHeader("Authorization"));

            // And later we can clear the default headers for all requests
            RestClient.ClearDefaultHeaders();

        }).Catch(err => this.LogMessage("Error", err.Message));*/
    }
}
