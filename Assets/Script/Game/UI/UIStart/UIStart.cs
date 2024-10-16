using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 开始界面
/// </summary>
public class UIStart : SMono<UIStart>
{
    private SensitiveWordsFilter _sensitiveWordsFilter; //敏感词过滤器
    [Header("跳转的关卡数据")] [SerializeField] private SceneGate _tutorialGate;

    [SerializeField] private EventTrigger Start1;
    [SerializeField] private EventTrigger Option;
    [SerializeField] private EventTrigger Achievement;
    [SerializeField] private EventTrigger Quit;
    [SerializeField] private EventTrigger About;
    [SerializeField] private EventTrigger Continue;
    [SerializeField] private EventTrigger Delete;

    public CanvasGroup canvasGroup;


    private void Awake()
    {
        canvasGroup = transform.Find("Panel").GetComponent<CanvasGroup>();

        Start1 = transform.Find("Panel/Start").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        Continue = transform.Find("Panel/Continue").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        Delete = transform.Find("Panel/Delete").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        Option = transform.Find("Panel/Option").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        Achievement = transform.Find("Panel/Achievement").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        About = transform.Find("Panel/About").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        Quit = transform.Find("Panel/Quit").GetComponent<UnityEngine.EventSystems.EventTrigger>();

        //监听事件
        Start1.AddEventTrigger(EventTriggerType.PointerClick, OnStart);
        Option.AddEventTrigger(EventTriggerType.PointerClick, OnOption);
        Achievement.AddEventTrigger(EventTriggerType.PointerClick, OnAchievement);
        About.AddEventTrigger(EventTriggerType.PointerClick, OnAbout);
        Continue.AddEventTrigger(EventTriggerType.PointerClick, OnStart);
        Quit.AddEventTrigger(EventTriggerType.PointerClick, OnQuit);
        Delete.AddEventTrigger(EventTriggerType.PointerClick, OnDelete);

        GameBtn();

        if (!LevelManager.SceneName.Equals(CScene.InitScene)) return;
        UIStartRe();
        GameEvent.AfterSwitchingWorlds.Register(OnAfterSwitchingWorlds);
    }

    private void GameBtn()
    {
        if (SaveManager.IsAutoSaveDataExists)
        {
            Start1.gameObject.SetActive(false);
            Continue.gameObject.SetActive(true);
            Delete.gameObject.SetActive(true);
        }
        else
        {
            Start1.gameObject.SetActive(true);
            Continue.gameObject.SetActive(false);
            Delete.gameObject.SetActive(false);
        }
    }

    private void UIStartRe()
    {
        _tutorialGate = SceneGateManager.I.FindGate(1);
        canvasGroup.FadeTo(1);
        GameBtn();
        //_black.color = Color.black;
        R.Camera.Controller.IsFollowPivot = false;
        R.Camera.Controller.MovableCamera.position = Vector3.zero.SetZ(-10f);
        R.Mode.Reset();
        R.Mode.EnterMode(Mode.AllMode.UI);
        UIPause.I.Enabled = false;
        UICharactor.I.HideUI(true);
        //UIKeyInput.SaveAndSetHoveredObject(circle.gameObject);//手柄的提示
        StartCoroutine(FadeInCoroutine());
        if (!SaveManager.IsAutoSaveDataExists) //是否存在自动保存数据
        {
            // _mainContainerGameObject.SetActive(false);
            // _userNameGameObject.SetActive(true);
            _sensitiveWordsFilter = new SensitiveWordsFilter();
        }
    }

    private void OnAfterSwitchingWorlds(object udata)
    {
        if (LevelManager.SceneName.Equals(CScene.InitScene))
        {
            UIStartRe();
        }
    }

    private void OnDelete(PointerEventData obj)
    {
        R.SaveReset();
        R.Mode.EnterMode(Mode.AllMode.UI);
        //R.Ui.Pause.Enabled = false;
        LevelManager.LoadLevelByGateId(CScene.InitScene);
        GameBtn();
    }

