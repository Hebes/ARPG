using System.Collections.Generic;
using LitJson;
using UnityEngine;

/// <summary>
/// 预加载
/// </summary>
public class Preload : MonoBehaviour
{
    private void Awake()
    {
        ResolutionOption.I.SetResolutionByQualitylevel(); //设置分辨率
        EnemyGenerator.PreloadEnemyPrefabs(); //预加载敌人预制体
        DB.Preload();
        EffectController.AllowPreload = true;
    }
}

/// <summary>
/// 数据
/// </summary>
public class DB
{
    private static bool _isPreloaded;

    public static IDictionary<int, AudioClipData> AudioClipDataDic; //音频数据文件

    //public static IDictionary<int, CameraEffectProxyPrefabData> CameraEffectProxyPrefabDataDic; //摄像机效果代理预制数据
    public static IList<EnemyAttrData> EnemyAttrDataList; //敌方攻击数据
    //public static IDictionary<string, Enhancement> EnhancementDic; //增强数据
    //public static IDictionary<string, VoiceOver> VoiceOversDic; //配音

    public static JsonData WeaponConfigure; //组合攻击
    public static JsonData PlayerAtkData; //玩家攻击数据
    public static JsonData LevelEnemyData; //关卡敌人
    public static JsonData EnemyHurtData; //敌人受伤数据

    public static void Preload()
    {
        if (_isPreloaded) return;
        WeaponConfigure = JsonMapper.ToObject(JsonMapper.ToJson(WeaponConfigureDic)); //MobileCombo
        PlayerAtkData = JsonMapper.ToObject(JsonMapper.ToJson(PlayerAtkDataTemp));
        LevelEnemyData = JsonMapper.ToObject(JsonMapper.ToJson(LevelEnemyDataTemp));
        EnemyHurtData = JsonMapper.ToObject(JsonMapper.ToJson(EnemyHurtDataTemp));
        AudioClipDataDic = CSVHelper.Csv2Dictionary("AudioClipData", int.Parse, AudioClipData.SetValue);
        //CameraEffectProxyPrefabDataDic = CSVHelper.Csv2Dictionary("CameraEffectProxyPrefabData", int.Parse, CameraEffectProxyPrefabData.SetValue);
        EnemyAttrDataList = CSVHelper.Csv2List("EnemyAttrData", EnemyAttrData.SetValue);
        //EnhancementDic = CSVHelper.Csv2Dictionary("Enhancement", a => a, Enhancement.SetValue);
        //VoiceOversDic = CSVHelper.Csv2Dictionary("VoiceOver", a => a, VoiceOver.SetValue);
        _isPreloaded = true;
    }

    private static T ParseJson<T>(string name) where T : UnityEngine.Object
    {
        return Asset.LoadFromResources<T>("conf/", name);
    }


    /// <summary>
    /// {
    ///   "normalAttack":{
    ///        "1":{"anim":"Atk1","nextID":"2","nextCirtID":"-1"},
    ///        "2":{"anim":"Atk2","nextID":"3","nextCirtID":"5"},
    ///        "3":{"anim":"Atk5","nextID":"4","nextCirtID":"19"},
    ///        "4":{"anim":"Atk15","nextID":"-1","nextCirtID":"-1"},
    ///        "5":{"anim":"Atk23","nextID":"-1","nextCirtID":"6"},
    ///        "6":{"anim":"Atk3","nextID":"-1","nextCirtID":"7"},
    ///        "7":{"anim":"Atk4","nextID":"-1","nextCirtID":"-1"},
    ///        "8":{"anim":"Atk16","nextID":"-1","nextCirtID":"-1"},
    ///        "9":{"anim":"AtkHv1","nextID":"-1","nextCirtID":"10"},
    ///        "10":{"anim":"AtkHv2","nextID":"-1","nextCirtID":"11"},
    ///        "11":{"anim":"AtkHv3","nextID":"-1","nextCirtID":"-1"},
    ///        "12":{"anim":"AtkHv1Push","nextID":"-1","nextCirtID":"10"},
    ///        "13":{"anim":"RollGround","nextID":"-1","nextCirtID":"-1"},
    ///        "14":{"anim":"Atk6","nextID":"-1","nextCirtID":"-1"},
    ///        "15":{"anim":"AtkUpRising","nextID":"-1","nextCirtID":"-1"},
    ///        "16":{"anim":"AtkRollEnd","nextID":"-1","nextCirtID":"-1"},
    ///        "17":{"anim":"RollGround","nextID":"-1","nextCirtID":"-1"},
    ///        "18":{"anim":"Atk14","nextID":"-1","nextCirtID":"-1"},
    ///        "19":{"anim":"DoubleFlash","nextID":"-1","nextCirtID":"20"},
    ///        "20":{"anim":"AtkFlashRollEnd","nextID":"-1","nextCirtID":"-1"}
    ///      },
    /// "airAttack":{
    ///       "1":{"anim":"AirAtk1","nextID":"2","nextCirtID":"13"},
    ///       "2":{"anim":"AirAtk2","nextID":"3","nextCirtID":"-1"},
    ///       "3":{"anim":"AirAtk6","nextID":"8","nextCirtID":"7"},
    ///       "4":{"anim":"AirAtkHv1","nextID":"-1","nextCirtID":"5"},
    ///       "5":{"anim":"AirAtkHv2","nextID":"-1","nextCirtID":"6"},
    ///       "6":{"anim":"AirAtkHv3","nextID":"-1","nextCirtID":"-1"},
    ///       "7":{"anim":"AirAtkRoll","nextID":"-1","nextCirtID":"-1"},
    ///       "8":{"anim":"RollReady","nextID":"-1","nextCirtID":"-1"},
    ///       "10":{"anim":"AirAtkHv1Push","nextID":"-1","nextCirtID":"5"},
    ///       "11":{"anim":"DoubleFlashAir","nextID":"-1","nextCirtID":"-1"},
    ///       "12":{"anim":"RollEndFrame","nextID":"-1","nextCirtID":"-1"},
    ///       "13":{"anim":"RollEndFrame","nextID":"-1","nextCirtID":"-1"}
    ///     }
    ///}
    /// </summary>
    private static readonly Dictionary<string, Dictionary<string, Dictionary<string, string>>> WeaponConfigureDic = new()
    {
        {
            "normalAttack", new Dictionary<string, Dictionary<string, string>>()
            {
                { "1", new Dictionary<string, string> { { "anim", $"{PlayerStaEnum.Atk1}" }, { "nextID", "2" }, { "nextCirtID", "-1" }, } },
                { "2", new Dictionary<string, string> { { "anim", $"{PlayerStaEnum.Atk2}" }, { "nextID", "3" }, { "nextCirtID", "4" }, } },
                { "3", new Dictionary<string, string> { { "anim", $"{PlayerStaEnum.Atk3}" }, { "nextID", "-1" }, { "nextCirtID", "-1" }, } },
                { "4", new Dictionary<string, string> { { "anim", $"{PlayerStaEnum.DoubleFlash}" }, { "nextID", "-1" }, { "nextCirtID", "5" }, } },
                { "5", new Dictionary<string, string> { { "anim", $"{PlayerStaEnum.AtkFlashRollEnd}" }, { "nextID", "-1" }, { "nextCirtID", "-1" }, } },
            }
        },
        {
            "airAttack", new Dictionary<string, Dictionary<string, string>>()
            {
                { "1", new Dictionary<string, string> { { "anim", $"{PlayerStaEnum.AirAtk1}" }, { "nextID", "2" }, { "nextCirtID", "13" }, } },
                { "2", new Dictionary<string, string> { { "anim", $"{PlayerStaEnum.AirAtk2}" }, { "nextID", "3" }, { "nextCirtID", "-1" }, } },
                { "3", new Dictionary<string, string> { { "anim", $"{PlayerStaEnum.AirAtk3}" }, { "nextID", "-1" }, { "nextCirtID", "7" }, } },
            }
        },
    };

