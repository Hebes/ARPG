using System;

/// <summary>
/// 状态事件参数
/// </summary>
public class StateEventArgs : EventArgs
{
    public readonly string state;
    public StateEventArgs(string state) => this.state = state;
}