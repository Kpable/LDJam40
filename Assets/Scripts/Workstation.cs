using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WorkType { Programs, Circuits, Manuals }

public class Workstation : MonoBehaviour {

    public WorkType type;
    public Sprite[] entries;
    public List<Task> inputTasks = new List<Task>();
    public List<Task> outputTasks = new List<Task>();

    Task currentTask;
    int[] entryOptions;

    // Use this for initialization
    void Start () {
        entryOptions = new int[entries.Length];

        for (int i = 0; i < entryOptions.Length; i++)
        {
            entryOptions[i] = i;
        }

        StartCoroutine("AddTask");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator AddTask()
    {
        inputTasks.Add(new Task(type, entryOptions));
        yield return new WaitForSeconds(Random.Range(3, 5));
        StartCoroutine("AddTask");
    }
}
