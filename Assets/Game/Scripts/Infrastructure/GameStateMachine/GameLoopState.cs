﻿namespace Infrastructure.StateMachine
{
    public class GameLoopState : IState
    {
        private GameStateMachine _stateMachine;

        public void Init(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}