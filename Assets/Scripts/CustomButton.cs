using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] public int id;
    
    [Header("Events")]
    public UnityEvent onHovered;
    public UnityEvent onDishovered;
    public UnityEvent onClick;
    public IntEvent onClickWhithId;
    public UnityEvent onClickAnimationEnds;
    public IntEvent onClickAnimationEndsWhithId;
    public UnityEvent onInteractableOn;
    public UnityEvent onInteractableOff;
    public UnityEvent onDisable;
    
    [SerializeField]
    private bool isHovered = false;
    private bool interactable = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!interactable) return;
        
        if (!isHovered)
        {
            isHovered = true;
            onHovered?.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!interactable) return;
        
        if (isHovered)
        {
            isHovered = false;
            onDishovered?.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;
        
        onClick?.Invoke();
        onClickWhithId.Invoke(id);
    }
    
    public void OnClickAnimationEnds()
    {
        onClickAnimationEnds?.Invoke();
    }
    
    public void OnClickAnimationEndsWhithId()
    {
        onClickAnimationEndsWhithId?.Invoke(id);
    }

    public void SetInteractable(bool value) 
    {
        if (value)
        {
            interactable = true;
            onInteractableOn.Invoke();
        }
        else
        {
            interactable = false;
            onInteractableOff.Invoke();
        }
    }

    private void OnDisable()
    {
        isHovered = false;
        onDisable.Invoke();
    }
}