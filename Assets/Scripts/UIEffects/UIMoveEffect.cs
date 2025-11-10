using System;
using UnityEngine;
using DG.Tweening;

public class UIMoveEffect : MonoBehaviour
{
    [SerializeField] private RectTransform objectRectTransform;
    [SerializeField] private UIThemeConfig uiThemeConfig;
    [SerializeField] private RectTransform targetPoint;
    
    [Header("CustomValue")]
    [SerializeField] private bool useCustomDuration;
    [SerializeField] private float duration;
    [SerializeField] private Ease moveToPointEase = Ease.Linear;
    
    private Vector2 _originPoint;
    
    private Tween _currentTween;

    private void Reset()
    {
        if (objectRectTransform == null)
        {
            objectRectTransform = GetComponent<RectTransform>();
        }
    }

    private void Awake()
    {
        if (objectRectTransform == null)
        {
            objectRectTransform = GetComponent<RectTransform>();
            
        }
    }

    private void Start()
    {
        if (objectRectTransform != null)
        {
            _originPoint = objectRectTransform.anchoredPosition;
            
            Debug.Log(_originPoint);
        }
    }

    public void MoveToTargetPoint(Action onComplete = null)
    {
        MoveToPoint(targetPoint.anchoredPosition, onComplete);
    }
    
    public void MoveToOriginPoint(Action onComplete = null)
    {
        MoveToPoint(_originPoint, onComplete);
    }

    public void MoveToPoint(Vector3 position, Action onComplete = null)
    {
        if (objectRectTransform == null) return;

        var selectedDuration = useCustomDuration ? duration : uiThemeConfig.moveDuration;
        var selectedEase = useCustomDuration ? moveToPointEase : uiThemeConfig.moveToPointEase;
        
        _currentTween?.Kill();
        _currentTween = objectRectTransform.DOAnchorPosX(position.x, selectedDuration)
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
