using System;
using UnityEngine;

/// <summary>
/// 女猎手行动
/// </summary>
public class HuntressAction : EnemyBaseAction
{
    #region 组件

    private void GetComponent()
    {
        atkBox = transform.FindChildByName("AtkBox 攻击区");
    }

    #endregion

    protected override void Start()
    {
        GetComponent();
        stateMachine.AddStates(typeof(EnemyStaEnum).ToArray());
        stateMachine.OnEnter += OnMyStateEnter;
        stateMachine.OnTransfer += OnStateTransfer;
        ChangeState(EnemyStaEnum.Idle);
        ChangeFace(-1);
    }

    private void FixedUpdate()
    {
        //收到伤害的话
        if (stateMachine.currentState == EnemyStaEnum.Hurt.ToString())
        {
            Vector2 currentSpeed = eAttr.timeController.GetCurrentSpeed();
            float f = currentSpeed.x;
            f = Mathf.Clamp(Mathf.Abs(f) - airFric * Time.fixedDeltaTime, 0f, float.MaxValue) * Mathf.Sign(f);
            currentSpeed.x = Mathf.Clamp(Mathf.Abs(f) - airFric * Time.fixedDeltaTime, 0f, float.PositiveInfinity) * Mathf.Sign(f);
            eAttr.timeController.SetSpeed(currentSpeed);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (stateMachine.currentState == EnemyStaEnum.Hurt.ToString())
        {
            Vector2 currentSpeed = eAttr.timeController.GetCurrentSpeed();
            if (currentSpeed.y > 0f)
            {
                currentSpeed.y = 0f;
                eAttr.timeController.SetSpeed(currentSpeed);
            }
        }
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
                EnemyAnim.Play(enemySta.ToString(), true, false, eAttr.atkSpeed);
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
                EnemyAnim.Play(enemySta.ToString(), false, true, eAttr.atkSpeed);
                break;
            default:
                throw new ArgumentOutOfRangeException();
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
        atkBox.GetComponent<Collider2D>().enabled = true;
        if (ExitAtkSta(args.lastState, args.nextState)) //是否退出战斗状态
        {
            eAttr.paBody = false;
            atkBox.localScale = Vector3.zero;
        }

        if (args.nextState == "DaoAtk2")
        {
            //GetComponent < HuntressEnemyAnimListener DaoEnemyAnimListener > ().Atk2Start();
        }

        if (EnterAtkSta(args.lastState, args.nextState))
        {
            GetComponentInChildren<EnemyAtk>().atkId = Incrementor.GetNextId();
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
    /// 是否是正常状态
    /// </summary>
    /// <returns></returns>
    public override bool IsInNormalState()
    {
        return stateMachine.currentState.IsInArray(NormalSta) && base.IsInNormalState();
    }

    public override bool IsInDeadState(string state)
    {
        return state.IsInArray(DeadSta);
    }

    /// <summary>
    /// 翻滚到等待
    /// </summary>
    public void RollToIdle()
    {
        // base.AnimChangeState(StateEnum.DaoAtk2HitBack, 1f);
        // GetComponent<EnemyDaoHurt>().SetHitSpeed(new Vector2(eAttr.faceDir * -4, 15f));
    }

    public void Jump()
    {
        if (eAttr.isDead) return;
        if (!IsInNormalState()) return;
        ChangeState(EnemyStaEnum.Jump);
    }

    public void JumpBack()
    {
        if (eAttr.isDead)return;
        if (!IsInNormalState())return;
        eAttr.timeController.SetSpeed(Vector2.zero);
        eAttr.timeController.SetGravity(0f);
        ChangeState(EnemyStaEnum.JumpBack);
    }

    public override void Attack1(int dir)
    {
        if (eAttr.isDead) return;
        if (!IsInNormalState()) return;
        ChangeFace(dir);
        ChangeState(EnemyStaEnum.Atk1);
    }

    public override void Attack2(int dir)
    {
        if (eAttr.isDead) return;
        if (!IsInNormalState()) return;
        ChangeFace(dir);
        ChangeState(EnemyStaEnum.Atk2);
    }

    public override void Attack3(int dir)
    {
        if (eAttr.isDead) return;
        if (!IsInNormalState()) return;
        ChangeFace(dir);
        ChangeState(EnemyStaEnum.Atk3);
    }
    
    
    public override void AtkRemote(int dir)
    {
        if (eAttr.isDead) return;
        if (!IsInNormalState()) return;
        ChangeFace(dir);
        ChangeState(EnemyStaEnum.AtkRemote);
    }

    public override void Idle1()
    {
        if (!IsInNormalState()) return;
        ChangeState(EnemyStaEnum.Idle);
    }

    public override void AnimReady()
    {
        ChangeState(EnemyStaEnum.Idle);
    }

    public override void AnimMove()
    {
        ChangeState(EnemyStaEnum.Run);
    }

    /// <summary>
    /// 回避
    /// </summary>
    public override void SideStep()
    {
        "回避".Log();
        // if (eAttr.isDead)return;
        // if (IsInSideStepState())return;
        // if (!eAttr.isOnGround)return;
        // base.SideStep();
        // eAttr.timeController.SetSpeed(Vector2.zero);
        // base.AnimChangeState((!isPao) ? StateEnum.JumpBack : StateEnum.RunAwayReady, 1f);
    }

    /// <summary>
    /// 是否进入攻击状态
    /// </summary>
    /// <param name="lastState"></param>
    /// <param name="nextState"></param>
    /// <returns></returns>
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
    /// 处于攻击状态
    /// </summary>
    /// <returns></returns>
    public override bool IsInAttackState()
    {
        return stateMachine.currentState.IsInArray(AttackSta);
    }

    /// <summary>
    /// 处于弱状态
    /// </summary>
    /// <returns></returns>
    public override bool IsInWeakSta()
    {
        return eAttr.inWeakState;
    }

    public override bool IsInIdle()
    {
        return stateMachine.currentState.IsInArray(IdleSta);
    }

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

    /// <summary>
    /// 等待状态
    /// </summary>
    public static string[] IdleSta =
    {
        EnemyStaEnum.Idle.ToString(),
        EnemyStaEnum.Idle2.ToString(),
        EnemyStaEnum.Idle3.ToString(),
    };

    /// <summary>
    /// 死亡状态
    /// </summary>
    public static string[] DeadSta =
    {
        EnemyStaEnum.Death.ToString(),
    };

    /// <summary>
    /// 受伤状态
    /// </summary>
    public static string[] HurtSta =
    {
        EnemyStaEnum.Hurt.ToString(),
        EnemyStaEnum.Fall.ToString(),
    };

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
    /// 侧边脚步状态
    /// </summary>
    public static string[] SideStepSta =
    {
        "RunAway",
        "RunAwayReady",
        "RunAwayEnd",
        "JumpBack"
    };

    public bool isPao;

    public float airFric = 8f;

    public enum HuntressSta
    {
        Idle,
    }
}