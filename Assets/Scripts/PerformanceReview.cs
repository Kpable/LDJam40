using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceReview : MonoBehaviour {

    public Text tasksAssigned, successText, partialText, incompleteText;

    
	// Use this for initialization
	void Start () {
		
	}
	
    public void Review(Workstation[] workstations)
    {
        Task[] assignedTasks = workstations[0].outputTasks.ToArray();
        tasksAssigned.text += " " + assignedTasks.Length;

        int success = 0, partial = 0, incomplete = 0;

        for (int i = 0; i < assignedTasks.Length; i++)
        {
            switch (assignedTasks[i].status)
            {
                case TaskStatus.Skipped:
                    incomplete++;
                    break;
                case TaskStatus.Partial:
                    partial++;
                    break;
                case TaskStatus.Success:
                    success++;
                    break;
                default:
                    break;
            }
        }

        successText.text += " " + success;
        partialText.text += " " + partial;
        incompleteText.text += " " + incomplete;
    }

    public void BackToWork()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);

        GameManager.Instance.RestartWork();
    }
}
