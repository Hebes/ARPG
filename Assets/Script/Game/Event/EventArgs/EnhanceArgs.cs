using System;

/// <summary>
/// 增强的参数
/// </summary>
public class EnhanceArgs : EventArgs
{
    public EnhanceArgs(string name, int upToLevel)
    {
        Name = name;
        UpToLevel = upToLevel;
    }

    public string Name;

    public int UpToLevel;
}