using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIGameObjectSwitcherChild : MonoBehaviour
{
    [SerializeField] private UIFadeEffect _fadeEffect;

    public void SetOn()
    {
        if (_fadeEffect != null)
            _fadeEffect.FadeIn();
    }

    public void SetOff()
    {       
        if (_fadeEffect != null)
            _fadeEffect.FadeOut();
    }

    public void SetActive(bool active)
    {
        if (active)
            SetOn();
        else
            SetOff();
    }
}
