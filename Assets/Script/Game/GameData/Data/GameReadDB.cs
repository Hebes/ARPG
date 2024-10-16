using System.Collections.Generic;
using LitJson;
using UnityEngine;

/// <summary>
/// 游戏数据
/// </summary>
public class GameReadDB
{
    /// <summary>
    /// 玩家攻击数据
    /// </summary>
    public static readonly Dictionary<string, Dictionary<PlayerAtkDataType, string>> PlayerAtkData =
        new Dictionary<string, Dictionary<PlayerAtkDataType, string>>
        {
            {
                PlayerStaEnum.Atk1.ToString(), new Dictionary<PlayerAtkDataType, string>
                {
                    { PlayerAtkDataType.hitTimes, "0" }, //命中次数
                    { PlayerAtkDataType.interval, "0" }, //间隔
                    { PlayerAtkDataType.hitType, "0" }, //间隔
                    { PlayerAtkDataType.damagePercent, "1" }, //伤害百分比
                    { PlayerAtkDataType.atkName, PlayerStaEnum.Atk1.ToString() }, //攻击名称
                    { PlayerAtkDataType.shakeClip, "0" }, //振动帧
                    { PlayerAtkDataType.shakeOffset, "0" }, //振动偏移
                    { PlayerAtkDataType.shakeType, "0" }, //振动类型
                    { PlayerAtkDataType.frozenClip, "0" }, //冻结帧
                    { PlayerAtkDataType.frameShakeClip, "8" }, //帧振动
                    { PlayerAtkDataType.joystickShakeNum, "-1" }, //摇杆摇数
                }
            },
            {
                PlayerStaEnum.Atk2.ToString(), new Dictionary<PlayerAtkDataType, string>
                {
                    { PlayerAtkDataType.hitTimes, "0" }, //命中次数
                    { PlayerAtkDataType.interval, "0" }, //间隔
                    { PlayerAtkDataType.hitType, "0" }, //间隔
                    { PlayerAtkDataType.damagePercent, "1" }, //伤害百分比
                    { PlayerAtkDataType.atkName, PlayerStaEnum.Atk2.ToString() }, //攻击名称
                    { PlayerAtkDataType.shakeClip, "0" }, //振动帧
                    { PlayerAtkDataType.shakeOffset, "0" }, //振动偏移
                    { PlayerAtkDataType.shakeType, "0" }, //振动类型
                    { PlayerAtkDataType.frozenClip, "0" }, //冻结帧
                    { PlayerAtkDataType.frameShakeClip, "8" }, //帧振动
                    { PlayerAtkDataType.joystickShakeNum, "-1" }, //摇杆摇数
                }
            },
        };

    /// <summary>
    /// 关卡敌人数据
    /// </summary>
    /// <returns></returns>
    public static readonly Dictionary<string, LevelEnemyData> LevelEnemyData = new Dictionary<string, LevelEnemyData>
    {
        {
            CScene.Game2, new LevelEnemyData
            {
                enemyQueueData = new EnemyQueueData(),
                enemyJsonObject = new[]
                {
                    new EnemyJsonObject("女猎手1", new Vector2(-13.69f, -3.31f), "0-0"),
                },
            }
        }
    };

