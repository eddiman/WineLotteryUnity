using System.Collections;
using System.Collections.Generic;
using Helpers;
using NativeWebSocket;
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
        public RawImage NoSignalImage;
        private byte[] rawByte;
        public ImgEncoding imgEncoding = ImgEncoding.JPG;

        public async void StartSenderStream() {
            Application.runInBackground = true;
            Texture2D raw = Resources.Load<Texture2D>("Images/no-signal");
            rawByte = raw.EncodeToJPG();
            ws = GetComponent<WebSocketHelper>().getWebSocket();
            InvokeRepeating(nameof(SendData), 0f, 0.1f);
            await ws.Connect();
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


        private byte[] EncodeImage() {
            if(imgEncoding == ImgEncoding.PNG) return source.EncodeToPNG();
            return source.EncodeToJPG();
        }

        // stop everything
        private async void OnApplicationQuit()
        {

            StartCoroutine(waitTillClose());
            await ws.Send(rawByte);
        }

        IEnumerator waitTillClose()
        {
            yield return new WaitForSeconds(3);
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
        public RawImage NoSignalImage;
        private byte[] oldMsg;
        private int timeOutCounter = 0;
        private int timeOutCounterOld = -1;



        // Use this for initialization
        public async void Start() {

            ws = new WebSocket(WebSocketSettings.WEBSOCKET_URL);
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
                LoadReceivedImage(msg);
                timeOutCounter++;
            };
            //InvokeRepeating(nameof(CheckIfMsgTimeOut), 0f, 3f);
            await ws.Connect();

        }

        private void CheckIfMsgTimeOut()
        {
            timeOutCounterOld = timeOutCounter;

            if (timeOutCounter != timeOutCounterOld) return;
            Texture2D raw = (Texture2D) NoSignalImage.texture;
            LoadReceivedImage(raw.EncodeToJPG());
        }
        void LoadReceivedImage(byte[] receivedImageBytes)
        {
            if(texture) texture.LoadImage(receivedImageBytes);
        }

        public void SetTargetTexture (Texture2D t) {
            texture = t;
        }

        async void OnApplicationQuit() {
            stop = true;
            await ws.Close();


        }

    }



}
