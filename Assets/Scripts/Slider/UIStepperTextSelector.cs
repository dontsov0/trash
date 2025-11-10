using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStepperTextSelector : MonoBehaviour
{
    [SerializeField] public List<TextMeshProUGUI> stepperTextList;
    [SerializeField] public GameObject textsRootContainer;
    [SerializeField] private TextMeshProUGUI selectedStepperText;
    [SerializeField] private int _selectedStepperTextIndex;

    public int SelectedStepperTextIndex
    {
        get => _selectedStepperTextIndex;
        set
        {
            if (_selectedStepperTextIndex != value)
            {
                _selectedStepperTextIndex = value;

                UpdateUI();
            }
        }
    }

    public void SetStartState()
    {
        SetSelectedTextComponent();
        HandleTextsColorState(instant: true);
    }

    private void Start()
    {
        if (textsRootContainer == null || stepperTextList == null) return;

        stepperTextList = new List<TextMeshProUGUI>(textsRootContainer.GetComponentsInChildren<TextMeshProUGUI>());
    }
    
    private void Reset()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        SetSelectedTextComponent();
        HandleTextsColorState();
    }

    private void SetSelectedTextComponent()
    {
        if (_selectedStepperTextIndex >= 0 && _selectedStepperTextIndex < stepperTextList.Count)
            selectedStepperText = stepperTextList[_selectedStepperTextIndex];
    }

    private void HandleTextsColorState(bool instant = false)
    {
        if (selectedStepperText == null) return;
        
        foreach (var text in stepperTextList)
        {
            var colorEffect = text.gameObject.GetComponent<UITextColorEffect>();
            if (colorEffect == null) continue;
            
            if (text == selectedStepperText)
            {
                if  (instant)
                    colorEffect.SetSelectedColorInstant();
                else 
                    colorEffect.SetSelectedColor();
            }
            else
            {   if (instant)
                    colorEffect.SetDisabledColorInstant();
                else 
                    colorEffect.SetDisabledColor();
            }
        }
    }
}
