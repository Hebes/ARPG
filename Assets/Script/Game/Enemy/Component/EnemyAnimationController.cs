using System;
using LitJson;
using UnityEngine;

/// <summary>
/// 敌人动画控制器
/// </summary>
[RequireComponent(typeof(Animator))]
public class EnemyAnimationController : MonoBehaviour
{
    private Animator _animator;
    private TimeController _timeController;


    public event EventHandler<EffectArgs> OnAnimChange; //动画切换事件
    public event EventHandler<EffectArgs> OnAnimSpeedChange; //动画速度切换事件

    private void Awake()
    {
        _timeController = transform.parent.FindComponent<TimeController>();
        _animator = GetComponent<Animator>();
        CurrentUnityAnim = string.Empty;
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animName">动画名称</param>
    /// <param name="loop">是否循环播放</param>
    /// <param name="forceChange">是否强制切换</param>
    /// <param name="animSpeed">动画速度</param>
    public void Play(string animName, bool loop = false, bool forceChange = false, float animSpeed = 1f)
    {
        _lastAnimSpeed = animSpeed;

        //重复的不切换
        if (loop && CurrentUnityAnim == animName && MathfX.isInMiddleRange(_lastAnimSpeed, animSpeed, 0.1f))
            return;

        //切换动画
        _resumeScale = animSpeed;
        if (WorldTime.I.IsFrozen)
        {
            animSpeed = 0f;
            if (_timeController != null)
                _timeController.isPause = true;
        }

        _animator.speed = animSpeed;
        OnAnimSpeedChange?.Invoke(this, new EffectArgs(animSpeed));

        if (forceChange || CurrentUnityAnim != animName)
        {
            CurrentUnityAnim = animName;
            _animator.Play(CurrentUnityAnim);
            OnAnimChange?.Invoke(this, new EffectArgs(CurrentUnityAnim, loop));
        }
    }

    public void Pause() => ChangeTimeScale(0f);

    public void Resume() => ChangeTimeScale(_resumeScale);

    /// <summary>
    /// 改变时间尺度
    /// </summary>
    /// <param name="animSpeed"></param>
    private void ChangeTimeScale(float animSpeed)
    {
        _animator.speed = animSpeed;
        OnAnimSpeedChange?.Invoke(this, new EffectArgs(animSpeed));
    }

    /// <summary>
    /// 最后一次的速度
    /// </summary>
    private float _lastAnimSpeed;

    /// <summary>
    /// 动画速度
    /// </summary>
    private float _resumeScale;

    /// <summary>
    /// 当前动画速度
    /// </summary>
    public float TimeScale => _animator.speed;

    [Header("当前动画名称")] public string CurrentUnityAnim;
}