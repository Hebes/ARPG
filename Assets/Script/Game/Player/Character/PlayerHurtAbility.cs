using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

/// <summary>
/// 玩家伤害能力
/// </summary>
public class PlayerHurtAbility : CharacterState
{
    /// <summary>
    /// 死去的标志位
    /// </summary>
    public bool DeadFlag;

    private List<int> hurtId = new List<int>();

    /// <summary>
    /// 通关率
    /// </summary>
    private float _clearRate;

    private PlayerSpHurt _spHurt;

    /// <summary>
    /// 护盾是否损坏
    /// </summary>
    private bool _broken;

    /// <summary>
    /// 能量恢复
    /// </summary>
    private float _energyRecover;

    /// <summary>
    /// 受伤计时器
    /// </summary>
    private int _hurtTimes;

    /// <summary>
    /// 伤害区间
    /// </summary>
    private float _hurtLimit;

    /// <summary>
    /// 是否无敌
    /// </summary>
    public bool Invincible;

    /// <summary>
    /// 起立无敌时间
    /// </summary>
    private int _getUpInvincibleTime;

    /// <summary>
    /// 伤害中断
    /// </summary>
    private float DamageCutOff
    {
        get
        {
            switch (R.Player.Enhancement.MaxEnergy)
            {
                case 1: return 0.7f;
                case 2: return 0.6f;
                case 3: return 0.5f;
                default: return 1f;
            }
        }
    }

    /// <summary>
    /// 护盾恢复时间
    /// </summary>
    private float ShieldRecoverTime => R.GameData.Difficulty > 1 ? 10f : 5f;


    public override void Start()
    {
        _spHurt = new PlayerSpHurt();
    }

    public override void OnEnable()
    {
        GameEvent.PlayerHurtAtk.Register(PlayeAllHurt);
    }

    public override void OnDisable()
    {
        GameEvent.PlayerHurtAtk.UnRegister(PlayeAllHurt);
    }

    public override void Update()
    {
        UpdateHurtId();
        UpdatePlayerDeath();
        UpdateShield();
        UpdateHurtGetUp();
        if (_hurtLimit > 0f)
        {
            _hurtLimit = Mathf.Clamp(_hurtLimit - Time.deltaTime, 0f, float.MaxValue);
        }
    }

    private void UpdateHurtId()
    {
        _clearRate -= 1f;
        if (_clearRate <= 0f)
        {
            _clearRate = 600f;
            hurtId.Clear();
        }
    }

    /// <summary>
    /// 更新玩家死亡
    /// </summary>
    private void UpdatePlayerDeath()
    {
        if (!Attribute.isDead || DeadFlag) return;
        PlayerDie();
    }

    /// <summary>
    /// 更新护盾
    /// </summary>
    private void UpdateShield()
    {
        if (Attribute.currentEnergy == 0)
        {
            _energyRecover += Time.deltaTime;
            if (_energyRecover >= ShieldRecoverTime)
            {
                Attribute.currentEnergy = Attribute.maxEnergy;
                _energyRecover = 0f;
            }
        }
    }

    private void PlayeAllHurt(object udata)
    {
        if (Attribute.isDead) return;
        PlayerHurtAtkEventArgs args = (PlayerHurtAtkEventArgs)udata;
        if (Attribute.flashFlag && R.Player.Enhancement.FlashAttack != 0) //FlashAttack等级
        {
            "玩家闪攻击".Log();
            hurtId.Add(args.atkId);
            //Abilities.flashAttack.FlashAttack(args.origin);
            return;
        }

        if (Invincible) return;
        HurtFeedback(args.sender.transform, args.damage, args.atkId, args.data, args.forceHurt);
    }

