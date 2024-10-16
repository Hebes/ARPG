using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 敌人属性
/// </summary>
public class EnemyAttribute : MonoBehaviour
{
    #region 组件

    [HideInInspector] public TimeController timeController;
    [NonSerialized] private EnemyBaseAction _action;

    private void GetComponent()
    {
        timeController = GetComponent<TimeController>();
    }

    #endregion

    /// <summary>
    /// 试图X前面
    /// </summary>
    public float viewXFront
    {
        get => _viewXFront;
        set => _viewXFront = Mathf.Clamp(value, 0f, float.MaxValue);
    }

    /// <summary>
    /// 试图X后面
    /// </summary>
    public float viewXBack
    {
        get => _viewXBack;
        set => _viewXBack = Mathf.Clamp(value, 0f, float.MaxValue);
    }

    /// <summary>
    /// 试图X上面
    /// </summary>
    public float viewYTop
    {
        get => _viewYTop;
        set => _viewYTop = Mathf.Clamp(value, 0f, float.MaxValue);
    }

    /// <summary>
    /// 试图X下面
    /// </summary>
    public float viewYDown
    {
        get => _viewYDown;
        set => _viewYDown = Mathf.Clamp(value, 0f, float.MaxValue);
    }

    /// <summary>
    /// 当前飞的高度
    /// </summary>
    public float currentFlyHeight => MathfX.PosToHeight(transform.position);

    /// <summary>
    /// 当前生命值
    /// </summary>
    public int currentHp
    {
        get => _currentHp;
        set => _currentHp = Mathf.Clamp(value, 0, maxHp);
    }

    /// <summary>
    /// 当前SP
    /// </summary>
    public int currentSp
    {
        get => _currentSp;
        set => _currentSp = Mathf.Clamp(value, 0, maxSP);
    }

    /// <summary>
    /// 是否可以反击
    /// </summary>
    public bool canCounterAttack => counterAttack > 0;

    /// <summary>
    /// 有动作中断
    /// </summary>
    public bool hasActionInterrupt => actionInterruptPoint > 0;

    /// <summary>
    /// 怪物防御
    /// </summary>
    public int monsterDefence => (int)(dynamicDefence * hardLevel);

    /// <summary>
    /// 怪物回避
    /// </summary>
    public int monsterSideStep => (int)(dynamicSideStep * hardLevel);

    /// <summary>
    /// 是否死亡
    /// </summary>
    public bool isDead => currentHp <= 0;

    /// <summary>
    /// 是否破甲了
    /// </summary>
    [HideInInspector]
    public bool isArmorBroken => currentSp <= 0;

