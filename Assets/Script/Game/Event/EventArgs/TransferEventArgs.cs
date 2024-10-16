using System;

/// <summary>
/// 切换事件参数
/// </summary>
public class TransferEventArgs : EventArgs
{
    /// <summary>
    /// 最后一个状态
    /// </summary>
    public readonly string lastState;
    
    /// <summary>
    /// 下一个状态
    /// </summary>
    public readonly string nextState;

    public TransferEventArgs(string lastState, string nextState)
    {
        this.lastState = lastState;
        this.nextState = nextState;
    }
}