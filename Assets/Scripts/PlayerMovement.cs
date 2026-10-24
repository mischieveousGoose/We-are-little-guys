

using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sr;
    PlayerMerger merger;
    public float speed;
    Rigidbody2D rb;
    SFXPlayer sfxPlayer;

    AudioSource footsteps;

    public ParticleSystem dust;

    public float[] sizeJumpHeights;

    public LayerMask groundLayer;

    public Transform groundCheck;
    public Vector2 groundCheckSize;

    public float coyoteTime;
    float coyoteTimeCounter;

    public float jumpBuffer;
    float jumpBufferCounter;

    float jumpCooldownTimer;

    float lastDir = 0f;


    void Start()
    {
        footsteps = GetComponent<AudioSource>();
        sfxPlayer = FindFirstObjectByType<SFXPlayer>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        merger = GetComponent<PlayerMerger>();
    }

    public void HandleAnimations()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {

            sr.flipX = horizontal == -1;
            if (!footsteps.isPlaying && IsGrounded())
                footsteps.Play();

            if (horizontal != lastDir)
            {
                CreateDust();
            }
            lastDir = horizontal;
        }

        int state = 0;
        if (horizontal != 0) state = 1;
        animator.SetInteger("State", state);
    }

    void CreateDust()
    {
        dust.Play();
    }



    void Update()
    {
        float yVel = rb.linearVelocity.y;
        float xVel = Input.GetAxisRaw("Horizontal") * speed;
        bool grounded = IsGrounded();
        HandleAnimations();

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
            sfxPlayer.PlaySFX(1);
            CreateDust();
        }

        rb.linearVelocity = new Vector2(xVel, yVel);
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize * merger.size, 0f, groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize * merger.size);
    }
}
