using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private PlayerMovement pm;

    private void Awake()
    {
        pm = GetComponentInParent<PlayerMovement>();
    }

    void OnTriggerEnter()
    {
        pm.grounded = true;
    }

    void OnTriggerStay()
    {
        pm.grounded = true;
    }

    void OnTriggerExit()
    {
        pm.grounded = false;
    }
}