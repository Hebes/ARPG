using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗检查点
/// </summary>
public class BattleCheckPoint : BaseBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < fileId.Count; i++)
            _levelEnemyData.Add(fileId[i],GameReadDB.LevelEnemyData[LevelManager.SceneName]);
    }

    private void OnEnable()
    {
        queue = GetComponent<EnemyQueue>();
    }

    private void Start()
    {
        if (!PointData.canMultiTrigger && R.GameData.BattleZoneDict.ContainsKey(gameObject.name))
        {
            isBattleOver = R.GameData.BattleZoneDict[gameObject.name];
        }
    }

    private void Update()
    {
        if (isBattleOver) return;
        //持续不断生成敌人
        if (isOpenBattleZone && noEnemyTime > 0.5f && fileId.Count > currentId)
        {
            noEnemyTime = 0f;
            StartCoroutine(GenerateEnemyRushOneAfterOne(fileId[currentId], PointData.HasWall));
            currentId++;
        }

        if (isOpenBattleZone && noEnemyTime > 1f && PointData.HasWall)
        {
            isBattleOver = true;
            LeftGate.GetComponent<BattleZoneGate>().DisAppear();
            RightGate.GetComponent<BattleZoneGate>().DisAppear();
            StartCoroutine(OpenArea());
        }
        else if (!PointData.HasWall && (isOpenBattleZone && noEnemyTime > 0.3f || R.Player.IsNearSceneGate()))
        {
            StartCoroutine(OpenArea());
        }

        if (useNoEnemyTime)
        {
            if (PointData.HasWall)
            {
                rect.xMin = LeftGate.position.x - 5f;
                rect.xMax = RightGate.position.x + 5f;
                rect.yMin = Mathf.Min(LeftGate.position.y, RightGate.position.y) - 5f;
                rect.height = 50f;
            }
            else
            {
                rect.x = GameArea.MapRange.x;
                rect.y = GameArea.MapRange.y;
                rect.width = GameArea.MapRange.width;
                rect.height = GameArea.MapRange.height;
            }

            if (!R.Enemy.HasEnemyInRect(rect) && (queue.GetEnemyCount() <= 0 || queue.queueData.CheckEnemy != EnemyType.未定))
            {
                noEnemyTime += Time.deltaTime;
            }
            else
            {
                noEnemyTime = 0f;
            }
        }
    }

    /// <summary>
    /// 开放区域
    /// </summary>
    /// <returns></returns>
    private IEnumerator OpenArea()
    {
        isOpenBattleZone = false;
        ExitBattleMode();
        R.GameData.BattleZoneDict.TryAdd(gameObject.name, true);
        yield return new WaitForSeconds(0.5f);
        GameArea.CameraRange.xMin = GameArea.MapRange.min.x;
        GameArea.CameraRange.xMax = GameArea.MapRange.max.x;
        GameArea.PlayerRange.xMin = GameArea.MapRange.min.x - 1f;
        GameArea.PlayerRange.xMax = GameArea.MapRange.max.x + 1f;
        GameEvent.Battle.Trigger(BattleEventArgs.End);
    }

    /// <summary>
    /// 退出战斗模式
    /// </summary>
    private void ExitBattleMode()
    {
        if (R.Mode.CheckMode(Mode.AllMode.Battle))
            R.Mode.ExitMode(Mode.AllMode.Battle);
    }

    /// <summary>
    /// 初始化战斗区域
    /// </summary>
    public void InitGameArea()
    {
        if (PointData.HasWall)
        {
            LeftGate.GetComponent<BattleZoneGate>().Appear();
            RightGate.GetComponent<BattleZoneGate>().Appear();
            GameArea.CameraRange.xMin = LeftGate.position.x;
            GameArea.CameraRange.xMax = RightGate.position.x;
            GameArea.EnemyRange.xMin = LeftGate.position.x;
            GameArea.EnemyRange.xMax = RightGate.position.x;
            GameArea.PlayerRange.xMin = LeftGate.Find("PlayerLimitPosLX").transform.position.x;
            GameArea.PlayerRange.xMax = RightGate.Find("PlayerLimitPosRX").transform.position.x;
        }
        else
        {
            GameArea.EnemyRange.xMin = GameArea.MapRange.min.x + 15f;
            GameArea.EnemyRange.xMax = GameArea.MapRange.max.x - 15f;
        }
    }

    /// <summary>
    /// 持续不断生成敌人
    /// </summary>
    /// <param name="num"></param>
    /// <param name="canShake">是否需要相机振动</param>
    /// <returns></returns>
    private IEnumerator GenerateEnemyRushOneAfterOne(int num, bool canShake)
    {
        if (_levelEnemyData.TryGetValue(num, out LevelEnemyData levelEnemyData))
        {
            GameEvent.BattleRush.Trigger((this, new BattleRushEventArgs(num, currentId)));
            foreach (EnemyJsonObject enemyJsonObject in levelEnemyData.enemyJsonObject)
            {
                "出现敌人".Log();
                //GameObject e = R.Enemy.Generate(enemyJsonObject.name, enemyJsonObject.position, PointData.HasWall);
                //e.GetComponent<EnemyAttribute>().id = enemyJsonObject.guid;
                if (canShake)
                {
                    CameraController.I.CameraShake(0.13333334f, ShakeTypeEnum.Rect);
                }

                yield return new WaitForSeconds(0.5f);
            }

            R.SceneData.CanAIRun = true;
            useNoEnemyTime = true;
            queue.queueData = levelEnemyData.enemyQueueData;
        }
        else
        {
            $"关卡{LevelManager.SceneName}第{num}波敌人尚未设置".Warning();
        }
    }

    [SerializeField] public CheckPointData PointData;

    public List<int> fileId = new List<int>();

    public Transform LeftGate;

    public Transform RightGate;

    /// <summary>
    /// 是开放的战场
    /// </summary>
    public bool isOpenBattleZone;

    /// <summary>
    /// 是否战斗结束
    /// </summary>
    public bool isBattleOver;

    private readonly Dictionary<int, LevelEnemyData> _levelEnemyData = new Dictionary<int, LevelEnemyData>();

    /// <summary>
    /// 没有敌人的时间
    /// </summary>
    private float noEnemyTime = 0.6f;

    /// <summary>
    /// 使用无敌人时间
    /// </summary>
    private bool useNoEnemyTime;

    private int currentId;

    private Rect rect;

    private EnemyQueue queue;
}

