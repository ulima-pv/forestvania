using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForestVania.Hero.States
{
    public class HeroJumpingState : HeroState
    {
        private Rigidbody2D rb;
        private Animator animator;
        private CapsuleCollider2D capsuleCollider;
        private float colliderHeight;
        public HeroJumpingState(HeroController controller, HeroStateMachine fsm) : base(controller, fsm)
        {
            rb = controller.GetComponent<Rigidbody2D>();
            animator = controller.GetComponent<Animator>();
            capsuleCollider = controller.GetComponent<CapsuleCollider2D>();
            colliderHeight = capsuleCollider.bounds.extents.y;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            animator.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, controller.jumpSpeed);
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnHandleInput()
        {
            base.OnHandleInput();
            if (controller.buildType == BuildType.Desktop)
            {
                controller.movement = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                controller.movement = controller.joystick.Horizontal;
            }
        }

        public override void OnLogicUpdate()
        {
            base.OnLogicUpdate();
            if (controller.movement < 0)
            {
                if (controller.transform.localScale.x != -1f)
                {
                    controller.transform.localScale = new Vector3(
                        -1f,
                        controller.transform.localScale.y,
                        controller.transform.localScale.z
                    );
                }

            }
            else if (controller.movement > 0)
            {
                if (controller.transform.localScale.x != 1f)
                {
                    controller.transform.localScale = new Vector3(
                        1f,
                        controller.transform.localScale.y,
                        controller.transform.localScale.z
                    );
                }
            }

            controller.transform.position +=
                    Vector3.right * controller.movement *
                    controller.speed * Time.deltaTime;
        }

        public override void OnPhysicsUpdate()
        {
            base.OnPhysicsUpdate();

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity * 
                    (controller.fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity * 
                    (controller.lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }

            if (!IsJumping())
            {
                controller.Stop();
            }
        }

        private bool IsJumping()
        {
            RaycastHit2D hit = Physics2D.Raycast(
                capsuleCollider.bounds.center,
                Vector2.down,
                colliderHeight + controller.extraSpace
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
                Vector2.down * (colliderHeight + controller.extraSpace),
                rayColor
            );
            return !hit;
        }
    }
}
