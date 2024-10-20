using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 大剑
/// </summary>
public class Claymore : MonoBehaviour
{
    private PlayerAttribute _attribute;
    private StateMachine _stateMachine;
    private PlayerAction _playerAction;

    [Header("重攻击攻击")] public bool cirtAttack;
    [Header("是否播放攻击动画")] [HideInInspector] public bool canChangeAtkAnim;

    [Header("是否播放空中攻击动画")] [HideInInspector]
    public bool canChangeAirAtkAnim;

    [Header("是否继续攻击")] private bool continueAttack;
    [Header("是否空袭重置")] public bool airAttackReset;
    [Header("是否空中继续攻击")] private bool continueAirAttack;
    [Header("是否充电")] private bool startCharge;
    [Header("充电时间")] private float chargeTime;
    [Header("最后一帧时间")] private float lastFrameTime;
    [Header("空中释放")] private float airRelease;

    //private PlayerExecuteTools execute;

    /// <summary>
    /// 最大充电时间
    /// </summary>
    private float maxChargeTime => _attribute.maxChargeTime;

    /// <summary>
    /// 是否可以冲锋攻击
    /// </summary>
    public bool canChargeAttack => chargeTime >= 2.5f;


    [Header("攻击ID")] private int attackID;

    private void Start()
    {
        _attribute = R.Player.Attribute;
        _stateMachine = GetComponent<StateMachine>();
        _playerAction = GetComponent<PlayerAction>();
        //execute = new PlayerExecuteTools();
        AirAttackReset();
    }


    private void Update()
    {
        UpdateCharge();
    }

    /// <summary>
    /// 处理攻击
    /// </summary>
    /// <param name="cirt">响应攻击</param>
    public void HandleAttack(bool cirt)
    {
        JsonData jsonData = DB.WeaponConfigure["normalAttack"];
        if (_stateMachine.currentState.IsInArray(PlayerAction.AttackSta))
        {
            continueAttack = true; //可以继续攻击
            cirtAttack = cirt; //可以响应攻击
        }
        else
        {
            if (cirt) //重攻击
            {
                attackID = 9;
                "AtkHv1".Log();
                //_playerAction.ChangeState(jsonData[9.ToString()].Get<string>("anim"));
            }
            else
            {
                attackID = 1; //普通攻击
                _playerAction.ChangeState(jsonData[attackID.ToString()].Get<string>("anim")); //第一次攻击
            }

            continueAttack = false;
            cirtAttack = false;
        }
    }

    /// <summary>
    /// 处理空中攻击
    /// </summary>
    /// <param name="cirt">是否是重攻击</param>
    public void HandleAirAttack(bool cirt)
    {
        "处理空中攻击".Log();
        JsonData jsonData = DB.WeaponConfigure["airAttack"];
        if (_stateMachine.currentState.IsInArray(PlayerAction.AirAttackSta) && _stateMachine.currentState != "AirAtkRoll")
        {
            continueAirAttack = true;
            cirtAttack = cirt;
        }
        else
        {
            if (cirt) //重攻击
            {
                if (!airAttackReset) return;
                // attackID = 4;
                // _playerAction.ChangeState(jsonData[attackID.ToString()].Get<string>("anim"));
                airAttackReset = false;
            }
            else //轻攻击
            {
                if (!airAttackReset) return;
                attackID = 1;
                _playerAction.ChangeState(jsonData[attackID.ToString()].Get<string>("anim"));
                airAttackReset = false;
            }

            continueAirAttack = false;
            cirtAttack = false;
        }
    }

    public void CirtAttackHold()
    {
        JsonData jsonData =DB.WeaponConfigure["normalAttack"];
        attackID = 12;
        _playerAction.ChangeState(jsonData[attackID.ToString()].Get<string>("anim"));
    }

    /// <summary>
    /// 播放下一个攻击状态
    /// </summary>
    public void CanPlayNextAttack()
    {
        if (canChangeAtkAnim)
        {
            PlayNextAttackAnim(cirtAttack);
            return;
        }
    
        if (continueAttack)
            PlayNextAttackAnim(cirtAttack);
        else
            canChangeAtkAnim = true;
    }

    /// <summary>
    /// 播放下一个攻击动画
    /// </summary>
    /// <param name="cirt">是否重攻击</param>
    private void PlayNextAttackAnim(bool cirt)
    {
        canChangeAtkAnim = false;
        continueAttack = false;
        //转向
        _playerAction.TurnRound(_playerAction.tempDir);
        //选择动画
        JsonData jsonData = DB.WeaponConfigure["normalAttack"];
        string key = cirt ? "nextCirtID" : "nextID";
        int nextID = jsonData[attackID.ToString()].Get(key, 0);
        if (!cirt && attackID == 4)
        {
            nextID = ComboCheck(nextID);
        }

        if (cirt)
        {
            nextID = CirtComboCheck(nextID);
        }

        if (!jsonData.ContainsKey(nextID.ToString()))
        {
            continueAttack = false;
            canChangeAtkAnim = false;
            return;
        }

        JsonData jsonData2 = jsonData[nextID.ToString()];
        _playerAction.ChangeState(jsonData2.Get<string>("anim"));
        attackID = nextID;
    }


