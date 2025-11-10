using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SelectorType
{
    Slider,
    Stepper
}
public class UISliderExtension : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler,
    IPointerExitHandler
{
    [SerializeField] private Slider slider;
    [SerializeField] public SelectorType selectorType;
    
    [SerializeField] private UIStepperTextSelector stepperTextSelector;
    [SerializeField] private UISliderTextSelector sliderTextSelector;
    
    [SerializeField] private Image fillImage;
    [SerializeField] private Sprite fillSpriteNormal;
    [SerializeField] private Sprite fillSpriteHover;
    [SerializeField] private bool isHovered = false;
    [SerializeField] private bool isPressed = false;
    
    [Header("Debug")]
    [SerializeField] private int _sliderCurrentValue;
    [SerializeField] private int _sliderMinValue;
    [SerializeField] private int _sliderMaxValue;
    
    private void Start()
    {
        if (slider == null) return;

        slider = GetComponent<Slider>();
        
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnEnable()
    {
        SendValuesToSelector();
    }

    public void SendValuesToSelector()
    {
        _sliderMinValue = GetSliderMinValue();
        _sliderMaxValue = GetSliderMaxValue();
        _sliderCurrentValue = GetSliderCurrentValue();
        
        switch (selectorType)
        {
            case SelectorType.Slider: 
                SendToSliderTextSelector();
                break;
            case SelectorType.Stepper:
                SendToStepperTextSelector();
                break;
            default:
                break;
        }
    }

    private void SendToSliderTextSelector()
    {
        if (sliderTextSelector == null) return;

        sliderTextSelector.SelectedSliderValue = _sliderCurrentValue;
        sliderTextSelector.SetMaxSliderValue(maxValue: _sliderMaxValue);
        sliderTextSelector.SetMinSliderValue(minValue: _sliderMinValue);
        
        sliderTextSelector.SetStartState();
    }

    private void SendToStepperTextSelector()
    {
        if (stepperTextSelector == null) return;

        stepperTextSelector.SelectedStepperTextIndex = _sliderCurrentValue;
        stepperTextSelector.SetStartState();

    }

    private int GetSliderMinValue()
    {
        if  (slider == null) return 0;
        
        return Mathf.RoundToInt(slider.minValue);
    }
    
    private int GetSliderMaxValue()
    {
        if  (slider == null) return 0;
        
        return Mathf.RoundToInt(slider.maxValue);
    }
    
    private int GetSliderCurrentValue()
    {
        if  (slider == null) return 0;
        
        return Mathf.RoundToInt(slider.value);
    }
    
    private void OnSliderValueChanged(float value)
    {
        var intValue = Mathf.RoundToInt(value);

        if (intValue == _sliderCurrentValue) return;
        
        _sliderCurrentValue = intValue;
        SendValuesToSelector();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (fillImage == null || fillSpriteHover == null) return;

        fillImage.sprite = fillSpriteHover;
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (fillImage == null || fillSpriteNormal == null) return;

        fillImage.sprite = isPressed ? fillSpriteHover : fillSpriteNormal;
        isHovered = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (fillImage == null || fillSpriteHover == null) return;

        fillImage.sprite = fillSpriteHover;
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (fillImage == null || fillSpriteNormal == null) return;
        
            fillImage.sprite = isHovered ? fillSpriteHover : fillSpriteNormal;
        
        isPressed = false;
    }
}
