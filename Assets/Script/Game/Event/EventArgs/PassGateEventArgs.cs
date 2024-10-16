using System;

/// <summary>
/// 通过门事件参数
/// </summary>
public class PassGateEventArgs : EventArgs
{
    public PassGateEventArgs(PassGateEventArgs.PassGateStatus status, SwitchLevelGateData data, string mySceneName) :
        this(status, data.ToLevelId, data.ToId, mySceneName, data.MyId)
    {
        
    }

    public PassGateEventArgs(PassGateEventArgs.PassGateStatus status, string toSceneName, int toId, string mySceneName,
        int myId)
    {
        this.Status = status;
        this.ToSceneName = toSceneName;
        this.ToId = toId;
        this.MySceneName = mySceneName;
        this.MyId = myId;
    }

    public readonly PassGateEventArgs.PassGateStatus Status;

    public readonly string ToSceneName;

    public readonly int ToId;

    public readonly string MySceneName;

    public readonly int MyId;

    /// <summary>
    /// 通过门事件的状态
    /// </summary>
    public enum PassGateStatus
    {
        Enter,
        Exit,
    }
}