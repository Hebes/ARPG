using System;
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
    }
    
    private void OnEnable()
    {
        GameEvent.OnPlayerTurnRound.Register(StopFollow);
        GameEvent.OnPlayerAnimChange.Register(StopFollow);
    }
    
    private void OnDisable()
    {
        GameEvent.OnPlayerTurnRound.UnRegister(StopFollow);
        GameEvent.OnPlayerAnimChange.UnRegister(StopFollow);
    }

    private void Update()
    {
        Animator.speed = _playerAnimationController.animSpeed;
    }

    private void StopFollow(object obj)
    {
        try
        {
            transform.parent = R.Effect.transform;
            if (isLoop)
            {
                EffectController.TerminateEffect(gameObject);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}