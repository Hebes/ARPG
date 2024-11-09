using System.Collections;
using UnityEngine;

/// <summary>
/// 敌人女猎人受伤
/// </summary>
public class HuntressHurt : EnemyBaseHurt
{
    private HuntressAnimEvent _anim;


    protected override void Update()
    {
        base.Update();
        Vector2? vector = atkFollowPos;
        if (vector != null)
        {
            Vector3 position = player.transform.position;
            Vector2? vector2 = atkFollowPos;
            Vector3 position2 = position - (vector2 == null ? default : (Vector3)vector2.GetValueOrDefault());
            position2.y = Mathf.Clamp(position2.y, LayerManager.YNum.GetGroundHeight(gameObject) + 1f, float.PositiveInfinity);
            position2.z = LayerManager.ZNum.MMiddleE(eAttr.rankType);
            transform.position = position2;
            atkFollowTime += Time.unscaledDeltaTime;
            if (atkFollowTime >= atkFollowEnd)
                atkFollowPos = null;
        }
    }

    protected override void Start()
    {
        _anim = GetComponentInChildren<HuntressAnimEvent>();
        hurtData = DB.EnemyHurtData[EnemyType.女猎手.ToString()];
    }

    /// <summary>
    /// 设置命中速度
    /// </summary>
    /// <param name="speed"></param>
    public override void SetHitSpeed(Vector2 speed)
    {
        if (playerAtkName is "UpRising" or "AtkUpRising" or "AtkRollEnd" or "NewExecute2_1")
            _anim.maxFlyHeight = 4.5f;
        else
            _anim.maxFlyHeight = -1f;
        base.SetHitSpeed(speed);
    }

    public override void NormalHurt(EnemyHurtAtkEventArgs.PlayerNormalAtkData atkData, int atkId, HurtCheck.BodyType body, Vector2 hurtPos)
    {
        if (action.stateMachine.currentState.IsInArray(HuntressAction.SideStepSta)) return;
        base.NormalHurt(atkData, atkId, body, hurtPos);
    }

    private void RollHurt()
    {
        Vector2 vector = new Vector2(eAttr.faceDir * -8, 12f);
        PlayHurtAnim("HitToFly1", "HitToFly1", vector, vector);
    }

    protected override void SpAttack()
    {
        if (eAttr.currentActionInterruptPoint < eAttr.actionInterruptPoint)
        {
            return;
        }

        if (playerAtkName == "RollEnd")
        {
            Vector2? vector = atkFollowPos;
            if (vector != null)
            {
                atkFollowPos = null;
            }

            return;
        }

        if (playerAtkName == "RollGround")
        {
            Vector2? vector2 = atkFollowPos;
            if (vector2 == null)
            {
                Transform transform = player.GetComponentInChildren<PlayerAtk>().transform;
                atkFollowPos = (transform.position - this.transform.position) * 0.9f;
            }

            atkFollowTime = 0f;
            atkFollowEnd = 0.2f;
            return;
        }

        if (playerAtkName is "RollReady" or "BladeStormReady")
        {
            Vector2? vector3 = atkFollowPos;
            if (vector3 == null)
            {
                atkFollowPos = (player.transform.position - transform.position) * 0.7f;
            }

            atkFollowTime = 0f;
            atkFollowEnd = 0.2f;
            return;
        }

        if (playerAtkName == "Atk4")
        {
            StartCoroutine(CloseToPlayer());
            return;
        }

        atkFollowTime = 0f;
        atkFollowEnd = 0f;
    }

    /// <summary>
    /// 靠近玩家
    /// </summary>
    /// <returns></returns>
    private IEnumerator CloseToPlayer()
    {
        "靠近玩家".Log();
        yield break;
        // WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        // closeToPlayer.state.SetAnimation(0, "Show", false);
        // closeToPlayer.skeleton.SetToSetupPose();
        // Vector3 endPos = player.transform.position + Vector3.right * (Attr.faceDir * Random.Range(0.8f, 1.5f));
        // endPos.z = LayerManager.ZNum.MMiddleE(eAttr.rankType);
        // Vector3 startPos = transform.position;
        // for (int i = 0; i < 5; i++)
        // {
        //     yield return waitForFixedUpdate;
        // }
        //
        // for (int j = 0; j < 6; j++)
        // {
        //     transform.position = Vector3.Lerp(startPos, endPos, j / 5f);
        //     yield return waitForFixedUpdate;
        // }
    }

