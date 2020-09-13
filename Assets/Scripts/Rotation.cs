using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Rotation : MonoBehaviour
{
    // [SerializeField] Transform projectileSocket;
    [Header("Adjustable Values")]
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] float clampMin = -35f;
    [SerializeField] float clampMax = 60f;
    float mouseX, mouseY;
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        CamControl();
    }


    public void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, clampMin, clampMax);
        //  projectileSocket.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        transform.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
