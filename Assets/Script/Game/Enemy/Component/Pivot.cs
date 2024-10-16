using UnityEngine;

/// <summary>
/// 支点
/// </summary>
public class Pivot : MonoBehaviour, IPivot
{
    /// <summary>
    /// 游戏助手
    /// </summary>
    public Vector3 gameAssistantOffset = new Vector3(0f, 1.3f, 0f);

    /// <summary>
    /// 攻击伤害效果抵消
    /// </summary>
    public Vector3 attackHurtEffectOffset;

    /// <summary>
    /// 攻击伤害数
    /// </summary>
    public Vector2 attackHurtNumberOffset;

    /// <summary>
    /// HPBar偏移
    /// </summary>
    public Vector2 HPBarOffset;
    
    /// <summary>
    /// 获得攻击伤害效果抵消
    /// </summary>
    /// <returns></returns>
    public Vector3 GetAttackHurtEffectOffset() => attackHurtEffectOffset;

    public Vector2 GetAttackHurtNumberOffset() => attackHurtNumberOffset;

    public Vector2 GetHPBarOffset() => HPBarOffset;
    
    public Vector3 GetGameAssistantOffset()
    {
        EnemyAttribute component = GetComponent<EnemyAttribute>();
        return component == null ? gameAssistantOffset : new Vector3(0f, component.bounds.size.y / 2f, 0f);
    }
}