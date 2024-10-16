using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏数据
/// </summary>
public class GameData
{
    /// <summary>
    /// 玩家位置
    /// </summary>
    public Vector3 PlayerPosition;

    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName = "Joanna";

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName = string.Empty;

    /// <summary>
    /// 当前场景名称
    /// </summary>
    public string SceneName;

    /// <summary>
    /// 宠物是否可见
    /// </summary>
    public bool WindyVisiable = true;

    /// <summary>
    /// 难度
    /// </summary>
    public int Difficulty;

    /// <summary>
    /// 保存有效存储
    /// </summary>
    public readonly Dictionary<string, int> ThisSaveValidStorage = new Dictionary<string, int>();

    /// <summary>
    /// 此轮有效存储
    /// </summary>
    public readonly Dictionary<string, int> ThisRoundValidStorage = new Dictionary<string, int>();

    /// <summary>
    /// 战斗地带字典
    /// </summary>
    public readonly Dictionary<string, bool> BattleZoneDict = new Dictionary<string, bool>();

    /// <summary>
    /// 玩家属性游戏数据
    /// </summary>
    public readonly PlayerAttributeGameData PlayerAttributeGameData = new PlayerAttributeGameData();

    /// <summary>
    /// 增强
    /// </summary>
    public readonly Enhancement Enhancement = new Enhancement();

    /// <summary>
    /// 使用的道具
    /// </summary>
    public readonly Equipment Equipment = new Equipment();

    /// <summary>
    /// 得分
    /// </summary>
    public readonly List<BloodPalaceTotalScore> BloodPalaceRecords = new List<BloodPalaceTotalScore>();

    /// <summary>
    /// 存档数据
    /// </summary>
    /// <param name="justSaveData"></param>
    /// <returns></returns>
    public YieldInstruction Save(bool justSaveData = false)
    {
        $"存档".Log();
        if (!justSaveData)
        {
            if (LevelManager.SceneName == CScene.InitScene)
            {
                "不能在开始界面存档".Log();
                return null;
            }

            PlayerPosition = R.Player.GameObject == null ? Vector3.zero : R.Player.Transform.position;
            SceneName = LevelManager.SceneName;
            SavePlayerAttribute(R.Player.Attribute);
        }

        return SaveManager.AutoSave(this);
    }

    /// <summary>
    /// 加载存档数据
    /// </summary>
    /// <returns></returns>
    public YieldInstruction Load()
    {
        //是否存在自动保存数据
        return SaveManager.IsAutoSaveDataExists ? LoadCoroutine().StartIEnumerator() : null;
    }

    private IEnumerator LoadCoroutine()
    {
        yield return SaveManager.AutoLoad();
        R.GameData = SaveManager.GameData;
        LoadPlayerAttribute(R.Player.Attribute, R.GameData.PlayerAttributeGameData); //加载玩家属性
    }

    /// <summary>
    /// 重置存档数据
    /// </summary>
    public void Reset()
    {
        R.GameData = new GameData();
        LoadPlayerAttribute(R.Player.Attribute, R.GameData.PlayerAttributeGameData);
    }

    /// <summary>
    /// 存储玩家属性
    /// </summary>
    /// <param name="attr"></param>
    private void SavePlayerAttribute(PlayerAttribute attr)
    {
        PlayerAttributeGameData.faceDir = attr.faceDir;
        PlayerAttributeGameData.moveSpeed = attr.moveSpeed;
        PlayerAttributeGameData.maxHP = attr.maxHP;
        PlayerAttributeGameData.currentHP = attr.currentHP;
        PlayerAttributeGameData.CurrentEnergy = attr.currentEnergy;
        PlayerAttributeGameData.flashLevel = attr.flashLevel;
    }

    /// <summary>
    /// 加载玩家属性
    /// </summary>
    /// <param name="attr"></param>
    /// <param name="playerAttributeGameData"></param>
    private void LoadPlayerAttribute(PlayerAttribute attr, PlayerAttributeGameData playerAttributeGameData)
    {
        attr.faceDir = playerAttributeGameData.faceDir;
        attr.moveSpeed = playerAttributeGameData.moveSpeed;
        attr.maxHP = playerAttributeGameData.maxHP;
        attr.currentHP = playerAttributeGameData.currentHP;
        attr.currentEnergy = playerAttributeGameData.CurrentEnergy;
        attr.flashLevel = playerAttributeGameData.flashLevel;
        attr.currentFlashTimes = attr.flashTimes;
    }
}