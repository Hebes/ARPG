using UnityEngine;

/// <summary>
/// 平台物理
/// </summary>
public interface IPlatformPhysics
{
    /// <summary>
    /// 速度
    /// </summary>
    Vector2 velocity { get; set; }

    /// <summary>
    /// 位置
    /// </summary>
    Vector2 position { get; set; }

    /// <summary>
    /// 是否在地面
    /// </summary>
    bool isOnGround { get; }
}