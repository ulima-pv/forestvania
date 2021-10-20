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
        public float movement;

        // FSM
        private HeroStateMachine fsm;
        private HeroIdleState heroIdleState;
        private HeroJumpingState heroJumpingState;
        private HeroRunningState heroRunningState;
        private HeroShootingState heroShootingState;


        private Animator animator;
        private Rigidbody2D rb;
        private CapsuleCollider2D capsuleCollider;
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

            initialPosition = transform.position;
        }

        private void FixedUpdate()
        {
        }

        private void Update()
        {
            fsm.CurrentState.OnHandleInput();

            if (isAlive)
            {

                // Verificacion si apreto tecla de disparo
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    Fire();
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

        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Water"))
            {
                isAlive = false;
                transform.position += Vector3.down * 2f;
                rb.bodyType = RigidbodyType2D.Static;
            }
        }

        public void Move()
        {
            fsm.ChangeState(heroRunningState);
        }

        public void Stop()
        {
            fsm.ChangeState(heroIdleState);
        }

        public void Jump()
        {
            fsm.ChangeState(heroJumpingState);
        }

    }
}
