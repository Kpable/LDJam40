using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Workstation[] workstations;
    public Timer timer;
    public ClockLabel clockLabel;

    public PerformanceReview performanceReview;

	// Use this for initialization
	void Start () {
        timer = new Timer();
        timer.OnTimeUp += HandleTimeUp;
        timer.OnSecondsChanged += HandleSecondsChanged;

        clockLabel.SetTimeRemaining(30);
        timer.SetTimer(30, true);
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
}
