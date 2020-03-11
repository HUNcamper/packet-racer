using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Turning camera with mouse
    public float sensitivityH = 2.0f;
    public float sensitivityV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private bool freeRotationMode = false;

    // Movement
    public float cameraSpeed = 5.0f;
    public float sprintMultiplier = 2.0f;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TOGGLE FREE ROTATION MODE //
        if (Input.GetKeyDown(KeyCode.Z))
        {
            freeRotationMode = freeRotationMode ? false : true;
        }

        // ROTATION //
        if (Input.GetMouseButton(1) || freeRotationMode)
        {
            yaw += sensitivityH * Input.GetAxis("Mouse X");
            pitch -= sensitivityV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            Screen.lockCursor = true;
        }
        else Screen.lockCursor = false;

        // MOVEMENT //
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        movement = transform.forward * moveVertical + transform.right * moveHorizontal;
        movement *= cameraSpeed * Time.deltaTime;

        // If we're holding down shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= sprintMultiplier;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            movement *= 1.0f / sprintMultiplier;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            movement.y = 0.2f;
        }

        transform.position += movement;
    }
}
