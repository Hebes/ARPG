using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 世界时间
/// 帧冻结变慢等
/// https://zhuanlan.zhihu.com/p/31102862 协程使用IEnumerator的几种方式
/// </summary>
public class WorldTime : SMono<WorldTime>
{
    /// <summary>
    /// 是否暂停
    /// </summary>
    public static bool IsPausing { get; private set; }

    public static float LevelTime => Time.time - _sceneStartTime;
    private static int _pauseNum => _timeScaleStack.Count;
    public static float Fps => Mathf.Clamp(_fps.Fps, 1f, float.MaxValue);
    public static float PhysicsFps => _fps.PhysicsFps;

    /// <summary>
    /// 是否冻结当中
    /// </summary>
    public bool IsFrozen { get; private set; }

    /// <summary>
    /// 是否缓慢
    /// </summary>
    public bool IsSlow { get; private set; }

    private void Start()
    {
        _sceneStartTime = Time.time;
        _fps.Start();
    }

    private void Update()
    {
        _fps.Update();
    }

    private void FixedUpdate()
    {
        _fps.FixedUpdate();
        if (IsFrozen && _autoRecover)
        {
            if (IsPausing) return;
            if (_frozeFrame > 0) _frozeFrame--;
            else
            {
                _autoRecover = false;
                FrozenResume();
            }
        }

        float num = _slowEnd - Time.time;
        if (IsSlow && num < 0f)
        {
            Time.timeScale = Mathf.Clamp01(Time.timeScale + _slowRecover);
            if (Math.Abs(Time.timeScale - 1f) < 1.401298E-45f)
            {
                IsSlow = false;
                _slowEnd = 0f;
            }
        }
    }

