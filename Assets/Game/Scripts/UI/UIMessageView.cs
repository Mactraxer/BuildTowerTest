using DG.Tweening;
using Infrastructure.Services;
using R3;
using TMPro;
using UnityEngine;
using Zenject;

public class UIMessageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private float _displayDuration = 2f;
    [SerializeField] private float _fadeDuration = 0.5f;

    private Tweener _fadeTweener;

    [Inject]
    private void Construct(IMessageService messageService)
    {
        messageService.OnMessage
            .ThrottleFirst(System.TimeSpan.FromSeconds(0.5))
            .Subscribe(DisplayMessage)
            .AddTo(this);
    }

    private void DisplayMessage(string msg)
    {
        _fadeTweener?.Kill();

        _messageText.text = msg;
        _messageText.alpha = 1f;
        _messageText.gameObject.SetActive(true);

        _fadeTweener = _messageText.DOFade(0f, _fadeDuration).SetDelay(_displayDuration).OnComplete(() =>
        {
            _messageText.gameObject.SetActive(false);
        });
    }

    private void OnDestroy()
    {
        _fadeTweener?.Kill();
    }
}