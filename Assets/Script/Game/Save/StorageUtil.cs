using System;
using System.Collections.Generic;

/// <summary>
/// 存储单元
/// </summary>
internal static class StorageUtil
{
    public static bool Contain(Dictionary<string, int> storageDict, string key)
    {
        return storageDict.ContainsKey(key);
    }

    public static void Set(Dictionary<string, int> storageDict, string id, bool value)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));
        storageDict[id] = value ? 1 : 0;
    }

    public static void Set(Dictionary<string, int> storageDict, string id, int value)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));
        storageDict[id] = value;
    }

    public static bool Get(Dictionary<string, int> storageDict, string id, bool defaultValue)
    {
        if (storageDict.ContainsKey(id))
            return storageDict[id] == 1;
        return defaultValue;
    }

    public static int Get(Dictionary<string, int> storageDict, string id, int defaultValue)
    {
        if (storageDict.ContainsKey(id))
            return storageDict[id];
        return defaultValue;
    }

    public static bool GetBool(Dictionary<string, int> storageDict, string id)
    {
        if (storageDict.ContainsKey(id))
            return storageDict[id] == 1;
        throw new KeyNotFoundException(id + " 不存在");
    }

    public static int Get(Dictionary<string, int> storageDict, string id)
    {
        if (storageDict.ContainsKey(id))
            return storageDict[id];
        throw new KeyNotFoundException(id + " 不存在");
    }

    public static void Clear(Dictionary<string, int> storageDict)
    {
        storageDict.Clear();
    }
}