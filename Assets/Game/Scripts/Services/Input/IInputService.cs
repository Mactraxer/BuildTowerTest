using Infrastructure.Services;
using System;

public interface IInputService : IService
{
    public event Action<float> OnChangeHorizontalInput;
    public event Action OnTapUpButton;
}
