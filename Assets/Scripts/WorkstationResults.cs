using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkstationResults : MonoBehaviour {

    public Text title, tasksAssigned, successText, partialText, incompleteText;

    public void SetText(WorkType title, int tasks, int success, int partial, int incomplete)
    {
        this.title.text = title.ToString();
        tasksAssigned.text =    "Tasks Assigned: " + tasks.ToString();
        successText.text =      "Successfully Completed: " + success.ToString();
        partialText.text =      "Partially Completed: " + partial.ToString();
        incompleteText.text =   "Not Completed: " + incomplete.ToString();
    }

}
