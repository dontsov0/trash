using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuLeftPanel : MonoBehaviour
{
    [SerializeField] private GameObject buttonsRoot;
    [SerializeField] private List<CustomButton> buttons;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private UIMoveEffect moveEffect;
    
    [Header("Config")]
    [SerializeField] private int campaignButtonId = 0;
    [SerializeField] private int multiplayerButtonId = 1;
    [SerializeField] private int optionsButtonId = 2;
    [SerializeField] private int exitButtonId = 3;
    
    [Header("MenuItemsClickEvents")]
    public UnityEvent onCampaignButtonClick;
    public UnityEvent onMultiplayerButtonClick;
    public UnityEvent onOptionsButtonClick;
    public UnityEvent onExitButtonClick;

    public UnityEvent onPanelShowed;
    public UnityEvent onPanelHidden;

    private void Start()
    {
        if (buttonsRoot == null || buttons != null)
        {
            buttons = new List<CustomButton>(buttonsRoot.GetComponentsInChildren<CustomButton>());
        }

        SetMenuItems();
    }
    
    private void OnValidate()
    {
        if (buttonsRoot == null) return;
        
        buttons = new List<CustomButton>(buttonsRoot.GetComponentsInChildren<CustomButton>());
    }
    
    private void SetMenuItems()
    { 
        if (buttons == null) return;
        
        var currentId = 0;
        foreach (var button in buttons)
        {
            button.id = currentId++;
            button.onClickAnimationEndsWhithId.AddListener(OnMenuItemClick);
        }
    }
    
    private void OnMenuItemClick(int id)
    {
        switch (id)
        {
            case 0:
                onCampaignButtonClick.Invoke();
                break;
            case 1:
                onMultiplayerButtonClick.Invoke();
                break;
            case 2:
                HideMenu();
                onOptionsButtonClick.Invoke();
                break;
            case 3:
                onExitButtonClick.Invoke();
                break;
            default:
                break;
        }
    }

    public void ShowMenu()
    {
        if (moveEffect != null)
        {
            moveEffect.MoveToOriginPoint(() =>
            {
                onPanelShowed.Invoke();
            });
        }
    }

    public void HideMenu()
    {
        if (moveEffect != null)
        {
            moveEffect.MoveToTargetPoint(() =>
            {
                onPanelHidden.Invoke();
            });
        }
    }
}
