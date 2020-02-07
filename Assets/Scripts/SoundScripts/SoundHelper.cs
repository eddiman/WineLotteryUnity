using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHelper : MonoBehaviour
{
    private float initialVolume;
    public float fadeInTime = .2f;
    public float fadeOutTime = .2f;

    private void Start()
    {
        initialVolume = GetComponent<AudioSource>().volume;
    }

    public void PlayAudioSource() {
        StartCoroutine(FadeInPlay(GetComponent<AudioSource>(), fadeInTime, initialVolume));

    }
    public void StopAudioSource() {
        StartCoroutine(FadeOutStop(GetComponent<AudioSource>(), fadeOutTime));
    }
    public void PauseAudioSource() {
        StartCoroutine(FadeOutPause(GetComponent<AudioSource>(), fadeOutTime));
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
    private static IEnumerator FadeOutPause(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        float adjustedVolume = startVolume;

        while (audioSource.volume > 0)
        {
            adjustedVolume -= startVolume * Time.deltaTime / FadeTime;
            audioSource.volume = adjustedVolume;
            yield return null;
        }
        audioSource.Pause();
    }
    private static IEnumerator FadeInPlay(AudioSource audioSource, float FadeTime, float currentVolume)
    {
        float startVolume = currentVolume;
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
