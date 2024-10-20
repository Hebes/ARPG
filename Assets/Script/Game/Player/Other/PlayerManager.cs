using UnityEngine;

/// <summary>
/// 玩家管理器
/// </summary>
public class PlayerManager
{
    private GameObject _gameObject;
    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;

    private PlayerAction _action;
    private PlayerTimeController _timeController;
    private StateMachine _stateMachine;
    private SpriteAnimation _playerAnimator;
    private PlayerAbilities _abilities;
    private PlatformMovement _platformMovement;
    private PlayerAnimationController _playerAnimationController;
    private Claymore _claymore;
    private PlayerAnimEvent _animEvent;

    public Enhancement Enhancement => R.GameData.Enhancement;
    
    public readonly PlayerAttribute Attribute = new PlayerAttribute(); // 玩家属性
    public GameObject GameObject => _gameObject ??= GameObject.FindGameObjectsWithTag(CTag.Player)[0];
    
    public Rigidbody2D Rigidbody2D => _rigidbody2D ??= GetComponent<Rigidbody2D>();
    public BoxCollider2D BoxCollider2D => _boxCollider2D ??= GetComponent<BoxCollider2D>();
    public Transform Transform => _transform ??= _gameObject.transform;
    public PlayerAction Action => _action ??= GetComponent<PlayerAction>();
    public StateMachine StateMachine => _stateMachine ??= GetComponent<StateMachine>();
    public PlayerAbilities Abilities => _abilities ??= GetComponent<PlayerAbilities>();
    public PlayerTimeController TimeController => _timeController ??= GetComponent<PlayerTimeController>();
    public Claymore Claymore => _claymore ??= GetComponent<Claymore>();
    public PlayerAnimEvent AnimEvent => _animEvent ??= GetComponentInChildren<PlayerAnimEvent>();
    public PlatformMovement PlatformMovement => _platformMovement ??= GetComponent<PlatformMovement>();
    public PlayerAnimationController PAnimationController => _playerAnimationController ??= GetComponent<PlayerAnimationController>();


    public T GetComponent<T>() => GameObject.GetComponent<T>();
    public T GetComponentInChildren<T>() => GameObject.GetComponentInChildren<T>();
    
    
    /// <summary>
    /// 在现场门附近吗?
    /// </summary>
    /// <returns></returns>
    public bool IsNearSceneGate()
    {
        for (int i = 0; i < R.SceneGate.GatesInCurrentScene.Count; i++)
        {
            SceneGate sceneGate = R.SceneGate.GatesInCurrentScene[i];
            if (MathfX.isInMiddleRange(this.Transform.position.x, sceneGate.transform.position.x, sceneGate.data.TriggerSize.x))
            {
                return true;
            }
        }
        return false;
    }
}