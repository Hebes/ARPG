using System;

/// <summary>
/// 玩家攻击类型
/// </summary>
public class PlayerAtkType
{
    public static string[] AirAttack = new string[]
    {
        "AirAtk1",
        "AirAtk2",
        "AirAtk6",
        "RollReady"
    };

    public static string[] AirAvatarAttack = new string[]
    {
        "DoubleFlashAir",
        "RollEndFrame"
    };

    public static string AirCombo1 = "AirAtkRoll";

    public static string AirCombo2 = "RollEndFrame";

    public static string[] Attack = new string[]
    {
        "Atk1",
        "Atk2",
        "Atk5",
        "Atk15",
        "AtkRollEnd"
    };

    public static string[] AvatarAttack = new string[]
    {
        "DoubleFlash",
        "AtkFlashRollEnd"
    };

    public static string BladeStorm = "RollGround";

    public static string Charging = "Charge1EndLevel1";

    public static string Chase = "AirAtk7";

    public static string Combo1 = "UpRising";

    public static string[] Combo2 = new string[]
    {
        "Atk23",
        "Atk3",
        "Atk4",
        "Atk16"
    };

    public static string HitGround = "HitGround";

    public static string Knockout = "Atk14";

    public static string ShadeAttack = "ShadeAtkSkill";

    public static string[] TripleAttack = new string[]
    {
        "AtkHv1",
        "AtkHv2",
        "AtkHv3",
        "AirAtkHv1",
        "AirAtkHv2",
        "AirAtkHv5"
    };

    public static string UpperChop = "AtkUpRising";

    public static string[] BreakDefense = new string[]
    {
        "Atk16",
        "AtkHv2",
        "AtkHv3",
        "AtkFlashRollEnd",
        "Atk14",
        "AirAtkHv2",
        "AirAtkHv5"
    };

    public static string[] LightAttack = new string[]
    {
        "Atk1",
        "Atk2",
        "Atk5",
        "Atk15",
        "AtkRollEnd"
    };

    /// <summary>
    /// 重效攻击
    /// </summary>
    public static string[] HeavyEffectAttack = new string[]
    {
        "AirAtkHv2",
        "AtkHv2",
        "Atk16",
        "RollEnd",
        "Atk6",
        "Charge1EndLevel1",
        "RollEndFrame",
        "AtkFlashRollEnd",
        "Atk14"
    };

    /// <summary>
    /// 能打破子弹的状态
    /// </summary>
    public static string[] CantBreakBullet = new string[]
    {
        "Atk1",
    };
}