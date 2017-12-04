using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceReview : MonoBehaviour {


    public Transform resultsPanel;
    public GameObject workstationResults;

    public Text chanceOfPromotion, status;

    int promotionChance = 0;
    
	// Use this for initialization
	void Start () {
		
	}
	
    public void Review(Workstation[] workstations)
    {
        promotionChance = 0;

        for (int j = 0; j < resultsPanel.childCount; j++)
        {
            Destroy(resultsPanel.GetChild(j).gameObject);
        }

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
            chanceOfPromotion.text = "Chance of " + ((promotionChance > 0) ? "Promotion: " : "Getting Fired: ") + Mathf.Abs(promotionChance).ToString() + "%";
            chanceOfPromotion.color = ((promotionChance > 0) ? Color.green : Color.red);

            if (promotionChance > 0)
            {
                if (UnityEngine.Random.value < (Mathf.Abs(promotionChance) / 100f))
                    status.text = "You got the promtion! Congratulations!";
                else
                    status.text = "You're doing great work! Keep it up!";
            }
            else
            {
                if (UnityEngine.Random.value < (Mathf.Abs(promotionChance) / 100f))
                    status.text = "You missed too much work, your fired!";
                else
                    status.text = "You're really trying but you need to work harder!";

            }
        }
    }

    public void BackToWork()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);

        GameManager.Instance.RestartWork();
    }
}
