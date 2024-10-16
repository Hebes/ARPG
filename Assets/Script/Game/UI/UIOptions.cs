using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

/// <summary>
/// 选项图标->在UICharact开启
/// </summary>
public class UIOptions : SMono<UIOptions>
{
    public CanvasGroup canvasGroup;
    [SerializeField] private EventTrigger Option;

    public bool optionsClick;
    
    public bool IsActive
    {
        get => canvasGroup.alpha >= 0.99f;
        set => gameObject.SetActive(value);
    }
    
    private void Awake()
    {
        Option = transform.Find("Option").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        canvasGroup = transform.Find("Option").GetComponent<UnityEngine.CanvasGroup>();

        Option.AddEventTrigger(EventTriggerType.PointerDown, OnOptionPointerClick);
        Option.AddEventTrigger(EventTriggerType.PointerUp,OnOptionsPointerUp);
    }
    
    private void OnOptionsPointerUp(PointerEventData obj)
    {
        optionsClick = false;
    }

    private void OnOptionPointerClick(PointerEventData obj)
    {
        optionsClick = true;
    }
}