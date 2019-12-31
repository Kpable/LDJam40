using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kpable.Mechanics;
using System;

public class DeskClock : MonoBehaviour
{

    public ObservedStringVariable timeText;

    public Timer timer;
    TimeSpan time;

    public int dayCycle = 110; // Seconds

    private bool gameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        //timeText.Value = string.Format("{0:D2}:{1:D2}", ((time.Hours > 12) ? time.Hours - 12 : time.Hours), time.Minutes);

        timer = new Timer();
        timer.OnTimeUp += HandleTimeUp;
        timer.OnSecondsChanged += HandleSecondsChanged;
        timer.Set(dayCycle, true);
        time = new TimeSpan(8, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update();
    }

    #region Event Handlers

    void HandleTimeUp()
    {
        // Open perfomance Review
        Debug.Log("Time is up");
        StartCoroutine("BlinkText");
    }

    void HandleSecondsChanged(int seconds)
    {
        //time = new TimeSpan(seconds * 10000000);
        time = time.Add(new TimeSpan(0, 5, 0));
        //Debug.Log("time set to: " + string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds));
        timeText.Value = string.Format("{0:D2}:{1:D2}", ((time.Hours > 12) ? time.Hours - 12 : time.Hours), time.Minutes);
    }
    #endregion Event Handlers

    #region Canvas Events Handlers

    public void TogglePause()
    {
        if (gameIsPaused)
        {
            timer.Start();
            StopCoroutine("BlinkText");
            timeText.Value = string.Format("{0:D2}:{1:D2}", ((time.Hours > 12) ? time.Hours - 12 : time.Hours), time.Minutes);

        }
        else
        {
            timer.Stop();
            StartCoroutine("BlinkText");
        }
        gameIsPaused = !gameIsPaused;
    }

    #endregion Canvas Event Handlers

    public IEnumerator BlinkText()
    {
        timeText.Value = "";
        yield return new WaitForSeconds(0.5f);
        timeText.Value = string.Format("{0:D2}:{1:D2}", ((time.Hours > 12) ? time.Hours - 12 : time.Hours), time.Minutes);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("BlinkText");
    }
}
