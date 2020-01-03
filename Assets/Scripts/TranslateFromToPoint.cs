using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TranslateFromToPoint : MonoBehaviour
{

    public GameObject objectToTranslate;
    public GameObject fromObject;
    public GameObject toObject;
    public float duration = 1f;
    public bool disableMeshRendererAtRuntime = true;
    [Header("Events only apply when object is translating from the 'fromObject' to 'toObject', not initialPosition")]
    public UnityEvent onStart;
    public UnityEvent onFinished;

    private Vector3 _initialPosition;

    private void Start()
    {
        toObject.GetComponent<MeshRenderer>().enabled = !disableMeshRendererAtRuntime;
        objectToTranslate.transform.position = fromObject.transform.position;
        _initialPosition = objectToTranslate.transform.position;
       // StartCoroutine(MoveToPosition(fromObject.transform.position, toObject.transform.position, objectToTranslate, duration));
        if(fromObject != objectToTranslate)
            fromObject.GetComponent<MeshRenderer>().enabled = !disableMeshRendererAtRuntime;

    }

    public void StartTranslation()
    {
        StartCoroutine(MoveToPosition(fromObject.transform.position, toObject.transform.position, objectToTranslate, duration, false));

    }
    public void StartTranslationToInitialPos()
    {
        StartCoroutine(MoveToPosition(fromObject.transform.position, _initialPosition, objectToTranslate, duration, true));

    }


    bool _isMoving = false;


   private IEnumerator MoveToPosition(Vector3 fromPosition, Vector3 toPosition, GameObject objToTranslate, float duration, bool initialPos)
    {
        //Make sure there is only one instance of this function running
        if (_isMoving)
        {
            yield break; //exit if this is still running
        }
        if(!initialPos)
            onStart.Invoke();

        _isMoving = true;

        float counter = 0;

        //Get the current position of the object to be moved

        while (counter < duration)
        {
            counter += Time.deltaTime;
            objToTranslate.transform.position = Vector3.Lerp(fromPosition, toPosition, counter / duration);
            yield return null;
        }
        if(!initialPos)
            onFinished.Invoke();
        _isMoving = false;
    }
}
