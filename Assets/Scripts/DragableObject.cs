using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour {

    public int entryValue;
    public LayerMask layers;
    public float dragSpeed = 10f;
    // debug
    Color orig;
    Color pressed = Color.cyan;
    Renderer rend;

    Ray ray;
    RaycastHit hit;

    Vector3 position;
    public Vector3 originalPosition;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        orig = rend.material.color;
        originalPosition = transform.position;
    }

    private void OnMouseDown()
    {
        //debug
        rend.material.color = pressed;

    }

    // debug
    private void OnMouseUp()
    {
        rend.material.color = orig;
        transform.position = originalPosition;
    }

    private void OnMouseDrag()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 20, layers))
        {
            if (hit.collider.name == "Drag Zone")
            {
                position = hit.point;
            }
        }
        else position = originalPosition;
        position.x = originalPosition.x;
        //Debug.Log(position);
        transform.position = Vector3.Lerp(transform.position, position, dragSpeed * Time.deltaTime);
        
    }
}
