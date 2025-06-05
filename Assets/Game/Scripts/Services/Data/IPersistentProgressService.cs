using Data;
using Infrastructure.Services;

namespace Services.Data
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}