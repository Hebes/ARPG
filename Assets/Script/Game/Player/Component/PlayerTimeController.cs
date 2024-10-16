using System;
using UnityEngine;

/// <summary>
/// 玩家时间控制器
/// </summary>
public class PlayerTimeController : MonoBehaviour, IPlatformPhysics
{
    private PlatformMovement _platform;
    private Animator _animator;

    public bool isOnGround => R.Player.Attribute.isOnGround;
    [Header("当前速度")] private Vector2? currentSpeed;
    [Header("是否暂停")] [SerializeField] public bool isPause;
    private Vector2? nextSpeed;

    public Vector2 velocity
    {
        get => GetCurrentSpeed();
        set => SetSpeed(value);
    }

    public Vector2 position
    {
        get => _platform.position;
        set => _platform.position = value;
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _platform = GetComponent<PlatformMovement>();
        GameEvent.WorldTimeFrozenEvent.Register(ClipFrozen);
        GameEvent.WorldTimeResumeEvent.Register(ClipResume);
    }

    /// <summary>
    /// 获取当前速度
    /// </summary>
    /// <returns></returns>
    public Vector2 GetCurrentSpeed()
    {
        Vector2? vector = currentSpeed;
        return vector ?? _platform.velocity;
    }

    private void FixedUpdate()
    {
        if (isPause) return;
        Vector2? vector = currentSpeed;
        if (vector != null)
        {
            _platform.velocity = currentSpeed.Value;
            currentSpeed = null;
        }
    }

    /// <summary>
    /// 设置移动速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetSpeed(Vector2 speed)
    {
        if (isPause)
            currentSpeed = speed;
        else
            _platform.velocity = speed;
    }

    public void NextPosition(Vector3 nextPos)
    {
        _platform.position = nextPos;
    }

    /// <summary>
    /// 片段冻结
    /// </summary>
    /// <param name="obj"></param>
    private void ClipFrozen(object obj)
    {
        FrozenArgs value = (FrozenArgs)obj;
        switch (value.Type)
        {
            case FrozenArgs.FrozenType.Enemy: return;
            case FrozenArgs.FrozenType.Target:
                if (gameObject != value.Target)
                    return;
                break;
        }

        if (isPause) return;
        isPause = true;
        currentSpeed = _platform.velocity;
        _platform.velocity = Vector2.zero;
        _platform.isKinematic = true;
        _animator.speed = 0;
    }

    /// <summary>
    /// 片段恢复
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="e"></param>
    private void ClipResume(object obj)
    {
        FrozenArgs value = (FrozenArgs)obj;
        switch (value.Type)
        {
            case FrozenArgs.FrozenType.Enemy: return;
            case FrozenArgs.FrozenType.Target:
                if (gameObject != value.Target)
                    return;
                break;
        }

        if (isPause) return;
        if (!isPause) return;
        _platform.isKinematic = false;
        _animator.speed = 1;
        isPause = false;
    }
}