    public void AttackFinish()
    {
        cirtAttack = false;
        continueAttack = false;
        canChangeAtkAnim = false;
    }

    private int CirtComboCheck(int nextID)
    {
        // if (attackID == 1 && nextID == 15 && R.Player.EnhancementSaveData.UpperChop == 0)
        // 	return -1;
        // if (attackID == 2 && nextID == 5 && R.Player.EnhancementSaveData.Combo2 == 0)
        // 	return -1;
        // if (attackID == 3 && nextID == 19 && R.Player.EnhancementSaveData.AvatarAttack == 0)
        // 	return -1;
        // if (attackID == 4 && nextID == 18 && R.Player.EnhancementSaveData.Knockout == 0)
        // 	return -1;
        return nextID;
    }

    /// <summary>
    /// 组合检查
    /// </summary>
    /// <param name="nextID"></param>
    /// <returns></returns>
    private int ComboCheck(int nextID)
    {
        // int attack = R.Player.EnhancementSaveData.Attack;
        // if (attack == 2)return 16;
        // if (attack != 3)return nextID;
        return 16;
    }


    public void AirCirtAttackHold()
    {
        // JsonData1 jsonData = comboConfig["airAttack"];
        // player.ChangeState(jsonData[10.ToString()].Get<string>("anim"));
        // attackID = 10;
    }

    /// <summary>
    /// 播放下次攻击动画
    /// </summary>
    /// <param name="cirt"></param>
    private void PlayNextAirAttackAnim(bool cirt)
    {
        continueAirAttack = false;
        canChangeAirAtkAnim = false;
        R.Player.TimeController.SetSpeed(Vector2.zero);
        _playerAction.TurnRound(_playerAction.tempDir);
        JsonData jsonData = DB.WeaponConfigure["airAttack"];
        string key = cirt ? "nextCirtID" : "nextID";
        int nextID = jsonData[attackID.ToString()].Get(key, 0);
        if (cirt)
        {
            nextID = AirCirtComboCheck(nextID);
        }

        if (!jsonData.ContainsKey(nextID.ToString()))
        {
            continueAirAttack = false;
            canChangeAirAtkAnim = false;
            return;
        }

        JsonData jsonData2 = jsonData[nextID.ToString()];
        _playerAction.ChangeState(jsonData2.Get<string>("anim"));
        attackID = nextID;
    }

    /// <summary>
    /// 是否可以进行下一次空中攻击
    /// </summary>
    public void CanPlayNextAirAttack()
    {
        if (_attribute.isOnGround)
        {
            AirAttackReset();
            _playerAction.ChangeState(PlayerStaEnum.GetUp);
            return;
        }

        if (canChangeAirAtkAnim)
        {
            PlayNextAirAttackAnim(cirtAttack);
            return;
        }

        if (continueAirAttack)
        {
            PlayNextAirAttackAnim(cirtAttack);
        }
        else
        {
            canChangeAirAtkAnim = true;
        }
    }

    public void AirAttackFinish()
    {
        cirtAttack = false;
        continueAirAttack = false;
        canChangeAirAtkAnim = false;
        _playerAction.ChangeState(PlayerStaEnum.Fall1);
    }

    /// <summary>
    /// 空中攻击恢复
    /// </summary>
    public void AirAttackRecover()
    {
        cirtAttack = false;
        AirAttackReset();
    }

    private int AirCirtComboCheck(int nextID)
    {
        // if (attackID == 1 && nextID == 13 && R.Player.EnhancementSaveData.AirCombo2 == 0)
        // {
        // 	return -1;
        // }
        // if (attackID == 2 && nextID == 11 && R.Player.EnhancementSaveData.AirAvatarAttack == 0)
        // {
        // 	return -1;
        // }
        // if (attackID == 3 && nextID == 7 && R.Player.EnhancementSaveData.AirCombo1 == 0)
        // {
        // 	return -1;
        // }
        return nextID;
    }

    /// <summary>
    /// 处理上升
    /// </summary>
    public void HandleUpRising()
    {
        "处理上升".Log();
        R.Player.AnimEvent.PhysicReset();
        AirAttackReset();
        _playerAction.ChangeState(PlayerStaEnum.UpRising);
    }

    public void HandleHitGround()
    {
        //player.ChangeState(PlayerAction.StateEnum.HitGround);
    }

