using System;
using System.Collections.Generic;
using Helpers;
using Models;
using Newtonsoft.Json;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{


    public class PostRequests : MonoBehaviour
    {
        private readonly string BASE_PATH = FirebaseSettings.DATABASE_URL;
        public UnityEvent PostingData;
        public LotteryEvent onPostFinished;
        public UnityEvent onIdTokenFinished;
        private RequestHelper currentRequest;
        private readonly string API_KEY = FirebaseSettings.API_KEY;
        public Lottery currentLottery;

        public void GetIdToken()
        {
            currentRequest = new RequestHelper {
                Uri = FirebaseSettings.API_URL + FirebaseSettings.API_KEY,
                Method = "POST",
                BodyString = "{\"email\":\"" + FirebaseSettings.ADMIN_MAIL + "\",\"password\":\"" + FirebaseSettings.ADMIN_PW + "\",\"returnSecureToken\":true}",
            };
            RestClient.Request(currentRequest)
                .Then(res => {
                    // And later we can clear the default query string params for all requests
                    Token token = JsonConvert.DeserializeObject<Token>(res.Text);
                    FirebaseSettings.idToken = token.idToken;
                    onIdTokenFinished.Invoke();
                })
                .Catch(err => this.LogMessage("Error", err.Message));
        }
        public void Post()
        {
            currentLottery = getCurrentLottery();
            if(currentLottery == null) return;
            // We can add default query string params for all requests
            RequestHelper requestOptions = new RequestHelper {
                Uri = BASE_PATH,
                Headers = new Dictionary<string, string> {
                    { "Authorization", "Bearer " + FirebaseSettings.idToken }
                }
            };
            currentRequest = new RequestHelper {
                Uri = BASE_PATH + "/" +  currentLottery.id,
                Headers = new Dictionary<string, string> {
                    { "Authorization", "Bearer " + FirebaseSettings.idToken }
                },
                BodyString = JsonConvert.SerializeObject(currentLottery)
            };
            RestClient.Post<Lottery>(currentRequest)
                .Then(res => {

                    // And later we can clear the default query string params for all requests
                    RestClient.ClearDefaultParams();

                    this.LogMessage("Success", JsonUtility.ToJson(res, true));
                })
                .Catch(err => this.LogMessage("Error", err.Message));
        }




        private Lottery getCurrentLottery()
        {
            return GameObject.FindWithTag("LotteryController").GetComponent<LotteryController>().currentLottery;
        }
        private void LogMessage(string title, string message) {
#if UNITY_EDITOR
            EditorUtility.DisplayDialog (title, message, "Ok");
#else
		Debug.Log(message);
#endif
        }
    }
}
