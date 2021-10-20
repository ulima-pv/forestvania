using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForestVania.Hero.States
{
    public abstract class HeroState
    {
        protected HeroController controller;
        protected HeroStateMachine fsm;

        public HeroState(HeroController controller, HeroStateMachine fsm)
        {
            this.controller = controller;
            this.fsm = fsm;
        }

        public virtual void OnPhysicsUpdate() { }
        public virtual void OnLogicUpdate() { }
        public virtual void OnHandleInput() { }
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}
