using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : EntryObject {

    public int entryValue;

    //public delegate void EntrySubmitted(int val);
    //public event EntrySubmitted OnEntrySubmitted;

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
        //if (OnEntrySubmitted != null) OnEntrySubmitted(entryValue);
        FireEvent(entryValue);
        //debug
        rend.material.color = pressed;

    }

    // debug
    private void OnMouseUp()
    {
        rend.material.color = orig;
    }
}
