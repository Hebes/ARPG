using System;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 怪物的时间控制器
/// </summary>
public class TimeController : MonoBehaviour, IPlatformPhysics
{
    private EnemyAttribute _eAttr;
    private Rigidbody2D _rigid;
    private Animator _animator;

    private void Awake()
    {
        Transform tr = transform;
        _animator = transform.FindComponent<Animator>();
        _rigid = transform.FindComponent<Rigidbody2D>();
        _eAttr = transform.FindComponent<EnemyAttribute>();
        
        GameEvent.WorldTimeFrozenEvent.Register(ClipFrozen);
        GameEvent.WorldTimeResumeEvent.Register(ClipResume);
    }

    public Vector2 GetCurrentSpeed()
    {
        Vector2? vector = currentSpeed;
        return vector ?? _rigid.velocity;
        
    }

    private void FixedUpdate()
    {
        if (isPause) return;
        Vector2? vector = currentSpeed;
        if (vector != null)
        {
            _rigid.velocity = currentSpeed.Value;
            currentSpeed = null;
        }

        float? num = currentGravity;
        if (num != null)
        {
            _rigid.gravityScale = currentGravity.Value;
            currentGravity = null;
        }
    }

    /// <summary>
    /// 设置速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetSpeed(Vector2 speed)
    {
        if (isPause)
            currentSpeed = speed;
        else
            _rigid.velocity = speed;
    }

    /// <summary>
    /// 设置重力
    /// </summary>
    /// <param name="scale"></param>
    public void SetGravity(float scale)
    {
        if (isPause)
            currentGravity = scale;
        else
            _rigid.gravityScale = scale;
    }

    /// <summary>
    /// 片段冻结
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void ClipFrozen(object obj)
    {
        FrozenArgs value = (FrozenArgs)obj;
        switch (value.Type)
        {
            case FrozenArgs.FrozenType.Player:
                return;
            case FrozenArgs.FrozenType.All:
            case FrozenArgs.FrozenType.Enemy:
                break;
            case FrozenArgs.FrozenType.Target:
                if (gameObject != value.Target)
                    return;
                break;
        }

        if (isPause) return;
        isPause = true;
        currentSpeed = _rigid.velocity;
        currentGravity = _rigid.gravityScale;
        _rigid.gravityScale = 0f;
        _rigid.velocity = Vector2.zero;
        _animator.speed = 0;
    }

    private void ClipResume(object obj)
    {
        if (!isPause) return;
        _animator.speed = 1;
        isPause = false;
    }

    public Vector2 velocity
    {
        get => GetCurrentSpeed();
        set => SetSpeed(value);
    }

    public Vector2 position
    {
        get => _rigid.position;
        set => _rigid.position = value;
    }

    /// <summary>
    /// 是否在地上
    /// </summary>
    public bool isOnGround => _eAttr.isOnGround;

    /// <summary>
    /// 当前重力
    /// </summary>
    public float? currentGravity;

    /// <summary>
    /// 当前速度
    /// </summary>
    public Vector2? currentSpeed;

    /// <summary>
    /// 是否暂停
    /// </summary>
    [SerializeField] public bool isPause;

    /// <summary>
    /// 下一次的速度
    /// </summary>
    private Vector2? nextSpeed;

  
}