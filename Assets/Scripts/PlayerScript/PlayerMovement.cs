using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] ContactFilter2D groundFilter;
    [SerializeField] float horizontalInput;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpPower = 4f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallSlidingSpeed = 2f;
    [SerializeField] private float doubleJumpingPower = 12f;


    private bool doubleJump;
    private bool isWallSliding;
    bool isGrounded;
    bool isFacingRight = false;
    bool isjumping = false;

    Rigidbody2D rb;

    void Start()

    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");



        if (IsGrounded() && !Input.GetButton("Jump"))

        {
            doubleJump = false;
        }


        if (Input.GetButtonDown("Jump") && !isjumping)
        {

            if (IsGrounded() || doubleJump)

                FlipSprite();
            {
                rb.velocity = new Vector2(rb.velocity.x, doubleJump ? doubleJumpingPower : jumpPower);

                doubleJump = !doubleJump;
            }

        }



        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f)

        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        WallSlide();
    }

    private void FixedUpdate()
    {
        if (!isWallSliding)
        {
            isGrounded = rb.IsTouching(groundFilter);
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }

        isGrounded = IsGrounded();
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private bool IsGrounded()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        return Physics2D.OverlapCircle(position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        Vector2 position = transform.position;
        return Physics2D.OverlapCircle(position, 0.2f, wallLayer);
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && IsGrounded();
    }

    private void WallSlide()
    {
        if (IsWalled() && !isGrounded && horizontalInput != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
        }
        else
        {
            isWallSliding = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        isjumping = false;
    }
}