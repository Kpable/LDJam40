using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StackType { Input, Output }

public class ObjectStack : MonoBehaviour {

    public Workstation workstation;
    public StackType type;

    int spawnPosition = 3;

    Queue<GameObject> gameObjects = new Queue<GameObject>();
	
    // Use this for initialization
	void Start () {
        switch (type)
        {
            case StackType.Input:
                break;
            case StackType.Output:
                break;
            default:
                break;
        }

        workstation.OnQueueEnqueue += AddToStack;
        workstation.OnQueueDequeue += RemoveFromStack;
    }

    void AddToStack(StackType type)
    {
        if(type == this.type)
        {
            spawnPosition++;
            Vector3 spawnPoint = Vector3.zero;
            spawnPoint.y += spawnPosition;
            GameObject taskObject = Instantiate(workstation.taskObject);
            taskObject.transform.SetParent(transform);
            taskObject.transform.localPosition = spawnPoint;
            gameObjects.Enqueue(taskObject);
        }
    }

    void RemoveFromStack(StackType type)
    {
        if (type == this.type)
        {
            spawnPosition--;
            Destroy(gameObjects.Dequeue());

        }
    }
}
