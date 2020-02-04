using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour {

    public float strength;
    public Vector3 direction;

    public void IncrementWindStrength(float seconds)
    {
        StartCoroutine(startIncrement(seconds, 0, 100));
    }

    IEnumerator startIncrement(float duration, float minStrength, float maxStrength)
    {
        bool isIncreasing = false;
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            strength = 0;
            strength = Mathf.Lerp(minStrength, maxStrength, counter / duration);
            yield return null;
        }

        isIncreasing = true;
    }
}
