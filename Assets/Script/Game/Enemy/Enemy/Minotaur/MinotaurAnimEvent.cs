using LitJson;
using UnityEngine;

/// <summary>
/// 弥诺陶洛斯BOSS事件
/// </summary>
public class MinotaurAnimEvent : MonoBehaviour
{
    private void Awake()
    {
        Transform tr = transform.parent;
        _atk = tr.FindComponent<EnemyAtk>();
        _onion = tr.FindComponent<OnionCreator>();
        _eAction = tr.FindComponent<MinotaurAction>();
        _eAttr = tr.FindComponent<EnemyAttribute>();
        _rigidbody2D = tr.FindComponent<Rigidbody2D>();
        _animator = tr.FindComponent<Animator>();
         timeController=   tr.FindComponent<TimeController>();
        //动画监听
        _animator.UnAnimatorAddEventAll();
        
        _animator.AddAnimatorEvent(EnemyStaEnum.Atk1.ToString(),0,nameof(SetAtkData));
        _animator.AddAnimatorEvent(EnemyStaEnum.Atk1.ToString(), 1, nameof(PlaySound), 53);
        _animator.AddAnimatorEvent(EnemyStaEnum.Atk1.ToString(), 8, nameof(PlayAnim), EnemyStaEnum.Idle.ToString());
        
        return;
        _animator.AddAnimatorEvent(EnemyStaEnum.Hurt.ToString(), 3, nameof(BackToIdle));
        
        _animator.AddAnimatorEvent(EnemyStaEnum.Death.ToString(), 0, nameof(DieBlock));
        _animator.AddAnimatorEvent(EnemyStaEnum.Death.ToString(), 5, nameof(RealDestroy));
        
        _animator.AddAnimatorEvent(EnemyStaEnum.HitToFly1.ToString(), 5, nameof(FlyUp));
        
        _animator.AddAnimatorEvent(EnemyStaEnum.HitToFly3.ToString(), 0, nameof(AirPhysic),0f);
        _animator.AddAnimatorEvent(EnemyStaEnum.HitToFly3.ToString(), 7, nameof(PhysicReset));//下面Update在检测高度
        _animator.AddAnimatorEvent(EnemyStaEnum.HitToFly3.ToString(), 7, nameof(HitGround));//下面Update在检测高度
        
        _animator.AddAnimatorEvent(EnemyStaEnum.FallHitGround.ToString(), 0, nameof(PlayHitGroundSound));
        _animator.AddAnimatorEvent(EnemyStaEnum.FallHitGround.ToString(), 5, nameof(GetUp));
        
        _animator.AddAnimatorEvent(EnemyStaEnum.Fall.ToString(), 0, nameof(HitGround));
    }

    private void Start()
    {
        _atkData = DB.PlayerHurtData[EnemyType.弥诺陶洛斯.ToString()];
    }

    private void Update()
    {
        if (_eAttr.isDead) return;
        if (_eAttr.isFlyingUp)
        {
            bool flag = maxFlyHeight > 0f && _eAttr.height >= maxFlyHeight;//到了最大高度
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
        
        if (_eAttr.checkHitGround && _eAttr.isOnGround)//检查击中地面,是否在地面
        {
            _eAttr.checkHitGround = false;
            R.Effect.Generate(6, transform);
            if (_eAction.stateMachine.currentState == "HitFall")
            {
                "暂时无用".Log();
                // maxFlyHeight = 4f;
                // _eAttr.timeController.SetSpeed(Vector2.up * 25f);
                //_eAction.ChangeState(EnemyStaEnum.HitToFly1, 1f);
            }
            else
            {
                _eAction.ChangeState(EnemyStaEnum.FallHitGround);//原本需要起立啥的动画但是都没有，直接idle
            }
        }
    }

    public void PlaySound(int index)
    {
        R.Audio.PlayEffect(index, transform.position);
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

    public void PlayAnim(string sta)
    {
        _eAction.ChangeState(sta);
    }

    public void SetAtkData()
    {
        _atk.atkData = _atkData[_eAction.stateMachine.currentState];
    }

    public void DieBlock()
    {
        R.Effect.Generate(163, null, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f), Vector3.zero);
    }
    
    public void FlyUp()
    {
        _eAttr.isFlyingUp = true;
    }
    
    public void HitGround()
    {
        _eAttr.checkHitGround = true;
    }

    public void RealDestroy()
    {
        Destroy(gameObject);
    }
    
    /// <summary>
    /// 空中的物理
    /// </summary>
    /// <param name="gravityScale"></param>
    public void AirPhysic(float gravityScale)
    {
        timeController.SetSpeed(Vector2.zero);
        _rigidbody2D.gravityScale = gravityScale;
    }
    
    /// <summary>
    /// 重置物理
    /// </summary>
    public void PhysicReset()
    {
        _rigidbody2D.WakeUp();
        _rigidbody2D.gravityScale = 1f;
    }
    
    /// <summary>
    /// 起立
    /// </summary>
    public void GetUp()
    {
        if (_eAttr.isDead)
        {
            DieBlock();
            DestroySelf();
        }
        else
        {
            _eAction.ChangeState(EnemyStaEnum.Idle);
        }
    }
    
    /// <summary>
    /// 用于事件回调
    /// </summary>
    public void DestroySelf()
    {
        RealDestroy();
    }
    
    public void PlayHitGroundSound()
    {
        //R.Audio.PlayEffect(hitGroundSound[Random.Range(0, hitGroundSound.Length)], transform.position);
    }

    //初始化获取
    private JsonData _atkData;
    private EnemyAttribute _eAttr;
    private MinotaurAction _eAction;
    private EnemyAtk _atk;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private TimeController timeController;
    private OnionCreator _onion;
    
    //变量
    public float maxFlyHeight=2.5f;
}