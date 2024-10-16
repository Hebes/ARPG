using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景门管理器
/// </summary>
public class SceneGateManager : SMono<SceneGateManager>
{
    /// <summary>
    /// 允许场景激活
    /// </summary>
    public bool AllowSceneActivation = true;

    /// <summary>
    /// 异步加载
    /// </summary>
    private AsyncOperation Async;

    /// <summary>
    /// 加载场景的时候被锁定
    /// </summary>
    public bool IsLocked;

    /// <summary>
    /// 当前场景中的门
    /// </summary>
    public List<SceneGate> GatesInCurrentScene = new List<SceneGate>();

    /// <summary>
    /// 进度
    /// </summary>
    public float Progress => Async?.progress ?? 0f;

    /// <summary>
    /// 加载等级场景携程
    /// </summary>
    /// <param name="data"></param>
    /// <param name="needProgressBar"></param>
    /// <returns></returns>
    private IEnumerator LoadLevelCoroutine(SwitchLevelGateData data, bool needProgressBar)
    {
        IsLocked = true;
        R.Player.StateMachine.SetState(PlayerStaEnum.Idle);
        var player2Ground = Physics2D.Raycast(R.Player.Transform.position, Vector2.down, 100f, LayerManager.GroundMask).distance;
        Async = LevelManager.LoadScene(data.ToLevelId);
        Async.allowSceneActivation = false;
        while (!AllowSceneActivation)
            yield return null;

        if (needProgressBar)
            yield return UIBlackPanel.I.FadeBlack();

        Async.allowSceneActivation = true;
        yield return Async;
        StartCoroutine(Preload());
        IsLocked = false;
        if (data.ToId != -1)
        {
            SceneGate sceneGate = FindGate(data.ToId);
            Exit(sceneGate.data, player2Ground, data.OpenType);
        }
        else
        {
            SwitchLevelGateData data2 = new SwitchLevelGateData();
            data2.SelfPosition = data.TargetPosition;
            Exit(data2, 0f, SceneGate.OpenType.None);
        }
    }

    /// <summary>
    /// 进入场景的一个门
    /// </summary>
    /// <param name="data"></param>
    /// <param name="needProgressBar">需要进度条</param>
    /// <returns></returns>
    public Coroutine Enter(SwitchLevelGateData data, bool needProgressBar = false)
    {
        //通过门触发事件
        PassGateEventArgs passGateEventArgs = new PassGateEventArgs(PassGateEventArgs.PassGateStatus.Enter, data, LevelManager.SceneName);
        GameEvent.PassGate.Trigger((gameObject, passGateEventArgs));
        $"从 大门Gate {data.MyId} 离开 {LevelManager.SceneName}".Log();
        return StartCoroutine(EnterCoroutine(data, needProgressBar));

        //进入携程
        IEnumerator EnterCoroutine(SwitchLevelGateData dataValue, bool needProgressBarValue)
        {
            R.SceneData.CanAIRun = false; //是否跑AI
            InputSetting.Stop(false);
            IsLocked = true;
            if (!needProgressBarValue)
                yield return UIBlackPanel.I.FadeBlack();
            IsLocked = false;
            yield return StartCoroutine(LoadLevelCoroutine(dataValue, needProgressBarValue));
        }
    }

    /// <summary>
    /// 离开
    /// </summary>
    /// <param name="data">大门数据</param>
    /// <param name="groundDis">玩家与地面的距离</param>
    /// <param name="enterGateOpenType">大门的打开方式</param>
    /// <returns></returns>
    public Coroutine Exit(SwitchLevelGateData data, float groundDis, SceneGate.OpenType enterGateOpenType)
    {
        GameEvent.PassGate.Trigger((gameObject, new PassGateEventArgs(PassGateEventArgs.PassGateStatus.Exit, data, LevelManager.SceneName)));
        (string.Format("从 Gate {1} 进入 {0}", LevelManager.SceneName, data.MyId)).Log();
        return StartCoroutine(ExitCoroutine(data, groundDis, enterGateOpenType));
    }

    /// <summary>
    /// 退出
    /// </summary>
    /// <param name="data">切换关卡大门数据</param>
    /// <param name="player2Ground">玩家与地面的距离</param>
    /// <param name="enterGateOpenType">打开方式</param>
    /// <returns></returns>
    private IEnumerator ExitCoroutine(SwitchLevelGateData data, float player2Ground, SceneGate.OpenType enterGateOpenType)
    {
        IsLocked = true;
        var pos = R.Player.Transform.position;
        pos.x = data.SelfPosition.x;
        if (data.InAir)
        {
            pos.y = data.SelfPosition.y;
        }
        else //不在地上
        {
            //射线检测->朝下检测
            float distance = Physics2D.Raycast(data.SelfPosition, Vector2.down, 100f, LayerManager.GroundMask).distance;
            float num = data.SelfPosition.y - distance;
            pos.y = player2Ground + num;
            if (data.OpenType != SceneGate.OpenType.PressKey)
            {
                if (enterGateOpenType != SceneGate.OpenType.Left)
                {
                    if (enterGateOpenType == SceneGate.OpenType.Right)
                        pos.x -= data.TriggerSize.x + 1f;
                }
                else
                {
                    pos.x += data.TriggerSize.x + 1f;
                }
            }
        }

        R.Player.Transform.position = pos; //设置玩家位置
        R.Player.Rigidbody2D.position = pos; //设置玩家刚体位置

        if (!InputSetting.IsWorking())
            InputSetting.Resume();

        R.Camera.Controller.CameraResetPostionAfterSwitchScene();
        yield return new WaitForFixedUpdate();
        if (enterGateOpenType != SceneGate.OpenType.Left)
        {
            if (enterGateOpenType == SceneGate.OpenType.Right)
                R.Player.Action.TurnRound(1);
        }
        else
        {
            R.Player.Action.TurnRound(-1);
        }

        GameEvent.AfterSwitchingWorlds.Trigger(null);
        yield return UIBlackPanel.I.FadeTransparent();
        IsLocked = false;
    }

    /// <summary>
    /// 寻找门
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public SceneGate FindGate(int id)
    {
        for (var i = 0; i < GatesInCurrentScene.Count; i++)
        {
            if (GatesInCurrentScene[i].data.MyId != id) continue;
            return GatesInCurrentScene[i];
        }

        throw new NullReferenceException(string.Concat("场景中没有门 ", LevelManager.SceneName, " id ", id));
    }

    /// <summary>
    /// 预加载
    /// </summary>
    /// <returns></returns>
    private IEnumerator Preload()
    {
        yield break;
    }
}