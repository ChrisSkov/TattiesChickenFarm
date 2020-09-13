using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    [SerializeField] Transform target = null;
    [SerializeField] float smoothing = 5f;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        transform.LookAt(target);
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
