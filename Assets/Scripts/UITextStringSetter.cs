using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UITextStringSetter: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private List<string> texts;
    [SerializeField] private string defaultOnText = "On";
    [SerializeField] private string defaultOffText = "Off";
    
    private void Reset()
    {
        if (targetText == null)
        {
            targetText = GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetText_On()
    {
        if (targetText == null) return;

        targetText.text = defaultOnText;
    }
    
    
    public void SetText_Off()
    {
        if (targetText == null) return;

        targetText.text = defaultOffText;
    }
    
    public void SetText(int id)
    {
        if (targetText == null) return;
        
        targetText.text = texts[id];
    }
}

