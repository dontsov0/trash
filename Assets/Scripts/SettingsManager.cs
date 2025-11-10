using System;
using UnityEngine;
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }
    
    public static event Action<bool> OnDirtyStateChanged;
    public static event Action OnSettingsReset;
    public static event Action<bool> OnDefaultSettingsStateChanged;
    public static event Action OnSettingsResetToDefault;
    
    [Header("Default Settings")]
    [SerializeField] private int defaultSensitivity = 50;
    [SerializeField] private int defaultDifficulty = 1;
    [SerializeField] private bool defaultSubtitles = true;
    [SerializeField] private int defaultWindowMode = 1;
    [SerializeField] private int defaultResolution = 1;
    [SerializeField] private int defaultTextureQuality = 2;
    [SerializeField] private int defaultAdjustGamma = 5;
    [SerializeField] private bool defaultVsync = true;
    [SerializeField] private int defaultResolutionScale = 100;
    
    private bool areSettingsDirty = false;
    private bool areSettingsDefault = false;

    // CurrentValues
    public int Sensitivity { get; private set; }
    public int Difficulty { get; private set; }
    public bool Subtitles { get; private set; }
    public int WindowMode { get; private set; }
    public int Resolution { get; private set; }
    public int TextureQuality { get; private set; }
    public int AdjustGamma { get; private set; }
    public bool Vsync { get; private set; }
    public int ResolutionScale { get; private set; }
    
    private const string SENSITIVITY_KEY = "Sensitivity";
    private const string DIFFICULTY_KEY = "Difficulty";
    private const string SUBTITLES_KEY = "Subtitles";
    private const string WINDOW_MODE_KEY = "Window mode";
    private const string RESOLUTION_KEY = "Resolution";
    private const string TEXTURE_QUALITY_KEY = "Texture Quality";
    private const string ADJUST_GAMMA_KEY = "Adjust Gamma";
    private const string VSYNC_KEY = "VSync";
    private const string RESOLUTION_SCALE_KEY = "Resolution Scale";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
            CheckSettingsAreDefault();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadSettings()
    {
        //Gameplay
        Sensitivity = PlayerPrefs.GetInt(SENSITIVITY_KEY, defaultSensitivity);
        Difficulty = PlayerPrefs.GetInt(DIFFICULTY_KEY, defaultDifficulty);
        Subtitles = PlayerPrefs.GetInt(SUBTITLES_KEY, defaultSubtitles ? 1 : 0) != 0;

        // Video
        WindowMode = PlayerPrefs.GetInt(WINDOW_MODE_KEY, defaultWindowMode);
        Resolution = PlayerPrefs.GetInt(RESOLUTION_KEY, defaultResolution);
        TextureQuality = PlayerPrefs.GetInt(TEXTURE_QUALITY_KEY, defaultTextureQuality);
        AdjustGamma = PlayerPrefs.GetInt(ADJUST_GAMMA_KEY, defaultAdjustGamma);
        Vsync = PlayerPrefs.GetInt(VSYNC_KEY, defaultVsync ? 1 : 0) != 0;
        ResolutionScale = PlayerPrefs.GetInt(RESOLUTION_SCALE_KEY, defaultResolutionScale);
        
        ApplySettings();
        SetDirty(false);
        Debug.Log("Настройки загружены.");
    }
    
    public void SaveSettings()
    {
        //Gameplay
        PlayerPrefs.SetInt(SENSITIVITY_KEY, Sensitivity);
        PlayerPrefs.SetInt(DIFFICULTY_KEY, Difficulty);
        PlayerPrefs.SetInt(SUBTITLES_KEY, Subtitles ? 1 : 0);

        // Video
        PlayerPrefs.SetInt(WINDOW_MODE_KEY, WindowMode);
        PlayerPrefs.SetInt(RESOLUTION_KEY, Resolution);
        PlayerPrefs.SetInt(TEXTURE_QUALITY_KEY, TextureQuality);
        PlayerPrefs.SetInt(ADJUST_GAMMA_KEY, AdjustGamma);
        PlayerPrefs.SetInt(VSYNC_KEY, Vsync ? 1 : 0);
        PlayerPrefs.SetInt(RESOLUTION_SCALE_KEY, ResolutionScale);

        PlayerPrefs.Save();
        SetDirty(false);
        Debug.Log("Настройки сохранены.");
    }
    
    public void ApplySettings()
    {
        Debug.Log("Настройки применены.");
    }
    
    public void RevertChanges()
    {
        Debug.Log("Отмена изменений. Перезагрузка последних сохраненных настроек.");
        LoadSettings();
        CheckSettingsAreDefault();
        OnSettingsReset?.Invoke();
    }
    
    public void ResetToDefaults()
    {
        Sensitivity = defaultSensitivity;
        Difficulty = defaultDifficulty;
        Subtitles = defaultSubtitles;
        WindowMode = defaultWindowMode;
        Resolution = defaultResolution;
        TextureQuality = defaultTextureQuality;
        AdjustGamma = defaultAdjustGamma;
        Vsync = defaultVsync;
        ResolutionScale = defaultResolutionScale;
        
        ApplySettings();
        SetDirty(true);
        SetDefaultSettingsState(true);
        OnSettingsResetToDefault?.Invoke();
        Debug.Log("Настройки сброшены до значений по умолчанию.");
    }
    
    private void SetDirty(bool isDirty)
    {
        if (areSettingsDirty == isDirty) return;

        areSettingsDirty = isDirty;
        OnDirtyStateChanged?.Invoke(areSettingsDirty);
    }

    public void CheckSettingsAreDefault()
    {
        var isDefault =
            Sensitivity == defaultSensitivity &&
            Difficulty == defaultDifficulty &&
            Subtitles == defaultSubtitles &&
            WindowMode == defaultWindowMode &&
            Resolution == defaultResolution &&
            TextureQuality == defaultTextureQuality &&
            AdjustGamma == defaultAdjustGamma &&
            Vsync == defaultVsync &&
            ResolutionScale == defaultResolutionScale;

        SetDefaultSettingsState(isDefault);
    }
    
    private void SetDefaultSettingsState(bool isDefault)
    {
        if (areSettingsDefault == isDefault) return;

        areSettingsDefault = isDefault;
        OnDefaultSettingsStateChanged?.Invoke(areSettingsDefault);
    }
    
    

    public void SetSensitivity(int value) 
    {
        if (Sensitivity == value) return;
        Sensitivity = value;
        SetDirty(true);
        CheckSettingsAreDefault();
    }

    public void SetDifficulty(int value)
    {
        if (Difficulty == value) return;
        Difficulty = value;
        SetDirty(true);
        CheckSettingsAreDefault();
    }

    public void SetSubtitles(bool value)
    {
        if (Subtitles == value) return;
        Subtitles = value;
        SetDirty(true);
        CheckSettingsAreDefault();
    }

    public void SetWindowMode(int value)
    {
        if (WindowMode == value) return;
        WindowMode = value;
        SetDirty(true);
        CheckSettingsAreDefault();
    }

    public void SetResolution(int value)
    {
        if (Resolution == value) return;
        Resolution = value;
        SetDirty(true);
        CheckSettingsAreDefault();
    }

    public void SetTextureQuality(int value)
    {
        if (TextureQuality == value) return;
        TextureQuality = value;
        SetDirty(true);
        CheckSettingsAreDefault();
    }

    public void SetAdjustGamma(int value)
    {
        if (AdjustGamma == value) return;
        AdjustGamma = value;
        SetDirty(true);
        CheckSettingsAreDefault();
    }

    public void SetVsync(bool value)
    {
        if (Vsync == value) return;
        Vsync = value;
        SetDirty(true);
        CheckSettingsAreDefault();
    }

    public void SetResolutionScale(int value)
    {
        if (ResolutionScale == value) return;
        ResolutionScale = value;
        SetDirty(true);
        CheckSettingsAreDefault();
    }
}
