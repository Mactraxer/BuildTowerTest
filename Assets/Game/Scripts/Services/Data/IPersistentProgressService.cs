using AnyColorBall.Infrastructure;

namespace AnyColorBall.Services.Data
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}