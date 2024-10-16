using System;

/// <summary>
/// 玩家属性保存数据
/// </summary>
[Serializable]
public class PlayerAttributeGameData
{
    /// <summary>
    /// 朝向
    /// </summary>
    public int faceDir;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// 最大HP
    /// </summary>
    public int maxHP;

    /// <summary>
    /// 当前HP
    /// </summary>
    public int currentHP;

    /// <summary>
    /// 最大能量
    /// </summary>
    public int maxEnergy;

    /// <summary>
    /// 当前能量
    /// </summary>
    public int CurrentEnergy;

    /// <summary>
    /// 闪等级
    /// </summary>
    public int flashLevel = 1;
}