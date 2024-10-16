using System.Collections.Generic;
using LitJson;
using UnityEngine;

[System.Serializable]
public class SettingData
{
    public void Save()
    {
        $"{GameSetting.GameSettingsKey}数据保存".Log();
        GameSetting.GameSettingsKey.SetString(JsonMapper.ToJson(this));
        PlayerPrefsTools.Save();
    }


    /// <summary>
    /// 是否开启作弊模式
    /// </summary>
    public bool CheatMode;

    /// <summary>
    /// 副标题有形
    /// </summary>
    public bool SubtitleVisiable = true;

    /// <summary>
    /// 影响音量
    /// </summary>
    public float EffectsVolume = 100f;

    /// <summary>
    /// BGM音量
    /// </summary>
    public float BGMVolume = 100f;
    
    /// <summary>
    /// 全局音量
    /// </summary>
    public float GlobalVolume = 100f;

    /// <summary>
    /// 是否静音
    /// </summary>
    public bool IsEffectsMute;

    /// <summary>
    /// BGM是否静音
    /// </summary>
    public bool IsBGMMute;

    /// <summary>
    /// 全局是否静音
    /// </summary>
    public bool IsGlobalMute;

    /// <summary>
    /// 语言
    /// </summary>
    public string Language;

    /// <summary>
    /// 自动语言
    /// </summary>
    public string AudioLanguage;

    /// <summary>
    /// 是否振动
    /// </summary>
    public bool IsVibrate = true;

    /// <summary>
    /// 垂直同步
    /// </summary>
    public int VSync = 1;

    /// <summary>
    /// FPS
    /// </summary>
    public int FPS = 60;

    /// <summary>
    /// 动态操纵杆打开
    /// </summary>
    public bool DynamicJoystickOpen;

    /// <summary>
    /// 按键映射
    /// </summary>
    public Dictionary<string, KeyCode> KeyMap = new Dictionary<string, KeyCode>();

    /// <summary>
    /// 成就信息列表
    /// </summary>
    public List<int> AchievementInfo = new List<int>();
}