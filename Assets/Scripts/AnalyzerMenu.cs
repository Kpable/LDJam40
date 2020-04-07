using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Analyzer Menu. 
/// </summary>
public class AnalyzerMenu : Menu
{
    private GameFlowController gameFlowController;

    /// <summary>
    /// Awake is called before start.
    /// </summary>
    private void Awake()
    {
        gameFlowController = GameObject.FindObjectOfType<GameFlowController>();
    }

    /// <summary>
    /// Start called before first update frame.
    /// </summary>
    private void Start()
    {
        gameFlowController.DayStateChanged += OnDayStateChanged;
    }

    /// <summary>
    /// Handles the change in day state.
    /// </summary>
    /// <param name="newState"> The new state of the day. </param>
    private void OnDayStateChanged(GameFlowController.DayState newState)
    {
        switch (newState)
        {
            case GameFlowController.DayState.Morning:
            case GameFlowController.DayState.Afternoon:
                EnableMenu();
                break;

            case GameFlowController.DayState.PreWork:
            case GameFlowController.DayState.Lunch:
            case GameFlowController.DayState.PostWork:
                DisableMenu();
                break;
            default:
                break;
        }
    }
}
