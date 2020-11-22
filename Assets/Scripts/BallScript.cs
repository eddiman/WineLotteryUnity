using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class BallScript : MonoBehaviour
{
    public bool inWindZone = false;
    public GameObject windZone;
    Rigidbody rb;
    public bool hasBeenDrawn;
    public TextMeshPro btnText;
    public Participant participant;
    private int colorCode;

    void Start()
    {
        colorCode = UnityEngine.Random.Range(1, 8);
        rb = GetComponent<Rigidbody>();
        List<Material> matList = new List<Material>();
        GetComponent<MeshRenderer>().GetMaterials(matList);
        matList[0].color = getRandomColor();

    }

    public Color getRandomColor()
    {
        switch (colorCode)
        {
            case 1:
                return new Color(0.8f, 0.02f, 0f);
            case 2:
                return new Color(0.99f, 1f, 0f);
            case 3:
                return new Color(0.28f, 0.75f, 0.34f);
            case 4:
                return new Color(0.21f, 0.85f, 0.85f);
            case 5:
                return new Color(1f, 0.61f, 0f);
            case 6:
                return new Color(0.13f, 0.23f, 0.85f);
            case 7:
                return new Color(0.43f, 0f, 0.85f);
            case 8:
                return new Color(0.85f, 0.33f, 0.8f);

            default:
                return new Color(0,0,0);

        }
    }

    private void FixedUpdate()
    {
        if(inWindZone) {
            rb.AddForce(windZone.GetComponent<WindArea>().direction * windZone.GetComponent<WindArea>().strength);
        }
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

    public void SetParticipant(Participant p)
    {
        participant = p;
        btnText.text = p.name;
    }

}
