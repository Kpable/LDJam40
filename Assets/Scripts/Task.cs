using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskStatus { Pending, Skipped, Partial, Success }

public class Task {

    public int[] solution;
    public int[] entry;
    // Tracks the entries that have been inputted
    public int entryIndex; 
    public TaskStatus status;
    public WorkType type;

    public Task(WorkType type, int[] entryOptions)
    {
        this.type = type;
        SetSolution(entryOptions);
        //Debug.Log("Task created: Type - " + type.ToString() + " Solution - " + solution.ArrayOutput());
    }

    void SetSolution(int[] entryOptions)
    {
        switch (type)
        {
            case WorkType.Programs:
                // Solution will be 4 random entries
                solution = new int[4];
                entry = new int[4];
                break;
            case WorkType.Circuits:
                // Solution will be 4 random entries
                solution = new int[4];   
                entry = new int[4];
                break;
            case WorkType.Manuals:
                // Solution will be 2 random entries
                solution = new int[2];
                entry = new int[2];
                break;
            default:
                break;
        }

        for (int i = 0; i < solution.Length; i++)
        {
            solution[i] = entryOptions[Random.Range(0, entryOptions.Length)];
        }
    }

    public void ValidateEntry()
    {
        bool correct = true;

        for (int i = 0; i < solution.Length; i++)
        {
            if (solution[i] != entry[i])
            {
                correct = false;
            }
        }

        status = ((correct) ? TaskStatus.Success : TaskStatus.Partial);

        //Debug.Log("Task Validated: Status: " + status.ToString());
    }
}
