using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionsWindow : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private CustomTabGroup customTabGroup;
    [SerializeField] private UIGameObjectSwitcher uiGameObjectSwitcher;
    [SerializeField] private GameObject body;
    [SerializeField] private CustomButton closeButton;
    [SerializeField] private GameObject optionsButtonsRoot;
    
    [Header("Options")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider difficultySlider;
    [SerializeField] private Toggle subtitlesToggle;
    [SerializeField] private Slider windowModeSlider;
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    [SerializeField] private Slider textureQualitySlider;
    [SerializeField] private Slider adjustGammaSlider;
    [SerializeField] private Toggle vsyncToggle;
    [SerializeField] private Slider resolutionScaleSlider;
    
    [Header("Effects")]
    [SerializeField] private UIFadeEffect fadeEffect;
    [SerializeField] private UIScaleEffect scaleEffect;
    
    [Header("Debug")]
    [SerializeField] private int defaultTabId = 0;
    [SerializeField] private int selectedTabId = 0;

    public UnityEvent onCloseWindow;

    private void Start()
    {
        SettingsManager.OnSettingsReset += SetOptionsValue;
        SettingsManager.OnSettingsResetToDefault += SetOptionsValue;
        
        if (customTabGroup != null)
        {
            customTabGroup.onTabChanged.AddListener(OnTabChanged);
        
            customTabGroup.SelectTab(defaultTabId);
            selectedTabId = defaultTabId;
        }

        AddOptionListeners();
        SetOptionsValue();
    }

    private void AddOptionListeners()
    {
        if (sensitivitySlider != null)
            sensitivitySlider.onValueChanged.AddListener(SetSensitivityValue);
        
        if (difficultySlider != null)
            difficultySlider.onValueChanged.AddListener(SetDifficaltyValue);
        
        if (subtitlesToggle != null)
            subtitlesToggle.onValueChanged.AddListener(SetSubtitlesValue);
        
        if (windowModeSlider != null)
            windowModeSlider.onValueChanged.AddListener(SetWindowModeValue);
        
        if (textureQualitySlider != null)
            textureQualitySlider.onValueChanged.AddListener(SetTextureQualityValue);
        
        if  (resolutionDropDown != null)
            resolutionDropDown.onValueChanged.AddListener(SetResolutionValue);
        
        if (adjustGammaSlider != null)
            adjustGammaSlider.onValueChanged.AddListener(SetAdjustGammaValue);
        
        if (vsyncToggle != null)
            vsyncToggle.onValueChanged.AddListener(SetVSyncValue);
        
        if (resolutionScaleSlider  != null)
            resolutionScaleSlider.onValueChanged.AddListener(SetResolutionScale);
    }

    private void SetSensitivityValue(float value)
    {
        SettingsManager.Instance.SetSensitivity(Mathf.RoundToInt(value));
    }
    
    private void SetDifficaltyValue(float value)
    {
        SettingsManager.Instance.SetDifficulty(Mathf.RoundToInt(value));
    }
    
    private void SetSubtitlesValue(bool value)
    {
        SettingsManager.Instance.SetSubtitles(value);
    }
    
    private void SetWindowModeValue(float value)
    {
        SettingsManager.Instance.SetWindowMode(Mathf.RoundToInt(value));
    }
    
    private void SetResolutionValue(int value)
    {
        SettingsManager.Instance.SetResolution(value);
    }
    
    private void SetTextureQualityValue(float value)
    {
        SettingsManager.Instance.SetTextureQuality(Mathf.RoundToInt(value));
    }
    
    private void SetAdjustGammaValue(float value)
    {
        SettingsManager.Instance.SetAdjustGamma(Mathf.RoundToInt(value));
    }
    
    private void SetVSyncValue(bool value)
    {
        SettingsManager.Instance.SetVsync(value);
    }
    
    private void SetResolutionScale(float value)
    {
        SettingsManager.Instance.SetResolutionScale(Mathf.RoundToInt(value));
    }

    private void SetOptionsValue()
    {
        if (SettingsManager.Instance == null) return;
        
        if (sensitivitySlider != null)
            sensitivitySlider.value = SettingsManager.Instance.Sensitivity;
        
        if (difficultySlider != null)
            difficultySlider.value = SettingsManager.Instance.Difficulty;
        
        if (subtitlesToggle != null)
            subtitlesToggle.isOn = SettingsManager.Instance.Subtitles;
        
        if (windowModeSlider != null)
            windowModeSlider.value = SettingsManager.Instance.WindowMode;
        
        if  (resolutionDropDown != null)
            resolutionDropDown.value = SettingsManager.Instance.Resolution;
        
        if (textureQualitySlider != null)
            textureQualitySlider.value = SettingsManager.Instance.TextureQuality;
        
        if (adjustGammaSlider != null)
            adjustGammaSlider.value = SettingsManager.Instance.AdjustGamma;
        
        if (vsyncToggle != null)
            vsyncToggle.isOn = SettingsManager.Instance.Vsync;
        
        if (resolutionScaleSlider  != null)
            resolutionScaleSlider.value = SettingsManager.Instance.ResolutionScale;
    }

    public void ShowWindow()
    {
        if (closeButton != null)
        {
            closeButton.gameObject.SetActive(true);
            closeButton.onClickAnimationEnds.AddListener(CloseWindow);
        }
        
        if (optionsButtonsRoot != null)
        {
            optionsButtonsRoot.gameObject.SetActive(true);
        }
        
        if (fadeEffect != null)
        {
            fadeEffect.FadeIn();
        }

        if (scaleEffect != null)
        {
            scaleEffect.ScaleIn();
        }
    }

    [ContextMenu("Close Window")]
    public void CloseWindow()
    {
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(CloseWindow);
            closeButton.gameObject.SetActive(false);
        }
        
        if (optionsButtonsRoot != null)
        {
            optionsButtonsRoot.gameObject.SetActive(false);
        }
        
        if (fadeEffect != null)
        {
            fadeEffect.FadeOut();
        }

        if (scaleEffect != null)
        {
            scaleEffect.ScaleOut(() =>
            {
                onCloseWindow.Invoke();
                gameObject.SetActive(false);
            });
        }
    }

    public void SetWindowClosed()
    {
        if (fadeEffect != null)
        {
            fadeEffect.SetFadeOutState();
        }

        if (scaleEffect != null)
        {
            scaleEffect.SetScaleOutState();
        }
    }
    
    private void Reset()
    {
        if (fadeEffect == null)
        {
            fadeEffect = GetComponent<UIFadeEffect>();
        }
    }


    private void OnValidate()
    {
        if (body != null && scaleEffect == null)
        {
            scaleEffect = body.GetComponent<UIScaleEffect>();
        }
        
        if (fadeEffect == null)
        {
            fadeEffect = GetComponent<UIFadeEffect>();
        }
    }

    private void OnTabChanged(int id)
    {
        selectedTabId = id;
        
        if (uiGameObjectSwitcher !=  null)
            uiGameObjectSwitcher.SwitchGameObject(selectedTabId);
    }
}
