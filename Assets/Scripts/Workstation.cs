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
    public GameObject taskObject;


    public Queue<Task> inputTasks = new Queue<Task>();
    public Queue<Task> outputTasks = new Queue<Task>();

    public delegate void QueueEnqueue(StackType val);
    public event QueueEnqueue OnQueueEnqueue;
    public delegate void QueueDequeue(StackType val);
    public event QueueDequeue OnQueueDequeue;


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

    private void Update()
    {
        Task[] tasksArray = inputTasks.ToArray();
        
        for (int i = 0; i < tasksArray.Length; i++)
        {
            tasksArray[i].timer.Update();
        }
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
                outputTasks.Enqueue(currentTask);
                if(OnQueueEnqueue != null) OnQueueEnqueue(StackType.Output);

                // load new task
                if (inputTasks.Count > 0)
                {
                    StartTask(inputTasks.Dequeue());
                    if (OnQueueDequeue != null) OnQueueDequeue(StackType.Input);
                }
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
        Task newTask = new Task(type, entryOptions);
        newTask.OnTaskTimedOut += TaskTimedOut;
        newTask.timer.SetTimer(5, true);
        inputTasks.Enqueue(newTask);
        //inputTasks.Enqueue(new Task(type, entryOptions));
        //inputTasks.Peek().OnTaskTimedOut += TaskTimedOut;
        //inputTasks.Peek().timer.SetTimer(5, true);

        if (OnQueueEnqueue != null) OnQueueEnqueue(StackType.Input);

        if (currentTask == null)
        {
            StartTask(inputTasks.Dequeue());
            if (OnQueueDequeue != null) OnQueueDequeue(StackType.Input);
        }

        yield return new WaitForSeconds(Random.Range(3, 3));
        StartCoroutine("AddTask");
    }

    void StartTask(Task task)
    {
        currentTask = task;
        task.timer.StopTimer();
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

    // Todo This gets called twice for some reason
    // Todo Timers are not being updated independantly.
    void TaskTimedOut(Task task)
    {
        
        Debug.Log("Failing task");
        Task failedTask = inputTasks.Dequeue();
        if (OnQueueDequeue != null) OnQueueDequeue(StackType.Input);

        failedTask.status = TaskStatus.Skipped;

        outputTasks.Enqueue(failedTask);
        if (OnQueueEnqueue != null) OnQueueEnqueue(StackType.Output);
        

    }
}
