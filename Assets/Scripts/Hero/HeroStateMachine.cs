using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForestVania.Hero.States;

namespace ForestVania.Hero
{
    public class HeroStateMachine
    {
        public HeroState CurrentState { get; private set; }

        public void Start(HeroState initialState)
        {
            CurrentState = initialState;
            CurrentState.OnEnter();
        }

        public void ChangeState(HeroState newState)
        {
            CurrentState.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }
    }
}
