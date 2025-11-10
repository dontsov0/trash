using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIToggleClickEffect : MonoBehaviour
{
    [SerializeField] private CanvasGroup toggleSelectorCanvasGroup;
    [SerializeField] private TextMeshProUGUI toggleTitle;
    [SerializeField] private float animDuration = 0.3f;
    [SerializeField] private Color normalColor = Color.antiqueWhite;
    [SerializeField] private Color selectedColor = Color.gray1;

    private Tween _selectorTween;
    private Tween _titleColorTween;

    public void Click()
    {
        if (toggleSelectorCanvasGroup == null) return;

        _selectorTween?.Kill();
        _titleColorTween.Kill();
        
        toggleSelectorCanvasGroup.gameObject.SetActive(true);
        _selectorTween = toggleSelectorCanvasGroup.DOFade(1f, animDuration).SetEase(Ease.OutQuad);
        _titleColorTween = toggleTitle.DOColor(selectedColor, animDuration)
            .SetEase(Ease.OutQuad);
    }

    public void Unselect()
    {
        if (toggleSelectorCanvasGroup == null) return;

        _selectorTween?.Kill();
        _titleColorTween.Kill();
        
        _selectorTween = toggleSelectorCanvasGroup.DOFade(0f, animDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() => toggleSelectorCanvasGroup.gameObject.SetActive(false));
        
        _titleColorTween = toggleTitle.DOColor(normalColor, animDuration).SetEase(Ease.OutQuad);
    }

    private void OnDisable()
    {
        _selectorTween?.Kill();
    }
}

