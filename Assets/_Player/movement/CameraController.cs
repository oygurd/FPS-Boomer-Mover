using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Adjust sensitivity as needed

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(-mouseY, mouseX, 0); // Rotate around Y-axis for horizontal view and X-axis for vertical view
    }
}
