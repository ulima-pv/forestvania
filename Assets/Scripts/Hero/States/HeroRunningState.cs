using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForestVania.Hero.States
{
    public class HeroRunningState : HeroState
    {
        private Animator animator;
        public HeroRunningState(HeroController controller, HeroStateMachine fsm) : base(controller, fsm)
        {
            animator = controller.GetComponent<Animator>();
        }
        public override void OnEnter()
        {
            base.OnEnter();
            animator.SetBool("isRunning", true);
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

            if (controller.buildType == BuildType.Desktop)
            {
                if (Input.GetButton("Jump"))
                {
                    controller.Jump();
                }
            }
            else
            {
                float vertical = controller.joystick.Vertical;
                if (vertical > 0.5f)
                {
                    controller.Jump();
                }
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
                controller.transform.position += 
                    Vector3.right * controller.movement * 
                    controller.speed * Time.deltaTime;

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
                controller.transform.position +=
                    Vector3.right * controller.movement *
                    controller.speed * Time.deltaTime;
            }
            else
            {
                // Ya no se esta moviendo
                // Cambiar al estado idle
                controller.Stop();
            }
        }

        public override void OnPhysicsUpdate()
        {
            base.OnPhysicsUpdate();
        }
    }
}