    private void OnStart(PointerEventData obj)
    {
        StartCoroutine(OnStartIEnumerator());

        IEnumerator OnStartIEnumerator()
        {
            //cg.CanvasGroupInit(0);//UIStart关闭
            InputSetting.Stop(false); //输入停止

            //显示进度条
            yield return canvasGroup.DOUIFade(1f, 0f, GameSetting.UISplashScreenTimer);
            R.SceneGate.AllowSceneActivation = false;
            UIPause.I.Enabled = true;
            if (SaveManager.IsAutoSaveDataExists) //如果存在数据文件
            {
                yield return R.GameData.Load();
                //SingletonMono<MobileInputPlayer>.Instance.VisiableBladeStorm();//显示按钮
                LevelManager.LoadLevelByPosition(R.GameData.SceneName, R.GameData.PlayerPosition, true);
                yield return StartCoroutine(ProgressAnimCoroutine()); //进度条
                R.Player.Transform.position = R.GameData.PlayerPosition;
                R.Player.Action.TurnRound(R.GameData.PlayerAttributeGameData.faceDir); //玩家转向
                R.Camera.Controller.CameraResetPostionAfterSwitchScene(); //切换场景后相机复位位置
            }
            else
            {
                _tutorialGate.Enter(true);
                MobileInputPlayer.I.VisiableBladeStorm();
                StartCoroutine(ProgressAnimCoroutine());
            }
        }
    }

    /// <summary>
    /// 成就
    /// </summary>
    /// <param name="obj"></param>
    private void OnAchievement(PointerEventData obj)
    {
        canvasGroup.FadeTo(0);
        StartCoroutine(UIAchievement.I.cg.DOUIFade(0f, 1f, GameSetting.UISplashScreenTimer));
    }

    /// <summary>
    /// 选项
    /// </summary>
    private void OnOption(PointerEventData obj)
    {
        canvasGroup.FadeTo(0);
        StartCoroutine(UIOption.I.Panel.DOUIFade(0f, 1f, GameSetting.UISplashScreenTimer));
    }

    private void OnQuit(PointerEventData obj)
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    /// <summary>
    /// 关于
    /// </summary>
    /// <param name="obj"></param>
    private void OnAbout(PointerEventData obj)
    {
        canvasGroup.FadeTo(0);
        StartCoroutine(UIAbout.I.panel.DOUIFade(0f, 1f, GameSetting.UISplashScreenTimer));
    }

    /// <summary>
    /// 加载界面的进度协同程序
    /// </summary>
    /// <returns></returns>
    private IEnumerator ProgressAnimCoroutine()
    {
        float nowprocess = 0f; //当前进度
        float currentVelocity = 0f; //流速
        float smoothTime = 0.1f; //光滑的时间

        UILoading.I.Panel.FadeTo(1);
        while (nowprocess < 99.5f)
        {
            float toProcess;
            if (R.SceneGate.Progress < 0.9f)
                toProcess = R.SceneGate.Progress * 100f;
            else
                toProcess = 100f;

            if (nowprocess < toProcess)
                nowprocess = Mathf.SmoothDamp(nowprocess, toProcess, ref currentVelocity, smoothTime);
            UILoading.I.SetProgressBar(nowprocess / 100f);
            yield return null;
        }

        yield return UILoading.I.Panel.DOUIFade(1f, 0f, 2f);
        //DOTween.To(() => this._black.color, delegate(Color c) { this._black.color = c; }, Color.black, 2f).WaitForCompletion();
        InputSetting.Resume(); //输入系统复位
        R.Mode.ExitMode(Mode.AllMode.UI);
        //UIKeyInput.LoadHoveredObject();
        R.SceneGate.AllowSceneActivation = true;

        UICharactor.I.ShowUI();
        yield return new WaitForSeconds(3f);
    }

    private IEnumerator FadeInCoroutine()
    {
        // yield return DOTween.To(() => _black.color, delegate(Color c)
        // {
        //     _black.color = c;
        // }, Color.clear, 1f).WaitForCompletion();
        // _startBtn.SetActive(true);
        // _optionsBtn.SetActive(true);
        yield return null;
    }
}