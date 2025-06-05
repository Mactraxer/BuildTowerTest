public interface IExitableState
{
    public void Exit();
}

public interface IState : IExitableState
{
    public void Enter();
}

public interface IPayloadableState<TPayload> : IExitableState
{
    public void Enter(TPayload payload);
}