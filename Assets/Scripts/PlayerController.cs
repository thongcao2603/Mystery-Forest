using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    private float gravity = -9.81f * 2;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private Transform groundCheck;
    private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool canDoubleJump;

    Rigidbody2D rigi;
    Animator anim;

    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        Movement();
    }

    void Movement()
    {
        if (velocity.y < 0 && isGrounded)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");

        if (x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        velocity.x = x * speed;

        if (isGrounded)
        {
            canDoubleJump = true;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if (canDoubleJump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                canDoubleJump = false;
            }
        }
        velocity.y += gravity * Time.deltaTime;
        anim.SetFloat("moveSpeed", Mathf.Abs(velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        rigi.velocity = velocity;

    }

    void IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundMask);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }
}
