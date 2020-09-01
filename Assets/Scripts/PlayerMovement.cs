using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody playerRB;
    public float speed;
    Vector3 moveDirection;
    Animator playerAnim;
    int layerMask = 1 << 8;
    Vector3 mouseWorldPositon;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
        ControlRotation();
    }

    private void Move()
    {

        moveDirection = new Vector3(MertInput.horizontal, 0.0f, MertInput.vertical);
        //Sikre sig at diagonal movement ikke er hurtigere
        if (moveDirection.magnitude >= 1)
        {
            moveDirection = moveDirection.normalized;
        }
        //Sætter base speed
        moveDirection *= speed;
        //Move
        playerRB.MovePosition(playerRB.position + moveDirection * Time.deltaTime);
    }

    private void ControlRotation()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
        mouseWorldPositon = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
        transform.rotation = Quaternion.LookRotation(mouseWorldPositon, Vector3.up);
    }
}
