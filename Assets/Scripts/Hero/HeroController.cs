using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForestVania.Hero.States;

namespace ForestVania.Hero
{
    public enum BuildType
    {
        Mobile, Desktop
    }

    public class HeroController : MonoBehaviour
    {
        public BuildType buildType;
        public float speed;
        public float jumpSpeed;
        public float extraSpace;
        public float fallMultiplier;
        public float lowJumpMultiplier;
        public GameObject bulletPrefab;
        public GhostController ghost;
        public Joystick joystick;

        // FSM
        private HeroStateMachine fsm;
        private HeroIdleState heroIdleState;
        private HeroJumpingState heroJumpingState;
        private HeroRunningState heroRunningState;
        private HeroShootingState heroShootingState;


        private Animator animator;
        private Rigidbody2D rb;
        private CapsuleCollider2D capsuleCollider;
        private float colliderHeight;
        private bool isJumping = false;
        private bool isAlive = true;
        private Vector3 initialPosition;
        private Transform firePoint;

        private void Awake()
        {
            firePoint = transform.Find("FirePoint");
            fsm = new HeroStateMachine();
            heroIdleState = new HeroIdleState(this, fsm);
            heroJumpingState = new HeroJumpingState(this, fsm);
            heroRunningState = new HeroRunningState(this, fsm);
            heroShootingState = new HeroShootingState(this, fsm);
        }

        private void Start()
        {
            fsm.Start(heroIdleState);
            
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();
            colliderHeight = capsuleCollider.bounds.extents.y;

            initialPosition = transform.position;
        }

        private void FixedUpdate()
        {
            if (isAlive)
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
        }

        private void Update()
        {
            fsm.CurrentState.OnHandleInput();













            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Terminar el juego
                Application.Quit();
            }
            if (isAlive)
            {
                if (isJumping)
                {
                    animator.SetBool("isJumping", true);
                }else
                {
                    animator.SetBool("isJumping", false);
                }

                // Verificacion si apreto tecla de disparo
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    Fire();
                }

                float movement = 0f;
                if (buildType == BuildType.Desktop)
                {
                    movement = Input.GetAxisRaw("Horizontal");
                }else
                {
                    movement = joystick.Horizontal;
                }

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

                if (buildType == BuildType.Desktop)
                {
                    if (Input.GetButton("Jump") && !isJumping)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                    }
                }else
                {
                    float vertical = joystick.Vertical;
                    if (vertical > 0.5f && !isJumping)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                    }
                }
            } else
            {
                // Si apretamos la tecla espacio, reiniciamos el juego
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    transform.position = initialPosition;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    isAlive = true;
                }
            }
        }

        public void Fire()
        {
            animator.SetTrigger("fire");
            if (transform.localScale.x == -1f)
            {
                // Se dispara hacia la izquierda
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                bullet.transform.Rotate(0f, 0f, -180f);
            }
            else
            {
                // Se dispara hacia la derecha
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Water"))
            {
                isAlive = false;
                transform.position += Vector3.down * 2f;
                rb.bodyType = RigidbodyType2D.Static;
            }
        }

    }
}
