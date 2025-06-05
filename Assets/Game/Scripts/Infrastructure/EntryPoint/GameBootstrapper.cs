using Infrastructure.StateMachine;
using UnityEngine;
using Zenject;

namespace Infrastructure.EntryPoint
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        [Inject]
        private void Init(Game game)
        {
            _game = game;
        }

        private void Start()
        {
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}