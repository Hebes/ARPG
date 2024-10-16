using System.Collections.Generic;

/// <summary>
/// 整数存储
/// </summary>
public static class RoundStorage
{
    private static Dictionary<string, int> StorageDict => R.GameData.ThisRoundValidStorage;

    public static bool Contain(string key)
    {
        return StorageUtil.Contain(RoundStorage.StorageDict, key);
    }

    public static void Set(string id, bool value)
    {
        StorageUtil.Set(RoundStorage.StorageDict, id, value);
    }

    public static void Set(string id, int value)
    {
        StorageUtil.Set(RoundStorage.StorageDict, id, value);
    }

    public static bool Get(string id, bool defaultValue)
    {
        return StorageUtil.Get(RoundStorage.StorageDict, id, defaultValue);
    }

    public static int Get(string id, int defaultValue)
    {
        return StorageUtil.Get(RoundStorage.StorageDict, id, defaultValue);
    }

    public static bool GetBool(string id)
    {
        return StorageUtil.GetBool(RoundStorage.StorageDict, id);
    }

    public static int Get(string id)
    {
        return StorageUtil.Get(RoundStorage.StorageDict, id);
    }

    public static void Clear()
    {
        RoundStorage.StorageDict.Clear();
    }
}