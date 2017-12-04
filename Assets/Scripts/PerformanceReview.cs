using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceReview : MonoBehaviour {


    public Transform resultsPanel;
    public GameObject workstationResults;

    public Text chanceOfPromotion;

    int promotionChance = 0;
    
	// Use this for initialization
	void Start () {
		
	}
	
    public void Review(Workstation[] workstations)
    {
        for (int i = 0; i < workstations.Length; i++)
        {
            Task[] assignedTasks = workstations[i].outputTasks.ToArray();

            GameObject workstationResultsPanel = Instantiate(workstationResults);
            workstationResultsPanel.transform.SetParent(resultsPanel, false);
            WorkstationResults wr = workstationResultsPanel.GetComponent<WorkstationResults>();

            int success = 0, partial = 0, incomplete = 0;

            for (int j = 0; j < assignedTasks.Length; j++)
            {
                switch (assignedTasks[j].status)
                {
                    case TaskStatus.Skipped:
                        incomplete++;
                        promotionChance-=3;
                        break;
                    case TaskStatus.Partial:
                        partial++;
                        promotionChance++;
                        break;
                    case TaskStatus.Success:
                        promotionChance += 2;
                        success++;
                        break;
                    default:
                        break;
                }
            }

            wr.SetText(workstations[i].type, assignedTasks.Length, success, partial, incomplete);
            
        }
    }

    public void BackToWork()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);

        GameManager.Instance.RestartWork();
    }
}