    /// <summary>
    /// 敌人攻击数据
    /// {"DaoAtk1":{"xSpeed":3.0,"ySpeed":0.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtk1","airAnimName":"UnderAtkHitToFly","damagePercent":1.0,"shieldDamage":3},
    /// "DaoAtk2":{"xSpeed":5.0,"ySpeed":7.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtkHitToFly","airAnimName":"UnderAtkHitToFly","damagePercent":0.800000011920929,"shieldDamage":3},
    /// "DaoAtk3":{"xSpeed":3.0,"ySpeed":0.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtk1","airAnimName":"UnderAtkHitToFly","damagePercent":1.0,"shieldDamage":3},
    /// "DaoAtk4":{"xSpeed":5.0,"ySpeed":7.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtkHitToFly","airAnimName":"UnderAtkHitToFly","damagePercent":1.5,"shieldDamage":2},
    /// "DaoAtk5":{"xSpeed":5.0,"ySpeed":7.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtkHitToFly","airAnimName":"UnderAtkHitToFly","damagePercent":1.0,"shieldDamage":5},
    /// "DaoAtk6_1":{"xSpeed":3.0,"ySpeed":0.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtk1","airAnimName":"UnderAtkHitToFly","damagePercent":1.0,"shieldDamage":3},
    /// "DaoAtk6_2":{"xSpeed":5.0,"ySpeed":7.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtkHitToFly","airAnimName":"UnderAtkHitToFly","damagePercent":1.5,"shieldDamage":3},
    /// "Bullet":{"xSpeed":5.0,"ySpeed":0.0,"airXSpeed":5.0,"airYSpeed":7.0,"animName":"UnderAtk1","airAnimName":"UnderAtkHitToFly","damagePercent":1.0,"shieldDamage":2}}
    /// </summary>
    public static readonly Dictionary<EnemyType, Dictionary<string, Dictionary<EnemyAttackDataType, string>>> EnemyAttackData =
        new Dictionary<EnemyType, Dictionary<string, Dictionary<EnemyAttackDataType, string>>>
        {
            {
                EnemyType.女猎手, new Dictionary<string, Dictionary<EnemyAttackDataType, string>>
                {
                    {
                        EnemyStaEnum.Atk1.ToString(), new Dictionary<EnemyAttackDataType, string>()
                        {
                            { EnemyAttackDataType.xSpeed, "3.0" },
                            { EnemyAttackDataType.ySpeed, "0.0" },
                            { EnemyAttackDataType.airXSpeed, "0.0" },
                            { EnemyAttackDataType.airYSpeed, "0.0" },
                            { EnemyAttackDataType.animName, "UnderAtk1" },
                            { EnemyAttackDataType.airAnimName, "UnderAtkHitToFly" },
                            { EnemyAttackDataType.damagePercent, "1.0" },
                            { EnemyAttackDataType.shieldDamage, "3" },
                        }
                    },
                    {
                        EnemyStaEnum.Atk2.ToString(), new Dictionary<EnemyAttackDataType, string>()
                        {
                            { EnemyAttackDataType.xSpeed, "3.0" },
                            { EnemyAttackDataType.ySpeed, "0.0" },
                            { EnemyAttackDataType.airXSpeed, "0.0" },
                            { EnemyAttackDataType.airYSpeed, "0.0" },
                            { EnemyAttackDataType.animName, "UnderAtk1" },
                            { EnemyAttackDataType.airAnimName, "UnderAtkHitToFly" },
                            { EnemyAttackDataType.damagePercent, "1.0" },
                            { EnemyAttackDataType.shieldDamage, "3" },
                        }
                    },
                    {
                        EnemyStaEnum.Atk3.ToString(), new Dictionary<EnemyAttackDataType, string>()
                        {
                            { EnemyAttackDataType.xSpeed, "3.0" },
                            { EnemyAttackDataType.ySpeed, "0.0" },
                            { EnemyAttackDataType.airXSpeed, "0.0" },
                            { EnemyAttackDataType.airYSpeed, "0.0" },
                            { EnemyAttackDataType.animName, "UnderAtk1" },
                            { EnemyAttackDataType.airAnimName, "UnderAtkHitToFly" },
                            { EnemyAttackDataType.damagePercent, "1.0" },
                            { EnemyAttackDataType.shieldDamage, "3" },
                        }
                    },
                }
            },
        };

