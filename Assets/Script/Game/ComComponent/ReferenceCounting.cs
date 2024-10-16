using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// API引用计数
/// </summary>
public class APIReferenceCouting : MonoBehaviour
{
    public const string Effect = "Effect";
    public const string Audio = "Audio";
    private static readonly Dictionary<string, Dictionary<string, int>> _referenceCountingDic = new Dictionary<string, Dictionary<string, int>>();
    
    private void Awake()
    {
        _referenceCountingDic.Add("Effect", new Dictionary<string, int>());
        _referenceCountingDic.Add("Audio", new Dictionary<string, int>());
    }

    [Conditional("UNITY_EDITOR")]
    public static void OnUse(string flag, string id)
    {
        $"添加引用计数{flag}".Log();
        Dictionary<string, int> dictionary = _referenceCountingDic[flag];
        if (!dictionary.TryAdd(id, 1))
        {
            Dictionary<string, int> dictionary2;
            (dictionary2 = dictionary)[id] = dictionary2[id] + 1;
        }
    }
}