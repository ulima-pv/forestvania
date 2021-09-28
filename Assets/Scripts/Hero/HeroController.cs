using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float extraSpace;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    private Animator animator;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private float colliderHeight;
    private bool isJumping = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        colliderHeight = capsuleCollider.bounds.extents.y;
    }

    private void FixedUpdate()
    {
        isJumping = IsJumping();

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.fixedDeltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
        if (isJumping)
        {
            animator.SetBool("isJumping", true);
        }else
        {
            animator.SetBool("isJumping", false);
        }

        float movement = Input.GetAxisRaw("Horizontal");

        if (movement != 0f)
        {
            // Cambiar la animacion a Run
            animator.SetBool("isRunning", true);
            // Validar direccion de movimiento
            if (movement < 0)
            {
                if (transform.localScale.x != -1f)
                {
                    transform.localScale = new Vector3(
                        -1f,
                        transform.localScale.y,
                        transform.localScale.z
                    );
                }
            }
            else
            {
                if (transform.localScale.x != 1f)
                {
                    transform.localScale = new Vector3(
                        1f,
                        transform.localScale.y,
                        transform.localScale.z
                    );
                }
            }
        }else
        {
            // Cambiar la animacion a Idle
            animator.SetBool("isRunning", false);
        }

        transform.position += Vector3.right * movement * speed * Time.deltaTime;

        if (Input.GetButton("Jump") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    private bool IsJumping()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            capsuleCollider.bounds.center,
            Vector2.down,
            colliderHeight + extraSpace
        );
        Color rayColor;
        if (hit)
        {
            //Debug.Log("Esta en el suelo");
            rayColor = Color.white;
        }
        else
        {
            //Debug.Log("Esta saltando");
            rayColor = Color.yellow;
        }
        Debug.DrawRay(
            capsuleCollider.bounds.center,
            Vector2.down * (colliderHeight + extraSpace),
            rayColor
        );
        return !hit;
    }

}
