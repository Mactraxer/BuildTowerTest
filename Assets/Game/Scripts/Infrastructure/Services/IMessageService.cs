using R3;
using System;

public interface IMessageService
{
    void ShowMessage(GameEventType type);
    Subject<string> OnMessage { get; }
}
