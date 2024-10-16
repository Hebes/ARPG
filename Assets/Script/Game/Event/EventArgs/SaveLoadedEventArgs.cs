using System;

/// <summary>
/// 存储数据的事件
/// </summary>
public class SaveLoadedEventArgs : EventArgs
{
    public GameData GameData;

    public SaveLoadedEventArgs(GameData gameData)
    {
        GameData = gameData;
    }
}