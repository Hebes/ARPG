using System;

/// <summary>
/// 战斗冲刺事件
/// </summary>
public class BattleRushEventArgs : EventArgs
{
    public BattleRushEventArgs(int num, int wave)
    {
        Num = num;
        Wave = wave;
    }

    public readonly int Num;

    public readonly int Wave;
}