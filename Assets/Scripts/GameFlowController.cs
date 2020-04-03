using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    [SerializeField]
    private InGameClock inGameClock;

    private void Awake()
    {
        if(!inGameClock)
        {
            Debug.LogError(name + ": InGameClock is not linked");
            return;
        }

        inGameClock.RegisterTimedCallback(new TimeSpan(0, 8, 0, 0), Morning);
        inGameClock.RegisterTimedCallback(new TimeSpan(0, 12, 0, 0), Lunch);
        inGameClock.RegisterTimedCallback(new TimeSpan(0, 13, 0, 0), Afternoon);
        inGameClock.RegisterTimedCallback(new TimeSpan(0, 17, 0, 0), PostWork);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PreWork()
    {
        Debug.Log("Its pre work time!");

    }

    private void Morning()
    {
        Debug.Log("Its morning time!");

    }

    private void Lunch()
    {
        Debug.Log("Its lunch time!");
    }

    private void Afternoon()
    {
        Debug.Log("Its noon time!");

    }

    private void PostWork()
    {
        Debug.Log("Its after work time!");

    }


}