    /// <summary>
    /// 成就
    /// </summary>
    public static readonly Dictionary<string, Dictionary<int, AchievementInfo>> AchievementInfoDic = new()
    {
        {
            AchievementManager.Language, new Dictionary<int, AchievementInfo>()
            {
                { 1, new AchievementInfo(1, "第一次游戏", "第一次玩游戏的成就") },
            }
        }
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
    /// 效果
    /// </summary>
    public static readonly List<EffectAttr> EffectAttrList = new()
    {
        new EffectAttr(0, FXRotationCondition.Preference, "EnemyAppear", new Vector3(1.2f, 1.2f, 1f), "敌人出现特效", 3, false, 20),
        new EffectAttr(1, FXRotationCondition.HaveEnemyDirectionY, "SparkEffectOld", new Vector3(1f, 1f, 1f), "机器人被击中特效", 3, false, 20),
        new EffectAttr(6, FXRotationCondition.RandomRotationZ, "EnemyFall", new Vector3(1f, 1f, 1f), "机器人坠地尘土", 4, false, 20),
        new EffectAttr(9, FXRotationCondition.Preference, "BlueExplosion", new Vector3(1f, 1f, 1f), "蓝色爆炸", 3, false, 20),
        new EffectAttr(14, FXRotationCondition.Preference, "NormalKillSparkEffect", new Vector3(1f, 1f, 1f), "敌人普通死亡蓝色火星", 1, false, 20),
        new EffectAttr(17, FXRotationCondition.Preference, "HaloEffect", new Vector3(3f, 3f, 3f), "BOSS吼叫的圈", 1, true, 20),
        new EffectAttr(22, FXRotationCondition.Preference, "PlayerFall", new Vector3(1f, 1f, 1f), "主角尘土", 3, false, 20),
        new EffectAttr(23, FXRotationCondition.Preference, "DistortionCore-PS4", new Vector3(1f, 1f, 0.7f), "空气扭曲", 2, false, 20),
        new EffectAttr(40, FXRotationCondition.Preference, "DustEffect", new Vector3(1f, 1f, 1f), "后退尘土", 2, false, 20),
        new EffectAttr(48, FXRotationCondition.Preference, "Flash_Distort", new Vector3(2.5f, 2.5f, 1f), "飞燕特效", 5, false, 20),
        new EffectAttr(49, FXRotationCondition.Preference, "DistortionCore-PS4", new Vector3(1f, 1f, 0.7f), "普通扭曲空气特效", 2, false, 20),
        new EffectAttr(61, FXRotationCondition.Preference, "PaoSisterDust", new Vector3(1f, 1f, 1f), "炮姐发射尘土", 1, false, 20),
        new EffectAttr(70, FXRotationCondition.RandomRotationZ, "EnemyHitEffect", new Vector3(1f, 1f, 1f), "主角被攻击黄色特效", 3, false, 20),
        new EffectAttr(71, FXRotationCondition.Preference, "EnemyHurt", new Vector3(1f, 1f, 1f), "机器人被攻击动画", 3, false, 20),
        new EffectAttr(76, FXRotationCondition.Preference, "PlayerArmorSpark01", new Vector3(1f, 1f, 1f), "主角护盾火星", 5, true, 10),
        new EffectAttr(80, FXRotationCondition.Preference, "PlayerArmorSpark02", new Vector3(1f, 1f, 1f), "主角护盾火星2", 3, true, 10),
        new EffectAttr(90, FXRotationCondition.Preference, "BulletExplosion", new Vector3(1f, 1f, 1f), "无属性子弹爆炸", 2, false, 10),
        new EffectAttr(91, FXRotationCondition.Preference, "EnergyBallBlue2", new Vector3(0.4f, 0.4f, 1f), "蓝色能量球特效", 7, false, 30),
        new EffectAttr(92, FXRotationCondition.Preference, "BlockParticle_Enemy", new Vector3(1f, 1f, 1f), "敌人死亡小方块", 7, false, 20),
        new EffectAttr(95, FXRotationCondition.Preference, "LightGunHitGroundSpark", new Vector3(1f, 1f, 1f), "主角轻攻击子弹击中地面", 10, false, 20),
        new EffectAttr(98, FXRotationCondition.Preference, "NewATK1", new Vector3(1f, 1f, 1f), "透明刀光ATK1", 1, true, 5),
        new EffectAttr(99, FXRotationCondition.Preference, "NewATK2", new Vector3(1f, 1f, 1f), "刀光ATK2", 1, true, 5),
        new EffectAttr(100, FXRotationCondition.Preference, "NewATK15", new Vector3(1f, 1f, 1f), "刀光ATK15", 1, true, 5),
        new EffectAttr(101, FXRotationCondition.Preference, "NewATK5", new Vector3(1f, 1f, 1f), "刀光ATK5", 1, true, 5),
        new EffectAttr(102, FXRotationCondition.Preference, "NewATK23", new Vector3(1f, 1f, 1f), "刀光ATK23", 1, true, 5),
        new EffectAttr(103, FXRotationCondition.Preference, "NewAirATK1", new Vector3(1f, 1f, 1f), "透明刀光ATK4", 1, true, 5),
        new EffectAttr(104, FXRotationCondition.Preference, "NewAirATK2", new Vector3(1f, 1f, 1f), "透明刀光ATK5", 1, true, 5),
        new EffectAttr(105, FXRotationCondition.Preference, "NewAirATK6", new Vector3(1f, 1f, 1f), "空中刀光ATK6", 1, true, 5),
        new EffectAttr(106, FXRotationCondition.Preference, "NewUpRising", new Vector3(1f, 1f, 1f), "刀光上挑", 1, true, 5),
        new EffectAttr(107, FXRotationCondition.Preference, "NewATKHv1", new Vector3(1f, 1f, 1f), "突刺刀光1", 1, true, 5),
        new EffectAttr(108, FXRotationCondition.Preference, "NewATKHv2", new Vector3(1f, 1f, 1f), "突刺刀光2", 1, true, 5),
        new EffectAttr(109, FXRotationCondition.Preference, "NewATKHv3", new Vector3(1f, 1f, 1f), "突刺刀光3", 1, true, 5),
        new EffectAttr(110, FXRotationCondition.Preference, "NewAirATKHv1", new Vector3(1f, 1f, 1f), "透明刀光ATK7_5", 1, true, 5),
        new EffectAttr(111, FXRotationCondition.Preference, "NewAirATKHv2", new Vector3(1f, 1f, 1f), "透明刀光ATK7_6", 1, true, 5),
        new EffectAttr(112, FXRotationCondition.Preference, "NewAirATKHv5", new Vector3(1f, 1f, 1f), "透明刀光ATK7_7", 1, true, 5),
        new EffectAttr(114, FXRotationCondition.Preference, "NewATK16", new Vector3(1f, 1f, 1f), "枪刃空中刀光ATK1", 1, true, 5),
        new EffectAttr(116, FXRotationCondition.Preference, "NewATKRollEnd", new Vector3(1f, 1f, 1f), "刀光ATKRollEnd", 1, true, 5),
        new EffectAttr(117, FXRotationCondition.Preference, "NewATKHv1Push", new Vector3(1f, 1f, 0.1f), "上挑刀光", 1, true, 5),
        new EffectAttr(118, FXRotationCondition.Preference, "NewATKRollGround", new Vector3(1f, 1f, 1f), "下劈刀光", 1, true, 5),
        new EffectAttr(121, FXRotationCondition.FollowPlayer, "Charging1", new Vector3(1f, 1f, 1f), "蓄力1", 1, false, 3),
        new EffectAttr(124, FXRotationCondition.Preference, "ChargingScreenEffect", new Vector3(1f, 1f, 1f), "蓄力攻击", 1, false, 3),
        new EffectAttr(125, FXRotationCondition.Preference, "ChipAbsorb", new Vector3(1f, 1f, 1f), "芯片吸收", 3, false, 10),
        new EffectAttr(126, FXRotationCondition.Preference, "EnemyChip", new Vector3(0.2f, 0.17f, 1f), "蓄力攻击", 3, true, 10),
        new EffectAttr(127, FXRotationCondition.Preference, "ChipExplosion", new Vector3(1f, 1f, 1f), "芯片生成飞出", 3, false, 10),
        new EffectAttr(128, FXRotationCondition.Preference, "Warning", new Vector3(1f, 1f, 1f), "警告", 3, false, 10),
        new EffectAttr(131, FXRotationCondition.Preference, "DahalBullet", new Vector3(0.15f, 0.15f, 1f), "DahalBullet", 3, false, 10),
        new EffectAttr(133, FXRotationCondition.Preference, "BeelzebubATK3", new Vector3(1f, 1f, 1f), "暴食ATK3", 3, false, 10),
        new EffectAttr(134, FXRotationCondition.HaveEnemyDirectionY, "BeelzebubATK1_1", new Vector3(1f, 1f, 1f), "暴食ATK1", 3, true, 10),
        new EffectAttr(139, FXRotationCondition.HaveEnemyDirectionY, "BeelzebubATK1_2", new Vector3(1f, 1f, 1f), "暴食技能ATK1_2", 1, true, 6),
        new EffectAttr(142, FXRotationCondition.Preference, "IceExplosion", new Vector3(1f, 1f, 1f), "使用技能召唤暴食砸到地面的爆炸", 1, false, 6),
        new EffectAttr(144, FXRotationCondition.HaveEnemyDirectionY, "SparkEffectLongTime", new Vector3(1f, 1f, 1f), "小怪被处决死的时候顺着裂缝散落的粒子", 1, false, 6),
        new EffectAttr(145, FXRotationCondition.FollowPlayer, "IceExplosion", new Vector3(1f, 1f, 1f), "暴食自己砸到地面的爆炸", 1, false, 6),
        new EffectAttr(148, FXRotationCondition.Preference, "DahalAtk4Bullet", new Vector3(1f, 1f, 1f), "达哈尔火箭", 1, false, 6),
        new EffectAttr(149, FXRotationCondition.Preference, "DahalATK6FallSpark", new Vector3(1f, 1f, 1f), "达哈尔ATK6下落火星", 1, false, 3),
        new EffectAttr(150, FXRotationCondition.Preference, "DahalTansferSpark", new Vector3(1f, 1f, 1f), "达哈尔切换状态火星", 1, false, 3),
        new EffectAttr(151, FXRotationCondition.RandomRotationZ, "EnemyHitEffect02", new Vector3(1f, 1f, 1f), "敌人受伤特效", 1, false, 5),
        new EffectAttr(152, FXRotationCondition.FollowTargeRotation, "CloseEnemyEffect", new Vector3(1f, 1f, 1f), "主角聚怪特效", 1, true, 5),
        new EffectAttr(153, FXRotationCondition.FollowTargeRotation, "CloseEnemyEffectBlast", new Vector3(1f, 1f, 1f), "主角聚怪后冲刺特效", 1, false, 5),
        new EffectAttr(154, FXRotationCondition.Preference, "BeeLaser", new Vector3(1f, 1f, 1f), "激光特效", 1, false, 5),
        new EffectAttr(155, FXRotationCondition.Preference, "BeeLaser2", new Vector3(1f, 1f, 1f), "激光特效", 1, false, 5),
        new EffectAttr(156, FXRotationCondition.Preference, "HeavyHitEffect", new Vector3(1f, 1f, 1f), "重击特效", 1, false, 1),
        new EffectAttr(157, FXRotationCondition.Preference, "EnemyBlockEffect", new Vector3(1f, 1f, 1f), "敌人防御特效", 1, true, 1),
        new EffectAttr(158, FXRotationCondition.Preference, "PlayerBlockEffect", new Vector3(1f, 1f, 1f), "主角防御特效", 1, true, 1),
        new EffectAttr(159, FXRotationCondition.Preference, "RedMoneyHalo", new Vector3(1f, 1f, 1f), "场景中的红色货币光晕", 4, true, 10),
        new EffectAttr(160, FXRotationCondition.Preference, "BreakShieldEnemy", new Vector3(1f, 1f, 1f), "敌人破盾特效", 4, false, 10),
        new EffectAttr(161, FXRotationCondition.Preference, "BreakShield", new Vector3(1f, 1f, 1f), "主角破盾特效", 4, false, 10),
        new EffectAttr(162, FXRotationCondition.Preference, "EnemyExplosion001", new Vector3(1f, 1f, 1f), "BOSS爆炸特效", 1, false, 2),
        new EffectAttr(163, FXRotationCondition.Preference, "DeadEffect", new Vector3(1f, 1f, 1f), "怪物消失", 1, true, 2),
        new EffectAttr(164, FXRotationCondition.Preference, "TwirlEffect", new Vector3(3f, 3f, 1f), "开启扭曲", 1, false, 2),
        new EffectAttr(165, FXRotationCondition.Preference, "ShadeAtk", new Vector3(1f, 1f, 1f), "分身斩特效", 1, false, 2),
        new EffectAttr(166, FXRotationCondition.Preference, "NewATKHitGround2", new Vector3(1f, 1f, 1f), "主角匝地", 1, true, 6),
        new EffectAttr(168, FXRotationCondition.Preference, "NewAirATKRoll", new Vector3(1f, 1f, 1f), "主角空中下劈转", 1, true, 6),
        new EffectAttr(169, FXRotationCondition.Preference, "NewHitGround", new Vector3(1f, 1f, 1f), "主角劈转", 1, true, 6),
        new EffectAttr(170, FXRotationCondition.Preference, "NewATK3", new Vector3(1f, 1f, 1f), "主角劈转", 1, true, 6),
        new EffectAttr(171, FXRotationCondition.Preference, "NewATK14", new Vector3(1f, 1f, 1f), "主角劈转", 1, true, 6),
        new EffectAttr(172, FXRotationCondition.Preference, "NewRoll", new Vector3(1f, 1f, 1f), "主角劈转", 1, true, 1),
        new EffectAttr(173, FXRotationCondition.Preference, "NewATKUpRising", new Vector3(1f, 1f, 1f), "主角ATK上挑", 1, true, 6),
        new EffectAttr(174, FXRotationCondition.Preference, "NewRollEnd", new Vector3(1f, 1f, 1f), "主角ATK上挑", 1, true, 6),
        new EffectAttr(175, FXRotationCondition.Preference, "NewAirATK7", new Vector3(1f, 1f, 1f), "主角空中ATK7", 1, true, 6),
        new EffectAttr(176, FXRotationCondition.Preference, "NewAirATKHv1Push", new Vector3(1f, 1f, 1f), "主角空中ATKHv1Push", 1, true, 6),
        new EffectAttr(177, FXRotationCondition.Preference, "ShadeAtk_ToLeftAir", new Vector3(1f, 1f, 1f), "Dahal处决分身斩", 1, true, 6),
        new EffectAttr(178, FXRotationCondition.Preference, "DahalQTEHurt", new Vector3(1f, 1f, 1f), "DahalQTE受伤", 1, true, 6),
        new EffectAttr(179, FXRotationCondition.Preference, "BeelzebubQTEHurt", new Vector3(1f, 1f, 1f), "暴食QTE受伤分身斩", 1, true, 6),
        new EffectAttr(180, FXRotationCondition.Preference, "ShadeAtk_Down", new Vector3(1f, 1f, 1f), "骑兵QTE死亡分身斩", 1, true, 6),
        new EffectAttr(181, FXRotationCondition.FollowPlayer, "AirCharging1", new Vector3(1f, 1f, 1f), "空中蓄力1", 1, true, 6),
        new EffectAttr(182, FXRotationCondition.Preference, "ShadeAtk_Chase", new Vector3(1f, 1f, 1f), "追击分身斩", 3, true, 6),
        new EffectAttr(183, FXRotationCondition.Preference, "NewDoubelFlash", new Vector3(1f, 1f, 1f), "左右穿梭刀光", 1, true, 6),
        new EffectAttr(184, FXRotationCondition.Preference, "NewRollEndFrameGround", new Vector3(1f, 1f, 1f), "地面下劈刀光", 1, true, 6),
        new EffectAttr(185, FXRotationCondition.Preference, "NewRollEndFrame1", new Vector3(1f, 1f, 1f), "空中下劈2刀光", 1, true, 6),
        new EffectAttr(186, FXRotationCondition.Preference, "NewRollEndFrame", new Vector3(1f, 1f, 1f), "空中下劈2刀光", 1, true, 6),
        new EffectAttr(188, FXRotationCondition.Preference, "SupplyBoxScreenEffect_Energy", new Vector3(1f, 1f, 1f), "Supply Box add Energy", 1, true, 6),
        new EffectAttr(189, FXRotationCondition.Preference, "SupplyBoxScreenEffect_EnergyMatrix", new Vector3(1f, 1f, 1f), "Supply Box add Energy Matrix", 1, true, 6),
        new EffectAttr(191, FXRotationCondition.Preference, "ShadeAtk_Skill", new Vector3(1f, 1f, 1f), "分身斩技能", 1, false, 6),
        new EffectAttr(192, FXRotationCondition.Preference, "CoinGenerator", new Vector3(1f, 1f, 1f), "宝箱喷金币", 1, false, 6),
        new EffectAttr(193, FXRotationCondition.Preference, "NewATKRollGround", new Vector3(1f, 1f, 1f), "大风车特效", 1, true, 6),
        new EffectAttr(194, FXRotationCondition.Preference, "NewATKHv4", new Vector3(1f, 1f, 1f), "主角突刺特效", 1, true, 6),
        new EffectAttr(195, FXRotationCondition.Preference, "HammerATK1DustEffect", new Vector3(1f, 1f, 1f), "大锤尘土", 1, false, 5),
        new EffectAttr(197, FXRotationCondition.Preference, "Missile", new Vector3(0.25f, 0.25f, 1f), "锤子导弹", 1, false, 10),
        new EffectAttr(198, FXRotationCondition.Preference, "MissileExplosion", new Vector3(1f, 1f, 1f), "锤子导弹爆炸效果", 1, false, 10),
        new EffectAttr(200, FXRotationCondition.Preference, "JackAim", new Vector3(0.7f, 0.7f, 1f), "Jack瞄准特效", 6, false, 20),
        new EffectAttr(201, FXRotationCondition.Preference, "JackAimEffect1", new Vector3(0.7f, 0.7f, 1f), "Jack真瞄准特效", 3, false, 6),
        new EffectAttr(202, FXRotationCondition.Preference, "JackAimExplosion", new Vector3(1f, 1f, 1f), "Jack瞄准爆炸特效", 3, false, 10),
        new EffectAttr(203, FXRotationCondition.Preference, "GiantRobotLaser", new Vector3(1f, 1f, 1f), "巨型机器人激光", 1, false, 5),
        new EffectAttr(204, FXRotationCondition.Preference, "GiantRobotLaser 1", new Vector3(1f, 1f, 1f), "巨型机器人激光", 1, false, 5),
        new EffectAttr(205, FXRotationCondition.Preference, "JudgesLaser", new Vector3(1f, 1f, 1f), "犹大激光", 1, false, 5),
        new EffectAttr(206, FXRotationCondition.Preference, "EnemyExplosion002", new Vector3(1f, 1f, 1f), "音乐播放器爆炸特效", 1, false, 1),
        new EffectAttr(207, FXRotationCondition.Preference, "FireShotGun01", new Vector3(1f, 1f, 1f), "霰弹利刃的霰弹", 1, false, 20),
        new EffectAttr(208, FXRotationCondition.Preference, "Dahal", new Vector3(1f, 1f, 1f), "达哈尔感谢", 1, false, 20),
        new EffectAttr(209, FXRotationCondition.Preference, "StickerAtk2Effect", new Vector3(1f, 1f, 1f), "珠子攻击2", 1, false, 20),
        new EffectAttr(210, FXRotationCondition.Preference, "EatingBossAtk4Effect", new Vector3(1f, 1f, 1f), "卡洛斯Atk4特效", 1, false, 20),
        new EffectAttr(211, FXRotationCondition.Preference, "JudgesLightingEffect", new Vector3(1f, 1f, 1f), "犹大攻击2", 1, false, 20),
        new EffectAttr(212, FXRotationCondition.Preference, "NewPlayerAirJumpEffect", new Vector3(1f, 1f, 1f), "跳跃特效", 10, false, 20),
        new EffectAttr(213, FXRotationCondition.Preference, "ExecuteBlack", new Vector3(1f, 1f, 1f), "敌人处决黑屏1", 2, false, 5),
        new EffectAttr(214, FXRotationCondition.Preference, "EnemyExecute", new Vector3(1f, 1f, 1f), "敌人处决屏幕特效1", 2, false, 5),
        new EffectAttr(215, FXRotationCondition.Preference, "CloseScreenEffect", new Vector3(1f, 1f, 1f), "e26关屏幕", 1, false, 20),
        new EffectAttr(216, FXRotationCondition.Preference, "PlayerHurt", new Vector3(1f, 1f, 1f), "受伤蜂窝特效", 1, false, 2),
        new EffectAttr(217, FXRotationCondition.Preference, "Onion", new Vector3(1f, 1f, 1f), "主角洋葱皮", 10, false, 20),
        new EffectAttr(218, FXRotationCondition.Preference, "Onion_DaoBrother", new Vector3(1f, 1f, 1f), "怪物洋葱皮", 10, false, 20),
        new EffectAttr(219, FXRotationCondition.Preference, "DaoBrotherAndPaoSisterPartA", new Vector3(1f, 1f, 1f), "刀哥碎片", 2, false, 5),
        new EffectAttr(220, FXRotationCondition.Preference, "DaoBrotherAndPaoSisterPartB", new Vector3(1f, 1f, 1f), "刀哥碎片", 2, false, 5),
        new EffectAttr(221, FXRotationCondition.Preference, "DaoPaoPartA", new Vector3(1f, 1f, 1f), "刀炮碎片", 2, false, 5),
        new EffectAttr(222, FXRotationCondition.Preference, "DaoPaoPartB", new Vector3(1f, 1f, 1f), "刀炮碎片", 2, false, 5),
        new EffectAttr(223, FXRotationCondition.Preference, "BombKillerIIPartA", new Vector3(1f, 1f, 1f), "半身自爆碎片", 2, false, 4),
        new EffectAttr(224, FXRotationCondition.Preference, "BombKillerIIPartB", new Vector3(1f, 1f, 1f), "半身自爆碎片", 2, false, 4),
        new EffectAttr(225, FXRotationCondition.Preference, "FooterPartA", new Vector3(1f, 1f, 1f), "大脚碎片", 1, false, 5),
        new EffectAttr(226, FXRotationCondition.Preference, "FooterPartB", new Vector3(1f, 1f, 1f), "大脚碎片", 1, false, 5),
        new EffectAttr(227, FXRotationCondition.Preference, "JumperPartA", new Vector3(1f, 1f, 1f), "跳拳碎片", 2, false, 5),
        new EffectAttr(228, FXRotationCondition.Preference, "JumperPartB", new Vector3(1f, 1f, 1f), "跳拳碎片", 2, false, 5),
        new EffectAttr(229, FXRotationCondition.Preference, "Onion1", new Vector3(1f, 1f, 1f), "主角幻影动画版", 10, false, 20),
    };


    /// <summary>
    /// 玩家攻击数据
    /// {"AirAtk1":{"atkName":"AirAtk1","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":0.800000011920929,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "AirAtk2":{"atkName":"AirAtk2","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":0.800000011920929,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "AirAtk6":{"atkName":"AirAtk6","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":0.899999976158142,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "AirAtk7":{"atkName":"AirAtk7","shakeOffset":0.200000002980232,"shakeSpeed":0.200000002980232,"shakeClip":8,"frameShakeClip":0,"frozenClip":8,"shakeType":0,"damagePercent":1.0,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "DoubleFlashAir":{"atkName":"DoubleFlashAir","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":0,"frozenClip":0,"shakeType":0,"damagePercent":0.800000011920929,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "RollEndFrame":{"atkName":"RollEndFrame","shakeOffset":0.200000002980232,"shakeSpeed":0.200000002980232,"shakeClip":20,"frameShakeClip":0,"frozenClip":10,"shakeType":0,"damagePercent":0.899999976158142,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":5,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "AirAtkHv1":{"atkName":"AirAtkHv1","shakeOffset":0.100000001490116,"shakeSpeed":0.100000001490116,"shakeClip":5,"frameShakeClip":4,"frozenClip":0,"shakeType":0,"damagePercent":1.29999995231628,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":2,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "AirAtkHv2":{"atkName":"AirAtkHv2","shakeOffset":0.200000002980232,"shakeSpeed":0.200000002980232,"shakeClip":16,"frameShakeClip":4,"frozenClip":15,"shakeType":0,"damagePercent":1.39999997615814,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":2,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "AirAtkHv5":{"atkName":"AirAtkHv5","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":0,"frozenClip":0,"shakeType":0,"damagePercent":0.5,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":6,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "AirAtkHv1Push":{"atkName":"AirAtkHv1Push","shakeOffset":0.100000001490116,"shakeSpeed":0.100000001490116,"shakeClip":10,"frameShakeClip":4,"frozenClip":4,"shakeType":0,"damagePercent":1.5,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":2,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "Atk1":{"atkName":"Atk1","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":0.800000011920929,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "Atk2":{"atkName":"Atk2","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":0.899999976158142,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "Atk3":{"atkName":"Atk3","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":0.400000005960464,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "Atk4":{"atkName":"Atk4","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":0,"frozenClip":0,"shakeType":0,"damagePercent":0.5,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "Atk5":{"atkName":"Atk5","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":1.0,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "Atk6":{"atkName":"Atk6","shakeOffset":0.400000005960464,"shakeSpeed":0.400000005960464,"shakeClip":20,"frameShakeClip":4,"frozenClip":0,"shakeType":0,"damagePercent":1.20000004768372,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "Atk14":{"atkName":"Atk14","shakeOffset":0.200000002980232,"shakeSpeed":0.200000002980232,"shakeClip":10,"frameShakeClip":4,"frozenClip":0,"shakeType":0,"damagePercent":1.79999995231628,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":5,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "Atk15":{"atkName":"Atk15","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":1.10000002384186,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "Atk16":{"atkName":"Atk16","shakeOffset":0.400000005960464,"shakeSpeed":0.400000005960464,"shakeClip":30,"frameShakeClip":0,"frozenClip":30,"shakeType":0,"damagePercent":1.79999995231628,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":3,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "Atk23":{"atkName":"Atk23","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":0,"frozenClip":0,"shakeType":0,"damagePercent":1.0,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "DoubleFlash":{"atkName":"DoubleFlash","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":0,"frozenClip":0,"shakeType":0,"damagePercent":0.800000011920929,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "AtkFlashRollEnd":{"atkName":"AtkFlashRollEnd","shakeOffset":0.400000005960464,"shakeSpeed":0.400000005960464,"shakeClip":10,"frameShakeClip":0,"frozenClip":10,"shakeType":0,"damagePercent":1.5,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "AtkHv1":{"atkName":"AtkHv1","shakeOffset":0.100000001490116,"shakeSpeed":0.100000001490116,"shakeClip":5,"frameShakeClip":16,"frozenClip":0,"shakeType":0,"damagePercent":1.60000002384186,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":2,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "AtkHv2":{"atkName":"AtkHv2","shakeOffset":0.200000002980232,"shakeSpeed":0.200000002980232,"shakeClip":20,"frameShakeClip":20,"frozenClip":15,"shakeType":0,"damagePercent":1.79999995231628,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":2,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "AtkHv3":{"atkName":"AtkHv3","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":0,"frozenClip":0,"shakeType":0,"damagePercent":0.699999988079071,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":6,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "AtkHv1Push":{"atkName":"AtkHv1Push","shakeOffset":0.100000001490116,"shakeSpeed":0.100000001490116,"shakeClip":10,"frameShakeClip":16,"frozenClip":0,"shakeType":0,"damagePercent":1.5,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":2,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "AirAtkRoll":{"atkName":"AirAtkRoll","shakeOffset":0.200000002980232,"shakeSpeed":0.200000002980232,"shakeClip":10,"frameShakeClip":0,"frozenClip":0,"shakeType":0,"damagePercent":0.349999994039536,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":2,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "AtkRollEnd":{"atkName":"AtkRollEnd","shakeOffset":0.400000005960464,"shakeSpeed":0.400000005960464,"shakeClip":18,"frameShakeClip":0,"frozenClip":18,"shakeType":0,"damagePercent":1.20000004768372,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "HitGround":{"atkName":"HitGround","shakeOffset":0.200000002980232,"shakeSpeed":0.200000002980232,"shakeClip":10,"frameShakeClip":8,"frozenClip":18,"shakeType":0,"damagePercent":1.0,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "RollReady":{"atkName":"RollReady","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":0.349999994039536,"interval":0.100000001490116,"hitTimes":0,"hitType":2,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "RollGround":{"atkName":"RollGround","shakeOffset":0.200000002980232,"shakeSpeed":0.200000002980232,"shakeClip":10,"frameShakeClip":0,"frozenClip":0,"shakeType":0,"damagePercent":0.449999988079071,"interval":0.100000001490116,"hitTimes":0,"hitType":2,"joystickShakeNum":2,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "RollEnd":{"atkName":"RollEnd","shakeOffset":0.400000005960464,"shakeSpeed":0.400000005960464,"shakeClip":18,"frameShakeClip":0,"frozenClip":18,"shakeType":0,"damagePercent":1.5,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":2,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "UpRising":{"atkName":"UpRising","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":1.0,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "AtkUpRising":{"atkName":"AtkUpRising","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":1.20000004768372,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "NewExecute2_1":{"atkName":"NewExecute2_1","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":0,"frozenClip":0,"shakeType":0,"damagePercent":0.0,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "Charge1EndLevel1":{"atkName":"Charge1EndLevel1","shakeOffset":0.400000005960464,"shakeSpeed":0.400000005960464,"shakeClip":30,"frameShakeClip":30,"frozenClip":20,"shakeType":0,"damagePercent":4.0,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":7,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "Charge1EndLevel2":{"atkName":"Charge1EndLevel2","shakeOffset":0.400000005960464,"shakeSpeed":0.400000005960464,"shakeClip":30,"frameShakeClip":30,"frozenClip":20,"shakeType":0,"damagePercent":5.0,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":7,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "Charge1EndLevel3":{"atkName":"Charge1EndLevel3","shakeOffset":0.400000005960464,"shakeSpeed":0.400000005960464,"shakeClip":30,"frameShakeClip":30,"frozenClip":20,"shakeType":0,"damagePercent":6.0,"interval":0.0,"hitTimes":0,"hitType":0,"joystickShakeNum":7,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":false},
    /// "BladeStormReady":{"atkName":"BladeStormReady","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":0.180000007152557,"interval":0.100000001490116,"hitTimes":0,"hitType":2,"joystickShakeNum":-1,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true},
    /// "ShadeAtkSkill":{"atkName":"ShadeAtkSkill","shakeOffset":0.0,"shakeSpeed":0.0,"shakeClip":0,"frameShakeClip":8,"frozenClip":0,"shakeType":0,"damagePercent":0.600000023841858,"interval":0.100000001490116,"hitTimes":0,"hitType":0,"joystickShakeNum":2,"assistanceType":0,"assistanceCheckType":0,"autoTurn":false,"keepDis":true}}
    /// </summary>
    private static readonly Dictionary<string, Dictionary<string, string>> PlayerAtkDataTemp = new()
    {
        {
            PlayerStaEnum.Atk1.ToString(), new Dictionary<string, string>
            {
                { PlayerAtkDataType.hitTimes.ToString(), "0" }, //命中次数
                { PlayerAtkDataType.interval.ToString(), "0" }, //间隔
                { PlayerAtkDataType.hitType.ToString(), "0" }, //命中类型 1 Once, 2 Limited, 3 UnLimited
                { PlayerAtkDataType.damagePercent.ToString(), "0.4" }, //伤害百分比
                { PlayerAtkDataType.atkName.ToString(), PlayerStaEnum.Atk1.ToString() }, //攻击名称
                { PlayerAtkDataType.shakeClip.ToString(), "0" }, //振动帧
                { PlayerAtkDataType.shakeSpeed.ToString(), "0" }, //振动速度
                { PlayerAtkDataType.shakeOffset.ToString(), "0" }, //振动偏移
                { PlayerAtkDataType.shakeType.ToString(), "0" }, //振动类型
                { PlayerAtkDataType.frozenClip.ToString(), "0" }, //冻结帧
                { PlayerAtkDataType.frameShakeClip.ToString(), "8" }, //帧振动
                { PlayerAtkDataType.joystickShakeNum.ToString(), "-1" }, //摇杆摇数
            }
        },
        {
            PlayerStaEnum.Atk2.ToString(), new Dictionary<string, string>
            {
                { PlayerAtkDataType.hitTimes.ToString(), "0" }, //命中次数
                { PlayerAtkDataType.interval.ToString(), "0" }, //间隔
                { PlayerAtkDataType.hitType.ToString(), "0" }, //间隔
                { PlayerAtkDataType.damagePercent.ToString(), "0.4" }, //伤害百分比
                { PlayerAtkDataType.atkName.ToString(), PlayerStaEnum.Atk2.ToString() }, //攻击名称
                { PlayerAtkDataType.shakeClip.ToString(), "0" }, //振动帧
                { PlayerAtkDataType.shakeSpeed.ToString(), "0" }, //振动速度
                { PlayerAtkDataType.shakeOffset.ToString(), "0" }, //振动偏移
                { PlayerAtkDataType.shakeType.ToString(), "0" }, //振动类型
                { PlayerAtkDataType.frozenClip.ToString(), "0" }, //冻结帧
                { PlayerAtkDataType.frameShakeClip.ToString(), "8" }, //帧振动次数
                { PlayerAtkDataType.joystickShakeNum.ToString(), "-1" }, //摇杆摇数
            }
        },
        {
            PlayerStaEnum.Atk3.ToString(), new Dictionary<string, string>
            {
                { PlayerAtkDataType.hitTimes.ToString(), "0" }, //命中次数
                { PlayerAtkDataType.interval.ToString(), "0" }, //间隔
                { PlayerAtkDataType.hitType.ToString(), "0" }, //间隔
                { PlayerAtkDataType.damagePercent.ToString(), "0.4" }, //伤害百分比
                { PlayerAtkDataType.atkName.ToString(), PlayerStaEnum.Atk3.ToString() }, //攻击名称
                { PlayerAtkDataType.shakeClip.ToString(), "0" }, //振动帧
                { PlayerAtkDataType.shakeSpeed.ToString(), "0" }, //振动速度
                { PlayerAtkDataType.shakeOffset.ToString(), "0" }, //振动偏移
                { PlayerAtkDataType.shakeType.ToString(), "0" }, //振动类型
                { PlayerAtkDataType.frozenClip.ToString(), "0" }, //冻结帧
                { PlayerAtkDataType.frameShakeClip.ToString(), "8" }, //帧振动次数
                { PlayerAtkDataType.joystickShakeNum.ToString(), "-1" }, //摇杆摇数
            }
        },
        {
            PlayerStaEnum.UpRising.ToString(), new Dictionary<string, string>
            {
                { PlayerAtkDataType.hitTimes.ToString(), "0" }, //命中次数
                { PlayerAtkDataType.interval.ToString(), "0" }, //间隔
                { PlayerAtkDataType.hitType.ToString(), "0" }, //间隔
                { PlayerAtkDataType.damagePercent.ToString(), "0.4" }, //伤害百分比
                { PlayerAtkDataType.atkName.ToString(), PlayerStaEnum.UpRising.ToString() }, //攻击名称
                { PlayerAtkDataType.shakeClip.ToString(), "0" }, //振动帧
                { PlayerAtkDataType.shakeSpeed.ToString(), "0" }, //振动速度
                { PlayerAtkDataType.shakeOffset.ToString(), "0" }, //振动偏移 摇晃偏移
                { PlayerAtkDataType.shakeType.ToString(), "0" }, //振动类型
                { PlayerAtkDataType.frozenClip.ToString(), "0" }, //冻结帧
                { PlayerAtkDataType.frameShakeClip.ToString(), "8" }, //帧振动次数
                { PlayerAtkDataType.joystickShakeNum.ToString(), "-1" }, //摇杆摇数
            }
        },
        {
            PlayerStaEnum.AirAtk1.ToString(), new Dictionary<string, string>
            {
                { PlayerAtkDataType.hitTimes.ToString(), "0" }, //命中次数
                { PlayerAtkDataType.interval.ToString(), "0" }, //间隔
                { PlayerAtkDataType.hitType.ToString(), "0" }, //间隔
                { PlayerAtkDataType.damagePercent.ToString(), "0.4" }, //伤害百分比
                { PlayerAtkDataType.atkName.ToString(), PlayerStaEnum.AirAtk1.ToString() }, //攻击名称
                { PlayerAtkDataType.shakeClip.ToString(), "0" }, //振动帧
                { PlayerAtkDataType.shakeSpeed.ToString(), "0" }, //振动速度
                { PlayerAtkDataType.shakeOffset.ToString(), "0" }, //振动偏移
                { PlayerAtkDataType.shakeType.ToString(), "0" }, //振动类型
                { PlayerAtkDataType.frozenClip.ToString(), "0" }, //冻结帧
                { PlayerAtkDataType.frameShakeClip.ToString(), "8" }, //帧振动次数
                { PlayerAtkDataType.joystickShakeNum.ToString(), "-1" }, //摇杆摇数
            }
        },
        {
            PlayerStaEnum.AirAtk2.ToString(), new Dictionary<string, string>
            {
                { PlayerAtkDataType.hitTimes.ToString(), "0" }, //命中次数
                { PlayerAtkDataType.interval.ToString(), "0" }, //间隔
                { PlayerAtkDataType.hitType.ToString(), "0" }, //间隔
                { PlayerAtkDataType.damagePercent.ToString(), "0.4" }, //伤害百分比
                { PlayerAtkDataType.atkName.ToString(), PlayerStaEnum.AirAtk2.ToString() }, //攻击名称
                { PlayerAtkDataType.shakeClip.ToString(), "0" }, //振动帧
                { PlayerAtkDataType.shakeSpeed.ToString(), "0" }, //振动速度
                { PlayerAtkDataType.shakeOffset.ToString(), "0" }, //振动偏移
                { PlayerAtkDataType.shakeType.ToString(), "0" }, //振动类型
                { PlayerAtkDataType.frozenClip.ToString(), "0" }, //冻结帧
                { PlayerAtkDataType.frameShakeClip.ToString(), "8" }, //帧振动次数
                { PlayerAtkDataType.joystickShakeNum.ToString(), "-1" }, //摇杆摇数
            }
        },
        {
            PlayerStaEnum.AirAtk3.ToString(), new Dictionary<string, string>
            {
                { PlayerAtkDataType.hitTimes.ToString(), "0" }, //命中次数
                { PlayerAtkDataType.interval.ToString(), "0" }, //间隔
                { PlayerAtkDataType.hitType.ToString(), "0" }, //间隔
                { PlayerAtkDataType.damagePercent.ToString(), "0.4" }, //伤害百分比
                { PlayerAtkDataType.atkName.ToString(), PlayerStaEnum.AirAtk3.ToString() }, //攻击名称
                { PlayerAtkDataType.shakeClip.ToString(), "0" }, //振动帧
                { PlayerAtkDataType.shakeSpeed.ToString(), "0" }, //振动速度
                { PlayerAtkDataType.shakeOffset.ToString(), "0" }, //振动偏移
                { PlayerAtkDataType.shakeType.ToString(), "0" }, //振动类型
                { PlayerAtkDataType.frozenClip.ToString(), "0" }, //冻结帧
                { PlayerAtkDataType.frameShakeClip.ToString(), "8" }, //帧振动次数
                { PlayerAtkDataType.joystickShakeNum.ToString(), "-1" }, //摇杆摇数
            }
        },
        {
            PlayerStaEnum.HitGroundStart.ToString(), new Dictionary<string, string>
            {
                { PlayerAtkDataType.hitTimes.ToString(), "0" }, //命中次数
                { PlayerAtkDataType.interval.ToString(), "0" }, //间隔
                { PlayerAtkDataType.hitType.ToString(), "0" }, //间隔
                { PlayerAtkDataType.damagePercent.ToString(), "0.4" }, //伤害百分比
                { PlayerAtkDataType.atkName.ToString(), PlayerStaEnum.HitGroundStart.ToString() }, //攻击名称
                { PlayerAtkDataType.shakeClip.ToString(), "0" }, //振动帧
                { PlayerAtkDataType.shakeSpeed.ToString(), "0" }, //振动速度
                { PlayerAtkDataType.shakeOffset.ToString(), "0.2" }, //振动偏移
                { PlayerAtkDataType.shakeType.ToString(), "0" }, //振动类型
                { PlayerAtkDataType.frozenClip.ToString(), "0" }, //冻结帧
                { PlayerAtkDataType.frameShakeClip.ToString(), "8" }, //帧振动次数
                { PlayerAtkDataType.joystickShakeNum.ToString(), "-1" }, //摇杆摇数
            }
        },
        {
            PlayerStaEnum.DoubleFlash.ToString(), new Dictionary<string, string>
            {
                { PlayerAtkDataType.hitTimes.ToString(), "0" }, //命中次数
                { PlayerAtkDataType.interval.ToString(), "0" }, //间隔
                { PlayerAtkDataType.hitType.ToString(), "0" }, //间隔
                { PlayerAtkDataType.damagePercent.ToString(), "0.4" }, //伤害百分比
                { PlayerAtkDataType.atkName.ToString(), PlayerStaEnum.DoubleFlash.ToString() }, //攻击名称
                { PlayerAtkDataType.shakeClip.ToString(), "0" }, //振动帧
                { PlayerAtkDataType.shakeSpeed.ToString(), "0" }, //振动速度
                { PlayerAtkDataType.shakeOffset.ToString(), "0.2" }, //振动偏移
                { PlayerAtkDataType.shakeType.ToString(), "0" }, //振动类型
                { PlayerAtkDataType.frozenClip.ToString(), "0" }, //冻结帧
                { PlayerAtkDataType.frameShakeClip.ToString(), "8" }, //帧振动次数
                { PlayerAtkDataType.joystickShakeNum.ToString(), "-1" }, //摇杆摇数
            }
        },
        {
            PlayerStaEnum.AtkFlashRollEnd.ToString(), new Dictionary<string, string>
            {
                { PlayerAtkDataType.hitTimes.ToString(), "0" }, //命中次数
                { PlayerAtkDataType.interval.ToString(), "0" }, //间隔
                { PlayerAtkDataType.hitType.ToString(), "0" }, //间隔
                { PlayerAtkDataType.damagePercent.ToString(), "0.4" }, //伤害百分比
                { PlayerAtkDataType.atkName.ToString(), PlayerStaEnum.AtkFlashRollEnd.ToString() }, //攻击名称
                { PlayerAtkDataType.shakeClip.ToString(), "10" }, //振动帧
                { PlayerAtkDataType.shakeSpeed.ToString(), "0.4" }, //振动速度
                { PlayerAtkDataType.shakeOffset.ToString(), "0.4" }, //振动偏移
                { PlayerAtkDataType.shakeType.ToString(), "0" }, //振动类型
                { PlayerAtkDataType.frozenClip.ToString(), "10" }, //冻结帧
                { PlayerAtkDataType.frameShakeClip.ToString(), "8" }, //帧振动次数
                { PlayerAtkDataType.joystickShakeNum.ToString(), "-1" }, //摇杆摇数
            }
        },
    };


    /// <summary>
    /// 关卡敌人数据
    /// </summary>
    /// <returns></returns>
    public static readonly Dictionary<string, LevelEnemyData> LevelEnemyDataTemp = new Dictionary<string, LevelEnemyData>
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
    public static readonly Dictionary<string, Dictionary<string, Dictionary<string, string>>> EnemyHurtDataTemp = new()
    {
        {
            EnemyType.女猎手.ToString(), new Dictionary<string, Dictionary<string, string>>
            {
                {
                    PlayerStaEnum.Atk1.ToString(), new Dictionary<string, string>()
                    {
                        { EnemyHurtDataType.xSpeed.ToString(), "1.0" },
                        { EnemyHurtDataType.ySpeed.ToString(), "0.0" },
                        { EnemyHurtDataType.airXSpeed.ToString(), "5.0" },
                        { EnemyHurtDataType.airYSpeed.ToString(), "3.0" },
                        { EnemyHurtDataType.normalAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                        { EnemyHurtDataType.airAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                    }
                },
                {
                    PlayerStaEnum.Atk2.ToString(), new Dictionary<string, string>()
                    {
                        { EnemyHurtDataType.xSpeed.ToString(), "2.0" },
                        { EnemyHurtDataType.ySpeed.ToString(), "5.0" },
                        { EnemyHurtDataType.airXSpeed.ToString(), "5.0" },
                        { EnemyHurtDataType.airYSpeed.ToString(), "3.0" },
                        { EnemyHurtDataType.normalAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                        { EnemyHurtDataType.airAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                    }
                },
                {
                    PlayerStaEnum.Atk3.ToString(), new Dictionary<string, string>()
                    {
                        { EnemyHurtDataType.xSpeed.ToString(), "2.0" },
                        { EnemyHurtDataType.ySpeed.ToString(), "5.0" },
                        { EnemyHurtDataType.airXSpeed.ToString(), "5.0" },
                        { EnemyHurtDataType.airYSpeed.ToString(), "3.0" },
                        { EnemyHurtDataType.normalAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                        { EnemyHurtDataType.airAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                    }
                },
            }
        },
        {
            EnemyType.稻草人.ToString(), new Dictionary<string, Dictionary<string, string>>
            {
                {
                    PlayerStaEnum.Atk1.ToString(), new Dictionary<string, string>()
                    {
                        { EnemyHurtDataType.xSpeed.ToString(), "3.0" },
                        { EnemyHurtDataType.ySpeed.ToString(), "0.0" },
                        { EnemyHurtDataType.airXSpeed.ToString(), "5.0" },
                        { EnemyHurtDataType.airYSpeed.ToString(), "3.0" },
                        { EnemyHurtDataType.normalAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                        { EnemyHurtDataType.airAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                    }
                },
                {
                    PlayerStaEnum.Atk2.ToString(), new Dictionary<string, string>()
                    {
                        { EnemyHurtDataType.xSpeed.ToString(), "1.0" },
                        { EnemyHurtDataType.ySpeed.ToString(), "5.0" },
                        { EnemyHurtDataType.airXSpeed.ToString(), "5.0" },
                        { EnemyHurtDataType.airYSpeed.ToString(), "3.0" },
                        { EnemyHurtDataType.normalAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                        { EnemyHurtDataType.airAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                    }
                },
                {
                    PlayerStaEnum.Atk3.ToString(), new Dictionary<string, string>()
                    {
                        { EnemyHurtDataType.xSpeed.ToString(), "2.0" },
                        { EnemyHurtDataType.ySpeed.ToString(), "5.0" },
                        { EnemyHurtDataType.airXSpeed.ToString(), "5.0" },
                        { EnemyHurtDataType.airYSpeed.ToString(), "3.0" },
                        { EnemyHurtDataType.normalAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                        { EnemyHurtDataType.airAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                    }
                },
                {
                    PlayerStaEnum.UpRising.ToString(), new Dictionary<string, string>()
                    {
                        { EnemyHurtDataType.xSpeed.ToString(), "2.0" },
                        { EnemyHurtDataType.ySpeed.ToString(), "5.0" },
                        { EnemyHurtDataType.airXSpeed.ToString(), "5.0" },
                        { EnemyHurtDataType.airYSpeed.ToString(), "3.0" },
                        { EnemyHurtDataType.normalAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                        { EnemyHurtDataType.airAtkType.ToString(), EnemyStaEnum.Hurt.ToString() },
                    }
                },
            }
        },
    };


    /// <summary>
    /// 敌人攻击玩家,玩家的受伤数据
    /// {"DaoAtk1":{"xSpeed":3.0,"ySpeed":0.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtk1","airAnimName":"UnderAtkHitToFly","damagePercent":1.0,"shieldDamage":3},
    /// "DaoAtk2":{"xSpeed":5.0,"ySpeed":7.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtkHitToFly","airAnimName":"UnderAtkHitToFly","damagePercent":0.800000011920929,"shieldDamage":3},
    /// "DaoAtk3":{"xSpeed":3.0,"ySpeed":0.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtk1","airAnimName":"UnderAtkHitToFly","damagePercent":1.0,"shieldDamage":3},
    /// "DaoAtk4":{"xSpeed":5.0,"ySpeed":7.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtkHitToFly","airAnimName":"UnderAtkHitToFly","damagePercent":1.5,"shieldDamage":2},
    /// "DaoAtk5":{"xSpeed":5.0,"ySpeed":7.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtkHitToFly","airAnimName":"UnderAtkHitToFly","damagePercent":1.0,"shieldDamage":5},
    /// "DaoAtk6_1":{"xSpeed":3.0,"ySpeed":0.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtk1","airAnimName":"UnderAtkHitToFly","damagePercent":1.0,"shieldDamage":3},
    /// "DaoAtk6_2":{"xSpeed":5.0,"ySpeed":7.0,"airXSpeed":5.0,"airYSpeed":3.0,"animName":"UnderAtkHitToFly","airAnimName":"UnderAtkHitToFly","damagePercent":1.5,"shieldDamage":3},
    /// "Bullet":{"xSpeed":5.0,"ySpeed":0.0,"airXSpeed":5.0,"airYSpeed":7.0,"animName":"UnderAtk1","airAnimName":"UnderAtkHitToFly","damagePercent":1.0,"shieldDamage":2}}
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
}