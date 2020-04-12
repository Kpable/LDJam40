using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private float logOnTime = 5f;

    private PoweredState computerPoweredState = PoweredState.Off;
    private LoggedOnState computerLoggedOnState = LoggedOnState.LoggedOff;

    /// <summary>
    /// The Powered State of the computer
    /// </summary>
    public enum PoweredState
    {
        /// <summary>
        /// Off state.
        /// </summary>
        Off,

        /// <summary>
        /// On State.
        /// </summary>
        On
    }

    /// <summary>
    /// The Logged On of the computer
    /// </summary>
    public enum LoggedOnState
    {
        /// <summary>
        /// Logged Off State.
        /// </summary>
        LoggedOff,

        /// <summary>
        /// Logged On State.
        /// </summary>
        LoggedOn
    }

    /// <summary>
    /// Gets or sets event. Alert registrants the state changed. 
    /// </summary>
    public UnityAction<PoweredState> PoweredStateStateChanged { get; set; }

    /// <summary>
    /// Gets or sets event. Alert registrants the state changed. 
    /// </summary>
    public UnityAction<LoggedOnState> LoggedOnStateChanged { get; set; }

    /// <summary>
    /// Gets or sets the Computer PoweredState.
    /// </summary>
    public PoweredState ComputerPoweredState
    {
        get
        {
            return computerPoweredState;
        }

        set
        {
            if (computerPoweredState != value)
            {
                computerPoweredState = value;
                PoweredStateStateChanged?.Invoke(computerPoweredState);
            }
        }
    }

    /// <summary>
    /// Gets or sets the Copmuter LoggedOnState
    /// </summary>
    public LoggedOnState ComputerLoggedOnState
    {
        get
        {
            return computerLoggedOnState;
        }

        set
        {
            if (computerLoggedOnState != value)
            {
                computerLoggedOnState = value;
                LoggedOnStateChanged?.Invoke(computerLoggedOnState);
            }
        }
    }

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
        Invoke("CompleteLogon", logOnTime);
    }

    /// <summary>
    /// Logs the user out.
    /// </summary>
    public void LogOff()
    {
        Invoke("CompleteLogoff", 0);
    }

    /// <summary>
    /// Completes the boot sequence.
    /// </summary>
    private void CompleteBoot()
    {
        ComputerPoweredState = PoweredState.On;
    }

    /// <summary>
    /// Enables the Screen Canvas.
    /// </summary>
    private void EnableScreen()
    {
        computerScreen.SetActive(true);
        bootCanvas.SetActive(false);
    }

    /// <summary>
    /// Disables the Screen Canvas.
    /// </summary>
    private void DisableScreen()
    {
        computerScreen.SetActive(false);
        bootCanvas.SetActive(true);
    }

    /// <summary>
    /// Completes the log on sequence. 
    /// </summary>
    private void CompleteLogon()
    {
        ComputerLoggedOnState = LoggedOnState.LoggedOn;
    }

    /// <summary>
    /// Completes the log off sequence. 
    /// </summary>
    private void CompleteLogoff()
    {
        computerLoggedOnState = LoggedOnState.LoggedOff;
    }

    /// <summary>
    /// Awake is called before start.
    /// </summary>
    private void Awake()
    {
        gameFlowController = GameObject.FindObjectOfType<GameFlowController>();
        analyzerMenu = GameObject.FindObjectOfType<AnalyzerMenu>();

        computerScreen.SetActive(false);

        PoweredStateStateChanged += OnPoweredStateChanged;
        LoggedOnStateChanged += OnLoggedOnStateChanged;
    }

    /// <summary>
    /// Start called before first update frame.
    /// </summary>
    private void Start()
    {
        gameFlowController.DayStateChanged += OnDayStateChanged;

        PoweredStateStateChanged?.Invoke(ComputerPoweredState);
        LoggedOnStateChanged?.Invoke(ComputerLoggedOnState);
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
                ////analyzerMenu.EnableMenu();
                break;

            case GameFlowController.DayState.PreWork:
            case GameFlowController.DayState.Lunch:
            case GameFlowController.DayState.PostWork:
                ////analyzerMenu.DisableMenu();
                LogOff();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Handles the change in logged on state.
    /// </summary>
    /// <param name="newState"> The new Logged on State. </param>
    private void OnLoggedOnStateChanged(LoggedOnState newState)
    {
        switch (newState)
        {
            case LoggedOnState.LoggedOff:
                analyzerMenu.DisableMenu();
                break;
            case LoggedOnState.LoggedOn:
                analyzerMenu.EnableMenu();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Handles the change in powered state.
    /// </summary>
    /// <param name="newState"> The new Powered State. </param>
    private void OnPoweredStateChanged(PoweredState newState)
    {
        switch (newState)
        {
            case PoweredState.Off:
                DisableScreen();
                break;
            case PoweredState.On:
                EnableScreen();
                break;
            default:
                break;
        }
    }
}
