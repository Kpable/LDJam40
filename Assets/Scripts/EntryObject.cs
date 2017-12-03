using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryObject : MonoBehaviour {

    public delegate void EntrySubmitted(int slot, int val);
    public  event EntrySubmitted OnEntrySubmitted;

    protected void FireEvent(int val)
    {
        if (OnEntrySubmitted != null) OnEntrySubmitted( -1, val);
    }

    protected void FireEvent(int slot, int val)
    {
        if (OnEntrySubmitted != null) OnEntrySubmitted(slot, val);
    }
}
