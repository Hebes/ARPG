using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 敌人基础伤害
/// </summary>
public class EnemyBaseHurt : MonoBehaviour
{
    //初始化获取的
    protected GameObject player => R.Player.GameObject;
    protected PlayerAttribute Attr => R.Player.Attribute;
    protected JsonData hurtData;
    protected Pivot Pivot;
    protected EnemyAttribute eAttr;
    protected EnemyBaseAction action;
    private EnemyArmor armor;
    public Transform atkBox;
    public Transform center;
    [SerializeField] private Transform frameShakeBody;

    //等待设置的属性

    /// <summary>
    /// 显示攻击伤害
    /// </summary>
    protected int flashAttackDamage
    {
        get
        {
            if (R.GameData.Enhancement.FlashAttack == 0) return 0;
            return (int)(eAttr.maxHp * 0.05f * Random.Range(0.95f, 1.05f));
        }
    }

    /// <summary>
    /// 显示百分比
    /// </summary>
    protected bool flashPercent
    {
        get
        {
            int num = Random.Range(1, 100);
            int flashAttack = R.GameData.Enhancement.FlashAttack;
            if (flashAttack == 1) return num < 30;
            if (flashAttack != 2) return flashAttack == 3;
            return num < 60;
        }
    }

    /// <summary>
    /// 相对的HP
    /// </summary>
    public List<int> phaseHp
    {
        get
        {
            if (_phaseHp != null && _phaseHp.Count != 0) return _phaseHp;
            _phaseHp = new List<int>();
            if (hpPercent.Length == 0)
            {
                maxPhase = 1;
                _phaseHp.Add(eAttr.maxHp);
                return _phaseHp;
            }

            maxPhase = hpPercent.Length;
            if (eAttr.rankType != EnemyAttribute.RankType.Normal)
            {
                for (var i = 0; i < hpPercent.Length; i++)
                    _phaseHp.Add(eAttr.maxHp * hpPercent[i] / 100);
            }

            return _phaseHp;
        }
    }

    /// <summary>
    /// 速度
    /// </summary>
    private float speedDir => Mathf.Sign(transform.localScale.x);

    protected void Awake()
    {
        Transform tr = transform;
        frameShakeBody = tr.FindChildByName("Body");
        atkBox = tr.FindChildByName("AtkBox");
        center = tr.FindChildByName("center");
        eAttr = tr.FindComponent<EnemyAttribute>();
        action = tr.FindComponent<EnemyBaseAction>();
        Pivot = tr.FindComponent<Pivot>();
        armor = tr.FindComponent<EnemyArmor>();

        GameEvent.EnemyHurtAtk.Register(EnemyHurtAtk);
    }

    protected virtual void Start()
    {
        frameShakeOffset = m_frameShakeOffset;
        spHurtAudio = 6;
        chaseEnd = 0f;
    }

    private void OnDestroy()
    {
        QTECameraFinish();
    }

    protected virtual void Update()
    {
        UpdateEnemyDie(); //更新敌人死亡
        UpdateExecuteFollow(); //更新执行跟踪
        if (deadFlag) return;
        UpdateChase(); //更新追逐
    }

    /// <summary>
    /// 更新敌人死亡
    /// </summary>
    private void UpdateEnemyDie()
    {
        if (!eAttr.isDead) return;
        if (deadFlag) return;
        if (eAttr.rankType != EnemyAttribute.RankType.Normal) return;
        EnemyDie();
    }

    /// <summary>
    /// 更新执行跟踪
    /// </summary>
    private void UpdateExecuteFollow()
    {
        "更新执行跟踪".Log();
        // if (eAttr.followLeftHand)
        // {
        //     Vector3 pos = new Vector3(eAttr.faceDir * centerOffset.x, centerOffset.y, centerOffset.z);
        //     Vector3 position = R.Player.Action.executeFollow.position + pos;
        //     position.z = LayerManager.ZNum.Fx;
        //     transform.position = position;
        // }
    }

    /// <summary>
    /// 更新追逐
    /// </summary>
    private void UpdateChase()
    {
        if (eAttr.canBeChased)
        {
            chaseEnd += Time.unscaledDeltaTime;
            if (chaseEnd >= 1.3f)
                ChaseEnd();
        }
    }

    /// <summary>
    /// 敌人受到伤害
    /// </summary>
    /// <param name="udata"></param>
    private void EnemyHurtAtk(object udata)
    {
        EnemyHurt((EnemyHurtAtkEventArgs)udata);
    }

    /// <summary>
    /// QTEZ位置恢复
    /// </summary>
    protected void QTEZPositionRecover()
    {
        Vector3 position = transform.position;
        position.z = LayerManager.ZNum.MMiddleE(eAttr.rankType);
        transform.position = position;
    }

