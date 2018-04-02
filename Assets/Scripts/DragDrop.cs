using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragDrop : MonoBehaviour {

   [System.Serializable]
     class IconColor
     {
         public Color hoverColor;
         public Color originalColor; 

     }

    private Color c1;
    private Color c2;

    IconColor icon;
    private bool dragging = false;
    private float distanceDragged;

    void Start()
    {
        c1 = icon.hoverColor;
        c2 = icon.originalColor;
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = c1;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = c2;
    }

    void OnMouseDown()
    {
        distanceDragged = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distanceDragged);
        transform.position = rayPoint;
    }
}
