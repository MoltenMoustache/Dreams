using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Triggerable : MonoBehaviour
{
    public LayerMask layers;
    public UnityEvent onTriggerEnter = new UnityEvent();
    public UnityEvent onTriggerExit = new UnityEvent();

    private List<GameObject> objectInside = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb)
        {
            GameObject obj = rb.gameObject;

            if ((layers & (1 << obj.layer)) == layers)
            {
                if (!objectInside.Contains(obj))
                {
                    objectInside.Add(obj);
                    onTriggerEnter.Invoke();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb)
        {
            GameObject obj = rb.gameObject;

            if ((layers & (1 << obj.layer)) == layers)
            {
                if (objectInside.Contains(obj))
                {
                    objectInside.Remove(obj);
                    onTriggerExit.Invoke();
                }
            }
        }
    }
}
