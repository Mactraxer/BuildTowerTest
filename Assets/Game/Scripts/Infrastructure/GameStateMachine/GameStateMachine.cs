using System;
using System.Collections.Generic;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(BootstrapState bootstrapState, LoadProgressState loadProgressState, LoadLevelState loadLevelState, GameLoopState gameLoopState)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = bootstrapState,
                [typeof(LoadProgressState)] = loadProgressState,
                [typeof(LoadLevelState)] = loadLevelState,
                [typeof(GameLoopState)] = gameLoopState,
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadableState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}