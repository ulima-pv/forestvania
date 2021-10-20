using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForestVania.Hero.States
{
    public class HeroRunningState : HeroState
    {
        public HeroRunningState(HeroController controller, HeroStateMachine fsm) : base(controller, fsm)
        {
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