    /// <summary>
    /// 敌人受伤数据
    /// {"AirAtk1":{"xSpeed":0.0,"ySpeed":5.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "AirAtk2":{"xSpeed":0.0,"ySpeed":5.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "AirAtk6":{"xSpeed":0.0,"ySpeed":5.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "AirAtk7":{"xSpeed":0.0,"ySpeed":0.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "DoubleFlashAir":{"xSpeed":0.0,"ySpeed":0.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "RollEndFrame":{"xSpeed":0.0,"ySpeed":-60.0,"airXSpeed":0.0,"airYSpeed":-60.0,"normalAtkType":"Fall","airAtkType":"Fall"},
    /// "AirAtkHv1":{"xSpeed":3.0,"ySpeed":8.0,"airXSpeed":3.0,"airYSpeed":9.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "AirAtkHv2":{"xSpeed":5.0,"ySpeed":-5.0,"airXSpeed":10.0,"airYSpeed":-10.0,"normalAtkType":"HitToFly1","airAtkType":"Fall"},
    /// "AirAtkHv5":{"xSpeed":0.0,"ySpeed":12.0,"airXSpeed":0.0,"airYSpeed":12.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "AirAtkHv1Push":{"xSpeed":10.0,"ySpeed":0.0,"airXSpeed":10.0,"airYSpeed":4.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "Atk1":{"xSpeed":0.0,"ySpeed":0.0,"airXSpeed":0.0,"airYSpeed":2.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "Atk2":{"xSpeed":0.0,"ySpeed":0.0,"airXSpeed":0.0,"airYSpeed":2.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "Atk3":{"xSpeed":0.0,"ySpeed":5.0,"airXSpeed":0.0,"airYSpeed":5.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "Atk4":{"xSpeed":0.0,"ySpeed":0.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "Atk5":{"xSpeed":6.0,"ySpeed":0.0,"airXSpeed":5.0,"airYSpeed":2.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "Atk6":{"xSpeed":20.0,"ySpeed":8.0,"airXSpeed":20.0,"airYSpeed":8.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "Atk14":{"xSpeed":8.0,"ySpeed":15.0,"airXSpeed":8.0,"airYSpeed":5.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "Atk15":{"xSpeed":6.0,"ySpeed":0.0,"airXSpeed":5.0,"airYSpeed":2.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "Atk16":{"xSpeed":8.0,"ySpeed":10.0,"airXSpeed":8.0,"airYSpeed":10.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "Atk23":{"xSpeed":0.0,"ySpeed":0.0,"airXSpeed":0.0,"airYSpeed":2.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "DoubleFlash":{"xSpeed":0.0,"ySpeed":0.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "AtkFlashRollEnd":{"xSpeed":0.0,"ySpeed":-30.0,"airXSpeed":0.0,"airYSpeed":-30.0,"normalAtkType":"Fall","airAtkType":"Fall"},
    /// "AtkHv1":{"xSpeed":6.0,"ySpeed":0.0,"airXSpeed":6.0,"airYSpeed":2.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "AtkHv2":{"xSpeed":6.0,"ySpeed":0.0,"airXSpeed":6.0,"airYSpeed":2.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "AtkHv3":{"xSpeed":0.0,"ySpeed":12.0,"airXSpeed":0.0,"airYSpeed":12.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "AtkHv1Push":{"xSpeed":10.0,"ySpeed":0.0,"airXSpeed":10.0,"airYSpeed":4.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "AtkRollEnd":{"xSpeed":0.0,"ySpeed":0.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "AirAtkRoll":{"xSpeed":0.0,"ySpeed":-30.0,"airXSpeed":0.0,"airYSpeed":-30.0,"normalAtkType":"HitFall","airAtkType":"HitFall"},
    /// "HitGround":{"xSpeed":0.0,"ySpeed":-30.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"Fall","airAtkType":"HitToFly2"},
    /// "RollReady":{"xSpeed":0.0,"ySpeed":0.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "BladeStormReady":{"xSpeed":0.0,"ySpeed":0.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"},
    /// "RollGround":{"xSpeed":0.0,"ySpeed":0.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "RollEnd":{"xSpeed":0.0,"ySpeed":-30.0,"airXSpeed":0.0,"airYSpeed":-30.0,"normalAtkType":"Fall","airAtkType":"Fall"},
    /// "UpRising":{"xSpeed":0.0,"ySpeed":25.0,"airXSpeed":0.0,"airYSpeed":25.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly1"},
    /// "AtkUpRising":{"xSpeed":0.0,"ySpeed":25.0,"airXSpeed":0.0,"airYSpeed":25.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly1"},
    /// "NewExecute2_1":{"xSpeed":0.0,"ySpeed":25.0,"airXSpeed":0.0,"airYSpeed":25.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly1"},
    /// "Charge1EndLevel1":{"xSpeed":1.0,"ySpeed":20.0,"airXSpeed":0.0,"airYSpeed":20.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "Charge1EndLevel2":{"xSpeed":1.0,"ySpeed":20.0,"airXSpeed":0.0,"airYSpeed":12.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "Charge1EndLevel3":{"xSpeed":1.0,"ySpeed":20.0,"airXSpeed":0.0,"airYSpeed":12.0,"normalAtkType":"HitToFly1","airAtkType":"HitToFly2"},
    /// "ShadeAtkSkill":{"xSpeed":0.0,"ySpeed":0.0,"airXSpeed":0.0,"airYSpeed":0.0,"normalAtkType":"Hit1","airAtkType":"HitToFly2"}}
    /// </summary>
    public static readonly Dictionary<EnemyType, Dictionary<string, Dictionary<EnemyHurtDataType, string>>> EnemyHurtData =
        new Dictionary<EnemyType, Dictionary<string, Dictionary<EnemyHurtDataType, string>>>
        {
            {
                EnemyType.女猎手, new Dictionary<string, Dictionary<EnemyHurtDataType, string>>
                {
                    {
                        EnemyStaEnum.Atk1.ToString(), new Dictionary<EnemyHurtDataType, string>()
                        {
                            { EnemyHurtDataType.xSpeed, "3.0" },
                            { EnemyHurtDataType.ySpeed, "0.0" },
                            { EnemyHurtDataType.airXSpeed, "5.0" },
                            { EnemyHurtDataType.airYSpeed, "3.0" },
                            { EnemyHurtDataType.normalAtkType, EnemyStaEnum.Hurt.ToString() },
                            { EnemyHurtDataType.airAtkType, EnemyStaEnum.Hurt.ToString() },
                        }
                    },
                    {
                        EnemyStaEnum.Atk2.ToString(), new Dictionary<EnemyHurtDataType, string>()
                        {
                            { EnemyHurtDataType.xSpeed, "5.0" },
                            { EnemyHurtDataType.ySpeed, "7.0" },
                            { EnemyHurtDataType.airXSpeed, "5.0" },
                            { EnemyHurtDataType.airYSpeed, "3.0" },
                            { EnemyHurtDataType.normalAtkType, EnemyStaEnum.Hurt.ToString() },
                            { EnemyHurtDataType.airAtkType, EnemyStaEnum.Hurt.ToString() },
                        }
                    },
                    {
                        EnemyStaEnum.Atk3.ToString(), new Dictionary<EnemyHurtDataType, string>()
                        {
                            { EnemyHurtDataType.xSpeed, "5.0" },
                            { EnemyHurtDataType.ySpeed, "7.0" },
                            { EnemyHurtDataType.airXSpeed, "5.0" },
                            { EnemyHurtDataType.airYSpeed, "3.0" },
                            { EnemyHurtDataType.normalAtkType, EnemyStaEnum.Hurt.ToString() },
                            { EnemyHurtDataType.airAtkType, EnemyStaEnum.Hurt.ToString() },
                        }
                    },
                }
            },
            {
                EnemyType.稻草人, new Dictionary<string, Dictionary<EnemyHurtDataType, string>>
                {
                    {
                        EnemyStaEnum.Atk1.ToString(), new Dictionary<EnemyHurtDataType, string>()
                        {
                            { EnemyHurtDataType.xSpeed, "3.0" },
                            { EnemyHurtDataType.ySpeed, "0.0" },
                            { EnemyHurtDataType.airXSpeed, "5.0" },
                            { EnemyHurtDataType.airYSpeed, "3.0" },
                            { EnemyHurtDataType.normalAtkType, EnemyStaEnum.Hurt.ToString() },
                            { EnemyHurtDataType.airAtkType, EnemyStaEnum.Hurt.ToString() },
                        }
                    },
                    {
                        EnemyStaEnum.Atk2.ToString(), new Dictionary<EnemyHurtDataType, string>()
                        {
                            { EnemyHurtDataType.xSpeed, "5.0" },
                            { EnemyHurtDataType.ySpeed, "7.0" },
                            { EnemyHurtDataType.airXSpeed, "5.0" },
                            { EnemyHurtDataType.airYSpeed, "3.0" },
                            { EnemyHurtDataType.normalAtkType, EnemyStaEnum.Hurt.ToString() },
                            { EnemyHurtDataType.airAtkType, EnemyStaEnum.Hurt.ToString() },
                        }
                    },
                    {
                        EnemyStaEnum.Atk3.ToString(), new Dictionary<EnemyHurtDataType, string>()
                        {
                            { EnemyHurtDataType.xSpeed, "5.0" },
                            { EnemyHurtDataType.ySpeed, "7.0" },
                            { EnemyHurtDataType.airXSpeed, "5.0" },
                            { EnemyHurtDataType.airYSpeed, "3.0" },
                            { EnemyHurtDataType.normalAtkType, EnemyStaEnum.Hurt.ToString() },
                            { EnemyHurtDataType.airAtkType, EnemyStaEnum.Hurt.ToString() },
                        }
                    },
                }
            },
        };


