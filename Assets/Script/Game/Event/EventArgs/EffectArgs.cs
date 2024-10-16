using System;

/// <summary>
/// 影响参数
/// </summary>
public class EffectArgs : EventArgs
{
    public EffectArgs(string _effectName, bool _loop)
    {
        effectName = _effectName;
        loop = _loop;
    }

    public EffectArgs(float _animSpeed)
    {
        animSpeed = _animSpeed;
    }

    /// <summary>
    /// 影响名称
    /// </summary>
    public string effectName;

    /// <summary>
    /// 是否循环
    /// </summary>
    public bool loop;

    /// <summary>
    /// 动画速度
    /// </summary>
    public float animSpeed;
}