using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyBoardEvent : MonoBehaviour
{
    public UnityEvent SpaceEvent;
    public UnityEvent XEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpaceEvent.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            XEvent.Invoke();
        }
    }
}
