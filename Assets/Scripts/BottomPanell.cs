using UnityEngine;

public class BottomPanel : MonoBehaviour
{
    [SerializeField] private CustomButton backButton;
    [SerializeField] private GameObject settingsButtonsRoot;
    [SerializeField] private CustomButton applyButton;
    [SerializeField] private CustomButton resetButton;
    [SerializeField] private CustomButton resetToDefaultButton;
    
    private void Start()
    {
        SetButtonsDisactive();
        SettingsManager.OnDirtyStateChanged += HandleDirtyStateChanged;
        SettingsManager.OnSettingsReset += HandleSettingsReset;
        SettingsManager.OnDefaultSettingsStateChanged += HandleResetToDefaultStateChanged;
        
        applyButton.onClickAnimationEnds.AddListener(SettingsManager.Instance.SaveSettings);
        resetButton.onClickAnimationEnds.AddListener(SettingsManager.Instance.RevertChanges);
        resetToDefaultButton.onClickAnimationEnds.AddListener(SettingsManager.Instance.ResetToDefaults);
        
        SettingsManager.Instance.CheckSettingsAreDefault();
    }

    private void SetButtonsDisactive()
    {
        if (backButton != null)
            backButton.gameObject.SetActive(false);
        
        if (settingsButtonsRoot != null)
           settingsButtonsRoot.gameObject.SetActive(false);
        
        if  (applyButton != null) 
            applyButton.SetInteractable(false);
        
        if  (resetButton != null) 
            resetButton.SetInteractable(false);
    }
    
    private void HandleDirtyStateChanged(bool isDirty)
    {
        applyButton.SetInteractable(isDirty);
        resetButton.SetInteractable(isDirty);
    }

    private void HandleResetToDefaultStateChanged(bool isDefault)
    {
        resetToDefaultButton.SetInteractable(!isDefault);
    }

    private void HandleSettingsReset()
    {
        applyButton.SetInteractable(false);
        resetButton.SetInteractable(false);
    }
 
}
