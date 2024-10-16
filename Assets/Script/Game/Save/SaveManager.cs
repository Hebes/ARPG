using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 存储管理器
/// </summary>
public class SaveManager : SMono<SaveManager>
{
    /// <summary>
    /// 游戏数据加载
    /// </summary>
    private static GameData _gameDataLoaded;

    public static event EventHandler<SaveLoadedEventArgs> OnGameLoaded;

    /// <summary>
    /// 游戏数据
    /// </summary>
    public static GameData GameData => _gameDataLoaded;

    /// <summary>
    /// 是否正在自动存档
    /// </summary>
    public static bool IsBusy => SaveData.IsBusy;

    private void OnEnable()
    {
        SaveData.OnGameLoaded += OnSavedGameLoaded;
    }

    private void OnDisable()
    {
        SaveData.OnGameLoaded -= OnSavedGameLoaded;
    }


    /// <summary>
    /// 是否存在自动保存数据
    /// </summary>
    public static bool IsAutoSaveDataExists => SaveData.IsAutoSaveDataExists();

    /// <summary>
    /// 自动存档
    /// </summary>
    /// <param name="gameData"></param>
    /// <returns></returns>
    public static Coroutine AutoSave(GameData gameData)
    {
        return I.StartCoroutine(AutoSaveWhenIsNotBusy(gameData));

        IEnumerator AutoSaveWhenIsNotBusy(GameData gameDataValue)
        {
            while (IsBusy)
                yield return null;
            SaveData.Save(gameDataValue);
        }
    }

    /// <summary>
    /// 自动加载
    /// </summary>
    /// <returns></returns>
    public static Coroutine AutoLoad()
    {
        _gameDataLoaded = null;
        SaveData.Load();
        return I.StartCoroutine(AutoLoadAndWaitForLoadedCoroutine());

        //自动加载和等待被加载的协程
        IEnumerator AutoLoadAndWaitForLoadedCoroutine()
        {
            while (_gameDataLoaded == null)
                yield return null;
        }
    }

    /// <summary>
    /// 自动删除
    /// </summary>
    public static void AutoDelete()
    {
        SaveData.Delete();
    }

    /// <summary>
    /// 修改保存数据
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public static Coroutine ModifySaveData(Action<GameData> action)
    {
        return ModifySaveDataCoroutine(action).StartIEnumerator();

        //修改保存数据协程
        IEnumerator ModifySaveDataCoroutine(Action<GameData> actionValue)
        {
            yield return AutoLoad();
            GameData gameData = GameData;
            actionValue(gameData);
            AutoSave(gameData);
        }
    }

    /// <summary>
    /// 保存游戏加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSavedGameLoaded(object sender, SaveLoadedEventArgs e)
    {
        GameData gameData = e.GameData;
        _gameDataLoaded = gameData;
        OnGameLoaded?.Invoke(sender, e);
    }
}