    /// <summary>
    /// 敌人攻击玩家,玩家的受伤数据
    /// </summary>
    public static readonly Dictionary<EnemyType, Dictionary<string, Dictionary<PlayerHurtDataType, string>>> PlayerHurtData =
        new Dictionary<EnemyType, Dictionary<string, Dictionary<PlayerHurtDataType, string>>>()
        {
            {
                EnemyType.女猎手, new Dictionary<string, Dictionary<PlayerHurtDataType, string>>()
                {
                    {
                        PlayerStaEnum.Atk2.ToString(), new Dictionary<PlayerHurtDataType, string>()
                        {
                            { PlayerHurtDataType.xSpeed, "3.0" },
                            { PlayerHurtDataType.ySpeed, "0.0" },
                            { PlayerHurtDataType.airXSpeed, "5.0" },
                            { PlayerHurtDataType.airYSpeed, "3.0" },
                            { PlayerHurtDataType.animName, EnemyStaEnum.Hurt.ToString() },
                            { PlayerHurtDataType.airAnimName, EnemyStaEnum.Hurt.ToString() },
                            { PlayerHurtDataType.damagerPercent, "1" },
                            { PlayerHurtDataType.shidelDamage, "3" },
                        }
                    },
                    {
                        PlayerStaEnum.AtkRemote.ToString(), new Dictionary<PlayerHurtDataType, string>()
                        {
                            { PlayerHurtDataType.xSpeed, "3.0" },
                            { PlayerHurtDataType.ySpeed, "0.0" },
                            { PlayerHurtDataType.airXSpeed, "5.0" },
                            { PlayerHurtDataType.airYSpeed, "3.0" },
                            { PlayerHurtDataType.animName, EnemyStaEnum.Hurt.ToString() },
                            { PlayerHurtDataType.airAnimName, EnemyStaEnum.Hurt.ToString() },
                            { PlayerHurtDataType.damagerPercent, "1" },
                            { PlayerHurtDataType.shidelDamage, "5" },
                        }
                    }
                }
            },
        };

