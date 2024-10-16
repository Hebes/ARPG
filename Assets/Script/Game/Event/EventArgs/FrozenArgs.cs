using System;
using UnityEngine;

/// <summary>
/// 片段冻结参数
/// </summary>
public class FrozenArgs : EventArgs
{
    public FrozenArgs(FrozenType type, GameObject target)
    {
        Type = type;
        Target = target;
    }

    public readonly FrozenType Type;

    public readonly GameObject Target;

    /// <summary>
    /// 冻结类型
    /// </summary>
    public enum FrozenType
    {
        /// <summary>
        /// 敌人
        /// </summary>
        Enemy,

        /// <summary>
        /// 玩家
        /// </summary>
        Player,

        /// <summary>
        /// 所有
        /// </summary>
        All,

        /// <summary>
        /// 目标
        /// </summary>
        Target
    }
}