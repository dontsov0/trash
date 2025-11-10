using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class UIFadeEffect : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private UIThemeConfig uiThemeConfig;
    
    [Header("CustomValue")]
    [SerializeField] private bool useCustomValue;
    [SerializeField] private float targetAlpha;
    [SerializeField] private Ease fadeInEase = Ease.Linear;
    [SerializeField] private Ease fadeOutEase = Ease.Linear;
    [SerializeField] private float duration = 3f;
    
    private float _originalAlpha;
    private Tween _currentTween;

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        
        if (canvasGroup != null)
            _originalAlpha = canvasGroup.alpha;
    }

    public void SetFadeInState()
    {
        canvasGroup.alpha = _originalAlpha;
    }
    
    public void SetFadeOutState()
    {
        canvasGroup.alpha = targetAlpha;
    }

    public void FadeOut(Action onComplete = null)
    {
        if (canvasGroup == null) return;

        var selectedAlpha = useCustomValue ? targetAlpha : uiThemeConfig.targetAlpha;
        var selectedDuration = useCustomValue ? duration : uiThemeConfig.fadeDuration;
        var selectedEase = useCustomValue ? fadeOutEase : uiThemeConfig.fadeOutEase;
        
        _currentTween?.Kill();
        _currentTween = canvasGroup.DOFade(selectedAlpha, selectedDuration)
            .SetEase(selectedEase)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            }); 
    }


    public void FadeIn(Action onComplete = null)
    {
        if (canvasGroup == null) return;
        
        var selectedDuration = useCustomValue ? duration : uiThemeConfig.fadeDuration;
        var selectedEase = useCustomValue ? fadeInEase : uiThemeConfig.fadeInEase;
        
        _currentTween?.Kill();
        _currentTween = canvasGroup.DOFade(_originalAlpha, selectedDuration)
            .SetEase(selectedEase)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }

    public void OnDisable()
    {
        _currentTween?.Kill();
    }
}
