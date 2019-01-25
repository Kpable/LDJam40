using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer {

    float seconds;

    public bool running;

    public delegate void SecondsChanged(int seconds);
    public event SecondsChanged OnSecondsChanged;

    public delegate void TimeUp();
    public event TimeUp OnTimeUp;

    float secondsTracker;

    TimeSpan time; 
	
	// Update is called once per frame
	public void Update () {
        if (running)
        {
            
            seconds -= Time.deltaTime;
            seconds = Mathf.Clamp(seconds, 0, seconds);

            if(secondsTracker - seconds >= 1 && OnSecondsChanged != null)
            {
                OnSecondsChanged((int)Mathf.Ceil(seconds));
            }

            if (seconds <= 0)
            {
                TimeExpired();
            }
        }        
	}

    public void StartTimer()
    {
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    public void SetTimer(int seconds, bool start=false)
    {
        //time = new TimeSpan(seconds * 10000000);
        time = new TimeSpan(8, 0,0);
        time.Add(new TimeSpan(0, 10, 0));
        Debug.Log("time set to: " + string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds));
        this.seconds = seconds;
        secondsTracker = seconds;
        if (start) StartTimer();
    }

    void TimeExpired()
    {
        StopTimer();
        if (OnTimeUp != null) OnTimeUp();

    }
}
