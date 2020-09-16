using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float stepSoundDelay;
    [SerializeField] AudioClip[] footStep;
    Rigidbody playerRB;
    Vector3 moveDirection;
    Animator playerAnim;
    int layerMask = 1 << 8;
    Vector3 mouseWorldPositon;
    AudioSource source;
    bool isPlayStepSound = false;
    float runTimer = Mathf.Infinity;
    void Start()
    {
        source = GetComponent<AudioSource>();
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        HandleMovement();
        Rotate();

    }
    private void Update()
    {
        runTimer += Time.deltaTime;
        if (playerAnim.GetBool("isRunning") && runTimer >= stepSoundDelay)
        {
            runTimer = 0;
            source.PlayOneShot(footStep[Random.Range(0, footStep.Length)]);

        }
    }
    private void HandleMovement()
    {
        //Movement
        moveDirection = new Vector3(MertInput.horizontal, 0.0f, MertInput.vertical);
        //Animation
        float angle = transform.eulerAngles.y * Mathf.Deg2Rad;

        float X = moveDirection.x * Mathf.Cos(angle) - moveDirection.z * Mathf.Sin(angle);
        float Y = moveDirection.x * Mathf.Sin(angle) + moveDirection.z * Mathf.Cos(angle);

        playerAnim.SetFloat("horizontalSpeed", X);
        playerAnim.SetFloat("forwardSpeed", Y);

        //Sikre sig at diagonal movement ikke er hurtigere
        if (moveDirection.magnitude >= 1)
        {
            moveDirection = moveDirection.normalized;
            playerAnim.SetBool("isRunning", true);

        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }

        //Sætter base speed
        moveDirection *= speed;
        //Move
        playerRB.MovePosition(playerRB.position + moveDirection * Time.deltaTime);
    }

    private void Rotate()
    {
        //Rotation
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
        mouseWorldPositon = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
        transform.rotation = Quaternion.LookRotation(mouseWorldPositon, Vector3.up);

    }
}
