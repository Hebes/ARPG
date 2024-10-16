using System;
using UnityEngine;

/// <summary>
/// 2d射线检测到的描述
/// </summary>
[Serializable]
public class CharacterRay2D
{
    [Header("位置")] public Vector2 m_position = Vector2.zero;
    [Header("穿透")] public float m_penetration;
    [Header("额外的距离")] public float m_extraDistance = 0.1f;
    [Header("射线信息")] public RayHitInfo m_hitInfo;
}

/// <summary>
/// 射线信息
/// </summary>
[Serializable]
public struct RayHitInfo
{
    [Header("碰撞体")] public Collider2D m_collider;
    //[Header("美术材质")] public APMaterial m_material;
    [Header("默认的值")] public Vector2 m_normal;
    [Header("穿透")] public float m_penetration;
}

/// <summary>
/// 忽略碰撞器
/// </summary>
public class IgnoreCollider
{
    /// <summary>
    /// 碰撞盒
    /// </summary>
    public Collider2D m_collider;

    /// <summary>
    /// 持续时间
    /// </summary>
    public float m_duration;

    /// <summary>
    /// 当前时间
    /// </summary>
    public float m_curTime;

    public IgnoreCollider(Collider2D a1, float a2, float a3)
    {
        m_collider = a1;
        m_duration = a2;
        m_curTime = a3;
    }
}