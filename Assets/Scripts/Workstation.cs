using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WorkType { Programs, Circuits, Manuals }

public class Workstation : MonoBehaviour {

    public WorkType type;
    public Material[] entryMaterials;
    public Material blankMaterial;
    public Renderer[] solutionPlanes;
    public EntryObject[] entryButtonObjects;
    public GameObject taskObject;


    public Queue<Task> inputTasks = new Queue<Task>();
    public Queue<Task> outputTasks = new Queue<Task>();

    public delegate void QueueEnqueue(StackType val);
    public event QueueEnqueue OnQueueEnqueue;
    public delegate void QueueDequeue(StackType val);
    public event QueueDequeue OnQueueDequeue;

    public delegate void TaskComplete();
    public event TaskComplete OnTaskComplete;


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
            entryButtonObjects[j].OnEntrySubmitted += EntryButtonPressed;
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

    void EntryButtonPressed(int slotValue, int entryValue)
    {
        //Debug.Log("Button pressed: " + buttonValue);
        if(currentTask != null)
        {
            // Input Entry
            if(slotValue < 0)
                currentTask.entry[currentTask.entryIndex] = entryValue;
            else
                currentTask.entry[slotValue] = entryValue;

            //entry entered event

            // Increment index
            // We still want to do this if the slot is not provided
            currentTask.entryIndex++;
            // Check if we have entered all entries
            if(currentTask.entryIndex == currentTask.solution.Length)
            {
                // Task is done
                currentTask.ValidateEntry();
                if (OnTaskComplete != null) OnTaskComplete();

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

        if (OnQueueEnqueue != null) OnQueueEnqueue(StackType.Input);

        if (currentTask == null)
        {
            StartTask(inputTasks.Dequeue());
            if (OnQueueDequeue != null) OnQueueDequeue(StackType.Input);
        }

        yield return new WaitForSeconds(Random.Range(2, 5));
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

    void TaskTimedOut(Task task)
    {
        
        Debug.Log("Failing task");
        Task failedTask = inputTasks.Dequeue();
        if (OnQueueDequeue != null) OnQueueDequeue(StackType.Input);

        failedTask.status = TaskStatus.Skipped;

        outputTasks.Enqueue(failedTask);
        if (OnQueueEnqueue != null) OnQueueEnqueue(StackType.Output);
    }

    public void ClearWorkstation()
    {
        for (int i = 0; i < inputTasks.Count; i++)
        {
            if (OnQueueDequeue != null) OnQueueDequeue(StackType.Input);
        }

        for (int j = 0; j < outputTasks.Count; j++)
        {
            if (OnQueueDequeue != null) OnQueueDequeue(StackType.Output);
        }

        inputTasks.Clear();
        outputTasks.Clear();
        currentTask = null;
        UpdateWorkstation();
    }
}
