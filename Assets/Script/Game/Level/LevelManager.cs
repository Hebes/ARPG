using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 管卡管理器
/// </summary>
public class LevelManager
{
    /// <summary>
    /// 当前场景名称
    /// </summary>
    public static string SceneName => SceneManager.GetActiveScene().name;

    private static AsyncOperation _asyncOperation;

    /// <summary>
    /// 死亡人数
    /// </summary>
    private static int DeathCount
    {
        get => RoundStorage.Get("DeathCount", 0);
        set => RoundStorage.Set("DeathCount", value);
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="levelName"></param>
    /// <returns></returns>
    public static AsyncOperation LoadScene(string levelName)
    {
        if (_asyncOperation is { isDone: false })
            return _asyncOperation;
        _asyncOperation = SceneManager.LoadSceneAsync(levelName);
        if (_asyncOperation == null)
            (levelName + "场景不存在，是不是没放在build里？还是名字写错了？").Warning();
        return _asyncOperation;
    }

    /// <summary>
    /// 通过门加载场景
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="openType"></param>
    /// <returns></returns>
    public static Coroutine LoadLevelByGateId(string levelName, SceneGate.OpenType openType = SceneGate.OpenType.None)
    {
        return LoadLevelByGateId(levelName, 1, openType);
    }

    public static Coroutine LoadLevelByGateId(string levelName, int gateId, SceneGate.OpenType openType = SceneGate.OpenType.None)
    {
        SwitchLevelGateData switchLevelGateData = new SwitchLevelGateData();
        switchLevelGateData.ToLevelId = levelName;
        switchLevelGateData.ToId = gateId;
        switchLevelGateData.OpenType = openType;
        return R.SceneGate.Enter(switchLevelGateData);
    }

    /// <summary>
    /// 按位置划分的负载等级
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="position"></param>
    /// <param name="needProgressBar"></param>
    /// <returns></returns>
    public static Coroutine LoadLevelByPosition(string levelName, Vector3 position, bool needProgressBar = false)
    {
        SwitchLevelGateData switchLevelGateData = new();
        switchLevelGateData.ToLevelId = levelName;
        switchLevelGateData.ToId = -1;
        switchLevelGateData.TargetPosition = position;
        return R.SceneGate.Enter(switchLevelGateData, needProgressBar);
    }

    public static Coroutine OnRoundOver()
    {
        return RoundOverCoroutine().StartIEnumerator();
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    /// <param name="recordDeath">死亡记录</param>
    /// <returns></returns>
    public static Coroutine OnPlayerDie(bool recordDeath = true)
    {
        return LoadGame(recordDeath).StartIEnumerator();
    }

    private static IEnumerator RoundOverCoroutine()
    {
        yield break;
        //yield return R.Ui.LevelSelect.OpenWithAnim(true, true);
    }

    /// <summary>
    /// 加载游戏
    /// </summary>
    /// <param name="recordDeath"></param>
    /// <returns></returns>
    private static IEnumerator LoadGame(bool recordDeath)
    {
        R.DeadReset();
        yield return R.GameData.Load();
        yield return LoadLevelByPosition(R.GameData.SceneName, R.GameData.PlayerPosition);
        if (recordDeath)
        {
            DeathCount++;
            R.GameData.Save(true);
        }

        R.Player.Transform.position = R.GameData.PlayerPosition;
        R.Player.Action.TurnRound(R.GameData.PlayerAttributeGameData.faceDir);
        R.Camera.Controller.CameraResetPostionAfterSwitchScene();
        PlayerAction.Reborn();
    }


    public struct LevelName
    {
        public const string Start = "ui_start";
    }
}