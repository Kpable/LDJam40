using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kpable.Utilities;
using Kpable.Mechanics;
using System; 

public class GameManager : SingletonBehaviour<GameManager> {

    public WorkDay[] days;

    public WorkDesk workDesk;

    // Use this for initialization
    void Start () {
        StartDay();       
    }

    // Update is called once per frame
    void Update () {
	}

    public void StartDay()
    {
        Debug.Log("Starting " + days[0]);
        workDesk.LoadDay(days[0]);

    }   
}

public enum ModuleType { Programming, LogicArray }
public enum CartridgeType { Floppy }