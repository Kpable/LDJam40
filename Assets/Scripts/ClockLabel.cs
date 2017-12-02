using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockLabel : MonoBehaviour {

    Text label;
    string origText;
	// Use this for initialization
	void Awake () {
        label = GetComponent<Text>();
        origText = label.text;
    }

    public void SetTimeRemaining(int time)
    {
        label.text = origText + " " + time.ToString();
    }
	

}
