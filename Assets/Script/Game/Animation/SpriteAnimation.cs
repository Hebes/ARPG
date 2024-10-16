using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 帧动画播放器
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    public string spriteName;
    public SpriteRenderer spriteRenderer;
    public Sprite[] frames;
    [Header("正数正向播放，负数反向播放")] public float framerate = 10.0f;
    [Header("是否循环")] public bool loop;
    [Header("是否暂停播放")] public bool isPlayStop;
    [Header("是否忽略timeScale")] public bool ignoreTimeScale = true;
    [Header("当前帧索引")] private int _currentFrameIndex;
    [Header("下一次更新时间")] private float _timer;
    [Header("当前帧率，通过曲线计算而来")] private readonly float currentFramerate = 20.0f;

    [Header("动画曲线")] [SerializeField] private AnimationCurve curve =
        new AnimationCurve(new Keyframe(0, 1, 0, 0), new Keyframe(1, 1, 0, 0));

    [Header("帧事件")] private readonly Dictionary<string, List<(int, Action)>> _frameEventDic = new();

    public float frameRate = 0.1f;
    private int currentFrameIndex;
    private float timer;

    private void Awake()
    {
        loop = true;
        isPlayStop = false;
    }

    private void Update()
    {
        //帧数据无效
        if (isPlayStop) return;
        if (frames == null || frames.Length == 0) return;

        //从曲线值计算当前帧率
        float curveValue = curve.Evaluate((float)_currentFrameIndex / frames.Length);
        float curvedFramerate = curveValue * framerate;
        //帧率有效
        if (curvedFramerate != 0)
        {
            //获取当前时间
            float time = ignoreTimeScale ? Time.unscaledTime : Time.time;
            //计算帧间隔时间
            float interval = Mathf.Abs(1.0f / curvedFramerate);
            //满足更新条件，执行更新操作
            if (time - _timer > interval)
                DoUpdate(); //执行更新操作
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogWarning("帧率为0，动画停止.");
        }
#endif
    }

    /// <summary>
    /// 添加帧事件
    /// </summary>
    public void AddFrameEvent(string spriteName, int frameNum, Action action)
    {
        if (!_frameEventDic.ContainsKey(spriteName))
            _frameEventDic.Add(spriteName, new List<(int, Action)>());
        _frameEventDic[spriteName].Add((frameNum, action));
    }

    public void SetFramesData(Sprite[] sprites)
    {
        frames = sprites;
        currentFrameIndex = 0;
    }

    /// <summary>
    /// 重设动画
    /// </summary>
    public void Reset()
    {
        _currentFrameIndex = framerate < 0 ? frames.Length - 1 : 0;
    }

    /// <summary>
    /// 从停止的位置播放动画
    /// </summary>
    public void Play() => isPlayStop = false;

    /// <summary>
    /// 暂停动画
    /// </summary>
    public void Pause() => isPlayStop = true;

    /// <summary>
    /// 停止动画，将位置设为初始位置
    /// </summary>
    public void Stop()
    {
        Pause();
        Reset();
    }

    /// <summary>
    /// 具体更新操作
    /// </summary>
    private void DoUpdate()
    {
        //计算新的索引
        int nextIndex = _currentFrameIndex + (int)Mathf.Sign(currentFramerate);

        //索引越界，表示已经到结束帧
        if (nextIndex < 0 || nextIndex >= frames.Length)
        {
            //非循环模式，禁用脚本
            if (loop == false)
            {
                _currentFrameIndex = Mathf.Clamp(_currentFrameIndex, 0, frames.Length - 1);
                return;
            }
        }


        //钳制索引
        _currentFrameIndex = nextIndex % frames.Length;
        spriteRenderer.sprite = frames[_currentFrameIndex];

        //触发事件
        TriggerEvent(spriteName, _currentFrameIndex);

        //设置计时器为当前时间
        _timer = ignoreTimeScale ? Time.unscaledTime : Time.time;
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    private void TriggerEvent(string spriteName, int currentFrameIndex)
    {
        if (!_frameEventDic.TryGetValue(spriteName, out var dataList)) return;
        if (dataList.Count == 0) return;
        foreach (var value in dataList)
        {
            if (value.Item1 != currentFrameIndex) continue;
            value.Item2.Invoke();
        }
    }
}