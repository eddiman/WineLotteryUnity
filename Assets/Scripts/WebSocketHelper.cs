using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Helpers;
using UnityEngine;

// Use plugin namespace
using NativeWebSocket;

public class WebSocketHelper : MonoBehaviour
{
    private const string uri = WebSocketSettings.WEBSOCKET_URL;


    int count = 0;
    private WebSocket ws;

    private void Awake()
    {
        Debug.Log("start websokcetk");

        //ws = WebSocketFactory.CreateInstance(uri);
        ws = new WebSocket(uri);

    }
    public WebSocket getWebSocket()
    {
        return ws;
    }


}
