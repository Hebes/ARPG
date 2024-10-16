using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

/// <summary>
/// 游戏场景初始化
/// </summary>
public class GameSceneInit : MonoBehaviour
{
    /// <summary>
    /// 是否是第一次
    /// </summary>
    private static bool _first = true;

    private void Awake()
    {
        if (!_first) return;
        QualitySettings.vSyncCount = R.Settings.VSync;
        //Cursor.visible = false; //隐藏鼠标
        //多语言
        if (string.IsNullOrEmpty(R.Settings.Language))
        {
            //R.Settings.Language = LocalizationManager.CurrentLanguage;
            R.Settings.Save();
        }

        //LocalizationManager.CurrentLanguage = R.Settings.Language;
        if (string.IsNullOrEmpty(R.Settings.AudioLanguage))
        {
            //R.Settings.AudioLanguage = UILanguage.CurrentLanguage.DefaultAudioLanguage;
            R.Settings.Save();
        }

        Load("GameBD", typeof(Preload), typeof(EnemyDataPreload)); //游戏数据
        Load("World", typeof(WorldTime), typeof(WorldSetting),typeof(SaveManager)); //世界时间
        Load("BattleZoneTrigger", typeof(BattleZoneTrigger)); //战斗区域触发
        Load("EffectGenerator", typeof(EffectController)); //效果生成数据
        Load("Prefab/Core", "MainCamera"); //主摄像机
        Load("Prefab/Core", "UI"); //UI摄像机
        Load("Prefab/Core", "AudioSource"); //音乐
    }

    private void Start()
    {
        if (!_first) return;
        PlayerInput player = Load("Prefab/Core", "Player").GetComponent<PlayerInput>(); //玩家
        Vector3 position = player.transform.position;
        position.z = LayerManager.ZNum.MMiddle_P;
        player.transform.position = position;

        //设置玩家宠物
        // if (R.GameData.WindyVisiable) 
        // {
        //     //玩家跟随的宠物
        //     GameObject windyTemp = GameObject.FindGameObjectWithTag(ConfigTag.Windy);
        //     if (windyTemp == null)
        //     {
        //         windyTemp = ConfigPrefab.Windy.Load<GameObject>(ConfigPath.RolePath);
        //         windyTemp = UnityEngine.Object.Instantiate<GameObject>(windyTemp);
        //     }
        //
        //     UnityEngine.Object.DontDestroyOnLoad(windyTemp);
        // }

        SceneGate sceneGate = SceneGateManager.I.FindGate(1);
        sceneGate?.Exit();
        _first = false;
    }

    private void Load(string nameValue, params Type[] typesArray)
    {
        GameObject temp = GameObject.Find(nameValue) ?? new GameObject(nameValue);
        foreach (var type in typesArray)
            temp.AddComponent(type);
        DontDestroyOnLoad(temp);
    }

    private GameObject Load(string pathValue, string nameValue)
    {
        GameObject temp = GameObject.Find(nameValue);
        if (temp == null)
        {
            temp = Asset.LoadFromResources<GameObject>(pathValue, nameValue);
            temp = Instantiate(temp);
        }

        DontDestroyOnLoad(temp);
        return temp;
    }
}