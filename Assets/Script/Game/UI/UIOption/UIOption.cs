using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 选项
/// </summary>
public class UIOption : SMono<UIOption>
{
    public CanvasGroup Panel;
    public CanvasGroup AudioSettingElement;
    
    private EventTrigger Quit;
    private EventTrigger Volume;
    private EventTrigger KeyCode;

    private UIAudioSettingElement _uiAudioSettingElement;

    private void Awake()
    {
        Panel = transform.Find("Panel").GetComponent<UnityEngine.CanvasGroup>();
        AudioSettingElement = transform.Find("Panel/Container/AudioSettingElement").GetComponent<UnityEngine.CanvasGroup>();
        
        Volume = transform.Find("Panel/Volume").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        KeyCode = transform.Find("Panel/KeyCode").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        Quit = transform.Find("Panel/Quit").GetComponent<UnityEngine.EventSystems.EventTrigger>();

        _uiAudioSettingElement = transform.FindChildByType<UIAudioSettingElement>();
        Panel.FadeTo(0f);

        Quit.AddEventTrigger(EventTriggerType.PointerClick, OnQuit);
        Volume.AddEventTrigger(EventTriggerType.PointerClick, OnVolume);
        KeyCode.AddEventTrigger(EventTriggerType.PointerClick, OnKeyCode);
    }

    /// <summary>
    /// 改键
    /// </summary>
    /// <param name="obj"></param>
    private void OnKeyCode(PointerEventData obj)
    {
        
    }

    /// <summary>
    /// 音量
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="PointerEventData"></exception>
    private void OnVolume(PointerEventData obj)
    {
        StartCoroutine(_uiAudioSettingElement.AudioSettingElement.DOUIFade(0f, 1f, GameSetting.UISplashScreenTimer));
        _uiAudioSettingElement.MusicalNotes.SetActive(true);
    }

    private void OnQuit(PointerEventData obj)
    {
        Panel.FadeTo(0);
        if (LevelManager.SceneName.Equals(CScene.InitScene))
        {
            StartCoroutine(UIStart.I.canvasGroup.DOUIFade(0, 1, GameSetting.UISplashScreenTimer)) ;
        }
        else
        {
            StartCoroutine(UIPause.I.canvasGroup.DOUIFade(0, 1, GameSetting.UISplashScreenTimer));
        }
    }
}