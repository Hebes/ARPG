using UnityEngine;

/// <summary>
/// 场景大门
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class SceneGate : MonoBehaviour
{
    /// <summary>
    /// 目标
    /// </summary>
    private BoxCollider2D _trigger;

    [Header("切换场景关卡数据")] public SwitchLevelGateData data;
    [Header("检查存储数据")] [SerializeField] private string _checkIdInSaveStorage;

    [Header("允许进入的方式与方向")] [SerializeField]
    public OpenType openType = OpenType.All;

    [Header("允许出现在空中")] [SerializeField] private bool InAir;

    private void Awake()
    {
        _trigger = GetComponent<BoxCollider2D>();
        if (_trigger == null)
            ("场景中没有Collider2D " + LevelManager.SceneName + " 大门 " + name).Error();
        else
            data.TriggerSize = Vector2.Scale(_trigger.size, transform.localScale);

        data.SelfPosition = transform.position;
        data.OpenType = openType;
        data.InAir = InAir;
    }

    public void OnEnable()
    {
        R.SceneGate.GatesInCurrentScene.Add(this);
    }

    public void OnDisable()
    {
        if (SceneGateManager.ApplicationIsQuitting) return;
        R.SceneGate.GatesInCurrentScene.Remove(this);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!AllowEnterGate(collision)) return;
        Vector3 position = R.Player.Transform.position;
        float num = 0.5f;
        bool flag = openType == OpenType.All;
        bool flag2 = openType == OpenType.Left && position.x < transform.position.x + num && R.Player.Attribute.faceDir == 1;
        bool flag3 = openType == OpenType.Right && position.x > transform.position.x - num && R.Player.Attribute.faceDir == -1;
        bool flag4 = openType == OpenType.Top && position.y > transform.position.y - num;
        bool flag5 = openType == OpenType.Bottom && position.y < transform.position.y + num;
        if (flag || flag2 || flag3 || flag4 || flag5)
            R.SceneGate.Enter(data);
        if (openType == OpenType.PressKey && Input.Game.Search.OnClick)
            R.SceneGate.Enter(data);
    }

    /// <summary>
    /// 允许进入门
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private bool AllowEnterGate(Collider2D collision)
    {
        bool flag = R.SceneGate.IsLocked || //加载场景的时候被锁定
                    R.Player == null || //玩家是空的
                    !collision.CompareTag(ConfigTag.Player) || //不是玩家
                    openType == OpenType.None ||
                    R.Mode.CheckMode(Mode.AllMode.Battle) || //如果是战斗状态
                    R.Player.Action.NotAllowPassSceneGate || !InputSetting.IsWorking() ||
                    (!string.IsNullOrEmpty(_checkIdInSaveStorage) && SaveStorage.Get(_checkIdInSaveStorage, false));
        //不允许进入
        return !flag;
    }

    /// <summary>
    /// 进入
    /// </summary>
    /// <param name="needProgressBar"></param>
    /// <returns></returns>
    public Coroutine Enter(bool needProgressBar = false)
    {
        return R.SceneGate.Enter(data, needProgressBar);
    }

    /// <summary>
    /// 离开
    /// </summary>
    /// <param name="groundDis"></param>
    /// <param name="lastGateOpenType"></param>
    /// <returns></returns>
    public Coroutine Exit(float groundDis = 0f, OpenType lastGateOpenType = OpenType.None)
    {
        return R.SceneGate.Exit(data, groundDis, lastGateOpenType);
    }

    /// <summary>
    /// 打开方式
    /// </summary>
    public enum OpenType
    {
        None = 1,

        /// <summary>
        /// 所有
        /// </summary>
        All,

        /// <summary>
        /// 左边
        /// </summary>
        Left,

        /// <summary>
        /// 右边
        /// </summary>
        Right,

        /// <summary>
        /// 顶部
        /// </summary>
        Top,

        /// <summary>
        /// 底部
        /// </summary>
        Bottom,

        /// <summary>
        /// 按键
        /// </summary>
        PressKey
    }
}