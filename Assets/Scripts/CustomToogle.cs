using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomToogle : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] public int id;
    
    [Header("Events")]
    public UnityEvent onHovered;
    public UnityEvent onDishovered;
    public UnityEvent onClick;
    public UnityEvent onUnselected;
    
    [SerializeField]
    private bool isHovered = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHovered)
        {
            isHovered = true;
            onHovered?.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHovered)
        {
            isHovered = false;
            onDishovered?.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }

    public void Unselect()
    {
        onUnselected?.Invoke();
    }

    public void Select()
    {
        onClick?.Invoke();
    }
    
}