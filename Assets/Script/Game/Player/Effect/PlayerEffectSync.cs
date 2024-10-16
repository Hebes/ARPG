using UnityEngine;

/// <summary>
/// 玩家效果同步 -》攻击的效果
/// </summary>
public class PlayerEffectSync : MonoBehaviour
{
    private Animator Animator;
    private PlayerAnimationController _playerAnimationController;
    public bool isLoop;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        _playerAnimationController = R.Player.GetComponent<PlayerAnimationController>();
        GameEvent.OnPlayerTurnRound.Register(StopFollow);
        GameEvent.OnPlayerAnimChange.Register(StopFollow);
    }

    private void Update()
    {
        Animator.speed = _playerAnimationController.animSpeed;
    }

    private void StopFollow(object obj)
    {
        transform.parent = R.Effect.transform;
        if (isLoop)
        {
            EffectController.TerminateEffect(gameObject);
        }
    }
}