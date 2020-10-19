using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    private bool grabbed;
    private Rigidbody rb;
    private Transform point;
    private float distance;

    public bool IsFree() => !grabbed;


    private void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (grabbed && point)
        {
            Vector3 targetPos = point.position + point.forward * distance;
            Vector3 dir = targetPos - transform.position;
            rb.velocity = dir * Time.fixedDeltaTime * 600;
        }
    }

    public void Grab (Transform t, float dist)
    {
        grabbed = true;

        point = t;
        distance = dist;

        rb.useGravity = false;
        //rb.freezeRotation = true;
    }

    public void Release ()
    {
        grabbed = false;

        point = null;
        distance = 0f;

        rb.useGravity = true;
        //rb.freezeRotation = false;

        transform.SetParent(null);
    }
}
