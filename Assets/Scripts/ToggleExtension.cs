using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ToggleExtension : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image fillImage;
    [SerializeField] private Sprite fillSpriteNormal;
    [SerializeField] private Sprite fillSpriteHover;
    [SerializeField] private bool isHovered = false;

    public UnityEvent onToggleIsOn;
    public UnityEvent onToggleIsOff;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (toggle == null) return;
        
        toggle.onValueChanged.AddListener(OnToggleValueChanged);

    }
    
    public void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            onToggleIsOn.Invoke();
        }
        else
        {
            onToggleIsOff.Invoke();
        }
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

        fillImage.sprite =  fillSpriteNormal;
        isHovered = false;
    }
}
