using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WorkType { Programs, Circuits, Manuals }

public class Workstation : MonoBehaviour {

    public WorkType type;
    public Material[] entryMaterials;
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
    }

	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator AddTask()
    {
        inputTasks.Push(new Task(type, entryOptions));
        SetCurrentTask(inputTasks.Pop());
        yield return new WaitForSeconds(Random.Range(3, 5));
        StartCoroutine("AddTask");
    }

    void SetCurrentTask(Task task)
    {
        currentTask = task;
        UpdateWorkstation();
    }

    void UpdateWorkstation()
    {
        for (int i = 0; i < currentTask.solution.Length; i++)
        {
            solutionPlanes[i].material = entryMaterials[currentTask.solution[i]]; 
        }
    }
}