    /// <summary>
    /// 振动数据
    /// </summary>
    public static readonly float[][] VibrationData = new[]
    {
        new float[] { 0.2f, 0.1f, 0f, 0f },
        new float[] { 0.5f, 0.4f, 0.3f, 0.1f },
        new float[] { 0f, 0f, 0f, 0f },
        new float[] { 0.8f, 0.7f, 0.6f, 0f },
        new float[] { 0.7f, 0.6f, 0.5f, 0f },
        new float[] { 0.8f, 0.8f, 0.8f, 0f },
        new float[] { 0.7f, 0.7f, 0.6f, 0.5f, 0f, 0f },
        new float[] { 0.9f, 0.9f, 0.8f, 0.7f, 0.6f, 0.2f },
        new float[] { 0.3f, 0.3f, 0.2f, 0f },
        new float[] { 0.8f, 0.7f, 0.6f, 0f },
        new float[] { 0.5f, 0.4f, 0.3f, 0.2f },
        new float[] { 0.9f, 0.9f, 0.8f, 0.7f },
        new float[] { 0.8f, 0.8f, 0.7f, 0.6f, 0.5f, 0f },
        new float[] { 0.9f, 0.9f, 0.8f, 0.7f, 0.6f, 0.5f },
        new float[] { 1f, 1f, 0.9f, 0.8f },
        new float[] { 0.9f, 0.9f, 0.9f, 0f },
    };

    

    /// <summary>
    /// 音乐映射列表
    /// </summary>
    public static readonly List<AudioManager.AudioMap> AudioList = new List<AudioManager.AudioMap>()
    {
        new AudioManager.AudioMap(AudioMapType.EnemyBoss.ToString(), AudioMapType.EnemyBoss.ToString()),
        new AudioManager.AudioMap(AudioMapType.EnemyElite.ToString(), AudioMapType.EnemyElite.ToString()),
        new AudioManager.AudioMap(AudioMapType.EnemyNormal.ToString(), AudioMapType.EnemyNormal.ToString()),
        new AudioManager.AudioMap(AudioMapType.PlayerAtk.ToString(), AudioMapType.PlayerAtk.ToString()),
        new AudioManager.AudioMap(AudioMapType.PlayerMaterial.ToString(), AudioMapType.PlayerMaterial.ToString()),
        new AudioManager.AudioMap(AudioMapType.PlayerMove.ToString(), AudioMapType.PlayerMove.ToString()),
        new AudioManager.AudioMap(AudioMapType.Scene.ToString(), AudioMapType.Scene.ToString()),
        new AudioManager.AudioMap(AudioMapType.Special.ToString(), AudioMapType.Special.ToString()),
        new AudioManager.AudioMap(AudioMapType.UnSorted.ToString(), AudioMapType.UnSorted.ToString()),
        new AudioManager.AudioMap(AudioMapType.Video.ToString(), AudioMapType.Video.ToString()),
    };

