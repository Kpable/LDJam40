﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    float seconds;

    public bool running;

    public delegate void SecondsChanged(int seconds);
    public event SecondsChanged OnSecondsChanged;

    public delegate void TimeUp();
    public event TimeUp OnTimeUp;

    float secondsTracker;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
        this.seconds = seconds;
        secondsTracker = seconds;
        if (start) StartTimer();
    }

    void TimeExpired()
    {
        if (OnTimeUp != null) OnTimeUp();
        StopTimer();
    }
}
