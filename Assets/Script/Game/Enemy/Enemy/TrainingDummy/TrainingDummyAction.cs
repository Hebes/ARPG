using System;
using UnityEngine;

public class TrainingDummyAction : EnemyBaseAction
{
    protected override void Start()
    {
        stateMachine.AddStates(typeof(EnemyStaEnum).ToArray());
        stateMachine.OnEnter += OnMyStateEnter;
        stateMachine.OnTransfer += OnStateTransfer;
        ChangeState(EnemyStaEnum.Idle);
        ChangeFace(-1);
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
        bool bool1 = nextState.IsInArray(AttackSta);
        bool bool2 = lastState.IsInArray(AttackSta);
        return bool1 && !bool2;
    }

    protected override bool ExitAtkSta(string lastState, string nextState)
    {
        return !nextState.IsInArray(AttackSta) && lastState.IsInArray(AttackSta);
    }

    /// <summary>
    /// 状态进入，切换动画
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void OnMyStateEnter(object sender, StateEventArgs args)
    {
        $"{gameObject.name}切换状态{args.state}".Log();
        EnemyStaEnum enemySta = args.state.ToEnum<EnemyStaEnum>();
        switch (enemySta)
        {
            case EnemyStaEnum.Run:
            case EnemyStaEnum.Idle:
            case EnemyStaEnum.Jumping:
            case EnemyStaEnum.Falling:
                enemyAnim.Play(enemySta.ToString(), true, false, eAttr.atkSpeed);
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
                enemyAnim.Play(enemySta.ToString(), false, true, eAttr.atkSpeed);
                break;
            default:
                throw new Exception("请添加类型");
        }
    }

    /// <summary>
    /// 状态切换后执行的
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void OnStateTransfer(object sender, TransferEventArgs args)
    {
        GetComponent<EnemyBaseHurt>().StopFollowLeftHand(); //停止跟随左手
        if (ExitAtkSta(args.lastState, args.nextState)) //是否退出战斗状态
        {
            eAttr.paBody = false;
            atkBox.localScale = Vector3.zero;
        }


        //浮空
        if (args.nextState == EnemyStaEnum.HitToFly3.ToString())
        {
            Vector2 currentSpeed = eAttr.timeController.GetCurrentSpeed();
            currentSpeed.y = 0f;
            eAttr.timeController.SetSpeed(currentSpeed);
            eAttr.timeController.SetGravity(0f);
        }

        if (args.lastState == EnemyStaEnum.HitToFly3.ToString() && !eAttr.willBeExecute)
        {
            eAttr.timeController.SetGravity(1f);
        }

        //默认状态
        if (args.nextState.IsInArray(NormalSta))
        {
            eAttr.timeController.SetGravity(1f);
        }

        if (args.nextState == EnemyStaEnum.Jump.ToString()) //|| args.nextState == State.PaoAtk3
        {
            eAttr.timeController.SetSpeed(Vector2.zero);
            eAttr.timeController.SetGravity(0f);
        }
    }

    /// <summary>
    /// 默认状态
    /// </summary>
    public static string[] NormalSta =
    {
        EnemyStaEnum.Idle.ToString(),
        EnemyStaEnum.Jump.ToString(),
        EnemyStaEnum.Run.ToString(),
    };

    /// <summary>
    /// 死亡状态
    /// </summary>
    public static string[] DeadSta =
    {
        EnemyStaEnum.Death.ToString(),
    };

    /// <summary>
    /// 攻击状态
    /// </summary>
    public static string[] AttackSta =
    {
        EnemyStaEnum.Atk1.ToString(),
        EnemyStaEnum.Atk2.ToString(),
        EnemyStaEnum.Atk3.ToString(),
        EnemyStaEnum.Atk4.ToString(),
        EnemyStaEnum.AtkRemote.ToString(),
    };
}