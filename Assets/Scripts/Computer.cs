using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all of the Computer's functions
/// </summary>
public class Computer : MonoBehaviour
{
    [SerializeField]
    private GameObject computerScreen;

    [SerializeField]
    private GameObject bootCanvas;

    private AnalyzerMenu analyzerMenu;

    private GameFlowController gameFlowController;

    private float bootTime = 5f;
    
    /// <summary>
    /// Turns on the Computer. 
    /// </summary>
    public void BootComputer()
    {
        Invoke("CompleteBoot", bootTime);
    }

    /// <summary>
    /// Logs the user in.
    /// </summary>
    public void Login()
    {
    }

    /// <summary>
    /// Completes the boot sequence.
    /// </summary>
    private void CompleteBoot()
    {
        computerScreen.SetActive(true);
        bootCanvas.SetActive(false);
    }

    /// <summary>
    /// Awake is called before start.
    /// </summary>
    private void Awake()
    {
        gameFlowController = GameObject.FindObjectOfType<GameFlowController>();
        analyzerMenu = GameObject.FindObjectOfType<AnalyzerMenu>();

        computerScreen.SetActive(false);
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
                analyzerMenu.EnableMenu();
                break;

            case GameFlowController.DayState.PreWork:
            case GameFlowController.DayState.Lunch:
            case GameFlowController.DayState.PostWork:
                analyzerMenu.DisableMenu();
                break;
            default:
                break;
        }
    }
}
