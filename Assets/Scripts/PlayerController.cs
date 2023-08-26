using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    [SerializeField] private float jumpForce = 17f;
    [SerializeField] private float moveSpeed = 7.5f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSound;

    private float dirX;
    private bool isGrounded;
    private bool canJump = false;
    private bool canDoubleJump = false;

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        if (rb.bodyType != RigidbodyType2D.Static)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }

        if (dirX < 0)
        {
            sprite.flipX = true;
        }
        else if (dirX > 0)
        {
            sprite.flipX = false;
        }

        animator.SetFloat("velocityX", Mathf.Abs(dirX));

        if (canJump && Input.GetButtonDown("Jump"))
        {
            jumpSound.Play();
            Jump(jumpForce);
            canJump = false;
            canDoubleJump = true;
        }
        else if (!canJump && canDoubleJump && Input.GetButtonDown("Jump"))
        {
            jumpSound.Play();
            Jump(jumpForce);
            canDoubleJump = false;
            animator.SetTrigger("doubleJump");  // Trigger double jump animation
        }

        animator.SetFloat("velocityY", rb.velocity.y);
        isGrounded = PlayerGrounded();
        if(isGrounded) canJump = true;
        animator.SetBool("grounded", isGrounded);
    }
    public void Jump(float jmpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, jmpForce);
    }

    private bool PlayerGrounded()
    {
        return (-0.1f < rb.velocity.y && rb.velocity.y < 0.1f) && !Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up, .1f, jumpableGround);
    }
}
