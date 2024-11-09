using UnityEngine;

/// <summary>
/// 动画移动控制器
/// </summary>
[ExecuteInEditMode]
public class AnimMoveController : MonoBehaviour
{
    private PlayerManager pm;

    public bool isLocal = true;
    [SerializeField] private Vector3 position;
    private Vector3 lastposition;
    private Animation lastAnim;
    private PlatformMovement platform;
    private bool _isPlayer;

    private void Awake()
    {
        pm = R.Player;
        _isPlayer = gameObject.CompareTag(ConfigTag.Player);
        platform ??= GetComponent<PlatformMovement>();
    }

    private void Start()
    {
        position = Vector3.zero;
        lastposition = position;
        pm.StateMachine.OnTransfer += OnStateTransfer;
    }

    private void OnStateTransfer(object sender, TransferEventArgs args)
    {
        lastposition = position = Vector3.zero;
    }

    public void AnimMoveLoopReset()
    {
        position = Vector3.zero;
        lastposition = position;
    }

    private void Update()
    {
        Vector3 vector = position - lastposition;
        if (vector.x == 0f && vector.y == 0f) return;
        vector.Scale(!isLocal ? Vector3.one : transform.localScale);
        if (!Application.isPlaying)
        {
            Vector3 vector2 = vector + transform.position;
            transform.position = vector2;
        }
        else if (_isPlayer)
        {
            platform.position = (Vector2)vector + platform.position;
        }
        else
        {
            Vector3 vector4 = vector + transform.position;
            vector4.x = Mathf.Clamp(vector4.x, GameArea.EnemyRange.xMin, GameArea.EnemyRange.xMax);
            if (vector.y < 0f)
            {
                vector4.y = Mathf.Clamp(vector4.y, LayerManager.YNum.GetGroundHeight(transform.gameObject), float.MaxValue);
            }

            transform.position = vector4;
        }

        lastposition = position;
    }

    public void Clear()
    {
        position = Vector3.zero;
    }
}