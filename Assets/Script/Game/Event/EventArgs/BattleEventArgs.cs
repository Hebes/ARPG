using System;

/// <summary>
/// 战斗事件
/// </summary>
public class BattleEventArgs : EventArgs
{
    public BattleEventArgs(BattleEventArgs.BattleStatus status)
    {
        this.Status = status;
    }

    public static BattleEventArgs Begin = new BattleEventArgs(BattleEventArgs.BattleStatus.Begin);

    public static BattleEventArgs End = new BattleEventArgs(BattleEventArgs.BattleStatus.End);

    public readonly BattleEventArgs.BattleStatus Status;

    public enum BattleStatus
    {
        Begin,
        End
    }
}