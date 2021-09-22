using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;

    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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

        if (Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        
    }
}
