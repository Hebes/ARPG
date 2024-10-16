using System;
using UnityEngine;

/// <summary>
/// 效果属性
/// </summary>
[Serializable]
public class EffectAttr
{
    public int id;
    [Header("是否可见")] public bool isFollow;
    [Header("最大数量")] public int maxCount = 20;
    [Header("效果开始数量")] public int effectStartCount = 3;
    [Header("效果旋转情况")] public FXRotationCondition rotation;
    [Header("效果")] public Transform effect;
    [Header("规模")] public Vector3 scale;
    [Header("预设路径")] [HideInInspector] public string prefabPath;
    [Header("功能名称")] public string functionName;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isFollow">是否可见</param>
    /// <param name="maxCount">最大数量</param>
    /// <param name="effectStartCount">效果开始数量</param>
    /// <param name="rotation">效果旋转情况</param>
    /// <param name="effectName">效果</param>
    /// <param name="scale">规模</param>
    /// <param name="functionName">功能名称</param>
    public EffectAttr(int id, FXRotationCondition rotation, string effectName,
        Vector3 scale, string functionName, int effectStartCount = 3, bool isFollow = false, int maxCount = 20)
    {
        this.id = id;
        this.isFollow = isFollow;
        this.maxCount = maxCount;
        this.effect = Asset.LoadFromResources<Transform>("Prefab/Effect", effectName);
        this.effectStartCount = effectStartCount;
        this.rotation = rotation;
        this.scale = scale;
        this.functionName = functionName;
    }
}