    /// <summary>
    /// 伤害反馈
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="damage"></param>
    /// <param name="atkId"></param>
    /// <param name="atkData"></param>
    /// <param name="forceHurt"></param>
    private void HurtFeedback(Transform enemy, int damage, int atkId, JsonData atkData, bool forceHurt)
    {
        "玩家受到伤害".Log();
        //return;
        if (HurtFilter(atkId)) return;
        if (StateMachine.currentState.IsInArray(PlayerAction.FlashAttackSta))
        {
            GameEvent.Assessment.Trigger(new AssessmentEventArgs(AssessmentEventArgs.EventType.CurrentComboFinish));
            return;
        }

        if (atkData == null)
            throw new Exception($"{enemy.name}  {enemy.GetComponent<EnemyBaseAction>().stateMachine.currentState}当前没有伤害数据");

        _broken = false;
        hurtId.Add(atkId); //受伤ID
        WorldTime.I.TimeFrozenByFixedFrame(6, FrozenArgs.FrozenType.Player); //时间被固定帧冻结
        R.Camera.CameraController.CameraShake(0.2f, ShakeTypeEnum.Rect); //相机抖动
        float damagePercent = atkData.Get<float>(EnemyAttackDataType.damagePercent.ToString(), 1f); //伤害百分比
        int shieldDamage = atkData.Get<int>(EnemyAttackDataType.shieldDamage.ToString(), 3); //盾伤害
        damage = (int)((float)damage * damagePercent);
        _energyRecover = 0f; //能量恢复=0
        int dir = JudgeDir(enemy); //设置玩家的朝向
        if (Attribute.currentEnergy > 0 && //能量大于0
            !StateMachine.currentState.IsInArray(PlayerAction.SpHurtSta) && //护盾受伤状态
            HurtInDefense(enemy, shieldDamage, ref damage)) //是否防御成功
        {
            return;
        }

        //敌人滚动击中后的效果
        if (_spHurt.DaoRoll(enemy))
        {
            // if (enemy.GetComponent<DaoAction>() != null)
            // {
            //     enemy.GetComponent<DaoAction>().RollToIdle();
            // }
            //
            // if (enemy.GetComponent<DaoPaoAction>() != null)
            // {
            //     enemy.GetComponent<DaoPaoAction>().RollToIdle();
            // }
        }

        // if (_spHurt.CanBeJumperCatach(enemy))
        // {
        //     // HurtStiffInit(dir);
        //     // HurtState("UnderAtkJumper");
        //     // enemy.GetComponent<JumperAction>().CatchSuccess();
        //     return;
        // }
        //
        // if (_spHurt.CanBeJumperFooterCatch(enemy))
        // {
        //     // HurtStiffInit(dir);
        //     // HurtState("UnderAtkJumper");
        //     // enemy.GetComponent<JumperFooterAction>().Attack2Success();
        //     return;
        // }
        //
        // if (_spHurt.CanBeBeelzebubEat(enemy))
        // {
        //     // HurtStiffInit(dir);
        //     // HurtState("UnderAtkEat");
        //     // enemy.GetComponent<BeelzebubAction>().EatAttack();
        //     return;
        // }
        //
        // if (_spHurt.CanEatingBossEat(enemy))
        // {
        //     // HurtStiffInit(dir);
        //     // HurtState("UnderAtkEat");
        //     // enemy.GetComponent<EatingBossAction>().Attack2Success();
        //     return;
        // }
        //
        // if (_spHurt.CanBeBeelzebubSaw(enemy))
        // {
        //     // HurtStiffInit(dir);
        //     // HurtState("UnderAtkHitSaw");
        //     // enemy.GetComponent<BeelzebubAction>().SawAttack();
        //     return;
        // }
        //
        // if (_spHurt.CanBombKillerCatch(enemy))
        // {
        //     // HurtStiffInit(dir);
        //     // HurtState("UnderAtkBombKillerII");
        //     // enemy.GetComponent<BombKillerAction>().Atk1Success();
        //     return;
        // }
        //
        // if (_spHurt.JumperCatachSuccess(enemy, forceHurt) || _spHurt.BombKillerAtkSuccess(enemy, forceHurt) ||
        //     _spHurt.JumperFooterCatchSuccess(enemy, forceHurt) || _spHurt.BeelzebubSpAttackSuccess(enemy, forceHurt) ||
        //     _spHurt.EatingBossEatSuccess(enemy, forceHurt))
        // {
        //     HurtStiff(dir, atkData, enemy);
        // }

        HpMinus(damage);
        if (Attribute.isDead || StateMachine.currentState.IsInArray(PlayerAction.SpHurtSta))
        {
            return;
        }

        HurtStiff(dir, atkData, enemy);
    }

