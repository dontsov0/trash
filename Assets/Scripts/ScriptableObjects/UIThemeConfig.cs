using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "UIThemeConfig", menuName = "GLobalConfig/UIThemeConfig")]
public class UIThemeConfig : ScriptableObject
{
    // [Header("TextColors")]
    // public Color normalTextColor;
    // public Color hoverTextColor;
    // public Color selectedTextColor;
    // public Color disabledTextColor;
    
    [Header("UIScaleEffect")]
    public float scaleDuration = .3f;
    public Vector2 targetScale = new Vector2(.5f, .5f);
    public Ease scaleInEase = Ease.Linear;
    public Ease scaleOutEase = Ease.Linear;
    
    [Header("UIFadeEffect")]
    public float fadeDuration = .3f;
    public float targetAlpha;
    public Ease fadeInEase = Ease.Linear;
    public Ease fadeOutEase = Ease.Linear;
    
    [Header("UIMoveEffect")]
    public float moveDuration = .3f;
    public Ease moveToPointEase = Ease.Linear;
    
    [Header("UITextColorEffect")]
    [SerializeField] public float textColorDuration = 0.3f;
    [SerializeField] public Color textNormalColor;
    [SerializeField] public Color textSelectedColor;
    [SerializeField] public Color textHoveredColor;
    [SerializeField] public Color textDisabledColor;
    [SerializeField] public Ease changeTextColorEase = Ease.Linear;
    
    [Header("UIClickEffect")]
    [SerializeField] public float clickAnimDuration = 0.3f;
}
