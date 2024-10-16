using System;

/// <summary>
/// 评估事件参数
/// </summary>
public class AssessmentEventArgs : EventArgs
{
    public AssessmentEventArgs(AssessmentEventArgs.EventType type)
    {
        this.Type = type;
    }

    public readonly AssessmentEventArgs.EventType Type;

    public enum EventType
    {
        AddFlashTime,//添加闪光时间
        CurrentComboFinish,//当前连击完成
        ContinueGame,//继续游戏
    }
}