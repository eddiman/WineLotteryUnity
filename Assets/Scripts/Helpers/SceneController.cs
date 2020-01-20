using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helpers
{
	public class SceneController : MonoBehaviour {
		private void Start()
		{
			Fading.BeginFade(-1);

		}

		public void RestartScene()
		{

			StartCoroutine(waitAndRestartScene());
		}

		public void GoToScene(string sceneName)
		{

			StartCoroutine(waitAndLoadScene(sceneName));

		}


		IEnumerator waitAndLoadScene(string sceneName)
		{
			Fading.BeginFade(1);
			yield return new WaitForSeconds(1);
			SceneManager.LoadScene(sceneName);
			SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
		}

		IEnumerator waitAndRestartScene()
		{
			Fading.BeginFade(1);
			yield return new WaitForSeconds(1);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

	}
}
