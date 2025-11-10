using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISliderTextSelector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI minCounter;
    [SerializeField] private TextMeshProUGUI currentValueCounter;
    [SerializeField] private TextMeshProUGUI maxCounter;
    [SerializeField] private bool useUnits;
    [SerializeField] private string unitsString;
    [SerializeField] private GameObject counterRootContainer;
    [SerializeField] private List<TextMeshProUGUI> textComponents;
    [SerializeField] private TextMeshProUGUI selectedCounter;
    
    [SerializeField] private int _selectedSliderValue;
    [SerializeField] private int _minSliderValue;
    [SerializeField] private int _maxSliderValue;
    
    public int SelectedSliderValue
    {
        get => _selectedSliderValue;
        set
        {
            if (_selectedSliderValue != value)
            {
                _selectedSliderValue = value;

                UpdateUI();
            }
        }
    }
    
    private void Awake()
    {
        if (counterRootContainer == null) return;
        
        if (textComponents == null) return;

        textComponents = new List<TextMeshProUGUI>(counterRootContainer.GetComponentsInChildren<TextMeshProUGUI>());
    }

    public void SetStartState()
    {
        SetCurrentValueCounter();
        SetCurrentValueCounterText();
        HandleTextsColorState(instant: true);
    }

    private void UpdateUI()
    {
        SetCurrentValueCounter();
        SetCurrentValueCounterText();
        HandleTextsColorState();
    }

    public void SetMinSliderValue(int minValue)
    {
        _minSliderValue = minValue;
        UpdateUI();
    }

    public void SetMaxSliderValue(int maxValue)
    {
        _maxSliderValue = maxValue;
        UpdateUI();
    }

    private void SetCurrentValueCounterText()
    {
        currentValueCounter.text = _selectedSliderValue switch
        {
            var val when val == _minSliderValue => "",
            var val when val == _maxSliderValue => "",
            _ => useUnits ? $"{_selectedSliderValue}{unitsString}" : _selectedSliderValue.ToString()
        };
    }
    
    private void SetCurrentValueCounter()
    {
        if (minCounter == null || maxCounter == null || currentValueCounter == null) return;
        
        selectedCounter = _selectedSliderValue switch
        {
            var val when val == _minSliderValue => minCounter,
            var val when val == _maxSliderValue => maxCounter,
            _ => currentValueCounter
        };
    }
    
    private void HandleTextsColorState(bool instant = false)
    {
        if (textComponents == null) return;
        
        foreach (var text in textComponents)
        {
            var colorEffect = text.gameObject.GetComponent<UITextColorEffect>();
            if (colorEffect == null) continue;
            
            if (text == selectedCounter)
            {
                if (instant)
                    colorEffect.SetSelectedColorInstant();
                else
                    colorEffect.SetSelectedColor();
            }
            else
            {
                if (instant)
                    colorEffect.SetDisabledColorInstant();
                else 
                    colorEffect.SetDisabledColor();
            }
        }
    }
}
