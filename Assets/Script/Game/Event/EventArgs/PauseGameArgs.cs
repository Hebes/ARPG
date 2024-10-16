using System;

/// <summary>
/// 暂停游戏参数
/// </summary>
public class PauseGameArgs : EventArgs
{
    public PauseGameArgs(PauseGameArgs.State status)
    {
        this.Status = status;
    }

    public PauseGameArgs.State Status;

    public static PauseGameArgs Pause = new PauseGameArgs(PauseGameArgs.State.Pause);

    public static PauseGameArgs Resume = new PauseGameArgs(PauseGameArgs.State.Resume);

    public enum State
    {
        Pause,
        Resume
    }
}