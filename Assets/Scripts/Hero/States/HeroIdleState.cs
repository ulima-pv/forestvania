using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForestVania.Hero.States
{
    public class HeroIdleState : HeroState
    {
        private float movement;
        public HeroIdleState(HeroController controller, HeroStateMachine fsm) : base(controller, fsm)
        {
            movement = 0f;
        }

        public override void OnEnter()
        {
            base.OnEnter();
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
                movement = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                movement = controller.joystick.Horizontal;
            }

        }

        public override void OnLogicUpdate()
        {
            base.OnLogicUpdate();
        }

        public override void OnPhysicsUpdate()
        {
            base.OnPhysicsUpdate();
        }
    }
}