/// <summary>
/// 检查点数据
/// </summary>
[Serializable]
public class CheckPointData
{
    public int PointId;
    public Vector2 TriggerSize = new Vector2(3f, 3f);
    public Vector2 TriggerOffset = Vector2.zero;
    [Header("是否有墙")] public bool HasWall;
    public bool canMultiTrigger;
    public float DelayTime = 0.5f;
    public Vector3 LeftWall = Vector3.zero;
    public Vector3 RightWall = Vector3.zero;
    public List<int> battleRoundId = new List<int>();
}

/// <summary>
/// 关卡敌人数据
/// </summary>
public class LevelEnemyData
{
    /// <summary>
    /// 敌人队列数据
    /// </summary>
    public EnemyQueueData enemyQueueData;

    /// <summary>
    /// 敌人Json对象
    /// </summary>
    public EnemyJsonObject[] enemyJsonObject;
}

/// <summary>
/// 敌人Json对象
/// </summary>
public struct EnemyJsonObject
{
    public EnemyJsonObject(string name, Vector2 position, string guid)
    {
        this.name = name;
        this.position.x = position.x;
        this.position.y = position.y;
        this.guid = guid;
    }

    /// <summary>
    /// 名称
    /// </summary>
    public string name;

    /// <summary>
    /// 位置
    /// </summary>
    public Vector2 position;

    /// <summary>
    /// 全局唯一标识符
    /// </summary>
    public string guid;
}