using System;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 弥诺陶洛斯BOSS
/// </summary>
public class MinotaurAction : EnemyBaseAction, IComponents
{
    private MinotaurComponent _c;

    public void InitAwake(Object o)
    {
        _c = o as MinotaurComponent;
        atkBox = _c.atkBox;
    }

    protected override void Start()
    {
        stateMachine.AddStates(typeof(EnemyStaEnum).ToArray());
        stateMachine.OnEnter += OnMyStateEnter;
        stateMachine.OnTransfer += OnStateTransfer;
        ChangeState(EnemyStaEnum.Idle);
    }

    private void OnStateTransfer(object sender, TransferEventArgs args)
    {
        GetComponent<EnemyBaseHurt>().StopFollowLeftHand(); //停止跟随左手
        atkBox.GetComponent<Collider2D>().enabled = true;
        if (ExitAtkSta(args.lastState, args.nextState)) //是否退出战斗状态
        {
            _c.attribute.paBody = false;
            _c.atkBox.localScale = Vector3.zero;
        }

        if (EnterAtkSta(args.lastState, args.nextState)) //是否进入战斗状态
        {
            _c.enemyAtk.atkId = Incrementor.GetNextId();
        }

        //敌人浮空顿住
        if (args.nextState == EnemyStaEnum.HitToFly3.ToString())
        {
            Vector2 currentSpeed = eAttr.timeController.GetCurrentSpeed();
            currentSpeed.y = 0f;
            _c.timeController.SetSpeed(currentSpeed);
            _c.timeController.SetGravity(0f);
        }

        //默认状态
        if (args.nextState.IsInArray(NormalSta))
        {
            _c.timeController.SetGravity(1f);
        }

        //敌人跳起来(原本是远程发动攻击)
        if (args.nextState == EnemyStaEnum.Jump.ToString()) //|| args.nextState == State.PaoAtk3
        {
            _c.timeController.SetSpeed(Vector2.zero);
            _c.timeController.SetGravity(0f);
        }
    }

    private void OnMyStateEnter(object sender, StateEventArgs e)
    {
        //$"{gameObject.name}切换状态{e.state}".Log();
        EnemyStaEnum enemySta = e.state.ToEnum<EnemyStaEnum>();
        switch (enemySta)
        {
            case EnemyStaEnum.Run:
            case EnemyStaEnum.Idle:
            case EnemyStaEnum.Jumping:
            case EnemyStaEnum.Falling:
                _c.enemyAnim.Play(enemySta.ToString(), true, false, eAttr.atkSpeed);
                break;
            case EnemyStaEnum.Atk1:
            case EnemyStaEnum.Hurt:
            case EnemyStaEnum.Atk2:
            case EnemyStaEnum.Atk3:
            case EnemyStaEnum.Atk4:
            case EnemyStaEnum.Death:
            case EnemyStaEnum.Land:
            case EnemyStaEnum.AtkRemote:
            case EnemyStaEnum.Jump:
            case EnemyStaEnum.Fall:
            case EnemyStaEnum.SlammedOn:
            case EnemyStaEnum.HitToFly1:
            case EnemyStaEnum.FallHitGround:
            case EnemyStaEnum.HitToFly3:
            case EnemyStaEnum.HitToFly2:
                _c.enemyAnim.Play(enemySta.ToString(), false, true, eAttr.atkSpeed);
                break;
            default:
                throw new ArgumentOutOfRangeException($"未添加动画状态{e.state}");
        }
    }

    public override void Attack1(int dir)
    {
        if (eAttr.isDead) return;
        if (!IsInNormalState()) return;
        ChangeFace(dir);
        ChangeState(EnemyStaEnum.Atk1);
    }

    public override bool IsInNormalState()
    {
        return stateMachine.currentState.IsInArray(NormalSta) && base.IsInNormalState();
    }

    public override bool IsInDeadState(string state)
    {
        return state.IsInArray(DeadSta);
    }

    public override void AnimReady()
    {
        ChangeState(EnemyStaEnum.Idle);
    }

    public override void AnimMove()
    {
        ChangeState(EnemyStaEnum.Run);
    }

    public override bool IsInWeakSta()
    {
        return eAttr.inWeakState;
    }

    public override bool IsInAttackState()
    {
        return stateMachine.currentState.IsInArray(AttackSta);
    }

    protected override bool EnterAtkSta(string lastState, string nextState)
    {
        return nextState.IsInArray(AttackSta) && !lastState.IsInArray(AttackSta);
    }

    protected override bool ExitAtkSta(string lastState, string nextState)
    {
        return !nextState.IsInArray(AttackSta) && lastState.IsInArray(AttackSta);
    }

    /// <summary>
    /// 攻击状态
    /// </summary>
    public static readonly string[] AttackSta =
    {
        EnemyStaEnum.Atk1.ToString(),
        EnemyStaEnum.Atk2.ToString(),
        EnemyStaEnum.Atk3.ToString(),
        EnemyStaEnum.Atk4.ToString(),
        EnemyStaEnum.AtkRemote.ToString(),
    };

    /// <summary>
    /// 默认状态
    /// </summary>
    public static readonly string[] NormalSta =
    {
        EnemyStaEnum.Idle.ToString(),
        EnemyStaEnum.Jump.ToString(),
        EnemyStaEnum.Run.ToString(),
    };

    /// <summary>
    /// 死亡状态
    /// </summary>
    public static readonly string[] DeadSta =
    {
        EnemyStaEnum.Death.ToString(),
    };
}