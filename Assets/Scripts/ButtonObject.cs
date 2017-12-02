using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour {

    public int buttonValue;

    public delegate void ButtonPressed(int val);
    public event ButtonPressed OnButtonPressed;

    // debug
    Color orig;
    Color pressed = Color.cyan;
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        orig = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (OnButtonPressed != null) OnButtonPressed(buttonValue);

        //debug
        rend.material.color = pressed;

    }

    // debug
    private void OnMouseUp()
    {
        rend.material.color = orig;
    }
}
