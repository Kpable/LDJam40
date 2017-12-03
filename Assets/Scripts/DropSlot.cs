using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSlot : EntryObject {

    //public delegate void EntrySubmitted(int val);
    //public event EntrySubmitted OnEntrySubmitted;
    public int entrySlot;
    public Workstation workstation;

    Color orig;
    Color pressed = Color.cyan;
    Renderer rend;
    DragableObject draggedObject;

    GameObject objectInSlot;

    

    bool slotFilled;

    void Start () {
        rend = GetComponent<Renderer>();
        orig = rend.material.color;

        workstation.OnTaskComplete += ClearSlot;
    }
	
	void Update () {
        if (Input.GetMouseButtonUp(0) && draggedObject && !slotFilled)
        {
            draggedObject.transform.position = transform.position;
            objectInSlot = draggedObject.gameObject;

            Instantiate(objectInSlot,
                draggedObject.originalPosition,
                draggedObject.transform.rotation,
                draggedObject.transform.parent);


            slotFilled = true;

            draggedObject.enabled = false;

            FireEvent(entrySlot, draggedObject.entryValue);

            

        }
    }

    private void OnMouseEnter()
    {
        rend.material.color = pressed;

    }

    private void OnMouseExit()
    {
        rend.material.color = orig;

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger Entered");
        draggedObject = other.GetComponent<DragableObject>();
    }

    private void OnTriggerExit(Collider other)
    {
        draggedObject = null;
    }

    public void ClearSlot()
    {
        Destroy(objectInSlot);
        slotFilled = false;
        draggedObject = null;
    }
}
