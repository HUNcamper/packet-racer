using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private float scrollSinceDrag = 0.0f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseWorldPos();

        scrollSinceDrag = 0.0f;
    }

    void OnMouseDrag()
    {
        scrollSinceDrag += Input.GetAxis("Mouse ScrollWheel");

        // Get camera's angle
        Vector3 cameraDirection = mainCamera.transform.forward;

        transform.position = GetMouseWorldPos() + mOffset;
        transform.position += cameraDirection * scrollSinceDrag;
    }

    Vector3 GetMouseWorldPos()
    {
        // pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
