using TextureSendReceiverCustom.Receiver;
using TextureSendReceiverCustom.Sender;
using UnityEngine;
using UnityEngine.UI;

namespace TextureSendReceiverCustom.Example {
    [RequireComponent(typeof(Camera), typeof(TextureSender))]
    public class CameraSender : MonoBehaviour {
        Camera camera;
        TextureSender sender;
        TextureReceiver ReceiverTEST;
        Texture2D sendTexture;
        RenderTexture videoTexture;

        public RawImage image;

        // Use this for initialization
        void Start () {
            camera = GetComponent<Camera>();
            sender = GetComponent<TextureSender>();

            sendTexture = new Texture2D((int)camera.targetTexture.width, (int)camera.targetTexture.width);

            // Set send texture
            sender.SetSourceTexture(sendTexture);

        }

        // Update is called once per frame
        void Update () {
            RenderTexture.active = camera.targetTexture;
            sendTexture.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0, false);


            // Set preview image target
            if (image == null) return;
            image.texture = camera.targetTexture;
        }
    }
}
