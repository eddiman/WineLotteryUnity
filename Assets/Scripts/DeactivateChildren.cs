using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateChildren : MonoBehaviour
{
    public Transform parent;


    public void SetActiveAllChildren(bool newBoolState)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(newBoolState);

        }

    }
}


