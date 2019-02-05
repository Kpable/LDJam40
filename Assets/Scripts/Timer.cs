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
	
	// Update is called once per frame
	public void Update () {
        if (running)
        {
            
            seconds -= Time.deltaTime;
            seconds = Mathf.Clamp(seconds, 0, seconds);

            if(secondsTracker - seconds >= 1 && OnSecondsChanged != null)
            {
                OnSecondsChanged((int)Mathf.Ceil(seconds));
                secondsTracker = seconds;
            }

            if (seconds <= 0)
            {
                OnSecondsChanged((int)Mathf.Ceil(seconds));
                TimeExpired();
            }
        }        
	}

    public void Start()
    {
        running = true;
    }

    public void Stop()
    {
        running = false;
    }

    public void Set(int seconds, bool start=false)
    {
        Set((float)seconds, start);
    }

    public void Set(float seconds, bool start = false)
    {
        this.seconds = seconds;
        secondsTracker = seconds;
        if (start) Start();
    }

    void TimeExpired()
    {
        Stop();
        if (OnTimeUp != null) OnTimeUp();

    }
}
