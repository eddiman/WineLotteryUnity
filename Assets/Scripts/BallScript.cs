using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public bool inWindZone = false;
    public GameObject windZone;
    Rigidbody rb;
    public bool hasBeenDrawn;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        if(inWindZone) {
            rb.AddForce(windZone.GetComponent<WindArea>().direction * windZone.GetComponent<WindArea>().strength);
        }
    }

    private void OnCollisionEnter(Collision other)
    {


    }

    private void OnTriggerEnter(Collider coll) {
        if (!coll.gameObject.CompareTag("WindArea")) return;
        if (coll.gameObject.CompareTag("NotWindArea"))
        {
            inWindZone = false;
        }
        windZone = coll.gameObject;

        inWindZone = true;
    }

    private void OnTriggerExit(Collider coll) {
        if(coll.gameObject.CompareTag("WindArea")) {
            inWindZone = false;
        }
    }

    public void SetForceToZero()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.zero);
    }

}
