using AnyColorBall.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
