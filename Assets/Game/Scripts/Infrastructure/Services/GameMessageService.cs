using System;
using R3;

public class GameMessageService : IMessageService
{
    private readonly Subject<string> _messageSubject = new Subject<string>();

    public Subject<string> OnMessage => _messageSubject;

    public void ShowMessage(GameEventType type)
    {
        string message = type switch
        {
            GameEventType.Placed => "Кубик установлен!",
            GameEventType.Removed => "Кубик удалён!",
            GameEventType.Missed => "Кубик исчез!",
            GameEventType.HeightLimit => "Башня достигла максимальной высоты!",
            _ => ""
        };

        _messageSubject.OnNext(message);
    }
}
