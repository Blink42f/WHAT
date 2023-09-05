using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float xSensitivity = 400f;
    public float ySensitivity = 400f;
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
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;
        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        camera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
