using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneReset : MonoBehaviour
{
    public UnityEvent resetScene;

    public void ResetEvent()
    {
        resetScene.Invoke();
    }
}