    public IDictionary<int, AudioClipData> AudioClipDataDic;
}

public enum PlayerAtkDataType
{
    /// <summary>
    /// 攻击名称
    /// </summary>
    atkName,

    /// <summary>
    /// 振动偏移
    /// </summary>
    shakeOffset,

    /// <summary>
    /// 振动速度
    /// </summary>
    shakeSpeed,

    /// <summary>
    /// 振动帧
    /// </summary>
    shakeClip,

    /// <summary>
    /// 振动帧
    /// </summary>
    frameShakeClip,

    /// <summary>
    /// 冻结帧
    /// </summary>
    frozenClip,

    /// <summary>
    /// 振动类型
    /// </summary>
    shakeType,

    /// <summary>
    /// 伤害百分比
    /// </summary>
    damagePercent,

    /// <summary>
    /// 间隔
    /// </summary>
    interval,

    /// <summary>
    /// 命中次数
    /// </summary>
    hitTimes,

    /// <summary>
    /// 命中类型 1 Once, 2 Limited, 3 UnLimited
    /// </summary>
    hitType,

    /// <summary>
    /// 摇杆是否振动 1 true -1 false
    /// </summary>
    joystickShakeNum,

    /// <summary>
    /// 援助类型
    /// </summary>
    assistanceType,

    /// <summary>
    /// 检查类型
    /// </summary>
    assistanceCheckType,

    /// <summary>
    /// 自动旋转 1 true -1 false
    /// </summary>
    autoTurn,

    /// <summary>
    /// 保持距离 1 true -1 false
    /// </summary>
    keepDis,
}

public enum EnemyAttackDataType
{
    /// <summary>
    /// x速度
    /// </summary>
    xSpeed,

    /// <summary>
    /// y速度
    /// </summary>
    ySpeed,

    /// <summary>
    /// 空中X速度
    /// </summary>
    airXSpeed,

    /// <summary>
    /// 空中y速度
    /// </summary>
    airYSpeed,

    /// <summary>
    /// 动画名称
    /// </summary>
    animName,

    /// <summary>
    /// 空中动画名称
    /// </summary>
    airAnimName,

    /// <summary>
    /// 伤害百分比
    /// </summary>
    damagePercent,

    /// <summary>
    /// 盾伤害
    /// </summary>
    shieldDamage,
}

public enum EnemyHurtDataType
{
    /// <summary>
    /// x速度
    /// </summary>
    xSpeed,

    /// <summary>
    /// y速度
    /// </summary>
    ySpeed,

    /// <summary>
    /// 空中X速度
    /// </summary>
    airXSpeed,

    /// <summary>
    /// 空中y速度
    /// </summary>
    airYSpeed,

    /// <summary>
    /// 默认攻击类型,敌人的受伤类型
    /// </summary>
    normalAtkType,

    /// <summary>
    /// 空中攻击类型
    /// </summary>
    airAtkType,
}

public enum PlayerHurtDataType
{
    /// <summary>
    /// x速度
    /// </summary>
    xSpeed,

    /// <summary>
    /// y速度
    /// </summary>
    ySpeed,

    /// <summary>
    /// 空中X速度
    /// </summary>
    airXSpeed,

    /// <summary>
    /// 空中y速度
    /// </summary>
    airYSpeed,

    /// <summary>
    /// 受到攻击的动画名称
    /// </summary>
    animName,

    /// <summary>
    /// 空中受到攻击的动画名称
    /// </summary>
    airAnimName,

    /// <summary>
    /// 伤害百分比
    /// </summary>
    damagerPercent,

    /// <summary>
    /// 护盾的损伤
    /// </summary>
    shidelDamage,
}

public enum AudioMapType
{
    /// <summary>
    /// BOSS
    /// </summary>
    EnemyBoss,

    /// <summary>
    /// 精英
    /// </summary>
    EnemyElite,

    /// <summary>
    /// 默认
    /// </summary>
    EnemyNormal,

    /// <summary>
    /// 玩家攻击
    /// </summary>
    PlayerAtk,

    /// <summary>
    /// 玩家材质
    /// </summary>
    PlayerMaterial,

    /// <summary>
    /// 玩家移动
    /// </summary>
    PlayerMove,

    /// <summary>
    /// 场景
    /// </summary>
    Scene,

    /// <summary>
    /// 特殊的
    /// </summary>
    Special,

    /// <summary>
    /// 未分类的
    /// </summary>
    UnSorted,

    /// <summary>
    /// 视频
    /// </summary>
    Video,
}