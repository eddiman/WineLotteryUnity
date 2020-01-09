using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{

    public Transform fromObject;
    public Transform toObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void setPosFromCollidedObj(Collider other)
    {
        other.transform.position = toObject.transform.position;
    }


}
