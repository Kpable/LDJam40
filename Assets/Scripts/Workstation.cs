﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WorkType { Programs, Circuits, Manuals }

public class Workstation : MonoBehaviour {

    public WorkType type;
    public Material[] entryMaterials;
    public Material blankMaterial;
    public Renderer[] solutionPlanes;
    public ButtonObject[] entryButtonObjects;


    public Stack<Task> inputTasks = new Stack<Task>();
    public Stack<Task> outputTasks = new Stack<Task>();

    Task currentTask;
    int[] entryOptions;

    // Use this for initialization
    void Start () {
        entryOptions = new int[entryMaterials.Length];

        for (int i = 0; i < entryOptions.Length; i++)
        {
            entryOptions[i] = i;
        }

        for (int j = 0; j < entryButtonObjects.Length; j++)
        {
            entryButtonObjects[j].OnButtonPressed += EntryButtonPressed;
        }

        StartCoroutine("AddTask");
    }
	
    void EntryButtonPressed(int buttonValue)
    {
        Debug.Log("Button pressed: " + buttonValue);
        if(currentTask != null)
        {
            // Input Entry
            currentTask.entry[currentTask.entryIndex] = buttonValue;

            //entry entered event
            
            // Increment index
            currentTask.entryIndex++;
            // Check if we have entered all entries
            if(currentTask.entryIndex == currentTask.solution.Length)
            {
                // Task is done
                currentTask.ValidateEntry();

                // move to output
                outputTasks.Push(currentTask);

                // load new task
                if (inputTasks.Count > 0)
                    StartTask(inputTasks.Pop());
                else
                {
                    currentTask = null;
                    UpdateWorkstation();
                }
            }
        }
    }

    IEnumerator AddTask()
    {
        inputTasks.Push(new Task(type, entryOptions));

        if(currentTask == null)
            StartTask(inputTasks.Pop());


        yield return new WaitForSeconds(Random.Range(3, 5));
        StartCoroutine("AddTask");
    }

    void StartTask(Task task)
    {
        currentTask = task;
        UpdateWorkstation();
    }

    void UpdateWorkstation()
    {
        if (currentTask != null)
        {
            for (int i = 0; i < currentTask.solution.Length; i++)
            {
                solutionPlanes[i].material = entryMaterials[currentTask.solution[i]];
            }
        }
        else
        {
            for (int i = 0; i < solutionPlanes.Length; i++)
            {
                solutionPlanes[i].material = blankMaterial;
            }

        }

    }
}