    /// <summary>
    /// 受伤硬直
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="atkData"></param>
    /// <param name="enemy"></param>
    private void HurtStiff(int dir, JsonData atkData, Transform enemy)
    {
        R.Player.Rigidbody2D.gravityScale = 1f; //重力
        float num = 0f;
        float y = 0f;
        float num2 = 3f;
        float y2 = 5f;
        string text = PlayerStaEnum.UnderAtk1.ToString();
        string text2 = PlayerStaEnum.UnderAtkHitToFly.ToString();
        if (atkData != null)
        {
            num = (float)atkData.Get<float>(EnemyAttackDataType.xSpeed.ToString(), 0);
            y = (float)atkData.Get<float>(EnemyAttackDataType.ySpeed.ToString(), 0);
            num2 = (float)atkData.Get<float>(EnemyAttackDataType.airXSpeed.ToString(), 3);
            y2 = (float)atkData.Get<float>(EnemyAttackDataType.airYSpeed.ToString(), 5);
            text = atkData.Get<string>(EnemyAttackDataType.animName.ToString(), "UnderAtk1");
        }

        if (_broken || (_hurtTimes >= 2 && _hurtLimit > 0f))
        {
            text = text2;
            num = 3f;
            y = 20f;
            num2 = 3f;
            y2 = 15f;
        }

        R.Effect.Generate(216);
        HurtStiffInit(dir); //设置玩家朝向
        //设置玩家后退
        Vector2 vector = new Vector2(num * Action.transform.localScale.x *-1, y);//* dir
        Vector2 vector2 = new Vector2(num2 * Action.transform.localScale.x*-1, y2);
        R.Player.TimeController.SetSpeed(Attribute.isOnGround ? vector : vector2);
        Input.Vibration.Vibrate(1);
        HurtState(Attribute.isOnGround ? text : text2);
        HitEffect(dir, enemy);
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    private void PlayerDie()
    {
        DeadFlag = true;
        R.Player.Rigidbody2D.gravityScale = 1f;
        R.Camera.CameraEffect.CameraBloom(0.5f, 1f);
        Vector2 speed = new Vector2(Attribute.faceDir * -7, 15f);
        TimeController.SetSpeed(speed);
        Action.ChangeState(PlayerStaEnum.Death);
        R.StartCoroutine(AnimEvent.PlayerDieEnumerator());
    }

    /// <summary>
    /// 伤害过滤器
    /// </summary>
    /// <param name="atkId"></param>
    /// <returns></returns>
    private bool HurtFilter(int atkId)
    {
        return hurtId.Contains(atkId) || StateMachine.currentState.IsInArray(PlayerAction.ExecuteSta);
    }

    /// <summary>
    /// 受伤僵硬初始化
    /// </summary>
    /// <param name="dir"></param>
    private void HurtStiffInit(int dir)
    {
        AnimEvent.checkFallDown = false;
        AnimEvent.isFalling = false;
        AnimEvent.airAtkDown = false;
        AnimEvent.checkHitGround = false;
        AnimEvent.flyHitFlag = false;
        AnimEvent.flyHitGround = false;
        AnimEvent.StopIEnumerator(nameof(AnimEvent.FlashPositionSet)); //闪的位置设置取消
        Action.TurnRound(dir);
        Abilities.Charge.CancelCharge(); //充能取消
    }

    /// <summary>
    /// 判断方向
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    private int JudgeDir(Transform enemy)
    {
        if (enemy == null)
        {
            return Attribute.faceDir;
        }

        EnemyAttribute component = enemy.GetComponent<EnemyAttribute>();
        if (component != null)
        {
            return component.faceDir * -1;
        }

        Rigidbody2D component2 = enemy.GetComponent<Rigidbody2D>();
        if (component2 != null)
        {
            return (component2.velocity.x >= 0f) ? -1 : 1;
        }

        return (enemy.position.x - Action.transform.position.x <= 0f) ? -1 : 1;
    }

    /// <summary>
    /// 生命减少
    /// </summary>
    /// <param name="damage"></param>
    private void HpMinus(int damage)
    {
        if (damage == 0) return;
        Attribute.currentHP -= damage;
        $"当前生命值{Attribute.currentHP}".Log();
    }

    /// <summary>
    /// 防御的伤害(是否防御成功)
    /// </summary>
    /// <param name="enemy">敌人</param>
    /// <param name="shieldDamage">护盾伤害</param>
    /// <param name="damage">伤害</param>
    /// <returns></returns>
    private bool HurtInDefense(Transform enemy, int shieldDamage, ref int damage)
    {
        //难度
        shieldDamage *= R.GameData.Difficulty > 1 ? 2 : 1;
        if (R.GameData.Difficulty == 3)
            shieldDamage *= 100;

        damage = Mathf.Clamp((int)(damage * DamageCutOff), 1, int.MaxValue); //伤害
        Attribute.currentEnergy -= shieldDamage; //减少护盾
        HitEffect(Attribute.faceDir, enemy); //效果
        if (Attribute.currentEnergy > 0) //当前能量值大于0
        {
            HitShidleEffect(158, 186);
            HpMinus(damage); //生命减少
            Input.Vibration.Vibrate(1);
            return true;
        }

        HitShidleEffect(161, 191);
        Input.Vibration.Vibrate(3);
        _broken = true;
        return false;
    }

    /// <summary>
    /// 击中护盾效果
    /// </summary>
    /// <param name="effectId"></param>
    /// <param name="soundId"></param>
    private void HitShidleEffect(int effectId, int soundId)
    {
        Vector3 position = Action.transform.position - Attribute.bounds.center;
        position.Set(position.x, position.y + 2f, position.z);
        R.Effect.Generate(effectId, Action.transform, position); //播放效果
        R.Audio.PlayEffect(soundId, Action.transform.position); //播放音效
    }

    /// <summary>
    /// 伤害效果
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="enemy"></param>
    private void HitEffect(int dir, Transform enemy)
    {
        Vector3 temp = Action.transform.position + new Vector3(dir * 0.2f, 1.3f, 0f); //玩家的位置和偏移
        int temp2 = enemy.position.x - Action.transform.position.x <= 0f ? 76 : 80; //效果ID
        R.Effect.Generate(temp2, null, temp, Vector3.zero);
        R.Effect.Generate(70, null, temp, Vector3.zero);
    }

    /// <summary>
    /// 受伤状态
    /// </summary>
    /// <param name="sta"></param>
    private void HurtState(string sta)
    {
        GameEvent.PlayerHurt.Trigger(Action.gameObject);
        Action.ChangeState(sta);
    }

    public override void OnStateTransfer(object sender, TransferEventArgs args)
    {
        "受伤起身的无敌时间".Log();
        if (args.lastState == "UnderAtkGetUp")
        {
            Invincible = true;
            _getUpInvincibleTime = WorldTime.SecondToFrame(0.2f);
        }

        if (args.nextState == "UnderAtkHitToFly")
        {
            _hurtTimes = 0;
            _hurtLimit = 0f;
        }

        if (args.nextState == "UnderAtk1")
        {
            _hurtTimes++;
            _hurtLimit = 0.5f;
        }
    }

    /// <summary>
    /// 更新受伤起身的无敌时间
    /// </summary>
    private void UpdateHurtGetUp()
    {
        if (_getUpInvincibleTime > 0)
        {
            _getUpInvincibleTime--;
            if (_getUpInvincibleTime <= 0)
            {
                Invincible = false;
            }
        }
    }
}