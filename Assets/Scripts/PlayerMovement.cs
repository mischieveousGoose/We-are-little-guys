

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerMerger merger;
    public float speed;
    Rigidbody2D rb;

    public float[] sizeJumpHeights;

    public LayerMask groundLayer;

    public Transform groundCheck;
    public Vector2 groundCheckSize;

    public float coyoteTime;
    float coyoteTimeCounter;

    public float jumpBuffer;
    float jumpBufferCounter;

    float jumpCooldownTimer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        merger = GetComponent<PlayerMerger>();
    }



    void Update()
    {

        float yVel = rb.linearVelocity.y;
        float xVel = Input.GetAxisRaw("Horizontal") * speed;
        bool grounded = IsGrounded();

        jumpCooldownTimer -= Time.deltaTime;

        if (grounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBuffer;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && jumpCooldownTimer < 0f)
        {
            jumpCooldownTimer = jumpBuffer + 0.1f;
            coyoteTimeCounter = -1;
            jumpBufferCounter = -1;
            yVel = sizeJumpHeights[merger.size - 1];
        }

        rb.linearVelocity = new Vector2(xVel, yVel);
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
    }
}
