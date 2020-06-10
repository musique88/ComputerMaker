using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DraggableComponent : MonoBehaviour
{
    private GameObject gameManager;
    private MouseManager mouseManager;
    public GameObject squarePreviewPrefab;
    private GameObject squarePreview;
    
    public bool grabbed;
    private Camera camera;
    private Vector3 pastMousePosition;
    private Vector3 colliderSize;
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        mouseManager = gameManager.GetComponent<MouseManager>();
        grabbed = false; 
        camera = Camera.main;
        colliderSize = GetComponent<BoxCollider2D>().bounds.size;
    }
    void Update()
    {
        Vector3 currentMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        if (grabbed)
        {
            Vector3 deltaMousePosition = currentMousePosition - pastMousePosition;
            transform.Translate(deltaMousePosition);
            var localPosition = transform.localPosition;
            squarePreview.transform.localPosition = new Vector3(
                Mathf.Ceil(localPosition.x) - colliderSize.x / 2 % 1f,
                Mathf.Ceil(localPosition.y) - colliderSize.y / 2 % 1f);
        }
        pastMousePosition = currentMousePosition;
    }

    private void OnMouseDown()
    {
        mouseManager.MouseHasGrabbedSomething = true;
        grabbed = true;
        squarePreview = Instantiate(squarePreviewPrefab, transform.parent);
    }

    private void OnMouseUp()
    {
        mouseManager.MouseHasGrabbedSomething = false;
        grabbed = false;
        var localPosition = transform.localPosition;
        localPosition = new Vector3(
            Mathf.Ceil(localPosition.x) - colliderSize.x / 2 % 1f,
            Mathf.Ceil(localPosition.y) - colliderSize.y / 2 % 1f);
        transform.localPosition = localPosition;
        Destroy(squarePreview);
    }
}
