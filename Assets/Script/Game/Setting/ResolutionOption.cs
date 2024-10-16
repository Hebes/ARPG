using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 分辨率的选择
/// </summary>
public class ResolutionOption : Singleton<ResolutionOption>
{
    /// <summary>
    /// 分辨率选项
    /// </summary>
    private Resolution[] _resolutions;

    /// <summary>
    /// 解析字符串
    /// </summary>
    private readonly List<string> _resolutionStrings = new List<string>();

    /// <summary>
    /// 标志位
    /// </summary>
    private int _index;

    /// <summary>
    /// 初始分辨率
    /// </summary>
    private Resolution _originResolution;

    public ResolutionOption()
    {
        QualitySettings.SetQualityLevel(2); // 设置质量级别为索引为1的级别
        InitResolutions();
        for (int i = 0; i < _resolutions.Length; i++)
        {
            Resolution resolution = _resolutions[i];
            if (resolution.width == Resolution.width && resolution.height == Resolution.height)
                _index = i;
            _index = QualitySettings.GetQualityLevel();
        }
    }

    /// <summary>
    /// 初始化分辨率选项
    /// </summary>
    private void InitResolutions()
    {
        _originResolution = new Resolution { width = Screen.width, height = Screen.height };
        float num = Screen.width / (float)Screen.height;
        _resolutions = new[]
        {
            new Resolution { width = (int)(540f * num), height = 540 },
            new Resolution { width = (int)(1080f * num), height = 1080 },
            _originResolution
        };
        _resolutionStrings.Add("low");
        _resolutionStrings.Add("normal");
        _resolutionStrings.Add("high");
    }

    /// <summary>
    /// 分辨率
    /// </summary>
    public Resolution Resolution
    {
        get => new() { width = Screen.width, height = Screen.height };
        set => Screen.SetResolution(value.width, value.height, Screen.fullScreen);
    }

    /// <summary>
    /// 解析字符串
    /// </summary>
    // public string ResolutionString
    // {
    //     get
    //     {
    //         return ScriptLocalization.Get("mobile/ui/quality_level/" + _resolutionStrings[_index]);
    //     }
    // }

    /// <summary>
    /// 设置偏移分辨率
    /// </summary>
    /// <param name="step"></param>
    public void SetResolutionByOffset(int step)
    {
        int num = _index + step;
        if (num >= 0 && num <= _resolutions.Length - 1)
        {
            _index = num;
            QualitySettings.SetQualityLevel(num);
            SetResolutionByQualitylevel();
            GameEvent.QualityChange.Trigger(this);
        }
    }

    /// <summary>
    /// 按质量级别设置分辨率
    /// </summary>
    public void SetResolutionByQualitylevel()
    {
        Debug.Log("按质量级别设置分辨率" + QualitySettings.GetQualityLevel());
        Resolution = _resolutions[QualitySettings.GetQualityLevel()];
    }
}