using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{

    [Serializable] public class TriggerColliderEvent : UnityEvent <Collider> {}

    public GameObject colliderRuleObject;
    public bool disableMeshRendererAtRuntime = true;

    public TriggerColliderEvent triggerEnter;
    public UnityEvent triggerStay;
    public UnityEvent triggerExit;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = !disableMeshRendererAtRuntime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //If there is no object set to collide with, then execute order
        if (colliderRuleObject == null)
        {
            triggerEnter?.Invoke(other);
        }
        //If there is an object set to collide with, then execute order when they collide
        else
        if (colliderRuleObject == other.gameObject)
        {
            triggerEnter?.Invoke(other);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        //If there is no object set to collide with, then execute order
        if (colliderRuleObject == null)
        {
            triggerStay?.Invoke();
        }
        //If there is an object set to collide with, then execute order when they collide
        else if (colliderRuleObject == other.gameObject)
        {
            triggerStay?.Invoke();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If there is no object set to collide with, then execute order
        if (colliderRuleObject == null)
        {
            triggerExit?.Invoke();
        }
        //If there is an object set to collide with, then execute order when they collide
        else if (colliderRuleObject == other.gameObject)
        {
            triggerExit?.Invoke();

        }
    }
}
