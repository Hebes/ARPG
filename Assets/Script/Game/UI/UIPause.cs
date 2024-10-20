using Colorful;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

/// <summary>
/// 暂停界面
/// </summary>
public class UIPause : SMono<UIPause>
{
    [FormerlySerializedAs("cg")] public CanvasGroup canvasGroup;
    [SerializeField] private EventTrigger Continue; //继续游戏
    [SerializeField] private EventTrigger Setting; //设置
    [SerializeField] private EventTrigger Return; //返回
    [SerializeField] private EventTrigger Achievement; //奖杯
    [SerializeField] private EventTrigger Save;

    /// <summary>
    /// 是否已进入级别选择系统
    /// </summary>
    private bool HasEnteredLevelSelectSystem
    {
        get => false; //SaveStorage.Get("HasEnteredLevelSelectSystem", false);
        set => SaveStorage.Set("HasEnteredLevelSelectSystem", value);
    }

    private void Awake()
    {
        Continue = transform.Find("Panel/Continue").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        Setting = transform.Find("Panel/Setting").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        Achievement = transform.Find("Panel/Achievement").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        Return = transform.Find("Panel/Return").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        Save = transform.Find("Panel/Save").GetComponent<UnityEngine.EventSystems.EventTrigger>();


        Continue.AddEventTrigger(EventTriggerType.PointerClick, OnContinue);
        Setting.AddEventTrigger(EventTriggerType.PointerClick, OnSetting);
        Achievement.AddEventTrigger(EventTriggerType.PointerClick, OnAchievement);
        Return.AddEventTrigger(EventTriggerType.PointerClick, OnReturn);
        Save.AddEventTrigger(EventTriggerType.PointerClick, OnSave);
        canvasGroup.FadeTo(0);
    }

    /// <summary>
    /// 存档
    /// </summary>
    /// <param name="obj"></param>
    private void OnSave(PointerEventData obj)
    {
        R.GameData.Save();
    }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled
    {
        get => Input.UI.Pause.IsOpen;
        set
        {
            Input.UI.Pause.IsOpen = value;
            MobileInputPlayer.I.OptionsVisible = value;
        }
    }

    /// <summary>
    /// 暂停，然后打开动画
    /// </summary>
    /// <returns></returns>
    public YieldInstruction PauseThenOpenWithAnim()
    {
        Open();
        Enabled = false;
        R.PauseGame();
        AnalogTV analogTV = UICamera.I.GetComponent<AnalogTV>();
        analogTV.Scale = 0f;
        
        Enabled = true;
        return new AsyncOperation();
        YieldInstruction temp = DOTween.To(() => analogTV.Scale,
            delegate(float scale) { analogTV.Scale = scale; },
            1.02f,
            0.5f).OnComplete(delegate { Enabled = true; }).SetUpdate(true).WaitForCompletion();
        return temp;
    }

    /// <summary>
    /// 关闭，然后打开动画
    /// </summary>
    /// <returns></returns>
    public YieldInstruction CloseWithAnimThenResume()
    {
        Enabled = false;
        AnalogTV analogTV = UICamera.I.GetComponent<AnalogTV>();
        
        Close();
        R.ResumeGame();
        Enabled = true;
        return new AsyncOperation();
        return DOTween.To(() => analogTV.Scale, delegate(float scale) { analogTV.Scale = scale; }, 0f, 0.5f).SetUpdate(true).OnComplete(delegate
        {
            Close();
            R.ResumeGame();
            Enabled = true;
        }).WaitForCompletion();
    }

    /// <summary>
    /// 打开
    /// </summary>
    private void Open()
    {
        //_levelSelect.transform.parent.gameObject.SetActive(this.HasEnteredLevelSelectSystem);//关卡选择
        R.Audio.PauseBGM();
        R.Mode.EnterMode(Mode.AllMode.UI);
        UICharactor.I.HideUI(true); //关闭血条等
        //UIKeyInput.SaveAndSetHoveredObject(this._resume.gameObject);
        I.canvasGroup.FadeTo(1);
        AnalogTV analogTV = UICamera.I.AddComponent<AnalogTV>();
        analogTV.Shader = Shader.Find("Hidden/Colorful/Analog TV");
        analogTV.NoiseIntensity = 1f;
        analogTV.ScanlinesIntensity = 0f;
        analogTV.ScanlinesCount = 696;
        analogTV.Distortion = 0.18f;
        analogTV.CubicDistortion = 0f;
        analogTV.Scale = 1.02f;
    }

    private void Close()
    {
        Destroy(UICamera.I.GetComponent<AnalogTV>());
        canvasGroup.FadeTo(0);
        //UIKeyInput.LoadHoveredObject();
        R.Mode.ExitMode(Mode.AllMode.UI);
        UICharactor.I.ShowUI(true);
        R.Audio.UnPauseBGM();
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    /// <param name="obj"></param>
    private void OnContinue(PointerEventData obj)
    {
        if (!Enabled) return;
        UIKeyInput.OnPauseClick();
    }

    /// <summary>
    /// 奖杯，成就
    /// </summary>
    /// <param name="obj"></param>
    private void OnAchievement(PointerEventData obj)
    {
        UIAchievement.I.Open();
    }

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="obj"></param>
    private void OnSetting(PointerEventData obj)
    {
        canvasGroup.FadeTo(0);
        StartCoroutine(UIOption.I.Panel.DOUIFade(0f, 1f, GameSetting.UISplashScreenTimer));
    }

    /// <summary>
    /// 返回主界面
    /// </summary>
    /// <param name="obj"></param>
    private void OnReturn(PointerEventData obj)
    {
        if (!Enabled) return;
        UITutorial.I.Kill();
        UIKeyInput.OnPauseClick();
        R.Ui.Reset();
        R.Player.Action.ChangeState(PlayerStaEnum.Idle);
        // R.Audio.StopVoiceOver();
        LevelManager.LoadLevelByGateId(CScene.InitScene);
    }
}