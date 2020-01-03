using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraTranslate : MonoBehaviour
{

    public GameObject objectToTranslate;
    public GameObject fromObject;
    public GameObject toObject;
    public GameObject lookAtObject;
    public float duration = 1f;
    public bool disableMeshRendererAtRuntime = true;
    public bool drawLineBetweenOnSelect = true;
    public UnityEvent onStart;
    public UnityEvent onFinished;

    private Quaternion _lookRotation;

    void Start()
    {
        toObject.GetComponent<MeshRenderer>().enabled = !disableMeshRendererAtRuntime;
        lookAtObject.GetComponent<MeshRenderer>().enabled = !disableMeshRendererAtRuntime;
        objectToTranslate.transform.position = fromObject.transform.position;

        _lookRotation = Quaternion.LookRotation(lookAtObject.transform.position - toObject.transform.position);

        if (fromObject != objectToTranslate)
        {
            fromObject.GetComponent<MeshRenderer>().enabled = !disableMeshRendererAtRuntime;

        }
    }

    public void OnDrawGizmosSelected () {
        // Draws a blue line from this transform to the target
        if (!drawLineBetweenOnSelect) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(toObject.transform.position, lookAtObject.transform.position);
    }

    public void StartTranslation()
    {
        StartCoroutine(MoveToPosition(fromObject.transform.position, toObject.transform.position, objectToTranslate, duration));

    }


    bool _isMoving = false;


    private IEnumerator MoveToPosition(Vector3 fromPosition, Vector3 toPosition, GameObject objToTranslate, float duration)
    {
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
            objToTranslate.transform.position = Vector3.Lerp(fromPosition, toPosition, counter / duration);
            objToTranslate.transform.rotation =
                Quaternion.Slerp(objToTranslate.transform.rotation, _lookRotation, counter / duration);
            yield return null;
        }
        onFinished.Invoke();
        _isMoving = false;
    }
}
