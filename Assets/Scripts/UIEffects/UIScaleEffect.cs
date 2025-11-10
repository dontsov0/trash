using System;
using UnityEngine;
using DG.Tweening;
using Unity.Collections;
using UnityEngine.Events;

public class UIScaleEffect : MonoBehaviour
{
    [SerializeField] private RectTransform animationContainer;
    [SerializeField] private UIThemeConfig uiThemeConfig;
    
    [Header("CustomValue")]
    [SerializeField] private bool useCustomValues;
    [SerializeField] private Vector2 targetScale = Vector2.zero;
    [SerializeField] private float duration = 3f;
    [SerializeField] private Ease scaleInEase = Ease.Linear;
    [SerializeField] private Ease scaleOutEase = Ease.Linear;
    
    private Vector2 _originalScale = Vector2.one;
    
    
    private Tween _currentTween;

    private void Reset()
    {
        if (animationContainer == null)
        {
            animationContainer = GetComponent<RectTransform>();
        }
    }

    private void Awake()
    {
        if (animationContainer == null)
        {
            animationContainer = GetComponent<RectTransform>();
        }
    }

    public void SetScaleOutState()
    {
        if (animationContainer == null) return;

        animationContainer.transform.localScale = targetScale;
    }
    
    public void SetScaleInState()
    {
        if (animationContainer == null) return;

        animationContainer.transform.localScale = _originalScale;
    }

    public void ScaleOut(Action onComplete = null)
    {
        if (animationContainer == null) return;

        var selectedScale = useCustomValues ? targetScale : uiThemeConfig.targetScale;
        var selectedDuration = useCustomValues ? duration : uiThemeConfig.scaleDuration;
        var selectedEase = useCustomValues ? scaleOutEase : uiThemeConfig.scaleOutEase;
        
        _currentTween?.Kill();
        _currentTween = animationContainer.DOScale(selectedScale, selectedDuration)
            .SetEase(selectedEase)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            }); 
    }


    public void ScaleIn(Action onComplete = null)
    {
        if (animationContainer == null) return;
        
        var selectedDuration = useCustomValues ? duration : uiThemeConfig.scaleDuration;
        var selectedEase = useCustomValues ? scaleInEase : uiThemeConfig.scaleInEase;
        
        _currentTween?.Kill();
        _currentTween = animationContainer.DOScale(_originalScale, selectedDuration)
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
