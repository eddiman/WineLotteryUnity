using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Helpers;
using NativeWebSocket;
using TextureSendReceiverCustom.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace TextureSendReceiverCustom {
    public enum ImgEncoding {
        JPG = 0,
        PNG = 1
    }
    public class _TextureSender : MonoBehaviour {
        Texture2D source;
        private bool stop = false;
        private WebSocket ws;

        public ImgEncoding imgEncoding = ImgEncoding.JPG;


        [Header("Must be the same in sender and receiver")]
        public int messageByteLength = 24;

        private async void Start() {
            Application.runInBackground = true;
            ws = GetComponent<WebSocketHelper>().getWebSocket();
            InvokeRepeating(nameof(SendData), 0f, 0.3f);
            await ws.Connect();
            //Start coroutine
            // StartCoroutine(initAndWaitForTexture());
        }

        private async void SendData()
        {
            bool readyToGetFrame = true;


            if (ws.State.ToString() != "Open") return ;
            byte[] imageBytes = EncodeImage();

            await ws.Send(imageBytes);
            Debug.Log("Sending "  + imageBytes.Length + " bytes per request");
        }
        public void SetSourceTexture(Texture2D t) {
            source = t;
        }

        public Texture2D GetSourceTexture()
        {
            return source;
        }

        //Converts the data size to byte array and put result to the fullBytes array
        void byteLengthToFrameByteArray(int byteLength, byte[] fullBytes) {
            //Clear old data
            Array.Clear(fullBytes, 0, fullBytes.Length);
            //Convert int to bytes
            byte[] bytesToSendCount = BitConverter.GetBytes(byteLength);
            //Copy result to fullBytes
            bytesToSendCount.CopyTo(fullBytes, 0);
        }

        IEnumerator initAndWaitForTexture() {
            while (source == null) {
                yield return null;
            }

/*

            ws.OnOpen += () =>
            {
                Debug.Log("WS connected in Sender!");
                Debug.Log("WS state:  in Sender" + ws.State.ToString());
            };

            // Add OnError event listener
            ws.OnError += (string errMsg) =>
            {
                Debug.Log("WS error  in Sender: " + errMsg);
            };

            // Add OnClose event listener
            ws.OnClose += (WebSocketCloseCode code) =>
            {
                Debug.Log("WS closed in Sender with code : " + code.ToString());
            };
            ws.Connect();*/

            //Start sending coroutine
            StartCoroutine(senderCOR());
        }

        WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
        IEnumerator senderCOR() {
            bool isConnected = false;
            TcpClient client = null;
            NetworkStream stream = null;

            bool readyToGetFrame = true;



            while (!stop) {
                //Wait for End of frame
                yield return endOfFrame;
                byte[] imageBytes = EncodeImage();

                //Set readyToGetFrame false
                readyToGetFrame = false;



                if (ws.State.ToString() != "Open") yield return null;
                Debug.Log("before asyncsa");

                readyToGetFrame = true;
                ws.Send(imageBytes);
                Debug.Log("Sending "  + imageBytes.Length + " bytes per request");

                //Wait until we are ready to get new frame(Until we are done sending data)
                while (!readyToGetFrame) {
                    yield return null;
                }
            }
        }

        private byte[] EncodeImage() {
            if(imgEncoding == ImgEncoding.PNG) return source.EncodeToPNG();
            return source.EncodeToJPG();
        }

        // stop everything
        private void OnApplicationQuit() {
            ws?.Close();
        }
    }

    // [END TextureSender]



    public class _TextureReceiver : MonoBehaviour {
        private WebSocket ws;
        [HideInInspector]
        public Texture2D texture;
        private int _byteSize;
        private bool stop = false;

        Texture2D source;


        [Header("Must be the same in sender and receiver")]
        public int messageByteLength = 24;

        // Use this for initialization
        async void Start() {
            //Application.runInBackground = true;

            //client = new TcpClient();
           // ws = GetComponent<WebSocketHelper>().getWebSocket();
            ws = new WebSocket(WebSocketSettings.WEBSOCKET_URL);
            //Connect to server from another Thread
            // if on desktop
            // client.Connect(IPAddress.Loopback, port);
            //client.Connect(IPAddress.Parse(IP), port);
            ws.OnOpen += () =>
            {
                Debug.Log("WS connected in TextureReceiver!");
                Debug.Log("WS state: in TextureReceiver" + ws.State.ToString());


            };
            // Add OnError event listener
            ws.OnError += (string errMsg) =>
            {
                Debug.Log("WS error in TextureReceiver: " + errMsg);
            };

            // Add OnClose event listener
            ws.OnClose += (WebSocketCloseCode code) =>
            {
                Debug.Log("WS closed with code in TextureReceiver: " + code.ToString());
            };
            //imageReceiver();
            ws.OnMessage += (byte[] msg) =>
            {

                Debug.Log("WS received " + msg.Length + " bytes in Receiver: " + (msg));
                Debug.Log("shawarma");


                bool readyToReadAgain = false;

                //Loom.QueueOnMainThread(() => {
                    loadReceivedImage(msg);
                    readyToReadAgain = true;
                //});
                while (!readyToReadAgain) {
                    System.Threading.Thread.Sleep(1);
                }

            };
            await ws.Connect();


        }
        void imageReceiver() {
            //While loop in another Thread is fine so we don't block main Unity Thread
            GetImgBytes(_byteSize);

        }
        private void SetImgByteSize(int imgByteSize)
        {
            _byteSize = (imgByteSize);
        }


        private void GetImgBytes(int size)
        {



        }

        void loadReceivedImage(byte[] receivedImageBytes)
        {

            if(texture) texture.LoadImage(receivedImageBytes);
        }

        public void SetTargetTexture (Texture2D t) {
            texture = t;
        }

        void OnApplicationQuit() {
            stop = true;
            ws.Close();


        }
        private Texture2D TextureToTexture2D(Texture texture)
        {
            return Texture2D.CreateExternalTexture(
                texture.width,
                texture.height,
                TextureFormat.RGB24,
                false, false,
                texture.GetNativeTexturePtr());
        }

    }



}