    /// <summary>
    /// 敌人受伤
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private bool EnemyHurt(EnemyHurtAtkEventArgs args)
    {
        if (args.hurted != gameObject) return false;
        switch (args.hurtType)
        {
            case EnemyHurtAtkEventArgs.HurtTypeEnum.Normal:
                NormalHurt(args.attackData, args.attackId, args.body, args.hurtPos);
                break;
            case EnemyHurtAtkEventArgs.HurtTypeEnum.ExecuteFollow:
                ExecuteFollow();
                break;
            case EnemyHurtAtkEventArgs.HurtTypeEnum.Execute:
                Execute(args.attackData.atkName);
                break;
            case EnemyHurtAtkEventArgs.HurtTypeEnum.QTEHurt:
                QTEHurt();
                break;
            case EnemyHurtAtkEventArgs.HurtTypeEnum.Flash:
                FlashAttackHurt();
                break;
        }

        return true;
    }

    /// <summary>
    /// 帧振动
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    public IEnumerator ClipShake(int frame)
    {
        for (int i = 0; i < frame; i++)
        {
            if (i % 2 == 0)
                frameShakeBody.localPosition += Vector3.right * frameShakeOffset;
            else
                frameShakeBody.localPosition -= Vector3.right * frameShakeOffset;
            yield return null;
        }

        frameShakeBody.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 时间冻结和相机抖动
    /// </summary>
    /// <param name="frozenFrame">冻结帧</param>
    /// <param name="frameShakeFrame">边框摇晃</param>
    /// <param name="shakeType">摇类型</param>
    /// <param name="shakeFrame">晃动帧</param>
    /// <param name="shakeOffset">摇晃偏移</param>
    protected void TimeFrozenAndCameraShake(int frozenFrame, int frameShakeFrame, int shakeType, int shakeFrame, float shakeOffset)
    {
        FrozenArgs.FrozenType type = playerAtkName != PlayerStaEnum.HitGround1.ToString() ? FrozenArgs.FrozenType.All : FrozenArgs.FrozenType.Enemy;
        WorldTime.I.TimeFrozenByFixedFrame(frozenFrame, type);
        StopCoroutine("ClipShake");
        StartCoroutine(ClipShake(frameShakeFrame));
        switch (shakeType)
        {
            case 0:
                R.Camera.CameraController.CameraShake(shakeFrame / 60f, ShakeTypeEnum.Rect, shakeOffset);
                break;
            case 1:
                R.Camera.CameraController.CameraShake(shakeFrame / 60f, ShakeTypeEnum.Horizon, shakeOffset);
                break;
            case 2:
                R.Camera.CameraController.CameraShake(shakeFrame / 60f, ShakeTypeEnum.Vertical, shakeOffset);
                break;
        }
    }

    /// <summary>
    /// 设置受伤速度
    /// </summary>
    /// <param name="speed"></param>
    public virtual void SetHitSpeed(Vector2 speed)
    {
        if (eAttr.isDead && speed.y > 0f) return;
        eAttr.timeController.SetSpeed(speed);
    }

    /// <summary>
    /// 播放受伤动画
    /// </summary>
    /// <param name="normalSta">默认的状态</param>
    /// <param name="airSta">空中的状态</param>
    /// <param name="speed">默认状态的速度</param>
    /// <param name="airSpeed">空中状态的速度</param>
    protected void PlayHurtAnim(string normalSta, string airSta, Vector2 speed, Vector2 airSpeed)
    {
        if (eAttr.isDead) return;
        if (!eAttr.iCanFly)
            eAttr.timeController.SetGravity(1f);
        eAttr.isFlyingUp = false;
        eAttr.checkHitGround = false;
        action.ChangeState(eAttr.isOnGround ? normalSta : airSta);
        SetHitSpeed(eAttr.isOnGround ? speed : airSpeed);
    }

    /// <summary>
    /// 伤害效果
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="atkName"></param>
    public void HitEffect(Vector3 pos, string atkName = "Atk1")
    {
        R.Effect.Generate(1, transform, pos, new Vector3(0f, 0f, 0f));
        R.Effect.Generate(71, transform, pos + Vector3.right * Attr.faceDir * 0.5f,
            new Vector3(0f, (Attr.faceDir <= 0) ? 0 : 180, Random.Range(-90f, 0f)));
        R.Effect.Generate(151, transform, pos, new Vector3(0f, 0f, 0f));
        if (atkName.IsInArray(PlayerAtkType.HeavyEffectAttack))
            R.Effect.Generate(156, transform, pos);
    }

    /// <summary>
    /// 打到弱效果
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="atkName"></param>
    private void HitIntoWeakEffect(Vector3 pos, string atkName)
    {
        WorldTime.I.TimeSlowByFrameOn60Fps(48, 0.5f);
        HitEffect(pos, atkName);
    }

    /// <summary>
    /// 播放伤害音频
    /// </summary>
    protected virtual void PlayHurtAudio()
    {
        R.Audio.PlayEffect(Random.Range(129, 132), transform.position);
    }

    /// <summary>
    /// 播放Sp受伤音频
    /// </summary>
    /// <returns></returns>
    protected bool PlaySpHurtAudio()
    {
        if (!SoundJudge())
        {
            return true;
        }

        bool flag = Random.Range(0, 100) < spHurtAudio;
        if (flag)
        {
            spHurtAudio = 6;
            return true;
        }

        spHurtAudio += 6;
        return false;
    }

    /// <summary>
    /// 声音判断
    /// </summary>
    /// <returns></returns>
    private bool SoundJudge()
    {
        int enemyType = (int)eAttr.baseData.enemyType;
        switch (enemyType)
        {
            case 1:
            case 2:
            case 3:
                break;
            default:
                switch (enemyType)
                {
                    case 22:
                    case 23:
                    case 26:
                        break;
                    default:
                        switch (enemyType)
                        {
                            case 15:
                            case 18:
                                break;
                            default:
                                if (enemyType != 33 && enemyType != 37)
                                {
                                    return false;
                                }

                                break;
                        }

                        break;
                }

                break;
        }

        return true;
    }

    /// <summary>
    /// 物理与效果
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="airSpeed"></param>
    /// <param name="normalAtkType"></param>
    /// <param name="airAtkType"></param>
    protected virtual void PhysicAndEffect(Vector2 speed, Vector2 airSpeed, string normalAtkType, string airAtkType)
    {
        if (playerAtkName != "Charge1EndLevel1" && !playerAtkName.IsInArray(PlayerAction.ExecuteSta))
        {
            if (eAttr.paBody) return;
            if (eAttr.currentActionInterruptPoint < eAttr.actionInterruptPoint && !defecnceBreak) return;
        }

        defecnceBreak = false;
        if (eAttr.isOnGround && normalAtkType != "NoStiff")
        {
            eAttr.stiffTime = 1f;
            PlayHurtAnim(normalAtkType, airAtkType, speed, airSpeed);
        }
        else if (!eAttr.isOnGround && airAtkType != "NoStiff")
        {
            eAttr.stiffTime = 1f;
            PlayHurtAnim(normalAtkType, airAtkType, speed, airSpeed);
        }
        else
        {
            eAttr.stiffTime = 0f;
        }
    }

    /// <summary>
    /// 相机快速启动
    /// </summary>
    public void QTECameraStart()
    {
        qteCameraEffectOn = true;
    }

    /// <summary>
    /// 相机快速结束
    /// </summary>
    public void QTECameraFinish()
    {
        qteCameraEffectOn = false;
    }

    protected bool BloodWeak()
    {
        return HpInWeak() && !eAttr.isDead && !action.IsInWeakSta() && eAttr.accpectExecute;
    }

    /// <summary>
    /// Hp疲软
    /// </summary>
    /// <returns></returns>
    protected bool HpInWeak()
    {
        int num = 0;
        for (int i = 0; i < currentPhase; i++)
            num += GetPhaseHp(i);
        num += (int)(GetPhaseHp(currentPhase) * ((eAttr.rankType != EnemyAttribute.RankType.Normal) ? 0.9f : 0.7f));
        return eAttr.currentHp < eAttr.maxHp - num;
    }

    /// <summary>
    /// 产生暴击伤害数
    /// </summary>
    /// <param name="damage"></param>
    public void GenerateCritHurtNum(int damage)
    {
        if (damage == 0) return;
        HpMinus(damage);
    }

    /// <summary>
    /// HP减少
    /// </summary>
    /// <param name="num"></param>
    protected virtual void HpMinus(int num)
    {
        $"{gameObject.name}受到的伤害{num}".Log();
        if (eAttr.rankType != EnemyAttribute.RankType.Normal)
        {
            eAttr.currentHp = Mathf.Clamp(eAttr.currentHp - num, MinLockedHp(), int.MaxValue);
            return;
        }

        eAttr.currentHp -= num;
    }

    protected void QTEHpMinus()
    {
        int num = 0;
        for (int i = 0; i < currentPhase; i++)
            num += GetPhaseHp(i);
        eAttr.currentHp = eAttr.maxHp - num;
    }

    protected int MinLockedHp()
    {
        int num = 0;
        for (int i = 0; i < currentPhase + 1; i++)
        {
            int phaseHp = GetPhaseHp(i);
            num += phaseHp;
        }

        return eAttr.maxHp - num + 1;
    }

    protected void HpRecover()
    {
        int num = 0;
        for (int i = 0; i < currentPhase; i++)
        {
            num += GetPhaseHp(i);
        }

        num += (int)(GetPhaseHp(currentPhase) * 0.7f);
        eAttr.currentHp = eAttr.maxHp - num;
    }

    /// <summary>
    /// 反击
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="groundOnly"></param>
    /// <returns></returns>
    protected virtual bool Counterattack(int damage, bool groundOnly)
    {
        return HurtDataTools.Counterattack(damage, groundOnly, ref actionInterrupt, ref eAttr, ref action);
    }

    /// <summary>
    /// 添加动作中断点
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="atkName"></param>
    protected void AddActionInterruptPoint(int damage, string atkName)
    {
        HurtDataTools.AddActionInterruptPoint(damage, atkName, ref eAttr, ref actionInterrupt);
    }

    protected bool CalculateMonsterDefence(int damage)
    {
        return HurtDataTools.CalculateMonsterDefence(damage, ref defenceTrigger, ref action, ref eAttr);
    }

    protected bool CalculateMonsterSideStep(int damage)
    {
        return HurtDataTools.CalculateMonsterSideStep(damage, ref sideStepTrigger, ref action, ref eAttr);
    }

    protected int GetPhaseHp(int phase)
    {
        if (phase < 0 || phase > phaseHp.Count)
        {
            return 0;
        }

        return phaseHp[phase];
    }

    /// <summary>
    /// 停止跟随左手
    /// </summary>
    public void StopFollowLeftHand()
    {
        eAttr.followLeftHand = false;
    }

    /// <summary>
    /// 进入弱状态
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="airSpeed"></param>
    /// <param name="normalAtkType"></param>
    /// <param name="airAtkType"></param>
    protected virtual void HitIntoWeakState(Vector2 speed, Vector2 airSpeed, string normalAtkType, string airAtkType)
    {
        ChaseEnd();
        action.EnterWeakState();
        eAttr.stiffTime = 1f;
        if (eAttr.isOnGround && normalAtkType != "NoStiff")
        {
            action.FaceToPlayer();
            PlayHurtAnim(normalAtkType, airAtkType, speed, airSpeed);
        }
        else if (!eAttr.isOnGround && airAtkType != "NoStiff")
        {
            action.FaceToPlayer();
            PlayHurtAnim(normalAtkType, airAtkType, speed, airSpeed);
        }

        if (!IsInvoking("ExitWeak"))
        {
            exitWeakPhase = currentPhase;
            Invoke("ExitWeak", 5f);
        }
    }

    /// <summary>
    /// 退出弱状态
    /// </summary>
    protected virtual void ExitWeak()
    {
        "弱状态退出".Log();
        return;
        if (!action.IsInWeakSta() || exitWeakPhase != currentPhase || eAttr.willBeExecute)
            return;
        HpRecover();
        action.ExitWeakState();
    }

    /// <summary>
    /// 正常的伤害
    /// </summary>
    /// <param name="atkData"></param>
    /// <param name="atkId"></param>
    /// <param name="body"></param>
    /// <param name="hurtPos"></param>
    public virtual void NormalHurt(EnemyHurtAtkEventArgs.PlayerNormalAtkData atkData, int atkId, HurtCheck.BodyType body, Vector2 hurtPos)
    {
        if (!hurtData.ContainsKey(atkData.atkName))
        {
            $"敌人没有受伤的{atkData.atkName},但是有些可以忽略".Error();
        }
        if (hurtId >= atkId || !hurtData.ContainsKey(atkData.atkName)) return;
        GetHurt(atkId);
        HurtAttribute hurtAttribute = new HurtAttribute(hurtData[atkData.atkName], defaultAnimName, defaultAirAnimName); //伤害属性
        SpeedAdjust(atkData.atkName, ref hurtAttribute);
        playerAtkName = atkData.atkName;
        Vector2 speed = new Vector2(hurtAttribute.xSpeed, hurtAttribute.ySpeed);
        Vector2 airSpeed = new Vector2(hurtAttribute.airXSpeed, hurtAttribute.airYSpeed);
        Vector3 pos = (Vector3)hurtPos - transform.position;
        int finalDamage = PlayerDamageCalculate.GetFinalDamage(atkData.damagePercent, HurtDataTools.GetAtkLevel(playerAtkName), playerAtkName);
        $"玩家正常攻击数据的最终伤害{finalDamage}".Log();
        if (eAttr.isArmorBroken)
        {
            AddActionInterruptPoint(finalDamage, playerAtkName);
            bool flag = CalDefense(finalDamage);
            bool flag2 = CalSideStep(finalDamage);
            canCounterattack = Counterattack(finalDamage, eAttr.isOnGround);
            if (atkData.joystickShakeNum > 0)
            {
                "Input.Vibration震动".Log();
                //Input.Vibration.Vibrate(atkData.joystickShakeNum);
            }

            if (canCounterattack) //可以反击
            {
                PlayHurtAudio();
                DoCounterAttack(atkData, pos);
                GenerateCritHurtNum(finalDamage);
                DoWeak(pos, speed, airSpeed, hurtAttribute, atkData);
                return;
            }

            CanDefenseOrSideStep(ref flag, ref flag2, ref finalDamage);
            if (InDefense(flag, atkData, pos))
            {
                return;
            }

            flag = false;
            NotInDefense();
            speed.x *= speedDir * -1;
            airSpeed.x *= speedDir * -1;
            if (!eAttr.isDead)
            {
                GenerateCritHurtNum(finalDamage);
            }

            //进入弱状态
            if (DoWeak(pos, speed, airSpeed, hurtAttribute, atkData)) return;
            if (!flag2) //没进入回避
            {
                HitEffect(pos, atkData.atkName); //伤害效果
                DoHurt(atkData, speed, airSpeed, hurtAttribute); //受伤
            }
        }
        else
        {
            armor.HitArmor(finalDamage, atkData.atkName);
        }
    }

    /// <summary>
    /// 获取伤害
    /// </summary>
    /// <param name="atkId"></param>
    private void GetHurt(int atkId)
    {
        hurtId = atkId;
        if (atkBox != null)
        {
            atkBox.localScale = Vector3.zero;
        }
    }

    /// <summary>
    /// 速度调整
    /// </summary>
    /// <param name="name"></param>
    /// <param name="hurtAttr"></param>
    private void SpeedAdjust(string name, ref HurtAttribute hurtAttr)
    {
        if ((name == "UpRising" || name == "AtkUpRising") && hurtAttr.airYSpeed > 0f)
        {
            float num = Mathf.Clamp(Physics2D.Raycast(transform.position, -Vector2.up, 100f, LayerManager.GroundMask).distance, 0f,
                float.PositiveInfinity);
            hurtAttr.airYSpeed = Mathf.Lerp(hurtAttr.ySpeed, 6f, num / 4f);
        }
    }

    private bool CalDefense(int hitNumber)
    {
        bool flag = CalculateMonsterDefence(hitNumber);
        if (flag)
        {
            eAttr.dynamicDefence = (int)(eAttr.baseDefence * Random.Range(0.7f, 1.3f));
            defenceTrigger = 0f;
        }

        return flag;
    }

    /// <summary>
    /// 能够回避
    /// </summary>
    /// <param name="hitNumber"></param>
    /// <returns></returns>
    private bool CalSideStep(int hitNumber)
    {
        bool flag = CalculateMonsterSideStep(hitNumber);
        if (flag)
        {
            eAttr.dynamicSideStep = (int)(eAttr.baseSideStep * Random.Range(0.7f, 1.3f));
            sideStepTrigger = 0f;
        }

        return flag;
    }

    /// <summary>
    /// 进行反击
    /// </summary>
    /// <param name="atkData"></param>
    /// <param name="pos"></param>
    private void DoCounterAttack(EnemyHurtAtkEventArgs.PlayerNormalAtkData atkData, Vector3 pos)
    {
        $"{gameObject.name}开始反击".Log();
        TimeFrozenAndCameraShake(atkData.frozenFrame, atkData.shakeFrame, atkData.shakeType, atkData.camShakeFrame, atkData.shakeStrength);
        HitEffect(pos, atkData.atkName);
        eAttr.currentDefence = 0;
        eAttr.currentSideStep = 0;
        int dir = InputSetting.JudgeDir(transform.position.x, player.transform.position.x);
        action.CounterAttack(dir);
    }

    /// <summary>
    /// 防守还是回避
    /// </summary>
    /// <param name="defence">防御</param>
    /// <param name="sideStep">回避</param>
    /// <param name="hitNumber">伤害</param>
    private void CanDefenseOrSideStep(ref bool defence, ref bool sideStep, ref int hitNumber)
    {
        if (defence || sideStep)
        {
            if (defence && sideStep)
            {
                if (Random.Range(0, 2) == 0)
                {
                    eAttr.currentSideStep /= 2;
                    action.Defence();
                    sideStep = false;
                }
                else
                {
                    eAttr.currentDefence /= 2;
                    action.SideStep();
                    defence = false;
                    hitNumber = 0;
                }
            }
            else if (defence)
            {
                action.Defence();
            }
            else
            {
                action.SideStep();
                hitNumber = 0;
            }
        }
    }

    /// <summary>
    /// 在防御
    /// </summary>
    /// <param name="defence">防御</param>
    /// <param name="atkData">攻击数据</param>
    /// <param name="pos">位置</param>
    /// <returns></returns>
    private bool InDefense(bool defence, EnemyHurtAtkEventArgs.PlayerNormalAtkData atkData, Vector3 pos)
    {
        if (defence)
        {
            DoDefenseSucces(atkData, pos);
            return true;
        }

        if (!action.IsInDefenceState())
        {
            return false;
        }

        if (!playerAtkName.IsInArray(PlayerAtkType.BreakDefense))
        {
            DoDefenseSucces(atkData, pos);
            return true;
        }

        DoFefenseBreak();
        return false;
    }

    /// <summary>
    /// 防御成功
    /// </summary>
    /// <param name="atkData"></param>
    /// <param name="pos"></param>
    private void DoDefenseSucces(EnemyHurtAtkEventArgs.PlayerNormalAtkData atkData, Vector3 pos)
    {
        R.Effect.Generate(157, transform.Find("HurtBox"), default(Vector3), default(Vector3), default(Vector3), true);
        action.FaceToPlayer();
        action.DefenceSuccess();
        HitEffect(pos, atkData.atkName);
    }

    /// <summary>
    /// 没在防御
    /// </summary>
    private void NotInDefense()
    {
        if (canChangeFace && !eAttr.paBody && eAttr.currentActionInterruptPoint >= eAttr.actionInterruptPoint)
        {
            int dir = (R.Player.Attribute.faceDir != 1) ? 1 : -1;
            action.ChangeFace(dir);
        }
    }

    /// <summary>
    /// 防御破裂
    /// </summary>
    private void DoFefenseBreak()
    {
        R.Audio.PlayEffect(192, transform.position);
        R.Effect.Generate(160, transform.Find("HurtBox"));
        Input.Vibration.Vibrate(4);//振动
        eAttr.currentActionInterruptPoint = 0;
        defecnceBreak = true;
        eAttr.paBody = false;
    }

    /// <summary>
    /// 进入弱状态
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="speed"></param>
    /// <param name="airSpeed"></param>
    /// <param name="hurtAttr"></param>
    /// <param name="atkData"></param>
    /// <returns></returns>
    private bool DoWeak(Vector3 pos, Vector2 speed, Vector2 airSpeed, HurtAttribute hurtAttr, EnemyHurtAtkEventArgs.PlayerNormalAtkData atkData)
    {
        if (BloodWeak())
        {
            HitIntoWeakState(speed, airSpeed, hurtAttr.normalAtkType, hurtAttr.airAtkType);
            HitIntoWeakEffect(pos, atkData.atkName);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 受伤
    /// </summary>
    /// <param name="atkData"></param>
    /// <param name="speed"></param>
    /// <param name="airSpeed"></param>
    /// <param name="hurtAttr"></param>
    private void DoHurt(EnemyHurtAtkEventArgs.PlayerNormalAtkData atkData, Vector2 speed, Vector2 airSpeed, HurtAttribute hurtAttr)
    {
        PlayHurtAudio();
        //没有死亡 可以追逐攻击
        if (!eAttr.isDead && HurtDataTools.ChaseAttack())
        {
            currentChaseTime = 0;
            ChaseStart();
        }

        SpAttack();
        PhysicAndEffect(speed, airSpeed, hurtAttr.normalAtkType, hurtAttr.airAtkType);
        TimeFrozenAndCameraShake(atkData.frozenFrame, atkData.shakeFrame, atkData.shakeType, atkData.camShakeFrame, atkData.shakeStrength);
    }

    protected virtual void FlashAttackHurt()
    {
        HurtDataTools.FlashHPRecover();
        if (eAttr.rankType == EnemyAttribute.RankType.Normal)
        {
            Execute("NewExecute1_2", false);
        }
        else if (flashPercent)
        {
            R.Effect.Generate(127, transform, default(Vector3), default(Vector3), default(Vector3), true);
        }

        Input.Vibration.Vibrate(4);
        if (BloodWeak())
        {
            action.EnterWeakState();
            if (!IsInvoking("ExitWeak"))
            {
                Invoke("ExitWeak", 5f);
            }
        }
    }

    public virtual void QTEHurt()
    {
        if (eAttr.rankType == EnemyAttribute.RankType.Normal) return;
        eAttr.willBeExecute = false;
        eAttr.inWeakState = false;
        eAttr.isFlyingUp = false;
        eAttr.checkHitGround = false;
        eAttr.timeController.SetGravity(1f);
        SetHitSpeed(Vector2.zero);
        action.hurtBox.gameObject.SetActive(true);
        currentPhase = Mathf.Clamp(currentPhase + 1, 0, maxPhase);
        ExecuteDieEffect();
        WorldTime.I.TimeSlowByFrameOn60Fps(30, 0.2f);
        R.Camera.CameraController.CameraShake(0.5f, ShakeTypeEnum.Rect, 0.3f);
        R.Effect.Generate(127, transform);
    }

    /// <summary>
    /// 执行跟踪
    /// </summary>
    protected virtual void ExecuteFollow()
    {
        int dir = player.transform.localScale.x >= 0f ? 1 : -1;
        action.ChangeFace(dir);
        action.WeakEffectDisappear("RollEnd");
        eAttr.followLeftHand = true;
        eAttr.willBeExecute = true;
        eAttr.checkHitGround = false;
        eAttr.flyToFall = false;
        eAttr.isFlyingUp = false;
        SetHitSpeed(Vector2.zero);
        eAttr.timeController.SetGravity(0f);
        StartCoroutine(ClipShake(17));
    }

    protected virtual void SpAttack()
    {
        if (eAttr.currentActionInterruptPoint < eAttr.actionInterruptPoint)
        {
        }
    }

    /// <summary>
    /// 追逐开始
    /// </summary>
    public void ChaseStart()
    {
        if (action.IsInWeakSta()) return;
        //if (player.GetComponent<PlayerAbilities>().flashAttack.CheckEnemy(gameObject)) return;
        if (currentChaseTime > 2) return;
        currentChaseTime++;
        chaseEnd = 0f;
        eAttr.canBeChased = true;
        if (_chaseCoroutine != null)
        {
            StopCoroutine(_chaseCoroutine);
        }

        _chaseCoroutine = StartCoroutine(ChaseCoroutine());
    }

    private IEnumerator ChaseCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        ChaseAttack();
        yield return new WaitForSeconds(0.3f);
        ChaseAttack();
        ChaseEnd();
    }

    private void ChaseAttack()
    {
        Vector3 position = this.transform.position;
        R.Audio.PlayEffect(Random.Range(18, 21), new Vector3?(this.transform.position));
        position.y = Mathf.Clamp(position.y, LayerManager.YNum.GetGroundHeight(gameObject), float.MaxValue);
        Transform transform = R.Effect.Generate(182, null, position, default(Vector3), default(Vector3), true);
        Vector3 localScale = transform.localScale;
        localScale.x = (Random.Range(0, 2) != 0) ? -1 : 1;
        transform.localScale = localScale;
    }

    /// <summary>
    /// 追逐结束
    /// </summary>
    public void ChaseEnd()
    {
        chaseEnd = 0f;
        currentChaseTime = 0;
        eAttr.canBeChased = false;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="playerState"></param>
    /// <param name="chargeUp"></param>
    protected void Execute(string playerState, bool chargeUp = true)
    {
        eAttr.willBeExecute = false;
        eAttr.inWeakState = false;
        action.hurtBox.gameObject.SetActive(true);
        playerAtkName = playerState;
        ExecuteDie();
        if (chargeUp || flashPercent)
        {
            R.Effect.Generate(127, transform);
        }
    }

    /// <summary>
    /// 执行死亡
    /// </summary>
    protected virtual void ExecuteDie()
    {
        R.Audio.PlayEffect(Random.Range(105, 108), transform.position);
        deadFlag = true;
        eAttr.currentHp = 0;
        eAttr.inWeakState = false;
        eAttr.isFlyingUp = false;
        eAttr.checkHitGround = false;
        eAttr.stiffTime = 0f;
        eAttr.timeController.SetGravity(1f);
        GameEvent.EnemyKilled.Trigger(eAttr);
        action.WeakEffectDisappear("Null");
        R.Effect.Generate(91, null, transform.position + new Vector3(0f, 1.2f, LayerManager.ZNum.Fx), Vector3.zero, default(Vector3), true);
        R.Effect.Generate(49, transform);
        R.Effect.Generate(14, null, transform.position + new Vector3(0f, 1.2f, LayerManager.ZNum.Fx));
        AddCoinAndExp();
        ExecuteDieEffect();
        R.Effect.Generate(213);
        R.Effect.Generate(214);
        WorldTime.I.TimeSlowByFrameOn60Fps(45, 0.2f);
        R.Camera.CameraController.CameraShake(0.9166667f, ShakeTypeEnum.Rect, 0.3f);
        R.Camera.CameraEffect.OpenMotionBlur(0.13333334f, 1f, transform.position);
    }

    /// <summary>
    /// 敌人死亡
    /// </summary>
    public virtual void EnemyDie()
    {
        if (eAttr.rankType != EnemyAttribute.RankType.Normal) return;
        R.Audio.PlayEffect(Random.Range(105, 108), transform.position); //播放效果
        deadFlag = true; //死亡标志位
        eAttr.inWeakState = false; //是否在虚弱状态中
        eAttr.isFlyingUp = false; //是否正在飞起来
        eAttr.checkHitGround = false; //检查击中地面
        eAttr.stiffTime = 0f; //硬直时间
        eAttr.timeController.SetGravity(1f); //设置重力
        GameEvent.EnemyKilled.Trigger(eAttr); //事件触发
        action.WeakEffectDisappear("Null"); //弱效果消失
        StartCoroutine(GenerateEnergyBall()); //产生能量球
        AddCoinAndExp(); //增加金币和经验值
    }

    /// <summary>
    /// 产生能量球
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateEnergyBall()
    {
        yield return new WaitForSeconds(0.8f);
        Vector3 targetPos = transform.position + new Vector3(0f, 1.2f, LayerManager.ZNum.Fx);
        R.Effect.Generate(91, null, targetPos, Vector3.zero);
    }

    /// <summary>
    /// 增加金币和经验值
    /// </summary>
    protected void AddCoinAndExp()
    {
        R.GameData.Equipment.CoinNum += eAttr.dropCoins;
    }

    /// <summary>
    /// 死亡时间控制
    /// </summary>
    protected void DieTimeControl()
    {
        WorldTime.I.TimeFrozenByFixedFrame(25, FrozenArgs.FrozenType.Enemy); //时间冻结
        R.Camera.CameraController.CameraShake(0.416666657f, ShakeTypeEnum.Rect, 0.3f); //相机抖动
        if (gameObject.activeSelf)
            StartCoroutine(ClipShake(12));
    }

    /// <summary>
    /// 正常的杀
    /// </summary>
    public void NormalKill()
    {
        R.Audio.PlayEffect(24, transform.position);
        R.Camera.CameraEffect.CameraBloom(0.25f, 0f);
        R.Effect.Generate(49, transform, new Vector3(0f, 1.2f, LayerManager.ZNum.Fx), Vector3.zero);
        R.Effect.Generate(9, transform, new Vector3(0f, 1.2f, LayerManager.ZNum.Fx), new Vector3(0f, 0f, 0f));
        R.Effect.Generate(14, transform, new Vector3(0f, 1.2f, -0.1f), new Vector3(0f, 0f, 0f));
    }

    /// <summary>
    /// 执行死亡效果
    /// </summary>
    protected virtual void ExecuteDieEffect()
    {
        R.Effect.Generate(156, null, center.position);
    }

    /// <summary>
    /// 死亡携程
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator DeathIEnumerator()
    {
        bool deadFly = true;
        bool deadFall = false;
        while (eAttr.isDead)
        {
            if (deadFly && eAttr.timeController.GetCurrentSpeed().y <= 0f)
            {
                deadFly = false;
                deadFall = true;
                action.ChangeState(airDieAnimName);
            }

            if (deadFall && eAttr.isOnGround)
            {
                deadFall = false;
                action.ChangeState(airDieHitGroundAnimName);
            }

            yield return null;
        }
    }

    /// <summary>
    /// 是否可以反击
    /// </summary>
    public bool canCounterattack;


    /// <summary>
    /// 当前阶段
    /// </summary>
    public int currentPhase;

    public const int maxChaseTime = 2;

    /// <summary>
    /// 当前追踪时间
    /// </summary>
    public int currentChaseTime;

    protected int hurtId;

    /// <summary>
    /// 死亡标志位
    /// </summary>
    protected bool deadFlag;

    protected string defaultAnimName, airDieHitGroundAnimName, defaultAirAnimName, airDieAnimName, flyToFallAnimName;

    /// <summary>
    /// 玩家攻击的名称
    /// </summary>
    protected string playerAtkName;

    /// <summary>
    /// 是否中断行为
    /// </summary>
    protected bool actionInterrupt;

    protected bool defecnceBreak;

    /// <summary>
    /// sp伤害音频
    /// </summary>
    protected int spHurtAudio;

    [SerializeField] private List<int> _phaseHp;

    protected int maxPhase;


    [SerializeField] private bool canChangeFace = true;
    [SerializeField] private Vector3 centerOffset;
    [Header("左右抖动幅度")] [SerializeField] private float m_frameShakeOffset = 0.1f;
    [SerializeField] private int[] hpPercent;

    /// <summary>
    /// 左右抖动幅度
    /// </summary>
    private float frameShakeOffset;

    /// <summary>
    /// 防御触发
    /// </summary>
    private float defenceTrigger;

    /// <summary>
    /// 闪避触发
    /// </summary>
    private float sideStepTrigger;

    /// <summary>
    /// 追逐结束
    /// </summary>
    private float chaseEnd;

    /// <summary>
    /// 退出若的阶段
    /// </summary>
    private int exitWeakPhase;

    /// <summary>
    /// 相机效果是否启动
    /// </summary>
    private bool qteCameraEffectOn;

    private Coroutine _chaseCoroutine;
    private GameObject _getSelf;


    /// <summary>
    /// 伤害属性
    /// </summary>
    private class HurtAttribute
    {
        public HurtAttribute(JsonData hurt, string defaultAnimName, string defaultAirAnimName)
        {
            xSpeed = hurt.Get($"{EnemyHurtDataType.xSpeed}", 0f);
            ySpeed = hurt.Get($"{EnemyHurtDataType.ySpeed}", 0f);
            airXSpeed = hurt.Get($"{EnemyHurtDataType.airXSpeed}", 0f);
            airYSpeed = hurt.Get($"{EnemyHurtDataType.airYSpeed}", 0f);
            normalAtkType = hurt.Get($"{EnemyHurtDataType.normalAtkType}", defaultAnimName);
            airAtkType = hurt.Get($"{EnemyHurtDataType.airAtkType}", defaultAirAnimName);
        }

        /// <summary>
        /// X速度
        /// </summary>
        public float xSpeed;

        /// <summary>
        /// Y速度
        /// </summary>
        public float ySpeed;

        /// <summary>
        /// 空中X速度
        /// </summary>
        public float airXSpeed;

        /// <summary>
        /// 空中Y速度
        /// </summary>
        public float airYSpeed;

        /// <summary>
        /// 正常攻击类型
        /// </summary>
        public string normalAtkType;

        /// <summary>
        /// 空中攻击类型
        /// </summary>
        public string airAtkType;
    }
}