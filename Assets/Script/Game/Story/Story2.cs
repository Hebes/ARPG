using System.Collections;
using UnityEngine;

/// <summary>
/// 场景2专用
/// 故事用
/// </summary>
public class Story2 : BaseBehaviour
{
    private bool E17P1
    {
        get => RoundStorage.Get("E17P1", true);
        set => RoundStorage.Set("E17P1", value);
    }

    private bool E17T6
    {
        get => RoundStorage.Get("E17T6", true);
        set => RoundStorage.Set("E17T6", value);
    }

    private int E17Process
    {
        get => RoundStorage.Get("E17Process", 0);
        set => RoundStorage.Set("E17Process", value);
    }

    private int E17MoveIn
    {
        get => RoundStorage.Get("E17_MoveIn", 0);
        set => RoundStorage.Set("E17_MoveIn", value);
    }

    private void Start()
    {
        _gate = SceneGateManager.I.FindGate(1);
        DoorCollider = GameObject.Find("Wall3");

        _gate.openType = SceneGate.OpenType.None;
        moveRight = R.Player.Transform.position.x < transform.position.x;
        if (E17MoveIn != 1)
        {
            DoorCollider.SetActive(true);
        }
    }

    private void Update()
    {
        if (moveRight && R.Player != null)
        {
            E17P1 = false;
            moveRight = false;
            StartCoroutine(SceneStart());
        }
        else if (_isInCollision && !_isPlayingVoiceOver && E17Process < 5)
        {
            _stayTime += Time.deltaTime;
            if (_stayTime > _waitTimes[E17Process])
            {
                _stayTime = 0f;
                E17Process++;
                _isPlayingVoiceOver = true;
                R.Audio.PlayVoiceOver("e17t" + E17Process, delegate
                {
                    _isPlayingVoiceOver = false;
                    if (E17Process == 5)
                    {
                        StartCoroutine(WaitForDoor());
                    }
                });
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(CTag.Player))
        {
            _isInCollision = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(CTag.Player))
        {
            _isInCollision = true;
        }
    }

    /// <summary>
    /// 等待开门
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForDoor()
    {
        R.Audio.PlayEffect(353);
        _stayTime = Time.time;
        yield return new WaitForSeconds(5f);
        InputSetting.Stop();
        //OuterDoor.state.SetAnimation(0, "CloseToOpen", false);
        //Door.state.SetAnimation(0, "CloseToOpen", false);
        yield return new WaitForSeconds(0.1f);
        E17T6 = false;
        R.Audio.PlayVoiceOver("e17t6");
        DoorCollider.SetActive(false);
        _gate.openType = SceneGate.OpenType.Right;
        if (!InputSetting.IsWorking())
        {
            InputSetting.Resume();
        }
    }

    /// <summary>
    /// 现场开始
    /// </summary>
    /// <returns></returns>
    private IEnumerator SceneStart()
    {
        R.Player.Action.ChangeState(PlayerStaEnum.Idle);
        R.Player.Action.TurnRound(1);
        R.Player.Action.StartMove();
        InputSetting.Stop();
        yield return new WaitForSeconds(1.2f);
        R.Player.Action.StopMove();
        yield return new WaitForSeconds(0.3f);
        DoorCollider.SetActive(true);
        //OuterDoor.state.SetAnimation(0, "OpenToClose", false);
        //Door.state.SetAnimation(0, "OpenToClose", false);
        yield return new WaitForSeconds(1f);
        if (!InputSetting.IsWorking())
        {
            InputSetting.Resume();
        }

        if (E17MoveIn == 1)
        {
            E17MoveIn = 2;
        }
    }

    public GameObject DoorCollider;
    [SerializeField] private SceneGate _gate;

    /// <summary>
    /// 是否发生碰撞
    /// </summary>
    private bool _isInCollision;

    private bool _isPlayingVoiceOver;

    private int[] _waitTimes =
    {
        10,
        10,
        15,
        15,
        10
    };

    private float _stayTime;


    private bool moveRight;
}