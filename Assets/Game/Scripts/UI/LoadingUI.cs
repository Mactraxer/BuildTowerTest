using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.alpha = 1;
    }

    public void Hide() 
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        var waitForSeconds = new WaitForSeconds(0.03f);

        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= 0.03f;
            yield return waitForSeconds;
        }

        gameObject.SetActive(false);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
