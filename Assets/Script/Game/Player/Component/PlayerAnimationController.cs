using System;
using UnityEngine;

/// <summary>
/// 玩家动画控制器 MultiSpineAnimationController
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    #region 组件

    [SerializeField] private Animator m_animation;

    private void GetComponent()
    {
        m_animation = R.Player.GetComponentInChildren<Animator>();
    }

    #endregion

    public event EventHandler<EffectArgs> OnAnimChange; //动画切换
    public event EventHandler<EffectArgs> OnAnimSpeedChange; //动画速度切换

    private void Awake()
    {
        GetComponent();
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animName">动画类型</param>
    /// <param name="loop">是否循环播放</param>
    /// <param name="forceChange">强制切换</param>
    /// <param name="animSpeed">动画播放速度</param>
    public void Play(string animName, bool loop = false, bool forceChange = false, float animSpeed = 1f)
    {
        if (m_animation.CheckClip(animName) == false)
            throw new Exception($"动画{animName}不存在");
        m_animation.speed = animSpeed;
        GameEvent.OnPlayerAnimSpeedChange.Trigger(new EffectArgs(animSpeed));

        if (forceChange)
        {
            GameEvent.OnPlayerAnimChange.Trigger(new EffectArgs(animName, loop));
            m_animation.Play(animName);
            currentAnim = animName;
        }

        if (currentAnim != animName)
        {
            m_animation.Play(animName);
            GameEvent.OnPlayerAnimChange.Trigger(new EffectArgs(animName, loop));
            currentAnim = animName;
        }
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void Pause()
    {
        m_animation.speed = 0f;
    }

    /// <summary>
    /// 恢复
    /// </summary>
    public void Resume(float animSpeed = 1f)
    {
        m_animation.speed = animSpeed;
    }

    [Header("当前动画名称")] public string currentAnim = string.Empty;
    [Header("动画播放速度")] public float animSpeed = 1f;
}