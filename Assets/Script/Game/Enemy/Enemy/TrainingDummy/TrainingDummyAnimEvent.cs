using UnityEngine;

public class TrainingDummyAnimEvent : MonoBehaviour
{
    #region 组件

    private Transform Player => R.Player.Transform;
    private EnemyAtk _atk;
    private OnionCreator _onion;
    private Rigidbody2D _rigidbody2D;
    private TrainingDummyAction _eAction;
    private EnemyAttribute _eAttr;
    private Animator _animator;

    private void GetComponent()
    {
        _eAction = GetComponent<TrainingDummyAction>();
        _eAttr = GetComponent<EnemyAttribute>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    #endregion

    private void Awake()
    {
        GetComponent();
        
         _animator.AddAnimatorEvent(EnemyStaEnum.Hurt.ToString(), 4, nameof(BackToIdle));
    }
    
    private void Update()
    {
        return;
        if (_eAttr.isDead)return;
        if (_rollAttackFly && _eAttr.isOnGround && _rigidbody2D.velocity.y <= 0f)
        {
            _rollAttackFly = false;
            _eAction.ChangeState(EnemyStaEnum.HitGround);
        }
        if (_eAttr.isFlyingUp)
        {
            bool flag = maxFlyHeight > 0f && _eAttr.height >= maxFlyHeight;
            if (flag)
            {
                Vector2 currentSpeed = _eAttr.timeController.GetCurrentSpeed();
                currentSpeed.y = 0f;
                _eAttr.timeController.SetSpeed(currentSpeed);
            }
            if (_rigidbody2D.velocity.y <= 0f)
            {
                _eAttr.isFlyingUp = false;
                _eAction.ChangeState(EnemyStaEnum.HitToFly3);
            }
        }
        if (_eAttr.checkHitGround && _eAttr.isOnGround)
        {
            _eAttr.checkHitGround = false;
            R.Effect.Generate(6, transform);
            if (_eAction.stateMachine.currentState == "HitFall")
            {
                maxFlyHeight = 4f;
                _eAttr.timeController.SetSpeed(Vector2.up * 25f);
                _eAction.ChangeState(EnemyStaEnum.HitToFly1, 1f);
            }
            else
            {
                _eAction.ChangeState(EnemyStaEnum.FallHitGround, 1f);
            }
        }
    }
    
    /// <summary>
    /// 回到闲置状态
    /// </summary>
    public void BackToIdle()
    {
        if (_eAction.IsInWeakSta())
            _eAttr.enterWeakMod = false;
        _eAction.ChangeState(EnemyStaEnum.Idle);
    }

    /// <summary>
    /// 最大飞行高度
    /// </summary>
    public float maxFlyHeight;
    
    /// <summary>
    /// 滚动攻击在天上
    /// </summary>
    private bool _rollAttackFly;
}