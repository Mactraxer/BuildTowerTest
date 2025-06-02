using System;
using UnityEngine;

public class MobileInputService : MonoBehaviour, IInputService
{
    public event Action<float> OnChangeHorizontalInput;
    public event Action OnTapUpButton;
}
