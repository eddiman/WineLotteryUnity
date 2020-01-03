using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    private static GameObject _staticBall;

    public static void SetBall(GameObject ball)
    {
        _staticBall = ball;
    }

    public static GameObject GetBall()
    {
        return _staticBall;

    }

    public void setVelocityOfAllBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            ball.GetComponent<BallScript>().inWindZone = false;
            Debug.Log("zeroing balls");

        }
    }
}
