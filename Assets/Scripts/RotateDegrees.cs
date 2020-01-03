using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotateDegrees : MonoBehaviour
{

    public GameObject objectToTranslate;
    public float rotationDeg;
    public float duration = 1f;
    public string rotationAxis = "X";
    public bool linearMovement;
    public bool disableMeshRendererAtRuntime = true;
    public UnityEvent onStart;
    public UnityEvent onFinished;

    private Quaternion _rotation;
    private Quaternion _initialRotation;
    private float _currentX;
    private float _currentY;
    private float _currentZ;
    private void Start()
    {
        rotationAxis = rotationAxis.ToUpper();

        _rotation = transform.rotation;
        _initialRotation = _rotation;

        _currentX = _rotation.eulerAngles.x;
        _currentY = _rotation.eulerAngles.y;
        _currentZ = _rotation.eulerAngles.z;
        GetComponent<MeshRenderer>().enabled = !disableMeshRendererAtRuntime;
    }

    public void Rotate()
    {
        StartCoroutine(Rotate(transform.rotation, GetRotationAxis(), objectToTranslate, duration));
    }
    public void RotateToInitial()
    {
        StartCoroutine(Rotate(transform.rotation, _initialRotation, objectToTranslate, duration));
    }

    public Quaternion GetRotationAxis()
    {
        Quaternion rot;
        switch (rotationAxis)
        {

            case "X":
                rot = Quaternion.Euler(rotationDeg, _currentY, _currentZ);
                break;
            case "Y":
                rot = Quaternion.Euler(_currentX, rotationDeg, _currentZ);
                break;
            case "Z":
                rot = Quaternion.Euler(_currentX, _currentY, rotationDeg);
                break;
            default:
                rot = Quaternion.Euler(_currentX, _currentY, _currentZ);
                break;
        }

        return rot;
    }

    bool _isMoving = false;


    private IEnumerator Rotate(Quaternion fromRotation, Quaternion toRotation, GameObject objToTranslate, float duration)
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
            objToTranslate.transform.rotation = linearMovement ? Quaternion.Lerp(fromRotation, toRotation, counter / duration) : Quaternion.Slerp(fromRotation, toRotation, counter / duration);
            yield return null;
        }
        onFinished.Invoke();
        _isMoving = false;
    }
}
