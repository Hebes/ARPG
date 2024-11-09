using System;
using BehaviorDesigner.Runtime.Tasks;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

public class MinotaurAIAction
{
    [TaskDescription("待机")]
    [TaskCategory("Enemy/弥诺陶洛斯/待机")]
    public class Idle : Action
    {
        private EnemyBaseAction _enemy;

        public override void OnAwake()
        {
             _enemy = GetComponent<EnemyBaseAction>();
        }

        public override void OnStart()
        {
            _enemy.Idle1();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }

    [TaskDescription("近战攻击")]
    [TaskCategory("Enemy/弥诺陶洛斯/近战攻击")]
    public class Attack : Action
    {
        private EnemyBaseAction _action;
        public EnemyAIAttribute.ActionEnum AtkType;

        public override void OnAwake()
        {
            _action = GetComponent<EnemyBaseAction>();
        }

        public override void OnStart()
        {
            int dir = InputSetting.JudgeDir(transform.position, R.Player.Transform.position);
            switch (AtkType)
            {
                case EnemyAIAttribute.ActionEnum.Atk1:
                    _action.Attack1(dir);
                    break;
                case EnemyAIAttribute.ActionEnum.Atk2:
                    _action.Attack2(dir);
                    break;
                case EnemyAIAttribute.ActionEnum.Atk3:
                    _action.Attack2(dir);
                    break;
                case EnemyAIAttribute.ActionEnum.AtkRemote:
                    _action.AtkRemote(dir);
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (_action.IsInAttackState())
                return TaskStatus.Running;
            return TaskStatus.Success;
        }
    }

    [TaskDescription("朝向玩家")]
    [TaskCategory("Enemy/弥诺陶洛斯/朝向玩家")]
    public class TowardsPlayer : Action
    {
        private EnemyBaseAction _action;

        public override void OnAwake()
        {
            _action = GetComponent<EnemyBaseAction>();
        }

        public override void OnStart()
        {
            int dir = InputSetting.JudgeDir(transform.position, R.Player.Transform.position);
            _action.ChangeFace(dir);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}