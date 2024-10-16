using System;
using UnityEngine;

/// <summary>
/// 层级管理器
/// </summary>
public class LayerManager
{
    public static readonly int PlayerMask = LayerMask.GetMask(new[] { "Player" });

    public static readonly int PlayerBodyMask = LayerMask.GetMask(new[] { "PlayerBody" });

    public static readonly int EnemyMask = LayerMask.GetMask(new[] { "Enemy" });

    /// <summary>
    /// 地面层级
    /// </summary>
    public static readonly int GroundMask = LayerMask.GetMask(new[] { "Ground" });

    /// <summary>
    /// 天花板
    /// </summary>
    public static readonly int CeilingMask = LayerMask.GetMask(new[] { "Ceiling" });

    /// <summary>
    /// 障碍物层级
    /// </summary>
    public static readonly int ObstacleMask = LayerMask.GetMask(new[] { "Obstacle" });

    /// <summary>
    /// 墙壁层级
    /// </summary>
    public static readonly int WallMask = LayerMask.GetMask(new[] { "Wall" });

    public static readonly int OneWayGroundMask = LayerMask.GetMask(new[] { "OneWayPlatforms" });

    public static readonly int PhysicCheckLineMask = LayerMask.GetMask(new[] { "PhysicCheckLine" });

    public static readonly int ColliderimpactMask = LayerMask.GetMask(new[] { "SoftCollidedImpact" });

    public static readonly int PlayerLayerID = LayerMask.NameToLayer("Player");

    public static readonly int PlayerBodyLayerID = LayerMask.NameToLayer("PlayerBody");

    public static readonly int EnemyLayerID = LayerMask.NameToLayer("Enemy");

    public static readonly int GroundLayerID = LayerMask.NameToLayer("Ground");

    public static readonly int CeilingLayerID = LayerMask.NameToLayer("Ceiling");

    public static readonly int ObstacleLayerID = LayerMask.NameToLayer("Obstacle");

    public static readonly int WallLayerID = LayerMask.NameToLayer("Wall");

    public static readonly int OneWayPlatformLayerID = LayerMask.NameToLayer("OneWayPlatforms");

    /// <summary>
    /// Z枚举
    /// </summary>
    public enum ZNumEnum
    {
        NNear,
        MNear,
        NMiddle,
        MMiddle_P,
        MMiddle_E,
        FMiddle,
        NFar,
        MFar,
        FFar,
        BgFar,
        Fx
    }

    public static class ZNum
    {
        public static float MMiddleE(EnemyAttribute.RankType rankType = EnemyAttribute.RankType.Normal)
        {
            switch (rankType)
            {
                case EnemyAttribute.RankType.Normal:
                    return LayerManager.ZNum.MMiddle_E;
                case EnemyAttribute.RankType.Elite:
                    return LayerManager.ZNum.MMiddle_E_Elite;
                case EnemyAttribute.RankType.BOSS:
                    return LayerManager.ZNum.MMiddle_E_Boss;
                default:
                    throw new ArgumentOutOfRangeException("等级类型", rankType, null);
            }
        }

        public static float NNear = -5f;

        public static float MNear = -4f;

        public static float NMiddle = -0.003f;

        public static float MMiddle_P = -0.002f;

        private static float MMiddle_E = -0.001f;

        private static float MMiddle_E_Elite = -0.0009f;

        private static float MMiddle_E_Boss = -0.0008f;

        public static float TempEnemy = -0.003f;

        public static float FMiddle;

        public static float NFar = 10f;

        public static float MFar = 20f;

        public static float FFar = 30f;

        public static float BgFar = 50f;

        public static float Fx = -0.2f;
    }

    public class YNum
    {
        /// <summary>
        /// 获取地面高度
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static float GetGroundHeight(GameObject target)
        {
            BoxCollider2D component = target.GetComponent<BoxCollider2D>();
            Vector3 position = target.transform.position;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(target.transform.position, Vector2.down, 100f,
                LayerManager.GroundMask | LayerManager.OneWayGroundMask);
            if (component == null)
                return raycastHit2D.point.y;
            RaycastHit2D raycastHit2D2 = Physics2D.Raycast(
                new Vector3(position.x + component.offset.x - component.size.x / 2f, position.y, position.z),
                Vector2.down, 100f, LayerManager.GroundMask | LayerManager.OneWayGroundMask);
            RaycastHit2D raycastHit2D3 = Physics2D.Raycast(
                new Vector3(position.x + component.offset.x + component.size.x / 2f, position.y, position.z),
                Vector2.down, 100f, LayerManager.GroundMask | LayerManager.OneWayGroundMask);
            return Mathf.Max(raycastHit2D2.point.y, raycastHit2D3.point.y);
        }

        public const float Zero = -4.2f;
    }
}