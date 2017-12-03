using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    public Workstation[] workstations;
    public Timer timer;
    public ClockLabel clockLabel;

    public PerformanceReview performanceReview;

    public int reviewCycle = 30;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Use this for initialization
    void Start () {
        timer = new Timer();
        timer.OnTimeUp += HandleTimeUp;
        timer.OnSecondsChanged += HandleSecondsChanged;

        clockLabel.SetTimeRemaining(reviewCycle);
        timer.SetTimer(reviewCycle, true);
    }

    // Update is called once per frame
    void Update () {
        timer.Update();
	}

    void HandleTimeUp()
    {
        // Open perfomance Review
        Debug.Log("Time is up");
        Time.timeScale = 0;
        performanceReview.gameObject.SetActive(true);
        performanceReview.Review(workstations);

    }

    void HandleSecondsChanged(int seconds)
    {
        clockLabel.SetTimeRemaining(seconds);

    }

    public void RestartWork()
    {
        for (int i = 0; i < workstations.Length; i++)
        {
            workstations[i].ClearWorkstation();
        }

        clockLabel.SetTimeRemaining(reviewCycle);
        timer.SetTimer(reviewCycle, true);

    }
}