    protected void OnDestroy()
    {
        Reset();
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public static void Pause()
    {
        ("时间暂停, 推入 " + Time.timeScale).Log();
        _timeScaleStack.Push(Time.timeScale);
        Time.timeScale = 0f;
        IsPausing = true;
    }

    /// <summary>
    /// 恢复
    /// </summary>
    public static void Resume()
    {
        if (_timeScaleStack.Count == 0) return;
        ("时间恢复, 取出 " + _timeScaleStack.Peek()).Log();
        Time.timeScale = _timeScaleStack.Pop();
        IsPausing = false;
    }

    /// <summary>
    /// 时间变慢
    /// </summary>
    /// <param name="slowTime">缓慢的时间</param>
    /// <param name="slowScale">缓慢的规模</param>
    public void TimeSlow(float slowTime, float slowScale)
    {
        if (IsPausing) return;
        //缓慢结束
        _slowEnd = Time.time + slowTime * slowScale; //Time.time：获取自游戏开始以来的累计时间
        IsSlow = true;
        Time.timeScale = slowScale;
        _slowRecover = (1f - slowScale) / 7f;//缓慢的恢复
    }

    /// <summary>
    /// 时间按帧慢速在60Fps
    /// </summary>
    /// <param name="slowFrame">迟钝帧</param>
    /// <param name="slowScale">迟钝规模</param>
    public void TimeSlowByFrameOn60Fps(int slowFrame, float slowScale)
    {
        TimeSlow(slowFrame / 60f, slowScale);
    }

    /// <summary>
    /// 时间冻结
    /// </summary>
    /// <param name="second"></param>
    /// <param name="type"></param>
    /// <param name="autoRecover"></param>
    public void TimeFrozen(float second, FrozenArgs.FrozenType type = FrozenArgs.FrozenType.All, bool autoRecover = true)
    {
        TimeFrozenByFixedFrame(FixedSecondToFrame(second), type, autoRecover);
    }

    /// <summary>
    /// 时间被固定帧冻结
    /// </summary>
    /// <param name="frame">帧</param>
    /// <param name="type">冻结类型</param>
    /// <param name="autoRecover">自动恢复</param>
    public void TimeFrozenByFixedFrame(int frame, FrozenArgs.FrozenType type = FrozenArgs.FrozenType.All, bool autoRecover = true)
    {
        if (frame == 0) return;
        _frozeFrame = frame;
        if (!IsFrozen)
        {
            _frozenArgs = new FrozenArgs(type, null);
            GameEvent.WorldTimeFrozenEvent.Trigger(_frozenArgs);
        }

        _autoRecover = autoRecover;
        IsFrozen = true;
    }

    /// <summary>
    /// 时间被固定帧冻结
    /// </summary>
    /// <param name="frame">帧</param>
    /// <param name="frozenTarget">冻结的目标</param>
    public void TimeFrozenByFixedFrame(int frame, GameObject frozenTarget)
    {
        if (frame == 0) return;
        _frozeFrame = frame;
        if (!IsFrozen)
        {
            _frozenArgs = new FrozenArgs(FrozenArgs.FrozenType.Target, frozenTarget);
            GameEvent.WorldTimeFrozenEvent.Trigger(_frozenArgs);
        }

        _autoRecover = true;
        IsFrozen = true;
    }

    /// <summary>
    /// 冻结恢复
    /// </summary>
    public void FrozenResume()
    {
        if (!IsFrozen) return;
        IsFrozen = false;
        GameEvent.WorldTimeResumeEvent.Trigger(_frozenArgs);
    }

    /// <summary>
    /// 帧到秒
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    public static float FrameToSecond(int frame)
    {
        return frame / (Fps <= 30f ? 60f : Fps);
    }

    /// <summary>
    /// 帧到固定秒
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    public static float FrameToFixedSecond(int frame)
    {
        return frame * Time.fixedDeltaTime;
    }

    /// <summary>
    /// 秒到帧
    /// </summary>
    /// <param name="second"></param>
    /// <returns></returns>
    public static int SecondToFrame(float second)
    {
        if (second < 0f)
        {
            "秒不能是负数".Log();
            return 0;
        }

        return (int)(second * ((Fps <= 30f) ? 60f : Fps));
    }

    /// <summary>
    /// 固定秒到帧
    /// </summary>
    /// <param name="second"></param>
    /// <returns></returns>
    public static int FixedSecondToFrame(float second)
    {
        if (second < 0f)
        {
            "秒不能是负数".Log();
            return 0;
        }

        return (int)(second / Time.fixedDeltaTime);
    }

    public static Coroutine WaitForSecondsIgnoreTimeScale(float seconds)
    {
        return I.StartCoroutine(WaitForSecondsIgnoreTimeScaleCoroutine(seconds));
    }

    /// <summary>
    /// 等待SecondsIgnore时间尺度协程
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    private static IEnumerator WaitForSecondsIgnoreTimeScaleCoroutine(float seconds)
    {
        //Time.unscaledDeltaTime:表示自上一帧到当前帧的时间间隔（不考虑时间缩放因素）。
        //这意味着即使游戏被暂停或时间缩放因素被修改，Time.unscaledDeltaTime 仍然会返回真实时间的增量。
        for (float totalSeconds = 0f; totalSeconds < seconds; totalSeconds += Time.unscaledDeltaTime)
            yield return null;
    }

    /// <summary>
    /// 重置
    /// </summary>
    public static void Reset()
    {
        IsPausing = false;
        _timeScaleStack.Clear();
        Time.timeScale = 1f;
    }

    private static float _sceneStartTime;

    private static Stack<float> _timeScaleStack = new Stack<float>();

    private static readonly FramesPerSecond _fps = new FramesPerSecond();

    public const float TargetFps = 60f;

    private FrozenArgs _frozenArgs;

    private int _frozeFrame;

    /// <summary>
    /// 是否自动恢复
    /// </summary>
    private bool _autoRecover;

    /// <summary>
    /// 慢速结束
    /// </summary>
    private float _slowEnd;

    /// <summary>
    /// 缓慢的恢复
    /// </summary>
    private float _slowRecover;


    private class FramesPerSecond
    {
        internal void Start()
        {
            _lastInterval = Time.realtimeSinceStartup;
            _physicsLastInterval = Time.realtimeSinceStartup;
            _frames = 0;
            _physicsFrames = 0;
        }

        internal void Update()
        {
            _frames++;
            if (Time.realtimeSinceStartup > _lastInterval + 0.5f)
            {
                Fps = _frames / (Time.realtimeSinceStartup - _lastInterval);
                _frames = 0;
                _lastInterval = Time.realtimeSinceStartup;
            }
        }

        internal void FixedUpdate()
        {
            _physicsFrames++;
            if (Time.realtimeSinceStartup > _physicsLastInterval + 0.5f)
            {
                PhysicsFps = _physicsFrames / (Time.realtimeSinceStartup - _physicsLastInterval);
                _physicsFrames = 0;
                _physicsLastInterval = Time.realtimeSinceStartup;
            }
        }

        private const float UpdateInterval = 0.5f;

        private float _lastInterval;

        private int _frames;

        internal float Fps;

        private const float PhysicsUpdateInterval = 0.5f;

        private float _physicsLastInterval;

        private int _physicsFrames;

        internal float PhysicsFps;
    }
}