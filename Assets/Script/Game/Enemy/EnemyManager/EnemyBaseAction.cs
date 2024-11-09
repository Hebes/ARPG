using System;
using UnityEngine;

/// <summary>
/// 敌人基础行动
/// </summary>
public abstract class EnemyBaseAction : MonoBehaviour
{
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public EnemyAttribute eAttr;
    [SerializeField] protected Animator weakEffect; //虚弱效果
    [SerializeField] protected GameObject weakThunder; //虚弱动画
    [SerializeField] public Transform hurtBox;
    [SerializeField] protected Transform atkBox;
    protected EnemyAnimationController enemyAnim;

    public Transform player => R.Player.Transform;

    /// <summary>
    /// 当前是否处于硬直状态
    /// </summary>
    private bool inStiff => eAttr.stiffTime > 0f;

    protected virtual void Awake()
    {
        enemyAnim = GetComponentInChildren<EnemyAnimationController>();
        eAttr = GetComponent<EnemyAttribute>();
        stateMachine = GetComponent<StateMachine>();
    }

    protected abstract void Start();

    protected virtual void Update()
    {
        eAttr.stiffTime = Mathf.Clamp(eAttr.stiffTime - Time.deltaTime, 0f, float.PositiveInfinity);
        if (QTE)
        {
            QTE.transform.localScale = (transform.localScale.x >= 0f ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f));
        }

        if (IsInWeakSta() && weakEffectShow)
        {
            QTEAnim(CurrentCanBeExecute() ? "Show" : "Null");
        }

        if (!IsInNormalState())
        {
            isAutoMoveing = false;
        }

        if (!isAutoMoveing || WorldTime.I.IsFrozen)
        {
            return;
        }

        float y = 0f;
        if (eAttr.iCanFly)
        {
            float distance = Physics2D.Raycast(transform.position + Vector3.right * eAttr.faceDir, -Vector2.up, 1000f, LayerManager.GroundMask)
                .distance;
            if (Mathf.Abs(distance - eAttr.flyHeight) > 0.1f)
            {
                float num = Mathf.Abs(distance - eAttr.flyHeight) / (1f / eAttr.moveSpeed);
                y = num * Time.deltaTime * Mathf.Sign(eAttr.flyHeight - distance);
            }
        }

