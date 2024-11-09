using System;
using LitJson;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 敌人动画控制器
/// </summary>
[RequireComponent(typeof(Animator))]
public class EnemyAnimationController : MonoBehaviour
{
    public event EventHandler<EffectArgs> OnAnimChange; //动画切换事件
    public event EventHandler<EffectArgs> OnAnimSpeedChange; //动画速度切换事件

    private void Awake()
    {
        Transform tr = transform.parent;
        _timeController = tr.FindComponent<TimeController>();
        _animator = tr.FindComponent<Animator>();
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
        if (loop && currentUnityAnim == animName && MathfX.isInMiddleRange(_lastAnimSpeed, animSpeed, 0.1f))
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

        if (forceChange || currentUnityAnim != animName)
        {
            currentUnityAnim = animName;
            _animator.Play(currentUnityAnim);
            OnAnimChange?.Invoke(this, new EffectArgs(currentUnityAnim, loop));
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

    public string currentUnityAnim = string.Empty;
    private Animator _animator;
    private TimeController _timeController;
}