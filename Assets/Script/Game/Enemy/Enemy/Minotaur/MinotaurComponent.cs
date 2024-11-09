using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 用于解决每个脚本都有组件获取的问题
/// </summary>
public interface IComponents
{
    public void InitAwake(UnityEngine.Object o);
}

/// <summary>
/// 弥诺陶洛斯BOSS组件
/// </summary>
public class MinotaurComponent : MonoBehaviour
{
    private readonly List<IComponents> _componentsList = new List<IComponents>();

    public TimeController timeController;
    public EnemyAtk enemyAtk;
    public Transform atkBox;
    public EnemyAttribute attribute;
    public EnemyAnimationController enemyAnim;
    public StateMachine stateMachine;
    public OnionCreator onionCreator;
    public MoveAreaLimit moveAreaLimit;
    public EnemyAIAttribute aIAttribute;

    private void Awake()
    {
        Transform tr = transform;

        enemyAtk = tr.FindComponent<EnemyAtk>();
        attribute = tr.FindComponent<EnemyAttribute>();
        enemyAnim = tr.FindComponent<EnemyAnimationController>();
        stateMachine = tr.FindComponent<StateMachine>();
        onionCreator = tr.FindComponent<OnionCreator>();
        moveAreaLimit = tr.FindComponent<MoveAreaLimit>();
        aIAttribute = tr.FindComponent<EnemyAIAttribute>();
        timeController = tr.FindComponent<TimeController>();
        atkBox = tr.FindChildByName("AtkBox");

        _componentsList.Add(tr.FindComponent<MinotaurAction>());
        _componentsList.Add(tr.FindComponent<MinotaurHurt>());
        foreach (var value in _componentsList)
            value.InitAwake(this);
    }
}