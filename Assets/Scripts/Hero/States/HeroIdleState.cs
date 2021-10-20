using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForestVania.Hero.States
{
    public class HeroIdleState : HeroState
    {
        private Animator animator;

        public HeroIdleState(HeroController controller, HeroStateMachine fsm) : base(controller, fsm)
        {
            controller.movement = 0f;
            animator = controller.GetComponent<Animator>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            animator.SetBool("isRunning", false);
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
                if (Input.GetButton("Jump") )
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
            if (controller.movement != 0)
            {
                // Hay movimiento
                controller.Move();
            }
        }

        public override void OnPhysicsUpdate()
        {
            base.OnPhysicsUpdate();
        }
    }
}
