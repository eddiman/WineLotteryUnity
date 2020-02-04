using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHelper : MonoBehaviour
{
    public float fadeInTime = .2f;
    public float fadeOutTime = .2f;
    public void PlayAudioSource() {
        StartCoroutine(FadeInPlay(GetComponent<AudioSource>(), fadeInTime, GetComponent<AudioSource>().volume));

    }
    public void StopAudioSource() {
        StartCoroutine(FadeOutStop(GetComponent<AudioSource>(), fadeOutTime));
    }


    private static IEnumerator FadeOutStop(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
    private static IEnumerator FadeInPlay(AudioSource audioSource, float FadeTime, float currentVolume)
    {
        float startVolume = audioSource.volume;
        audioSource.volume = 0;
        audioSource.Play();
        while (audioSource.volume < currentVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        audioSource.volume = startVolume;
    }
}
