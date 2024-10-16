using LitJson;

/// <summary>
/// 全局管理
/// </summary>
public static class R
{
    public static SettingData Settings
    {
        get
        {
            if (_settings != null) return _settings;
            if (GameSetting.GameSettingsKey.HasKey())
            {
                _settings = JsonMapper.ToObject<SettingData>(GameSetting.GameSettingsKey.GetString());
                "已加载设置".Log();
            }
            else
            {
                _settings = new SettingData();
                "创建设置".Log();
            }

            return _settings;
        }
    }
    public static Equipment Equipment => GameData.Equipment;

    /// <summary>
    /// 游戏数据
    /// </summary>
    public static GameData GameData
    {
        get => _gameData ??= new GameData();
        set => _gameData = value;
    }

    public static void DeadReset()
    {
        "死亡重置数据".Log();
        Mode.Reset();
         Cliff.Reset();
        WorldTime.Reset();
        Ui.Reset();
    }
    
    public static void RoundReset()
    {
        "周目结束重置数据".Log();
        Mode.Reset();
        Cliff.Reset();
        WorldTime.Reset();
        RoundStorage.Clear();
        PlayerAction.Reborn();
        GameData.BattleZoneDict.Clear();
        Ui.Reset();
    }

    /// <summary>
    /// 暂停游戏
    /// </summary>
    public static void PauseGame()
    {
        WorldTime.Pause();
        GameEvent.PauseGame.Trigger((UIPause.I, PauseGameArgs.Pause));
        Audio.PauseVoiceOver();
    }

    /// <summary>
    /// 恢复游戏
    /// </summary>
    public static void ResumeGame()
    {
        WorldTime.Resume();
        GameEvent.PauseGame.Trigger((UIPause.I, PauseGameArgs.Resume));
        Audio.ResumeVoiceOver();
    }
    
    /// <summary>
    /// 存档重置
    /// </summary>
    public static void SaveReset()
    {
        "删档重置数据".Log();
        Equipment.Clear();
        Mode.Reset();
        SceneData.Clear();
        GameData.Reset();
        BattleAssessmentManager.Reset();
        Cliff.Reset();
        PlayerAction.Reset();
        WorldTime.Reset();
        Ui.Reset();
        //UIStartController.IsEnterWithVoice = false;
        SaveManager.AutoDelete();
    }
    
    private static GameData _gameData;
    private static SettingData _settings;

    public static readonly EnemyManager Enemy = new EnemyManager();
    public static readonly PlayerManager Player = new PlayerManager();
    public static readonly CameraManager Camera = new CameraManager();
    public static readonly SceneData SceneData = new SceneData();
    public static readonly Mode Mode = new Mode();

    public static AchievementManager Trophy = AchievementManager.I;
    public static EffectController Effect => EffectController.I;
    public static SceneGateManager SceneGate => SceneGateManager.I;
    public static UIController Ui => UIController.I;
    public static AudioManager Audio => AudioManager.I;
}