using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

/// <summary>
/// 敌人伤害攻击事件参数
/// </summary>
public class EnemyHurtAtkEventArgs : EventArgs
{
    public EnemyHurtAtkEventArgs(GameObject _hurted, GameObject _sender, int _attackId, Vector3 _hurtPos, HurtCheck.BodyType _body,
        PlayerNormalAtkData _attackData, bool _forceHurt = false)
    {
        hurted = _hurted;
        sender = _sender;
        attackId = _attackId;
        hurtPos = _hurtPos;
        body = _body;
        attackData = _attackData;
        forceHurt = _forceHurt;
        hurtType = HurtTypeEnum.Normal;
    }

    public EnemyHurtAtkEventArgs(GameObject _hurted, HurtTypeEnum type)
    {
        hurted = _hurted;
        hurtType = type;
    }

    public EnemyHurtAtkEventArgs(GameObject _hurted, HurtTypeEnum type, string playerState)
    {
        hurted = _hurted;
        hurtType = type;
        attackData = new PlayerNormalAtkData(playerState);
    }

    public PlayerNormalAtkData attackData;

    public int attackId;

    public HurtCheck.BodyType body;

    public bool forceHurt;

    /// <summary>
    /// 受伤物体
    /// </summary>
    public GameObject hurted;

    /// <summary>
    /// 受伤位置
    /// </summary>
    public Vector3 hurtPos;

    public HurtTypeEnum hurtType;

    public GameObject sender;

    /// <summary>
    /// 伤害类型
    /// </summary>
    public enum HurtTypeEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        Normal,

        /// <summary>
        /// 执行跟踪
        /// </summary>
        ExecuteFollow,

        /// <summary>
        /// Execute
        /// </summary>
        Execute,
        QTEHurt,

        /// <summary>
        /// 闪
        /// </summary>
        Flash
    }

    /// <summary>
    /// 玩家正常攻击数据
    /// </summary>
    public class PlayerNormalAtkData : EventArgs
    {
        /// <summary>
        /// 伤害
        /// </summary>
        /// <param name="atkData"></param>
        /// <param name="_firstHurt">是否第一次伤害</param>
        public PlayerNormalAtkData(Dictionary<PlayerAtkDataType, string> atkData, bool _firstHurt)
        {
            "玩家默认攻击数据".Log();
            damagePercent = atkData[PlayerAtkDataType.damagePercent].ToFloat();
            atkName = atkData[PlayerAtkDataType.atkName];
            camShakeFrame = atkData[PlayerAtkDataType.shakeClip].ToInt();
            shakeStrength = atkData[PlayerAtkDataType.shakeOffset].ToFloat();
            shakeType = atkData[PlayerAtkDataType.shakeType].ToInt();
            frozenFrame = atkData[PlayerAtkDataType.frozenClip].ToInt();
            shakeFrame = atkData[PlayerAtkDataType.frameShakeClip].ToInt();
            joystickShakeNum = atkData[PlayerAtkDataType.joystickShakeNum].ToInt();
            firstHurt = _firstHurt;
        }

        public PlayerNormalAtkData(string _atkName)
        {
            atkName = _atkName;
        }

        /// <summary>
        /// 伤害百分比
        /// </summary>
        public float damagePercent = 1f;

        /// <summary>
        /// 攻击名称
        /// </summary>
        public string atkName;

        /// <summary>
        /// 是否第一次伤害
        /// </summary>
        public bool firstHurt;

        /// <summary>
        /// 振动帧
        /// </summary>
        public int camShakeFrame;

        /// <summary>
        /// 振动偏移
        /// </summary>
        public float shakeStrength = 1f;

        /// <summary>
        /// 振动类型
        /// </summary>
        public int shakeType;

        /// <summary>
        /// 冻结帧
        /// </summary>
        public int frozenFrame;

        /// <summary>
        /// 振动帧
        /// </summary>
        public int shakeFrame;

        /// <summary>
        /// 摇杆摇数
        /// </summary>
        public int joystickShakeNum;
    }
}