    /// <summary>
    /// 是否在地面
    /// </summary>
    public bool isOnGround
    {
        get
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, altitude,
                LayerManager.ObstacleMask | LayerManager.GroundMask | LayerManager.OneWayGroundMask);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.right * bounds.size.x / 2f,
                -Vector2.up, altitude,
                LayerManager.ObstacleMask | LayerManager.GroundMask | LayerManager.OneWayGroundMask);
            RaycastHit2D hit3 = Physics2D.Raycast(transform.position - Vector3.right * bounds.size.x / 2f,
                -Vector2.up, altitude,
                LayerManager.ObstacleMask | LayerManager.GroundMask | LayerManager.OneWayGroundMask);

            //Debug.DrawLine(transform.position, hit.point, Color.red); // 在场景中绘制射线
            //Debug.DrawLine(transform.position + Vector3.right * bounds.size.x / 2f, hit2.point, Color.red); // 在场景中绘制射线
            //Debug.DrawLine(transform.position - Vector3.right * bounds.size.x / 2f, hit2.point, Color.red); // 在场景中绘制射线
            return hit || hit2 || hit3;
        }
    }

    /// <summary>
    /// 高度
    /// </summary>
    public float height => Physics2D.Raycast(transform.position, Vector2.up * -1f, 100f,
        LayerManager.ObstacleMask | LayerManager.GroundMask | LayerManager.OneWayGroundMask).distance;

    /// <summary>
    /// 当前可执行
    /// </summary>
    public bool CurrentCanBeExecute
    {
        get
        {
            if (_action == null)
                _action = GetComponent<EnemyBaseAction>();
            return _action.CurrentCanBeExecute();
        }
    }

    protected void Awake()
    {
        GetComponent();
        currentSp = maxSP;
        currentHp = maxHp;
    }

    protected virtual void Start()
    {
        if (enemyGravity != null)
        {
            GameObject gameObject = Instantiate(enemyGravity);
            gameObject.transform.parent = transform;
        }

        dynamicDefence = (int)(baseDefence * Random.Range(0.7f, 1.3f));
        dynamicSideStep = (int)(baseSideStep * Random.Range(0.7f, 1.3f));
        bounds = GetComponentInChildren<Collider2D>().bounds;
        if (rankType == RankType.BOSS)
        {
            SetAsBoss();
        }
    }

    private void OnDrawGizmos()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, altitude,
            LayerManager.ObstacleMask | LayerManager.GroundMask | LayerManager.OneWayGroundMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.right * bounds.size.x / 2f,
            -Vector2.up, altitude,
            LayerManager.ObstacleMask | LayerManager.GroundMask | LayerManager.OneWayGroundMask);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position - Vector3.right * bounds.size.x / 2f,
            -Vector2.up, altitude,
            LayerManager.ObstacleMask | LayerManager.GroundMask | LayerManager.OneWayGroundMask);

        Debug.DrawLine(transform.position, hit.point, Color.red); // 在场景中绘制射线
        Debug.DrawLine(transform.position + Vector3.right * bounds.size.x / 2f, hit.point, Color.red); // 在场景中绘制射线
        Debug.DrawLine(transform.position - Vector3.right * bounds.size.x / 2f, hit.point, Color.red); // 在场景中绘制射线
    }

    protected virtual void Update()
    {
    }

    protected void OnEnable()
    {
        if (InTheWorld)
        {
            R.Enemy.AddEnemy(this);
        }
    }

    protected void OnDisable()
    {
        if (InTheWorld)
        {
            R.Enemy.RemoveEnemy(this);
        }
    }

    /// <summary>
    /// 基本数据设置
    /// </summary>
    /// <param name="baseData"></param>
    public void SetBasicData(EnemyAttrData baseData)
    {
        float num = R.GameData.Difficulty switch
        {
            0 => 0.7f,
            1 => 1f,
            2 or 3 => 1.5f,
            _ => 1f
        };

        float num2 = (R.GameData.Difficulty != 0) ? 1.2f : 1f;
        float num3 = (R.GameData.Difficulty != 1) ? 1.5f : 1f;
        this.baseData = baseData;
        maxHp = (int)(baseData.maxHp * num2);
        atk = (int)(baseData.atk * num);
        counterAttack = (int)(baseData.counterAttack * num3);
        level = baseData.level;
        maxSP = baseData.maxSP;
        atkSpeed = baseData.atkSpeed;
        scanSpeed = baseData.scanSpeed;
        moveSpeed = baseData.moveSpeed;
        flyHeight = MathfX.HeightToPos(baseData.flyHeight);
        counterAttackProbPercentage = baseData.counterAttackProbPercentage;
        actionInterruptPoint = baseData.actionInterruptPoint;
        currentHp = maxHp;
        currentSp = baseData.maxSP;
        currentActionInterruptPoint = 0;
        currentCounterAttackValue = 0;
        currentCounterAttackProbPercentage = baseData.counterAttackProbPercentage;
        dropCoins = baseData.dropCoins;
        dropExp = baseData.dropExp;
    }

    /// <summary>
    /// 设置BOSS
    /// </summary>
    public void SetAsBoss()
    {
        R.Enemy.Boss = gameObject;
        "显示BOSS血条".Log();
        //R.Ui.bossHpBar.Create(GetComponent<EnemyAttribute>(), GetComponent<EnemyBaseHurt>().phaseHp);
    }

    /// <summary>
    /// 敌人类型
    /// </summary>
    public RankType rankType;

    public Bounds bounds;

    [Header("看到玩家,X点前面最远的距离")][SerializeField] private float _viewXFront;
    [Header("看到玩家,X点后面最远的距离")][SerializeField] private float _viewXBack;
    [SerializeField] private float _viewYTop;
    [SerializeField] private float _viewYDown;
    [Header("敌人的朝向")] public int faceDir ;
    [Header("硬直时间")] public float stiffTime;
    [Header("是否被玩家看到")] public bool playerInView;
    [Header("敌人关卡基本属性值，编辑器修改无效")] public EnemyAttrData baseData;
    public string id;

    [Header("等级值")] public int level = 1;
    [Header("最大护盾值")] public int maxSP;
    [Header("最大血量")] public int maxHp = 1000;
    [Header("攻击力")] public int atk;
    [Header("攻击动画速率")] public float atkSpeed = 1f;
    [Header("扫描速度 ")] public float scanSpeed = 2f;
    [Header("移动速度")] public float moveSpeed = 4f;
    [Header("掉落金币数量")] public int dropCoins;
    [Header("经验值")] public int dropExp;
    [Header("飞行高度，指的是Y值")] public float flyHeight;
    [Header("表示怪物能不能飞")] public bool iCanFly;
    [Header("当前生命值")] [SerializeField] private int _currentHp;
    [Header("当前能量值")] [SerializeField] private int _currentSp;
    [Header("反击值")] public int counterAttack;
    [Header("当前反击值")] public int currentCounterAttackValue;
    [Header("当前反击几率")] public float currentCounterAttackProbPercentage;
    [Header("反击触发概率值")] [Range(0f, 100f)] public int counterAttackProbPercentage;
    [Header("硬直值")] public int actionInterruptPoint;
    [Header("当前的硬直值")] public int currentActionInterruptPoint;
    [Header("基础防御值")] public int baseDefence;
    [Header("基础闪避值")] public int baseSideStep;
    [Header("动态防御")] [HideInInspector] public int dynamicDefence;
    [Header("动态闪避")] [HideInInspector] public int dynamicSideStep;
    [Header("难度系数")] public float hardLevel;
    [Header("防御累计伤害")] public int currentDefence;
    [Header("开始防御")] public bool startDefence;
    [Header("闪避累计伤害")] public int currentSideStep;
    [Header("空中闪避")] public bool sideStepInAir;
    [Header("是否在霸体")] public bool paBody;
    [Header("是否在虚弱状态中")] public bool inWeakState;
    [Header("是否加入世界数据")] public bool InTheWorld = true;

    /// <summary>
    /// 接受执行
    /// </summary>
    public bool accpectExecute;

    /// <summary>
    /// 接受空中执行
    /// </summary>
    public bool accpectAirExecute;

    /// <summary>
    /// 是否进入弱模式
    /// </summary>
    public bool enterWeakMod;

    /// <summary>
    /// 将被执行
    /// </summary>
    public bool willBeExecute;

    /// <summary>
    /// 可以被追赶
    /// </summary>
    public bool canBeChased;

    /// <summary>
    /// 是否正在飞起来
    /// </summary>
    [HideInInspector] public bool isFlyingUp;

    /// <summary>
    /// 是否是左边的处理
    /// </summary>
    public bool followLeftHand;

    /// <summary>
    /// 检查击中地面
    /// </summary>
    [HideInInspector] public bool checkHitGround;

    /// <summary>
    /// 飞向坠落
    /// </summary>
    [HideInInspector] public bool flyToFall;

    /// <summary>
    /// 海拔高度
    /// </summary>
    public float altitude = 0.2f;


    /// <summary>
    /// 敌人的重力
    /// </summary>
    public GameObject enemyGravity;

    /// <summary>
    /// 等级类型
    /// </summary>
    public enum RankType
    {
        /// <summary>
        /// 默认
        /// </summary>
        Normal,

        /// <summary>
        /// 精英
        /// </summary>
        Elite,

        /// <summary>
        /// Boss
        /// </summary>
        BOSS
    }
}