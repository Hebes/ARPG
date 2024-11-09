using System;
using UnityEngine;

/// <summary>
/// 敌人AI属性
/// </summary>
public class EnemyAIAttribute : BaseBehaviour
{
    /// <summary>
    /// 面对玩家的朝向
    /// </summary>
    public int facePlayerDir => InputSetting.JudgeDir(transform.position, R.Player.Transform.position);

    /// <summary>
    /// 攻击的期望
    /// </summary>
    public ExpectationParam[] attackExpectations;

    /// <summary>
    /// 当前的期望参数
    /// </summary>
    public ExpectationParam currentEx;

    public enum ActionEnum
    {
        Atk1 = 0,
        Atk2,
        Atk3,
        AtkRemote,
    }

    public ExpectationParam GetExpectationParam(ActionEnum actionEnum)
    {
        foreach (var expectationParam in attackExpectations)
        {
            if (expectationParam.action != actionEnum) continue;
            return expectationParam;
        }

        throw new Exception($"没有{actionEnum}这个类型,请在EnemyAIAttribute添加");
    }

    /// <summary>
    /// 期望参数
    /// </summary>
    [Serializable]
    public class ExpectationParam
    {
        [Header("和玩家的距离")] public float distance;
        [Header("攻击范围")] public float range;
        [Header("攻击类型")] public ActionEnum action;
    }
}