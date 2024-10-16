using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

/// <summary>
/// 女猎手AI行动
/// </summary>
public class HuntressAIAction
{
    [TaskDescription("移动到攻击1和攻击2距离之间")]
    [TaskCategory("Enemy/Huntress")]
    public class MoveToAPosition : Action
    {
        private EnemyAIAttribute ai;
        private HuntressAction action;
        private EnemyAttribute eAttr;
        private static Transform Player => R.Player.Transform;
        private float attackLenght0;
        private float attackLenght1;
        private float aimPos;

        private float randomLen => Random.Range(0f,
            Mathf.Abs(ai.attackExpectations[0].distance + ai.attackExpectations[0].range -
                      (ai.attackExpectations[1].distance + ai.attackExpectations[1].range)));

        public override void OnAwake()
        {
            ai = GetComponent<EnemyAIAttribute>();
            action = GetComponent<HuntressAction>();
            eAttr = GetComponent<EnemyAttribute>();
            attackLenght0 = Mathf.Abs(ai.attackExpectations[0].distance + ai.attackExpectations[0].range);
            attackLenght1 = Mathf.Abs(ai.attackExpectations[1].distance + ai.attackExpectations[1].range);
        }

        public override void OnStart()
        {
            //敌人需要移动的距离 = 两个点之间的距离 - 最远距离
            float num = Mathf.Abs(transform.position.x - Player.position.x) - (ai.attackExpectations[1].range + ai.attackExpectations[1].distance);
            //敌人需要移动的距离 =敌人需要移动的距离 + 两攻击点之间的距离
            float num2 = num + randomLen;
            aimPos = transform.position.x - Player.position.x <= 0f ? transform.position.x + num2 : transform.position.x - num2;
            aimPos = Mathf.Clamp(aimPos, GameArea.EnemyRange.min.x + eAttr.bounds.size.x, GameArea.EnemyRange.max.x - eAttr.bounds.size.x);
        }

        public override TaskStatus OnUpdate()
        {
            if (!action.AutoMove())
                return TaskStatus.Failure;
            int dir = InputSetting.JudgeDir(action.transform.position.x, aimPos);
            if (dir == eAttr.faceDir)
                return TaskStatus.Running;
            return action.StopMoveToIdle() ? TaskStatus.Success : TaskStatus.Failure;
        }
    }


    [TaskDescription("是否在两种攻击的攻击力距离内")]
    [TaskCategory("Enemy/Huntress")]
    public class BetweenTwoAtkEange : Action
    {
        private Transform player => R.Player.Transform;
        private EnemyAIAttribute ai;
        private EnemyAIAttribute.ExpectationParam normalAtk => ai.attackExpectations[0]; //默认攻击

        private EnemyAIAttribute.ExpectationParam remoteAtk => ai.attackExpectations[1]; //远程攻击

        public override void OnAwake()
        {
            ai = GetComponent<EnemyAIAttribute>();
        }

        public override TaskStatus OnUpdate()
        {
            float offset = Mathf.Abs(transform.position.x - player.position.x);
            if (offset >= normalAtk.distance + normalAtk.range && //第一种攻击的范围
                offset < remoteAtk.distance + remoteAtk.range) //第二种攻击的范围
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }

    [TaskDescription("女猎人跳跃")]
    [TaskCategory("Enemy/Huntress")]
    public class HuntressJump : Action
    {
        private Transform player => R.Player.Transform;

        public override void OnAwake()
        {
            action = GetComponent<HuntressAction>();
        }

        public override void OnStart()
        {
            action.FaceToPlayer();
            if (back)
                action.JumpBack();
            else
                action.Jump();
        }

        public override TaskStatus OnUpdate()
        {
            string currentState = action.stateMachine.currentState;
            if (currentState == EnemyStaEnum.Jump.ToString() || currentState == EnemyStaEnum.JumpBack.ToString())
            {
                return TaskStatus.Running;
            }

            return TaskStatus.Success;
        }

        public bool back;

        private float moveDis;

        private float aimPos;

        private HuntressAction action;
    }

    [TaskDescription("女猎人卖萌")]
    [TaskCategory("Enemy/Huntress")]
    public class HuntressMoe : Action
    {
        public override void OnAwake()
        {
            action = GetComponent<HuntressAction>();
        }

        public override void OnStart()
        {
            action.FaceToPlayer();
            if (Random.Range(0, 2) == 0)
                action.Idle2();
            else
                action.Idle3();
        }

        public override TaskStatus OnUpdate()
        {
            string currentState = action.stateMachine.currentState;
            if (currentState.Equals(EnemyStaEnum.Idle2.ToString()) || currentState.Equals(EnemyStaEnum.Idle3.ToString()))
            {
                return TaskStatus.Running;
            }

            return TaskStatus.Success;
        }

        private HuntressAction action;
    }

    [TaskDescription("在远程攻击距离内")]
    [TaskCategory("Enemy/Huntress")]
    public class WithinNormalAtkRange : Action
    {
        private Transform player => R.Player.Transform;
        private EnemyAIAttribute.ExpectationParam normalAtk => ai.attackExpectations[0];

        public override void OnAwake()
        {
            ai = GetComponent<EnemyAIAttribute>();
        }

        public override TaskStatus OnUpdate()
        {
            if (Mathf.Abs(transform.position.x - player.position.x) < normalAtk.distance + normalAtk.range)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        private EnemyAIAttribute ai;
    }

    [TaskDescription("在滚动攻击距离外")]
    [TaskCategory("Enemy/Huntress")]
    public class WithoutRollAtkRange : Action
    {
        private Transform player => R.Player.Transform;
        private EnemyAIAttribute.ExpectationParam rollAtk => ai.attackExpectations[1];

        public override void OnAwake()
        {
            ai = GetComponent<EnemyAIAttribute>();
        }

        public override TaskStatus OnUpdate()
        {
            if (Mathf.Abs(transform.position.x - player.position.x) >= rollAtk.distance + rollAtk.range)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        private EnemyAIAttribute ai;
    }

    [TaskCategory("Enemy/Huntress")]
    [TaskDescription("移动到主角对面")]
    public class MoveToPlayer : Action
    {
        private Transform player => R.Player.Transform;

        public override void OnAwake()
        {
            action = GetComponent<HuntressAction>();
            eAttr = GetComponent<EnemyAttribute>();
        }

        public override void OnStart()
        {
            moveDis = Random.Range(1f, 5f);
            if (transform.position.x - player.position.x >= 0f)
                aimPos = player.position.x - moveDis;
            else
                aimPos = player.position.x + moveDis;

            aimPos = Mathf.Clamp(aimPos, GameArea.EnemyRange.min.x + eAttr.bounds.size.x, GameArea.EnemyRange.max.x - eAttr.bounds.size.x);
            action.FaceToPlayer();
        }

        public override TaskStatus OnUpdate()
        {
            if (!action.AutoMove())
            {
                return TaskStatus.Failure;
            }

            if (InputSetting.JudgeDir(action.transform.position.x, aimPos) == eAttr.faceDir)
            {
                return TaskStatus.Running;
            }

            return action.StopMoveToIdle() ? TaskStatus.Success : TaskStatus.Failure;
        }

        private float moveDis;
        private float aimPos;
        private HuntressAction action;
        private EnemyAttribute eAttr;
    }
}