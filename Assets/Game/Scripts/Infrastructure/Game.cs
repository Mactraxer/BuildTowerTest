using AnyColorBall.Infrastructure;
using Zenject;

public class Game
{
    public GameStateMachine StateMachine;

    public Game(GameStateMachine gameStateMachine)
    {
        StateMachine = gameStateMachine;
    }
}