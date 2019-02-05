using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kpable.Utilities;
using Kpable.Mechanics;
using System; 

public class GameManager : SingletonBehaviour<GameManager> {

    public ObservedStringVariable timeText;

    //public Workstation[] workstations;
    public Timer timer;
    TimeSpan time;
    //public ClockLabel clockLabel;

    //public PerformanceReview performanceReview;

    public int dayCycle = 48; // Seconds
    public WorkDay[] days;

    private bool gameIsPaused = false;

    public WorkDesk workDesk;

    // Use this for initialization
    void Start () {
        timer = new Timer();
        timer.OnTimeUp += HandleTimeUp;
        timer.OnSecondsChanged += HandleSecondsChanged;
        timer.Set(dayCycle, true);
        time = new TimeSpan(8, 0, 0);
        //timeText.Value = string.Format("{0:D2}:{1:D2}", ((time.Hours > 12) ? time.Hours - 12 : time.Hours), time.Minutes);


        StartDay();

       
    }

    // Update is called once per frame
    void Update () {
        timer.Update();
	}

    public void StartDay()
    {
        Debug.Log("Starting " + days[0]);
        workDesk.LoadDay(days[0]);

    }

    void HandleTimeUp()
    {
        // Open perfomance Review
        Debug.Log("Time is up");
        //Time.timeScale = 0;
        //performanceReview.gameObject.SetActive(true);
        //performanceReview.Review(workstations);
        StartCoroutine("BlinkText");


    }

    void HandleSecondsChanged(int seconds)
    {
        //time = new TimeSpan(seconds * 10000000);
        time = time.Add(new TimeSpan(0, 5, 0));
        //Debug.Log("time set to: " + string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds));
        timeText.Value = string.Format("{0:D2}:{1:D2}", ((time.Hours > 12) ? time.Hours - 12 : time.Hours), time.Minutes);
    }

    //public void RestartWork()
    //{
    //    for (int i = 0; i < workstations.Length; i++)
    //    {
    //        workstations[i].ClearWorkstation();
    //    }

    //    clockLabel.SetTimeRemaining(reviewCycle);
    //    timer.SetTimer(reviewCycle, true);

    //}

    public void TogglePause()
    {
        if(gameIsPaused)
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

    public IEnumerator BlinkText()
    {
        timeText.Value = "";
        yield return new WaitForSeconds(0.5f);
        timeText.Value = string.Format("{0:D2}:{1:D2}", ((time.Hours > 12) ? time.Hours - 12 : time.Hours), time.Minutes);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("BlinkText");
    }

   
}

public enum ModuleType { Programming, LogicArray }
public enum CartridgeType { Floppy }