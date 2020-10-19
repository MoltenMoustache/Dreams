using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    [Header("Values")]
    public float speed = 6f;
    public float jumpPower = 8f;
    public float groundDrag = 0.8f;

    [Header("Info (Read only)")]
    public bool grounded;
    [SerializeField]
    private bool walking;
    [SerializeField]
    private float drag;

    private float startSpeed;
    private Vector3 rbMove;
    private Rigidbody rb;
    private GroundCheck gcheck;

    void Start()
    {
        // References
        rb = GetComponent<Rigidbody>();
        gcheck = GetComponentInChildren<GroundCheck>();

        startSpeed = speed;
    }

    void Update()
    {
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump(jumpPower);
        }

        // Test if we are walking or not
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            walking = true;
        else
            walking = false;

        // Come to a stop, and drag physics
        if (grounded || walking) {
            drag = groundDrag;
        } else {
            drag = 1;
        }
    }

    void FixedUpdate()
    {
        // Drag
        rb.velocity = new Vector3(rb.velocity.x * drag, rb.velocity.y, rb.velocity.z * drag);

        // Movement
        rb.AddForce(transform.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime * speed, ForceMode.VelocityChange);
        rb.AddForce(transform.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, ForceMode.VelocityChange);
    }

    public void Jump(float power)
    {
        rb.velocity = new Vector3(rb.velocity.x, power, rb.velocity.z);
    }
}