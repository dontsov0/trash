using UnityEngine;
using DG.Tweening;
using TMPro;

public class UITextColorEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private UIThemeConfig uiThemeConfig;
    
    [Header("CustomValue")]
    [SerializeField] private bool useCustomValue = false;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color hoveredColor;
    [SerializeField] private Color disabledColor;
    [SerializeField] private Ease changeColorEase = Ease.Linear;

    private Tween _currentColorTween;

    private void Reset()
    {
        if (targetText == null)
        {
            targetText = GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetNormalColor()
    {
        var color = useCustomValue ? normalColor : uiThemeConfig.textNormalColor;
        
        if (uiThemeConfig != null) 
            SetColor(color);
    }
    
    public void SetNormalColorInstant()
    {
        var color = useCustomValue ? normalColor : uiThemeConfig.textNormalColor;
        
        if (uiThemeConfig != null) 
            SetColor(color, instant: true);
    }
    
    public void SetHoveredColor()
    {
        var color = useCustomValue ? hoveredColor : uiThemeConfig.textHoveredColor;
        
        if (uiThemeConfig != null) 
            SetColor(color);
    }
    
    public void SetHoveredColorInstant()
    {
        var color = useCustomValue ? hoveredColor : uiThemeConfig.textHoveredColor;
        
        if (uiThemeConfig != null) 
            SetColor(color, instant: true);
    }
    
    public void SetSelectedColor()
    {
        var color = useCustomValue ? selectedColor : uiThemeConfig.textSelectedColor;
        
        if (uiThemeConfig != null) 
            SetColor(color);
    }
    
    public void SetSelectedColorInstant()
    {
        var color = useCustomValue ? selectedColor : uiThemeConfig.textSelectedColor;
        
        if (uiThemeConfig != null) 
            SetColor(color, instant: true);
    }
    
    public void SetDisabledColor()
    {
        var color = useCustomValue ? disabledColor : uiThemeConfig.textDisabledColor;
        
        if (uiThemeConfig != null) 
            SetColor(color);
    }
    
    public void SetDisabledColorInstant()
    {
        var color = useCustomValue ? disabledColor : uiThemeConfig.textDisabledColor;
        
        if (uiThemeConfig != null) 
            SetColor(color, instant: true);
    }
    
    private void SetColor(Color color, bool instant = false)
    {
        if (targetText == null) return;

        _currentColorTween?.Kill();

        if (instant)
        {
            targetText.color = color;
        }
        else
        {
            var selectedDuration = useCustomValue ? duration : uiThemeConfig.textColorDuration;
            var selectedEase = useCustomValue ? changeColorEase : uiThemeConfig.changeTextColorEase;
            
            _currentColorTween = targetText
                .DOColor(color, selectedDuration)
                .SetEase(selectedEase);
        }
    }

    private void OnDisable()
    {
        _currentColorTween?.Kill();
    }
}

