using UnityEngine;
using DG.Tweening;

public class UIButtonHoverEffect : MonoBehaviour
{
    [SerializeField] private CanvasGroup hoverTarget;
    [SerializeField] private float fadeDuration = 0.3f;

    private Tween _currentTween;

    private void Reset()
    {
        if (hoverTarget == null)
        {
            foreach (var cg in GetComponentsInChildren<CanvasGroup>(true))
            {
                if (cg.name == "Hover")
                {
                    hoverTarget = cg;
                    break;
                }
            }
        }
    }

    public void ShowHover()
    {
        if (hoverTarget == null) return;

        _currentTween?.Kill();
        hoverTarget.gameObject.SetActive(true);
        _currentTween = hoverTarget.DOFade(1f, fadeDuration).SetEase(Ease.OutQuint);
    }

    public void HideHover()
    {
        if (hoverTarget == null) return;

        _currentTween?.Kill();
        _currentTween = hoverTarget.DOFade(0f, fadeDuration)
            .SetEase(Ease.InQuint)
            .OnComplete(() => hoverTarget.gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        _currentTween?.Kill();
    }
}