    /// <summary>
    /// 执行跟踪
    /// </summary>
    protected override void ExecuteFollow()
    {
        "ExecuteFollow执行跟踪".Log();
        action.ChangeState(EnemyStaEnum.Run);
        base.ExecuteFollow();
    }

    /// <summary>
    /// 播放受伤音效
    /// </summary>
    protected override void PlayHurtAudio()
    {
        R.Audio.PlayEffect(251, transform.position);//256
        if (PlaySpHurtAudio())
        {
            R.Audio.PlayEffect(Random.Range(57, 59), transform.position);
        }
    }

    /// <summary>
    /// 执行死亡
    /// </summary>
    protected override void ExecuteDie()
    {
        base.ExecuteDie();
        eAttr.timeController.SetGravity(1f);
        SetHitSpeed(Vector2.zero);
        SpDieEffect();
    }

    /// <summary>
    /// 敌人死亡
    /// </summary>
    public override void EnemyDie()
    {
        if (deadFlag) return;
        base.EnemyDie();
        if (eAttr.isOnGround)
        {
            SetHitSpeed(Vector2.zero);
            NormalKill();
            action.ChangeState(EnemyStaEnum.Death);
            Invoke("DieTimeControl", 0.12f);
        }
        else
        {
            NormalKill();
            DieTimeControl();
            StartCoroutine(DeathIEnumerator());
        }
    }

    /// <summary>
    /// 死亡效果
    /// </summary>
    private void SpDieEffect()
    {
        HuntressAction daoAction = (HuntressAction)action;
        Transform transform = R.Effect.Generate(219, null, center.position, Vector3.zero);
        transform.localScale = this.transform.localScale;
        //transform.GetComponent<SkeletonAnimation>().skeleton.SetSkin((!daoAction.isPao) ? "DaoBrother" : "PaoSister");
        Transform transform2 = R.Effect.Generate(220, null, center.position, Vector3.zero);
        transform2.localScale = this.transform.localScale;
        action.ChangeState(EnemyStaEnum.Idle);
        string playerAtkName = this.playerAtkName;
        if (playerAtkName != null)
        {
            if (playerAtkName != "NewExecute1_2" && playerAtkName != "NewExecute2_2" && playerAtkName != "NewExecuteAir2_2")
            {
                playerAtkName.Log();
                if (playerAtkName == "NewExecuteAir1_2")
                {
                    playerAtkName.Log();
                    transform.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(this.transform.localScale.x) * 2f, 15f);
                    transform2.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(this.transform.localScale.x) * -2f, 15f);
                }
            }
            else
            {
                playerAtkName.Log();
                transform.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(this.transform.localScale.x) * 2f, 15f);
                transform2.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(this.transform.localScale.x) * -2f, -5f);
            }
        }
    }

    /// <summary>
    /// 死亡携程
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator DeathIEnumerator()
    {
        bool deadFly = true;
        bool deadFall = false;
        while (eAttr.isDead)
        {
            if (deadFly && eAttr.timeController.GetCurrentSpeed().y <= 0f)
            {
                deadFly = false;
                deadFall = true;
                action.ChangeState(flyToFallAnimName);
            }

            if (deadFall && eAttr.isOnGround)
            {
                deadFall = false;
                action.ChangeState(airDieHitGroundAnimName);

                Invoke("RealDie", 0.5f);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// 真正的死亡
    /// </summary>
    private void RealDie()
    {
        GetComponent<HuntressAnimEvent>().GetUp();
    }

    /// <summary>
    /// 攻击跟踪位置
    /// </summary>
    private Vector2? atkFollowPos;

    private float atkFollowTime;

    private float atkFollowEnd;
}