using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallMaker : MonoBehaviour
{
    public GameObject coin;

    public int amount;

    public float frequency = 1f;
    public int _amountCounter = 0;

    public UnityEvent onStart;
    public UnityEvent onBallMaxAmount;
    // Start is called before the first frame update

    public void StartBallMaking()
    {
        onStart.Invoke();
        InvokeRepeating("Generate", 1f, frequency);
    }

    private void Generate()
    {
        if (_amountCounter >= amount)
        {
            onBallMaxAmount.Invoke();
            CancelInvoke();
        }
        else
        {
            var position = transform.position;
            var obj = Instantiate(coin, new Vector3(position.x, position.y, position.z), transform.rotation);
            obj.transform.parent = transform;
            _amountCounter++;
        }
    }
}
