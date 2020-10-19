using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGrab : MonoBehaviour
{
    public Transform origin;
    public LayerMask layerMask;

    public float holdDistance;
    public float grabDistance;

    private Grabbable itemGrabbed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemGrabbed) Release();
            else Reach();
        }
    }

    public void Reach ()
    {
        RaycastHit hit;

        if (Physics.Raycast(origin.position, origin.forward, out hit, grabDistance, layerMask))
        {
            Grabbable grabbable = hit.transform.GetComponent<Grabbable>();

            if (grabbable)
            {
                if (grabbable.enabled && grabbable.IsFree())
                {
                    Grab(grabbable);
                }
            }
        }
    }

    public void Grab (Grabbable gb)
    {
        gb.Grab(origin, holdDistance);
        gb.transform.SetParent(transform);
        itemGrabbed = gb;
    }

    public void Release ()
    {
        if (itemGrabbed)
        {
            itemGrabbed.Release();
            itemGrabbed = null;
        }
    }
}
