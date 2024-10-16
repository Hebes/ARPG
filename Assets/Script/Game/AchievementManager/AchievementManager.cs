using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 成就管理器
/// </summary>
public class AchievementManager : SMono<AchievementManager>
{
    /// <summary>
    /// 成就信息字典
    /// </summary>
    private Dictionary<string, Dictionary<int, AchievementInfo>> _achievementInfoDic => DB.AchievementInfoDic;

    /// <summary>
    /// 奖杯最大数量
    /// </summary>
    private static readonly int TrophyMaxCount = 1;

    public const string Language = "Chinese (Simplified)";

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.O))
        {
            AchievementManager.I.AwardAchievement(1);
        }
    }

    /// <summary>
    /// 解锁奖杯成就
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool AwardAchievement(int index)
    {
        if (GetAchievementUnlockState(index)) return false;
        UnlockAchievement(index);
        UITrophyNotification.I.AwardTrophy(GetAchievementInfo(index).Name, index.ToString());
        UnlockHastur();
        return true;
    }

    /// <summary>
    /// 全部奖杯
    /// </summary>
    public void AwardAll()
    {
        for (int i = 1; i < TrophyMaxCount; i++)
            AwardAchievement(i);
    }

    /// <summary>
    /// 获取成就信息
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public AchievementInfo GetAchievementInfo(int index)
    {
        return _achievementInfoDic[Language][index];
    }

    /// <summary>
    /// 获得成就解锁状态
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool GetAchievementUnlockState(int index)
    {
        return R.Settings.AchievementInfo.Contains(index);
    }

    /// <summary>
    /// 解锁成就
    /// </summary>
    /// <param name="index"></param>
    private void UnlockAchievement(int index)
    {
        if (!GetAchievementUnlockState(index))
            R.Settings.AchievementInfo.Add(index);
        R.Settings.Save();
    }

    private void UnlockHastur()
    {
        if (R.Settings.AchievementInfo.Count == TrophyMaxCount - 1)
        {
            AwardAchievement(0);
        }
    }
}

/// <summary>
/// 成就信息
/// </summary>
public struct AchievementInfo
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 名称->都是简体中文
    /// </summary>
    public string Name;

    /// <summary>
    /// 详细
    /// </summary>
    public string Detail;

    public AchievementInfo(long id, string name, string detail)
    {
        Id = id;
        Name = name;
        Detail = detail;
    }
}