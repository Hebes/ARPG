using System;
using UnityEngine;

/// <summary>
/// 玩家属性
/// </summary>
public class PlayerAttribute
{
    /// <summary>
    /// 面对敌人的方向-1表示后面，1表示前面
    /// </summary>
    public int faceDir;

    public int maxHP;

    [SerializeField] private int _currentHP;

    /// <summary>
    /// 最大能量
    /// </summary>
    public int maxEnergy;

    [SerializeField] private int _currentEnergy;

    public float moveSpeed;

    /// <summary>
    /// 闪的等级
    /// </summary>
    public int flashLevel = 1;

    /// <summary>
    /// 闪的当前次数
    /// </summary>
    public int currentFlashTimes;

    /// <summary>
    /// 闪的CD
    /// </summary>
    public int FlashCd;

    /// <summary>
    /// 是否躲避
    /// </summary>
    public bool flashFlag;

    [HideInInspector] public float maxChargeTime;

    public int maxChargeEndureDamage;

    public int baseAtk;


    /// <summary>
    /// 当前生命值
    /// </summary>
    public int currentHP
    {
        get => _currentHP;
        set => _currentHP = Mathf.Clamp(value, 0, maxHP);
    }

    /// <summary>
    /// 当前能量
    /// </summary>
    public int currentEnergy
    {
        get => _currentEnergy;
        set => _currentEnergy = Mathf.Clamp(value, 0, maxEnergy);
    }

    /// <summary>
    /// 闪的最大次数
    /// </summary>
    public int flashTimes => flashLevel < 2 ? 3 : 5;

    /// <summary>
    /// 正在充电
    /// </summary>
    public bool isInCharging => R.Player.StateMachine.currentState == "Charging1" ||
                                R.Player.StateMachine.currentState == "Charge1Ready" ||
                                R.Player.StateMachine.currentState == "AirCharging";

    /// <summary>
    /// 是否在地面
    /// </summary>
    public bool isOnGround => R.Player.PlatformMovement.State.IsDetectedGround;

    /// <summary>
    /// 是否死亡
    /// </summary>
    public bool isDead => currentHP <= 0;

    /// <summary>
    /// 玩家碰撞盒
    /// </summary>
    public Bounds bounds => R.Player.GetComponent<Collider2D>().bounds;

    /// <summary>
    /// 重置数据
    /// </summary>
    public void ResetData()
    {
        SetBaseLevelData();
        AllAttributeRecovery();
    }

    /// <summary>
    /// 设置基准数据
    /// </summary>
    private void SetBaseLevelData()
    {
        maxHP = 20; // DB.EnhancementDic["maxHP"].GetEnhanceEffect(R.Player.EnhancementSaveData.MaxHp);
        baseAtk = !Debug.isDebugBuild ? 40 : !R.Settings.CheatMode ? 40 : 9999;
        maxEnergy = R.GameData.Difficulty != 3 ? 10 : 1;
        moveSpeed = 9f;
        currentFlashTimes = flashTimes;
        maxChargeTime = 2.5f;
    }

    /// <summary>
    /// 全部属性恢复
    /// </summary>
    public void AllAttributeRecovery()
    {
        maxHP = 20; // DB.EnhancementDic["maxHP"].GetEnhanceEffect(R.Player.EnhancementSaveData.MaxHp);
        currentHP = maxHP;
        currentEnergy = maxEnergy;
        currentFlashTimes = flashTimes;
        //R.Ui.Flash.RecoverAll(flashTimes);//闪的格子
    }
}