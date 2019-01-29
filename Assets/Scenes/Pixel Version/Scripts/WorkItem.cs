using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorkItem
{
    public Problem[] problems;
}

[System.Serializable]
public class Problem
{
    public Module module;
    public int answers;
    public int solutions;

}
