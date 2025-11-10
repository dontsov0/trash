using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuHUDController : MonoBehaviour
{  
    [Header("Windows")]
    [SerializeField] private MainMenuLeftPanel leftPanel;
    
    [Header("Windows")]
    [SerializeField] private OptionsWindow optionsWindow;

    private void Start()
    {
        if (optionsWindow != null)
        {
            optionsWindow.onCloseWindow.AddListener(ShowLeftPanel);
            optionsWindow.SetWindowClosed();
            optionsWindow.gameObject.SetActive(false);
        }

        if (leftPanel != null)
        {
            leftPanel.onOptionsButtonClick.AddListener(() =>
            {
                HideLeftPanel();
                leftPanel.onPanelHidden.AddListener(OnOptionsButtonClick);
            });
        }
    }

    private void OnOptionsButtonClick()
    {
        leftPanel.onPanelHidden.RemoveListener(OnOptionsButtonClick);
        ShowOptionsWindow();
    }

    private void ShowLeftPanel()
    {
        if (leftPanel != null)
        {
            leftPanel.ShowMenu();
        }
    }

    private void HideLeftPanel()
    {
        leftPanel.HideMenu();
    }

    private void ShowOptionsWindow()
    {
        if (optionsWindow != null)
        {
            optionsWindow.gameObject.SetActive(true);
            optionsWindow.ShowWindow();
        }
    }
    
}
