using UnityEngine;
using UnityEngine.EventSystems;

public class UIAbout : SMono<UIAbout>
{
    public CanvasGroup panel;
    [SerializeField] private EventTrigger Quit;

    private void Awake()
    {
        panel = transform.Find("Panel").GetComponent<UnityEngine.CanvasGroup>();
        Quit = transform.Find("Panel/Quit").GetComponent<UnityEngine.EventSystems.EventTrigger>();

        Quit.AddEventTrigger(EventTriggerType.PointerClick, OnQuit);
        
        panel.FadeTo(0);
    }

    private void OnQuit(PointerEventData obj)
    {
        panel.FadeTo(0);
        StartCoroutine(UIStart.I.canvasGroup.DOUIFade(0f, 1f, GameSetting.UISplashScreenTimer));
    }
}