        float num2 = Mathf.Abs(eAttr.moveSpeed * Time.deltaTime);
        transform.position += new Vector3(num2 * eAttr.faceDir, y, 0f);
        if (!eAttr.iCanFly)
        {
            SetToGroundPos();
        }
    }

    public void ChangeState(string state, float speed = 1f)
    {
        if (eAttr.isDead && !IsInDeadState(state)) return;
        animSpeed = speed;
        stateMachine.SetState(state);
    }

    public void ChangeState(Enum nextState, float speed = 1f)
    {
        ChangeState(nextState.ToString(), speed);
    }

    /// <summary>
    /// 是否处于正常状态
    /// </summary>
    /// <returns></returns>
    public virtual bool IsInNormalState()
    {
        //没有死亡 没有处于硬直状态 没有处于虚弱状态
        return !eAttr.isDead && !inStiff && !IsInWeakSta();
    }

    /// <summary>
    /// 当前可执行
    /// </summary>
    /// <returns></returns>
    public bool CurrentCanBeExecute()
    {
        bool flag = IsInWeakSta() && (eAttr.rankType == EnemyAttribute.RankType.Normal ||
                                      (eAttr.rankType != EnemyAttribute.RankType.Normal && eAttr.isOnGround));
        bool flag2;
        if (R.Player.Attribute.isOnGround)
        {
            flag2 = eAttr.accpectExecute && (eAttr.isOnGround || (!eAttr.isOnGround && eAttr.timeController.GetCurrentSpeed().y < 0f)) &&
                    !eAttr.isDead;
        }
        else
        {
            flag2 = eAttr.accpectAirExecute && !eAttr.isDead;
        }

        bool flag3 = Vector2.Distance(transform.position, R.Player.Transform.position) <= 4f;
        return flag && flag2 && flag3;
    }

    public abstract bool IsInDeadState(string state);

    /// <summary>
    /// 切换朝向
    /// </summary>
    /// <param name="dir"></param>
    public void ChangeFace(int dir)
    {
        if (eAttr.faceDir == dir) return;
        eAttr.faceDir = dir;

        if (dir != -1)
        {
            if (dir == 1)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1f);
            }
        }
        else
        {
            transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1f);
        }
    }

    /// <summary>
    /// 面对玩家
    /// </summary>
    public void FaceToPlayer()
    {
        int dir = InputSetting.JudgeDir(transform.position, player.position);
        ChangeFace(dir);
    }

    /// <summary>
    /// 是否转向
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public bool TurnRound(int dir)
    {
        if (!IsInNormalState()) return false;
        ChangeFace(dir);
        return true;
    }

    public virtual void KillSelf()
    {
        if (eAttr.rankType == EnemyAttribute.RankType.Normal)
        {
            eAttr.currentHp = 0;
        }
    }

    public void SetToGroundPos()
    {
        if (eAttr.isOnGround) return;
        transform.position = transform.position.SetY(LayerManager.YNum.GetGroundHeight(gameObject) + 0.2f);
    }

    /// <summary>
    /// 自动移动
    /// </summary>
    /// <returns></returns>
    public bool AutoMove()
    {
        if (isAutoMoveing) return true;
        if (IsInNormalState())
        {
            isAutoMoveing = true;
            AnimMove();
            return true;
        }

        return false;
    }

    public bool StopMoveToIdle()
    {
        isAutoMoveing = false;
        if (IsInNormalState())
        {
            AnimReady();
            return true;
        }

        return false;
    }

    public void AppearAtPosition(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, LayerManager.ZNum.MMiddleE(eAttr.rankType));
        FaceToPlayer();
    }

    /// <summary>
    /// 显示效果
    /// </summary>
    /// <param name="pos"></param>
    public void AppearEffect(Vector2 pos)
    {
        float num = 3.18f;
        R.Effect.Generate(0, null, new Vector3(pos.x, pos.y + num, LayerManager.ZNum.Fx), Vector3.zero);
        R.Audio.PlayEffect(123, transform.position);
    }

    /// <summary>
    /// 动画做好了准备
    /// </summary>
    public abstract void AnimReady();

    public abstract void AnimMove();

    public virtual void Attack1(int dir)
    {
        if (eAttr.isDead) return;
        if (!IsInNormalState()) return;
        ChangeFace(dir);
    }

    public virtual void Attack2(int dir)
    {
        if (eAttr.isDead) return;
        if (!IsInNormalState()) return;
        ChangeFace(dir);
    }

    public virtual void Attack3(int dir)
    {
        if (eAttr.isDead) return;
        if (!IsInNormalState()) return;
        ChangeFace(dir);
    }

    public virtual void AtkRemote(int dir)
    {
        if (eAttr.isDead) return;
        if (!IsInNormalState()) return;
        ChangeFace(dir);
    }

    /// <summary>
    /// 反击
    /// </summary>
    /// <param name="dir"></param>
    public virtual void CounterAttack(int dir)
    {
    }

    public virtual void Idle1()
    {
    }

    public virtual void Idle2()
    {
        Idle1();
    }

    public virtual void Idle3()
    {
        Idle1();
    }

    /// <summary>
    /// 防御
    /// </summary>
    public virtual void Defence()
    {
        eAttr.timeController.SetSpeed(Vector2.zero);
        eAttr.currentDefence = 0;
        eAttr.startDefence = false;
    }

    public virtual void DefenceSuccess()
    {
    }

    /// <summary>
    /// 闪避
    /// </summary>
    public virtual void SideStep()
    {
        FaceToPlayer();
        eAttr.currentSideStep = 0;
    }

    public void QTEAnim(string anim)
    {
        $"显示终结文字{anim}".Log();
        // QTESetSkin();
        // if (!QTE || QTE.AnimationName == anim)return;
        // QTESetText(anim);
        // QTE.state.SetAnimation(0, anim, true);
        // QTE.skeleton.SetToSetupPose();
    }

    /// <summary>
    /// 退出弱状态
    /// </summary>
    /// <param name="forceQuit"></param>
    public void ExitWeakState(bool forceQuit = false)
    {
        if (!eAttr.accpectExecute) return;
        if (eAttr.isDead) return;
        if (!eAttr.willBeExecute || forceQuit)
        {
            eAttr.inWeakState = false;
            WeakEffectDisappear("RollEnd");
        }
    }

    /// <summary>
    /// 弱效果消失
    /// </summary>
    /// <param name="disappear">消失的若状态</param>
    public void WeakEffectDisappear(string disappear)
    {
        return;
        // if (!eAttr.accpectExecute) return;
        // if (weakThunder)weakThunder.SetActive(false);
        // if (weakEffect)
        // {
        //     weakEffect.state.SetAnimation(0, disappear, false);
        //     weakEffect.skeleton.SetSlotsToSetupPose();
        //     weakEffect.Update(0f);
        // }
        //
        // weakEffectShow = false;
        // QTEAnim("Null");
    }

    /// <summary>
    /// 进入弱状态
    /// </summary>
    public virtual void EnterWeakState()
    {
        if (!eAttr.accpectExecute) return;
        eAttr.inWeakState = true;
        WeakEffectAppear();
    }

    /// <summary>
    /// 弱效果出现
    /// </summary>
    protected virtual void WeakEffectAppear()
    {
        if (!eAttr.accpectExecute) return;
        if (eAttr.isDead) return;
        R.Audio.PlayEffect(104, transform.position);
        if (weakThunder)
        {
            weakThunder.SetActive(true);
        }

        if (weakEffect)
        {
            // weakEffect.state.SetAnimation(0, "Roll", true);
            // weakEffect.skeleton.SetSlotsToSetupPose();
            // weakEffect.Update(0f);
        }

        weakEffectShow = true;
        QTEAnim("Show");
    }

    /// <summary>
    /// 放在动画中当事件
    /// </summary>
    /// <param name="effectId"></param>
    public void PlayEffect(int effectId)
    {
        if (!R.Effect.fxData.ContainsKey(effectId))
        {
            $"敌人 {name} 尝试播放 第.{effectId} 效果, 但是不存在".Warning();
            return;
        }

        EnemyAtkEffector component = R.Effect.fxData[effectId].effect.GetComponent<EnemyAtkEffector>();
        Vector3 position = new Vector3(component.pos.x * transform.localScale.x, component.pos.y, component.pos.z);
        R.Effect.Generate(effectId, transform, position);
    }

    public virtual bool IsInDefenceState()
    {
        return false;
    }

    public virtual bool IsInSideStepState()
    {
        return false;
    }

    /// <summary>
    /// 处于弱状态
    /// </summary>
    /// <returns></returns>
    public abstract bool IsInWeakSta();

    public abstract bool IsInAttackState();

    protected abstract bool EnterAtkSta(string lastState, string nextState);

    protected abstract bool ExitAtkSta(string lastState, string nextState);

    public virtual bool IsInIdle()
    {
        return false;
    }

    public virtual void AnimExecute()
    {
        Vector3 position = transform.position;
        position.y = LayerManager.YNum.GetGroundHeight(gameObject);
        transform.position = position;
    }

    public virtual void AnimQTEHurt()
    {
        Vector3 position = transform.position;
        position.y = LayerManager.YNum.GetGroundHeight(gameObject);
        transform.position = position;
    }

    public const int LEFT = -1;
    public const int RIGHT = 1;
    public const int STOP = 0;


    /// <summary>
    /// 终结的字样
    /// </summary>
    [SerializeField] protected Animator QTE;

    [SerializeField] private TextMesh _qteTextMesh;


    protected float animSpeed;
    protected float startDefenceTime;
    protected float defenceTime;

    /// <summary>
    /// 弱效是否显示
    /// </summary>
    private bool weakEffectShow;

    /// <summary>
    /// 是否可以自动移动
    /// </summary>
    public bool isAutoMoveing;

    private Transform shadow;
}