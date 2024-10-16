using System.Collections.Generic;

/// <summary>
/// 存储数据
/// </summary>
public static class SaveStorage
{
    private static Dictionary<string, int> StorageDict => R.GameData.ThisSaveValidStorage;

    public static bool Contain(string key)
    {
        return StorageUtil.Contain(SaveStorage.StorageDict, key);
    }

    public static void Set(string id, bool value)
    {
        StorageUtil.Set(SaveStorage.StorageDict, id, value);
    }

    public static void Set(string id, int value)
    {
        StorageUtil.Set(SaveStorage.StorageDict, id, value);
    }

    public static bool Get(string id, bool defaultValue)
    {
        return StorageUtil.Get(SaveStorage.StorageDict, id, defaultValue);
    }

    public static int Get(string id, int defaultValue)
    {
        return StorageUtil.Get(SaveStorage.StorageDict, id, defaultValue);
    }

    public static bool GetBool(string id)
    {
        return StorageUtil.GetBool(SaveStorage.StorageDict, id);
    }

    public static int Get(string id)
    {
        return StorageUtil.Get(SaveStorage.StorageDict, id);
    }

    public static void Clear()
    {
        SaveStorage.StorageDict.Clear();
    }
}