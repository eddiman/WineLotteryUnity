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

    // Use this for initialization
    private void Start()
    {

        //OpenWebSocket(ws, Encoding.UTF8.GetBytes("Hello from Unity 3D!"  + " " + count ));
        //ListenToWebSocket(ws);
        // Add OnOpen event listener

        /*ws.OnOpen += () =>
        {
            Debug.Log("WS connected!");
            Debug.Log("WS state: " + ws.GetState().ToString());

            ws.Send(Encoding.UTF8.GetBytes("Sending in start" ));

        };

        // Add OnError event listener
        ws.OnError += (string errMsg) =>
        {
            Debug.Log("WS error: " + errMsg);
        };

        // Add OnClose event listener
        ws.OnClose += (WebSocketCloseCode code) =>
        {
            Debug.Log("WS closed with code: " + code.ToString());
        };*/

        /*ws.OnMessage += (byte[] msg) =>
        {
            count++;
            Debug.Log("WS received message: " + Encoding.UTF8.GetString(msg));
            ws.Send(Encoding.UTF8.GetBytes("Hello from Unity 3D!"  + " " + count ));
            ws.Close();
        };*/
        //ws.Connect();

        //InvokeRepeating("SendOnMessage", 1f, .1f);

    }

    private void FixedUpdate()
    {
        // Add OnMessage event listener
/*        ws.OnMessage += (byte[] msg) =>
        {
            count++;
            Debug.Log("WS received message: " + Encoding.UTF8.GetString(msg));
            ws.Send(Encoding.UTF8.GetBytes("Hello from Unity 3D!"  + " " + count ));
            //ws.Close();
        };*/
        //ws.Connect();

    }

    public void SendOnMessage()
    {
        Debug.Log("trying to snd msg");

        ws.OnMessage += (byte[] msg) =>
        {
            count++;
            Debug.Log("WS received message: " + Encoding.UTF8.GetString(msg));
            ws.Send(Encoding.UTF8.GetBytes("Hello from Unity 3D!"  + " " + count ));
            //ws.Close();
        };
        //ws.Connect();
    }

    public WebSocket getWebSocket()
    {
        return ws;
    }




    public static void ListenToWebSocket(WebSocket ws)
    {
        // Add OnMessage event listener
        ws.OnMessage += (byte[] msg) =>
        {
            Debug.Log("WS received message: " + Encoding.UTF8.GetString(msg));

            ws.Close();
        };

        // Add OnError event listener
        ws.OnError += (string errMsg) =>
        {
            Debug.Log("WS error: " + errMsg);
        };

        // Add OnClose event listener
        ws.OnClose += (WebSocketCloseCode code) =>
        {
            Debug.Log("WS closed with code: " + code.ToString());
        };

        // Connect to the server
        ws.Connect();

    }



    public void OpenWebSocket(WebSocket ws, byte[] msgBytes)
    {
        // Add OnOpen event listener
        ws.OnOpen += () =>
        {
            Debug.Log("WS connected!");
            Debug.Log("WS state: " + ws.State.ToString());

            ws.Send(msgBytes);
        };
        ws.Connect();

    }
    // Update is called once per frame

}
