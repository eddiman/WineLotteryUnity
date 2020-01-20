using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OpenLotteryHelper : MonoBehaviour
{
    [Serializable] public class StringEvent : UnityEvent<string> {}

    public StringEvent InitLotteryEvent;

    public GameObject inputField;

    public void InitEvent()
    {
        var pin = inputField.GetComponent<TMP_InputField>().text;
        var temp = pin.ToCharArray();


        InitLotteryEvent.Invoke(pin);
    }
    // Update is called once pe frame
}
