using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class UIButtonClickEffect : MonoBehaviour
{
    [SerializeField] private CanvasGroup buttonCanvasGroup;
    [SerializeField] private GameObject buttonAnimContainer;
    [SerializeField] private UIThemeConfig uiThemeConfig;
    
    [SerializeField] public UnityEvent onAnimationEnds;
    
    [Header("Custom Values")]
    [SerializeField] private bool useCustomValues = false;
    [SerializeField] private float animDuration = 0.3f;
    

    private Tween _currentTween;

    public void Click()
    {
        if (buttonCanvasGroup == null || buttonAnimContainer == null) return;

        _currentTween?.Kill();
        buttonAnimContainer.transform.localScale = Vector3.one;
        
        var selectedDuration = useCustomValues ? animDuration : uiThemeConfig.clickAnimDuration;

        _currentTween = buttonAnimContainer.transform
            .DOScale(0.97f, selectedDuration * 0.3f) // быстро сжимаем
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                _currentTween = buttonAnimContainer.transform
                    .DOScale(1f, selectedDuration * 0.5f) // отскакиваем
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        onAnimationEnds?.Invoke();
                    });
            });
    }

    private void OnDisable()
    {
        _currentTween?.Kill();
    }
}

