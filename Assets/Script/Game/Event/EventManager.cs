using System;
using System.Collections.Generic;

/// <summary>
/// 游戏事件
/// </summary>
public enum GameEvent
{
    /// <summary>
    /// 战斗评估
    /// </summary>
    Assessment = 1,

    /// <summary>
    /// 玩家转向
    /// </summary>
    OnPlayerTurnRound,

    /// <summary>
    /// 通过门
    /// </summary>
    PassGate,

    /// <summary>
    /// 切换分辨率
    /// </summary>
    QualityChange,

    /// <summary>
    /// 玩家受伤
    /// </summary>
    PlayerHurt,

    /// <summary>
    /// 敌人伤害攻击
    /// </summary>
    EnemyHurtAtk,

    /// <summary>
    /// 玩家受到伤害
    /// </summary>
    PlayerHurtAtk,

    /// <summary>
    /// 暂停游戏
    /// </summary>
    PauseGame,

    /// <summary>
    /// 世界时间片段冻结
    /// </summary>
    WorldTimeFrozenEvent,

    /// <summary>
    /// 世界时间片段恢复
    /// </summary>
    WorldTimeResumeEvent,

    /// <summary>
    /// 敌人死亡
    /// </summary>
    EnemyKilled,

    /// <summary>
    /// 击穿的地面
    /// </summary>
    HitWall,

    /// <summary>
    /// 战斗
    /// </summary>
    Battle,

    /// <summary>
    /// 战斗冲
    /// </summary>
    BattleRush,

    /// <summary>
    /// 敌人找到玩家
    /// </summary>
    EnemyFindPlayer,

    /// <summary>
    /// 切换世界之前
    /// </summary>
    BeforeSwitchingWorlds,

    /// <summary>
    /// 切换世界之后
    /// </summary>
    AfterSwitchingWorlds,

    /// <summary>
    /// 玩家动画切换
    /// </summary>
    OnPlayerAnimChange,
    
    /// <summary>
    /// 玩家动画速度切换
    /// </summary>
    OnPlayerAnimSpeedChange,
    
    /// <summary>
    /// 玩家死亡
    /// </summary>
    OnPlayerDead,
    
    /// <summary>
    /// 提升
    /// </summary>
    EnhanceLevelup
}

public delegate void OnEventAction(object udata);

/// <summary>
/// 事件管理器
/// </summary>
public class EventManager : SMono<EventManager>
{
    /// <summary>
    /// 事件字典
    /// </summary>
    private readonly Dictionary<Enum, EventData> _eventDic = new();

    /// <summary>
    /// 注册监听
    /// </summary>
    /// <param name="enumValue"></param>
    /// <param name="action"></param>
    public void Register(Enum enumValue, OnEventAction action)
    {
        if (!_eventDic.ContainsKey(@enumValue))
            _eventDic.Add(@enumValue, new EventData());
        _eventDic[enumValue].Add(action);
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="enumValue"></param>
    /// <param name="action"></param>
    public void UnRegister(Enum enumValue, OnEventAction action)
    {
        if (!_eventDic.ContainsKey(@enumValue)) return;
        _eventDic[enumValue].UnAdd(action);
    }

    /// <summary>
    /// 触发监听
    /// </summary>
    /// <param name="enumValue"></param>
    /// <param name="data"></param>
    public void Trigger(Enum enumValue, object data)
    {
        if (_eventDic.TryGetValue(enumValue, out EventData actionList))
            actionList?.Trigger(data);
    }
}

/// <summary>
/// 事件数据
/// </summary>
public class EventData
{
    private readonly List<OnEventAction> _actionList = new List<OnEventAction>();

    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="action"></param>
    /// <exception cref="Exception"></exception>
    public void Add(OnEventAction action)
    {
        if (_actionList.Contains(action))
            throw new Exception($"已经有当前方法{nameof(action)}");
        _actionList.Add(action);
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="action"></param>
    public void UnAdd(OnEventAction action)
    {
        _actionList.Remove(action);
    }

    /// <summary>
    /// 触发
    /// </summary>
    /// <param name="data"></param>
    public void Trigger(object data)
    {
        for (var i = 0; i < _actionList.Count; i++)
            _actionList[i].Invoke(data);
    }
}

/// <summary>
/// 全称EventExpand 事件拓展
/// </summary>
public static class EEvent
{
    /// <summary>
    /// 注册事件
    /// (GameObject,T) args = ((GameObject,T))udata;
    /// </summary>
    /// <param name="enumValue"></param>
    /// <param name="action"></param>
    public static void Register(this Enum enumValue, OnEventAction action) => EventManager.I.Register(enumValue, action);

    /// <summary>
    /// 取消事件注册
    /// </summary>
    /// <param name="enumValue"></param>
    /// <param name="action"></param>
    public static void UnRegister(this Enum enumValue, OnEventAction action) => EventManager.I.UnRegister(enumValue, action);

    /// <summary>
    /// 触发注册的事件
    /// </summary>
    /// <param name="enumValue"></param>
    /// <param name="data"></param>
    public static void Trigger(this Enum enumValue, object data) => EventManager.I.Trigger(enumValue, data);
}