    public void HandleExecute(bool inAir, EnemyAttribute eAttr)
    {
        // if (eAttr.rankType == EnemyAttribute.RankType.Normal)
        // {
        // 	NormalEnemyExecute(inAir, eAttr);
        // }
        // else
        // {
        // 	listener.isFalling = false;
        // 	listener.checkFallDown = false;
        // 	listener.airAtkDown = false;
        // 	listener.checkHitGround = false;
        // 	listener.PhysicReset();
        // 	transform.position = transform.position.SetY(eAttr.transform.position.y);
        // 	execute.SpecicalEnemyQTE(eAttr.transform);
        // }
        // eAttr.GetComponent<EnemyBaseHurt>().QTECameraStart();
        // listener.StopIEnumerator("FlashPositionSet");
    }

    private void NormalEnemyExecute(bool inAir, EnemyAttribute eAttr)
    {
        // if (inAir)
        // {
        // 	PlayerAction.StateEnum sta = (Random.Range(0, 2) != 0) ? PlayerAction.StateEnum.NewExecuteAir2_1 : PlayerAction.StateEnum.NewExecuteAir1_1;
        // 	player.ChangeState(sta);
        // }
        // else
        // {
        // 	PlayerAction.StateEnum sta2 = (Random.Range(0, 2) != 0) ? PlayerAction.StateEnum.NewExecute2_0 : PlayerAction.StateEnum.NewExecute1_1;
        // 	if (!eAttr.accpectAirExecute)
        // 	{
        // 		sta2 = PlayerAction.StateEnum.NewExecute1_1;
        // 	}
        // 	player.ChangeState(sta2);
        // 	if (eAttr.isOnGround)
        // 	{
        // 		eAttr.stiffTime = 1f;
        // 		eAttr.GetComponent<EnemyBaseAction>().AnimReady();
        // 	}
        // }
    }

    private void UpdateCharge()
    {
        // if (startCharge)
        // {
        // 	chargeTime = Mathf.Clamp(chargeTime + Time.deltaTime, 0f,pm.PAttribute.maxChargeTime);
        // 	bool flag = stateMachine.currentState == "AirCharging";
        // 	if (lastFrameTime < 2.5f && chargeTime >= 2.5f)
        // 	{
        // 		Input.Vibration.Vibrate(2);
        // 		if (flag)
        // 		{
        // 			airRelease = 5f;
        // 			listener.chargeAnim.ChargeOneOverAir();
        // 		}
        // 		else
        // 		{
        // 			listener.chargeAnim.ChargeOneOver();
        // 		}
        // 	}
        // 	lastFrameTime = chargeTime;
        // }
        // if (airRelease > 0f)
        // {
        // 	airRelease = Mathf.Clamp(airRelease - Time.unscaledDeltaTime, 0f, float.MaxValue);
        // 	if (Math.Abs(airRelease) < 1.401298E-45f)
        // 	{
        // 		ReleaseCharge(true);
        // 	}
        // }
    }

    public void StartCharge(bool inAir)
    {
        // startCharge = true;
        // listener.charge = false;
        //player.ChangeState((!inAir) ? PlayerAction.StateEnum.Charge1Ready : PlayerAction.StateEnum.AirCharging);
    }

    /// <summary>
    /// 释放充电
    /// </summary>
    /// <param name="inAir"></param>
    public void ReleaseCharge(bool inAir)
    {
        // startCharge = false;
        // listener.charge = true;
        // listener.ChargeEffectDisappear();
        // //PlayerAction.StateEnum sta = (!inAir) ? PlayerAction.StateEnum.Charge1End : PlayerAction.StateEnum.AirChargeEnd;
        // //player.ChangeState(sta);
        // R.Player.Abilities.charge.ChargeReset();
        // chargeTime = 0f;
        // airRelease = 0f;
    }

    public void ChargeCancel()
    {
        // listener.ChargeEffectDisappear();
        // startCharge = false;
        // chargeTime = 0f;
        // airRelease = 0f;
    }

    public void AddChargeLevel()
    {
        chargeTime = Mathf.Clamp(chargeTime + 1.5f, 0f, maxChargeTime);
    }

    /// <summary>
    /// 处理阴影攻击
    /// </summary>
    public void HandleShadeAttack()
    {
        "HandleShadeAttack".Log();
        // R.Player.TimeController.SetSpeed(Vector2.zero);
        // R.Player.Rigidbody2D.gravityScale = 0f;
        // listener.isFalling = false;
        // listener.checkFallDown = false;
        // listener.checkHitGround = false;
        // //player.ChangeState(PlayerAction.StateEnum.Disappear);
        // Transform transform = R.Effect.Generate(191, null, this.transform.position);
        // Vector3 localScale = transform.localScale;
        // localScale.x *= -(float)pAttr.faceDir;
        // transform.localScale = localScale;
    }

    public void HandleBladeStorm()
    {
        //R.Player.TimeController.SetSpeed(Vector2.zero);
        //player.ChangeState(PlayerAction.StateEnum.RollGround);
    }

    /// <summary>
    /// 空中攻击重置
    /// </summary>
    public void AirAttackReset()
    {
        if (!airAttackReset)
            airAttackReset = true;
    }
}