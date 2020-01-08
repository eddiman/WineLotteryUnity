using TextureSendReceiverCustom.Receiver;
using UnityEngine;
using UnityEngine.UI;

namespace TextureSendReceiverCustom.Example {
	[RequireComponent(typeof(TextureReceiver))]
	public class ExampleReceiver : MonoBehaviour {
		TextureReceiver receiver;
		public RawImage image;
		Texture2D texture;

		// Use this for initialization
		void Start () {
			receiver = GetComponent<TextureReceiver>();
			texture = new Texture2D(1, 1);
			image.texture = texture;

			receiver.SetTargetTexture(texture);
		}
	}
}
