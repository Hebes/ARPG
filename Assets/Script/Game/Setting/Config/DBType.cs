public enum PlayerAtkDataType
{
    /// <summary>
    /// 攻击名称
    /// </summary>
    atkName,

    /// <summary>
    /// 受伤 振动偏移 摇晃偏移
    /// </summary>
    shakeOffset,

    /// <summary>
    /// 振动速度
    /// </summary>
    shakeSpeed,

    /// <summary>
    /// 振动帧
    /// </summary>
    shakeClip,

    /// <summary>
    /// 帧振动次数
    /// </summary>
    frameShakeClip,

    /// <summary>
    /// 冻结帧
    /// </summary>
    frozenClip,

    /// <summary>
    /// 受伤 振动类型 0 Rect 矩形 1 Horizon 水平 2 Vertical 垂直
    /// </summary>
    shakeType,

    /// <summary>
    /// 伤害百分比
    /// </summary>
    damagePercent,

    /// <summary>
    /// 间隔
    /// </summary>
    interval,

    /// <summary>
    /// 命中次数
    /// </summary>
    hitTimes,

    /// <summary>
    /// 命中类型 1 Once, 2 Limited, 3 UnLimited
    /// </summary>
    hitType,

    /// <summary>
    /// 摇杆是否振动 1 true -1 false
    /// </summary>
    joystickShakeNum,

    /// <summary>
    /// 援助类型
    /// </summary>
    assistanceType,

    /// <summary>
    /// 检查类型
    /// </summary>
    assistanceCheckType,

    /// <summary>
    /// 自动旋转 1 true -1 false
    /// </summary>
    autoTurn,

    /// <summary>
    /// 保持距离 1 true -1 false
    /// </summary>
    keepDis,
}

public enum EnemyAttackDataType
{
    /// <summary>
    /// x速度
    /// </summary>
    xSpeed,

    /// <summary>
    /// y速度
    /// </summary>
    ySpeed,

    /// <summary>
    /// 空中X速度
    /// </summary>
    airXSpeed,

    /// <summary>
    /// 空中y速度
    /// </summary>
    airYSpeed,

    /// <summary>
    /// 动画名称
    /// </summary>
    animName,

    /// <summary>
    /// 空中动画名称
    /// </summary>
    airAnimName,

    /// <summary>
    /// 伤害百分比
    /// </summary>
    damagePercent,

    /// <summary>
    /// 盾伤害
    /// </summary>
    shieldDamage,
}

public enum EnemyHurtDataType
{
    /// <summary>
    /// x速度
    /// </summary>
    xSpeed,

    /// <summary>
    /// y速度
    /// </summary>
    ySpeed,

    /// <summary>
    /// 空中X速度
    /// </summary>
    airXSpeed,

    /// <summary>
    /// 空中y速度
    /// </summary>
    airYSpeed,

    /// <summary>
    /// 默认攻击类型,敌人的受伤类型
    /// </summary>
    normalAtkType,

    /// <summary>
    /// 空中攻击类型
    /// </summary>
    airAtkType,
}

public enum PlayerHurtDataType
{
    /// <summary>
    /// x速度
    /// </summary>
    xSpeed,

    /// <summary>
    /// y速度
    /// </summary>
    ySpeed,

    /// <summary>
    /// 空中X速度
    /// </summary>
    airXSpeed,

    /// <summary>
    /// 空中y速度
    /// </summary>
    airYSpeed,

    /// <summary>
    /// 受到攻击的动画名称
    /// </summary>
    animName,

    /// <summary>
    /// 空中受到攻击的动画名称
    /// </summary>
    airAnimName,

    /// <summary>
    /// 伤害百分比
    /// </summary>
    damagePercent,

    /// <summary>
    /// 盾伤害
    /// </summary>
    shieldDamage,
}

public enum AudioMapType
{
    /// <summary>
    /// BOSS
    /// </summary>
    EnemyBoss,

    /// <summary>
    /// 精英
    /// </summary>
    EnemyElite,

    /// <summary>
    /// 默认
    /// </summary>
    EnemyNormal,

    /// <summary>
    /// 玩家攻击
    /// </summary>
    PlayerAtk,

    /// <summary>
    /// 玩家材质
    /// </summary>
    PlayerMaterial,

    /// <summary>
    /// 玩家移动
    /// </summary>
    PlayerMove,

    /// <summary>
    /// 场景
    /// </summary>
    Scene,

    /// <summary>
    /// 特殊的
    /// </summary>
    Special,

    /// <summary>
    /// 未分类的
    /// </summary>
    UnSorted,

    /// <summary>
    /// 视频
    /// </summary>
    Video,
}