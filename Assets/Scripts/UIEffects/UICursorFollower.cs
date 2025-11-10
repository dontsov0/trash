using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class CursorFollowerUIRelative : MonoBehaviour
{
    private const float FPS = 120f;
    private const float SPF = 1f / FPS;
    private float _time;

    private RectTransform _rect;
    private RectTransform _parentRect;
    private Canvas _canvas;

    void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        if (_rect.parent == null)
        {
            enabled = false;
            return;
        }

        _parentRect = _rect.parent as RectTransform;
        if (_parentRect == null)
        {
            enabled = false;
            return;
        }

        if (_canvas == null)
        {
            enabled = false;
        }
    }

    void Update()
    {
        _time += Time.deltaTime;
        if (_time < SPF) return;
        _time = 0f;

        Vector2 mousePos = Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;

        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentRect, 
            mousePos,
            null,
            out localPos
        );

        localPos /= _canvas.scaleFactor;

        _rect.anchoredPosition = localPos;
    }
}
