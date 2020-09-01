using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Player movement variables")]
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float dashForce = 2f;
    [SerializeField] float timeBetweenDashes = 2f;
    [Header("Game feel")]
    [SerializeField] float rayOffSet = 0.5f;
    [Tooltip("How close to the ground do we have to be to jump")]
    [SerializeField] float groundCheckDistance = 5f;
    Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    Animator anim;

    float dashTimer = Mathf.Infinity;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        SideDash();
    }

    private void Update()
    {
        dashTimer += Time.deltaTime;
    }

    void Move()
    {
        if (IsGrounded())
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), rb.position.y, Input.GetAxis("Vertical"));
            if (moveDirection.magnitude > 1)
            {
                moveDirection = moveDirection.normalized;
            }
            moveDirection *= speed;
            moveDirection = transform.TransformDirection(moveDirection);
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);

    }

    void SideDash()
    {
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && CanDash())
        {
            dashTimer = 0f;
            rb.AddForce(transform.TransformDirection(Vector3.left) * dashForce, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && CanDash())
        {
            dashTimer = 0f;
            rb.AddForce(transform.TransformDirection(Vector3.right) * dashForce, ForceMode.Impulse);
        }
    }
    bool CanDash()
    {
        return dashTimer >= timeBetweenDashes;
    }
    bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position + new Vector3(0, rayOffSet, 0), Vector3.down, out hit, groundCheckDistance);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position + new Vector3(0, rayOffSet, 0), Vector3.down * groundCheckDistance);
    }
}
