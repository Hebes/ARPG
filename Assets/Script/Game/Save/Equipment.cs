using System;

/// <summary>
/// 使用的道具
/// </summary>
[Serializable]
public class Equipment
{
    public void Clear()
    {
        CoinNum = 0;
    }

    /// <summary>
    /// 金币
    /// </summary>
    public int CoinNum;
}