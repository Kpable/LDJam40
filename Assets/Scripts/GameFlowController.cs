using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the Game Flow.
/// </summary>
public class GameFlowController : MonoBehaviour
{
    [SerializeField]
    private InGameClock inGameClock;
   
    private DayState currentState = DayState.PreWork;

    private enum DayState
    {
        PreWork,
        Morning,
        Lunch,
        Afternoon,
        PostWork
    }

    /// <summary>
    /// Called first. 
    /// </summary>
    private void Awake()
    {
        if (!inGameClock)
        {
            Debug.LogError(name + ": InGameClock is not linked");
            return;
        }

        inGameClock.RegisterTimedCallback(new TimeSpan(0, 7, 0, 0), PreWork);
        inGameClock.RegisterTimedCallback(new TimeSpan(0, 8, 0, 0), Morning);
        inGameClock.RegisterTimedCallback(new TimeSpan(0, 12, 0, 0), Lunch);
        inGameClock.RegisterTimedCallback(new TimeSpan(0, 13, 0, 0), Afternoon);
        inGameClock.RegisterTimedCallback(new TimeSpan(0, 17, 0, 0), PostWork);
    }

    /// <summary>
    /// Time of Day
    /// </summary>
    private void PreWork()
    {
        Debug.Log("Its pre work time!");
    }

    /// <summary>
    /// Time of Day
    /// </summary>
    private void Morning()
    {
        Debug.Log("Its morning time!");
    }

    /// <summary>
    /// Time of Day
    /// </summary>
    private void Lunch()
    {
        Debug.Log("Its lunch time!");
    }

    /// <summary>
    /// Time of Day
    /// </summary>
    private void Afternoon()
    {
        Debug.Log("Its noon time!");
    }

    /// <summary>
    /// Time of Day
    /// </summary>
    private void PostWork()
    {
        Debug.Log("Its after work time!");
    }
}
