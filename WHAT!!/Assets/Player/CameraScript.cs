using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float xSensitivity = 0.1f;
    public float ySensitivity = 0.1f;
    public GameObject camera;
    private float yRotation;
    private float xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * xSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y")* ySensitivity;
        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(0f, yRotation, 0);
        camera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
