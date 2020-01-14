using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotateTowardsObject : MonoBehaviour
{

    public GameObject objectToRotate;
    public GameObject lookAtObject;
    public float duration = 1f;
    public bool rotateToZero = false;

    public UnityEvent onStart;
    public UnityEvent onFinished;


    private Quaternion _lookRotation;


    void Start()
    {
        if (rotateToZero || lookAtObject == null)
        {
            _lookRotation = Quaternion.Euler(Vector3.zero);
            lookAtObject = null;
        }  else {
            _lookRotation = Quaternion.LookRotation(lookAtObject.transform.position - transform.position);
        }
    }

    public void SetObjectToRotate(GameObject obj)
    {
        objectToRotate = obj;
    }

    public void StartRotation()
    {
        StartCoroutine(RotateTowards(duration, null));

    }
    public void StartRotationOfChild(string Tag)
    {
        var child = GameObject.FindGameObjectWithTag(Tag);
        if (child == null) return;
        StartCoroutine(RotateTowards(duration, child));

    }


    bool _isMoving = false;


    private IEnumerator RotateTowards(float duration, GameObject child)
    {
        if (child != null)
        {
            objectToRotate = child;
        }
        //Make sure there is only one instance of this function running
        if (_isMoving)
        {
            yield break; ///exit if this is still running
        }
        onStart.Invoke();
        _isMoving = true;

        float counter = 0;

        //Get the current position of the object to be moved

        while (counter < duration)
        {
            counter += Time.deltaTime;
            objectToRotate.transform.rotation =
                Quaternion.Lerp(objectToRotate.transform.rotation, _lookRotation, counter / duration);
            yield return null;
        }
        onFinished.Invoke();
        _isMoving = false;
    }
}
