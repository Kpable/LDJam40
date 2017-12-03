using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform left, forward, right;


    public void TurnRight()
    {
        if (transform.position == left.position && 
            transform.rotation == left.rotation)
        {
            transform.position = forward.position;
            transform.rotation = forward.rotation;
        }
        else if(transform.position == forward.position && 
            transform.rotation == forward.rotation)
        {
            transform.position = right.position;
            transform.rotation = right.rotation;
        }
    }

    public void TurnLeft()
    {
        if (transform.position == right.position && 
            transform.rotation == right.rotation)
        {
            transform.position = forward.position;
            transform.rotation = forward.rotation;
        }
        else if (transform.position == forward.position && 
            transform.rotation == forward.rotation )
        {
            transform.position = left.position;
            transform.rotation = left.rotation;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) TurnLeft();
        if (Input.GetKeyDown(KeyCode.D)) TurnRight();
    }
}
