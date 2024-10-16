using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 音量
/// </summary>
public class UIAudioSettingElement : MonoBehaviour
{
    public CanvasGroup AudioSettingElement;
    private Text global;
    private Text sound;
    private Text bg;

    private Slider Slider1;
    private Slider Slider2;
    private Slider Slider3;

    public EventTrigger Quit;
    
    public GameObject MusicalNotes;

    private void Awake()
    {
        AudioSettingElement = GetComponent<UnityEngine.CanvasGroup>();
        Slider1 = transform.Find("globalMusicTitle/Slider").GetComponent<UnityEngine.UI.Slider>();
        Slider2 = transform.Find("soundMusicTitle/Slider (1)").GetComponent<UnityEngine.UI.Slider>();
        Slider3 = transform.Find("bgMusicTitle/Slider (2)").GetComponent<UnityEngine.UI.Slider>();

        global = transform.Find("globalMusicTitle/Text").GetComponent<UnityEngine.UI.Text>();
        sound = transform.Find("soundMusicTitle/Text1").GetComponent<UnityEngine.UI.Text>();
        bg = transform.Find("bgMusicTitle/Text2").GetComponent<UnityEngine.UI.Text>();

        Quit = transform.Find("Quit").GetComponent<UnityEngine.EventSystems.EventTrigger>();

        Slider1.onValueChanged.AddListener(OnGlobalMusic);
        Slider2.onValueChanged.AddListener(OnSoundMusic);
        Slider3.onValueChanged.AddListener(OnBgMusic);
        Quit.AddEventTrigger(EventTriggerType.PointerClick, OnQuit);
    }

    /// <summary>
    /// 退出
    /// </summary>
    /// <param name="obj"></param>
    private void OnQuit(PointerEventData obj)
    {
        StartCoroutine(AudioSettingElement.DOUIFade(1, 0, GameSetting.UISplashScreenTimer));
        MusicalNotes.gameObject.SetActive(false);
    }

    private void Start()
    {
        MusicalNotes.gameObject.SetActive(false);
        Slider1.value = R.Audio.GlobalVolume / 100f;
        Slider2.value = R.Audio.EffectsVolume / 100f;
        Slider3.value = R.Audio.BGMVolume / 100f;
    }

    private void OnDisable()
    {
        R.Settings.Save();
    }

    /// <summary>
    /// 背景音乐
    /// </summary>
    /// <param name="arg0"></param>
    private void OnBgMusic(float arg0)
    {
        int volume = (int)(arg0 * 100);
        bg.text = $"{volume}";
        R.Audio.BGMVolume = volume;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arg0"></param>
    private void OnSoundMusic(float arg0)
    {
        int volume = (int)(arg0 * 100);
        sound.text = $"{volume}";
        R.Audio.EffectsVolume = volume;
    }

    /// <summary>
    /// 全局
    /// </summary>
    /// <param name="arg0"></param>
    private void OnGlobalMusic(float arg0)
    {
        int volume = (int)(arg0 * 100);
        global.text = $"{volume}";
        R.Audio.GlobalVolume = volume;
    }
}