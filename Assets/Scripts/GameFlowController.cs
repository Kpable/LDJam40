using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controls the Game Flow.
/// </summary>
public class GameFlowController : MonoBehaviour
{
    private const int ONESECONDSCALE = 1;
    private const int FIVESECONDSCALE = 5;

    [SerializeField]
    private InGameClock inGameClock;
   
    private DayState currentState = DayState.PreWork;

    /// <summary>
    /// State of the day.
    /// </summary>
    public enum DayState
    {
        /// <summary>
        /// The time before the work shift begins.
        /// </summary>
        PreWork,

        /// <summary>
        /// The morning hours. 
        /// </summary>
        Morning,

        /// <summary>
        /// The lunch hour.
        /// </summary>
        Lunch,

        /// <summary>
        /// The afternoon hours.
        /// </summary>
        Afternoon,

        /// <summary>
        /// The time after the work shift has ended. 
        /// </summary>
        PostWork
    }

    /// <summary>
    /// Gets or sets event. Alert registrants the state changed. 
    /// </summary>
    public UnityAction<DayState> DayStateChanged { get; set; }

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

        inGameClock.RegisterTimedCallback(new TimeSpan(0, 7, 0, 0), () => { ChangeState(DayState.PreWork); });
        inGameClock.RegisterTimedCallback(new TimeSpan(0, 8, 0, 0), () => { ChangeState(DayState.Morning); });
        inGameClock.RegisterTimedCallback(new TimeSpan(0, 12, 0, 0), () => { ChangeState(DayState.Lunch); });
        inGameClock.RegisterTimedCallback(new TimeSpan(0, 13, 0, 0), () => { ChangeState(DayState.Afternoon); });
        inGameClock.RegisterTimedCallback(new TimeSpan(0, 17, 0, 0), () => { ChangeState(DayState.PostWork); });
    }

    /// <summary>
    /// Changes the state of the day.
    /// </summary>
    /// <param name="state"> The Day State to change to. </param>
    private void ChangeState(DayState state)
    {
        switch (state)
        {
            case DayState.PreWork:
                Debug.Log("Its pre work time!");
                inGameClock.InGameMinutesPerRealtimeSeconds = ONESECONDSCALE;
                break;
            case DayState.Morning:
                inGameClock.InGameMinutesPerRealtimeSeconds = FIVESECONDSCALE;
                Debug.Log("Its morning time!");
                break;
            case DayState.Lunch:
                inGameClock.InGameMinutesPerRealtimeSeconds = ONESECONDSCALE;
                Debug.Log("Its lunch time!");
                break;
            case DayState.Afternoon:
                inGameClock.InGameMinutesPerRealtimeSeconds = FIVESECONDSCALE;
                Debug.Log("Its noon time!");
                break;
            case DayState.PostWork:
                inGameClock.InGameMinutesPerRealtimeSeconds = ONESECONDSCALE;
                Debug.Log("Its after work time!");
                break;
            default:
                break;
        }

        DayStateChanged?.Invoke(state);
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
