using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 敌人创建
/// </summary>
public class EnemyGenerator : Singleton<EnemyGenerator>
{
    private const string ConfigFileName = "EnemyGeneratorConfig";
    private const string EnemysPrefabPath = "Prefab/Enemys";
    private static IDictionary<string, EnemyType> _enemyNameToType;
    private static IDictionary<EnemyType, EnemyPrefab> _enemyPrefabs;

    /// <summary>
    /// 敌人列表
    /// </summary>
    private static IDictionary<EnemyType, EnemyPrefab> EnemyPrefabs
    {
        get
        {
            IDictionary<EnemyType, EnemyPrefab> result;
            if ((result = _enemyPrefabs) == null)
                result = _enemyPrefabs = LoadEnemyPrefabs();
            return result;
        }
    }

    /// <summary>
    /// 预加载敌人预制件
    /// </summary>
    public static void PreloadEnemyPrefabs()
    {
        _enemyPrefabs = LoadEnemyPrefabs();
    }

    /// <summary>
    /// 加载敌人预制体
    /// </summary>
    /// <returns></returns>
    private static IDictionary<EnemyType, EnemyPrefab> LoadEnemyPrefabs()
    {
        //加载敌人
        List<EnemyGeneratorJsonObject> temp = new List<EnemyGeneratorJsonObject>();

        Dictionary<EnemyType, EnemyPrefab> enemyPrefabDic = new Dictionary<EnemyType, EnemyPrefab>();
        foreach (var t in temp)
        {
            EnemyPrefab temp1 = new EnemyPrefab();
            temp1.inUse = true;
            temp1.prefab = Asset.LoadFromResources<GameObject>("Prefab/Enemys", t.prefabName);
            temp1.type = t.typeString.ToEnum<EnemyType>();
            enemyPrefabDic.Add(temp1.type, temp1);
        }

        return enemyPrefabDic;
    }

    /// <summary>
    /// 获取敌人类型
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static EnemyType GetEnemyType(string name)
    {
        name = name.Replace("(Clone)", string.Empty);

        _enemyNameToType ??= EnemyPrefabs.ToDictionary(
            e => e.Value.prefab.name,
            e => e.Key);

        return _enemyNameToType[name];
    }

    /// <summary>
    /// 创建敌人
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pos"></param>
    /// <param name="withEffect"></param>
    /// <param name="enemyPoint"></param>
    /// <returns></returns>
    public GameObject GenerateEnemy(string name, Vector2? pos = null, bool withEffect = true, bool enemyPoint = true)
    {
        return GenerateEnemy(GetEnemyType(name), pos, withEffect, enemyPoint);
    }

    /// <summary>
    /// 创建敌人
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    /// <param name="withEffect"></param>
    /// <param name="enemyPoint"></param>
    /// <returns></returns>
    public GameObject GenerateEnemy(EnemyType type, Vector2? pos = null, bool withEffect = true, bool enemyPoint = true)
    {
        return GenerateEnemy(EnemyPrefabs[type], pos ?? EnemyPrefabs[type].prefab.transform.position, withEffect, enemyPoint);
    }

    /// <summary>
    /// 生成的敌人
    /// </summary>
    /// <param name="enemyPrefab"></param>
    /// <param name="pos"></param>
    /// <param name="withEffect">是否显示出现效果</param>
    /// <param name="enemyPoint"></param>
    /// <returns></returns>
    private GameObject GenerateEnemy(EnemyPrefab enemyPrefab, Vector2 pos, bool withEffect, bool enemyPoint)
    {
        GameObject gameObject = Object.Instantiate<GameObject>(enemyPrefab.prefab);
        EnemyAttribute component = gameObject.GetComponent<EnemyAttribute>();
        if (component != null)
        {
            gameObject.transform.position = new Vector3(pos.x, pos.y, LayerManager.ZNum.MMiddleE(component.rankType));
            gameObject.transform.localScale = Vector3.one;
            if (Application.isPlaying)
            {
                component.SetBasicData(EnemyAttrData.FindBySceneNameAndType(LevelManager.SceneName, enemyPrefab.type));
                pos.y = ClampOverGround(pos);
                if (enemyPoint || component.InTheWorld)
                {
                    
                    R.Ui.CreateEnemyPoint(component);
                }

                EnemyBaseAction component2 = component.GetComponent<EnemyBaseAction>();
                component2.AppearAtPosition(pos);
                if (withEffect)
                {
                    component2.AppearEffect(pos);
                }
            }
        }
        else
        {
            "没有找到EnemyAttribute组件".Log();
            gameObject.transform.position = new Vector3(pos.x, pos.y, LayerManager.ZNum.MMiddleE(EnemyAttribute.RankType.Normal));
            gameObject.transform.localScale = Vector3.one;
        }

        return gameObject;
    }

    /// <summary>
    /// 距离地面
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private float ClampOverGround(Vector3 pos)
    {
        BoxCollider2D boxCollider2D = Physics2D.OverlapPoint(pos, LayerManager.GroundMask) as BoxCollider2D;
        if (boxCollider2D == null)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(pos, Vector2.down, GameArea.MapRange.height, LayerManager.GroundMask);
            pos.y -= raycastHit2D.distance;
            return pos.y;
        }

        return boxCollider2D.transform.position.y + boxCollider2D.size.y * boxCollider2D.transform.localScale.y / 2f;
    }
}

/// <summary>
/// 敌人预制体数据
/// </summary>
[Serializable]
public class EnemyPrefab
{
    /// <summary>
    /// 敌人预制体
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// 类型
    /// </summary>
    public EnemyType type;

    /// <summary>
    /// 是否正在使用
    /// </summary>
    public bool inUse;
}

/// <summary>
/// 敌人生成器
/// </summary>
[Serializable]
public class EnemyGeneratorJsonObject
{
    /// <summary>
    /// 物品名称
    /// </summary>
    public string prefabName;

    /// <summary>
    /// 类型的字符串
    /// </summary>
    public string typeString;

    /// <summary>
    /// 是否使用中
    /// </summary>
    public bool inUse;
}