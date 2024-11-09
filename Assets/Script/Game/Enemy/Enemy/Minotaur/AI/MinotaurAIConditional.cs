using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MinotaurAIConditional
{
    [TaskDescription("判断敌人是否在正常状态")]
    [TaskCategory("Enemy/弥诺陶洛斯/是否在近战范围内")]
    public class IsInAtk1Range : Conditional
    {
        private MinotaurComponent _component;
        public EnemyAIAttribute.ActionEnum AtkType;

        public override void OnAwake()
        {
            _component = GetComponent<MinotaurComponent>();
        }

        public override TaskStatus OnUpdate()
        {
            EnemyAIAttribute.ExpectationParam normalAtk = _component.aIAttribute.attackExpectations[(int)AtkType];
            float offset = Mathf.Abs(transform.position.x - R.Player.Transform.position.x);
            if (offset < normalAtk.distance + normalAtk.range)
                return TaskStatus.Success;
            return TaskStatus.Failure;
        }
    }
}