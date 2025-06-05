using R3;

namespace Infrastructure.Services
{
    public interface IMessageService
    {
        Subject<string> OnMessage { get; }

        void ShowMessage(GameEventType type);
    }
}