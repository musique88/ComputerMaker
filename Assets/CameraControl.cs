using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CameraControl : MonoBehaviour
{
    public GameObject plate;
    public bool grabbed;
    
    private Transform plateTransform;
    private MouseManager mouseManager;
    private Camera camera;
    private Vector3 pastMousePosition;

    void Awake()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        plateTransform = plate.transform;
        mouseManager = gameManager.GetComponent<MouseManager>();
        grabbed = false; 
        camera = Camera.main;
    }

    private void Update()
    {
        grabbed = Input.GetMouseButton((int) MouseButton.RightMouse);
        Vector3 currentMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        if (grabbed && !mouseManager.MouseHasGrabbedSomething)
        {
            Vector3 deltaMousePosition = currentMousePosition - pastMousePosition;
            plateTransform.Translate(deltaMousePosition);
        }
        
        if (camera.orthographicSize >= 1f || Input.mouseScrollDelta.y < 0)
            camera.orthographicSize += - Input.mouseScrollDelta.y;
        pastMousePosition = currentMousePosition;
    }
    
}
