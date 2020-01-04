using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;

public class StateEnumManager : MonoBehaviour
{
    [Serializable] public class StateEvent : UnityEvent <string> {}
    public string currentState;
    public StateEvent defaultState;
    public StateEvent loadingData;
    public StateEvent loadingDataFinished;
    [Header("Draw Ready")]
    [Header("Lottery States")]
    public StateEvent drawReady;
    [Header("Ball Making")]
    public StateEvent ballMakingState;
    [Header("Ball Making Finished")]
    public StateEvent ballMakingFinishedState;
    [Header("Draw Start")]
    public StateEvent drawStartState;
    [Header("Balls Fallen Down")]
    public StateEvent ballsFallenDownState;
    [Header("Ball Selected")]
    public StateEvent ballSelectedState;
    [Header("Ball Rolling Start")]
    public StateEvent ballRollingStartState;
    [Header("Ball Rolling Mid")]
    public StateEvent ballRollingMidState;
    [Header("Ball Rolling Finished")]
    public StateEvent ballRollingDownFinishedState;
    [Header("Ball Name Reveal")]
    public StateEvent ballNameRevealState;
    [Header("Draw Finished")]
    public StateEvent drawFinishedState;
    [Header("Draw Resetting")]
    public StateEvent drawResettingState;
    [Header("Draw Resetting Finished")]
    public StateEvent drawResettingFinishedState;

    private string[] States =
    {
        "Default",
        "LoadingData",
        "LoadingDataFinished",
        "DrawReady",
        "BallMaking",
        "BallMakingFinished",
        "DrawStart",
        "BallsFallenDown",
        "BallSelected",
        "BallRollingDownStart",
        "BallRollingDownMid",
        "BallRollingDownFinished",
        "BallNameReveal",
        "DrawFinished",
        "DrawResetting",
        "DrawResettingFinished"
    };

    private void Start()
    {
        SetState("Default");
    }

    public void SetState(string stateString)
    {
        for (int i = 0; i < States.Length; i++)
        {
            string state = States[i];
            if (state.Equals(stateString))
            {
                currentState = state;
                debugLogState(currentState);
                switch (currentState)
                {
                    case "LoadingData":
                        loadingData.Invoke(currentState);
                        break;
                    case "LoadingDataFinished":
                        loadingDataFinished.Invoke(currentState);
                        break;
                    case "DrawReady":
                        drawReady.Invoke(currentState);
                        break;
                    case "BallMaking":
                        ballMakingState.Invoke(currentState);

                        break;
                    case "BallMakingFinished":
                        ballMakingFinishedState.Invoke(currentState);

                        break;
                    case "DrawStart":
                        drawStartState.Invoke(currentState);

                        break;
                    case "BallsFallenDown":
                        ballsFallenDownState.Invoke(currentState);

                        break;
                    case "BallSelected":
                        ballSelectedState.Invoke(currentState);

                        break;
                    case "BallRollingDownStart":
                        ballRollingStartState.Invoke(currentState);

                        break;
                    case "BallRollingDownMid":
                        ballRollingMidState.Invoke(currentState);

                        break;
                    case "BallRollingDownFinished":
                        ballRollingDownFinishedState.Invoke(currentState);

                        break;
                    case "BallNameReveal":
                        ballNameRevealState.Invoke(currentState);

                        break;
                    case "DrawFinished":
                        drawFinishedState.Invoke(currentState);

                        break;
                    case "DrawResetting":
                        drawResettingState.Invoke(currentState);

                        break;
                    case "DrawResettingFinished":
                        drawResettingFinishedState.Invoke(currentState);

                        break;

                    case "Default":
                        defaultState.Invoke(currentState);

                        break;
                    default:
                        throw new Exception("This state: " + stateString + " does not exist in state list. Please review the state names in StateEnumManager.cs.");
                        break;
                }

                return;
            }
            if (i == States.Length-1)
            {
                throw new Exception("This state: " + stateString + " does not exist in state list. Please review the state names in StateEnumManager.cs.");

            }
        }
    }

    private void debugLogState(string stateString)
    {
        Debug.Log("State is now " + stateString + ", and the event of this state has been fired.");
    }

    public string GetState()
    {
        return currentState;
    }

}
