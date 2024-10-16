using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

/// <summary>
/// 基础AI条件 
/// </summary>
public class EnemyBaseAIConditional
{
    [TaskDescription("判断敌人是否在正常状态")]
    [TaskCategory("Enemy/Base条件")]
    public class IsInNormalState : Conditional
    {
        public override void OnAwake()
        {
            action = GetComponent<EnemyBaseAction>();
            if (action == null)
            {
                "EnemyBaseAIAction必须使用在有EnmeyBaseAction的对象上".Error();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (action.IsInNormalState())
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        private EnemyBaseAction action;
    }

    [TaskCategory("Enemy/Base条件")]
    [TaskDescription("判断玩家是否在我后面")]
    public class PlayerIsInBackGround : Conditional
    {
        public override void OnAwake()
        {
            enemy = GetComponent<EnemyAttribute>();
            if (enemy == null)
            {
                "EnemyBaseAIAction必须使用在有Enmey的对象上".Error();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (InputSetting.JudgeDir(transform.position, R.Player.Transform.position) == enemy.faceDir)
            {
                return TaskStatus.Failure;
            }

            return TaskStatus.Success;
        }

        private EnemyAttribute enemy;
    }

    [TaskCategory("Enemy/Base条件")]
    [TaskDescription("判断是否在Attack的范围内")]
    public class PlayerIsInAttackRange : Conditional
    {
        public override void OnAwake()
        {
            aiAttr = GetComponent<EnemyAIAttribute>();
            if (aiAttr == null)
            {
                "必须使用在有EnemyAIAttribute的对象上".Error();
            }

            ex = aiAttr.attackExpectations[(int)atk];   
            if (ex == null)
            {
                "不存在Attack".Error();
            }
        }

        public override TaskStatus OnUpdate()
        {
            bool flag = MathfX.isInMiddleRange(R.Player.Transform.position.x,
                (ex.distance + ex.range / 2f) * aiAttr.GetComponent<EnemyAttribute>().faceDir + aiAttr.transform.position.x, ex.range);
            if (flag)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        protected EnemyAIAttribute.ExpectationParam ex;

        protected EnemyAIAttribute aiAttr;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("选择的攻击类型")]
        public ATKType atk;

        public enum ATKType
        {
            ATK1,
            ATK2,
            ATK3,
            ATK4,
            ATK5
        }
    }

    [TaskCategory("Enemy/Base条件")]
    [TaskDescription("在两种攻击范围内")]
    public class InTwoAtkRange : Conditional
    {
        private Transform player => R.Player.Transform;

        public override void OnAwake()
        {
            aiAttr = GetComponent<EnemyAIAttribute>();
            firstEx = aiAttr.attackExpectations[(int)firstAtk];
            secondEx = aiAttr.attackExpectations[(int)secondAtk];
        }

        public override TaskStatus OnUpdate()
        {
            float num = Mathf.Min(firstEx.distance + firstEx.range, secondEx.distance + secondEx.range);
            float num2 = Mathf.Max(firstEx.distance + firstEx.range, secondEx.distance + secondEx.range);
            float num3 = Mathf.Abs(player.position.x - transform.position.x);
            if (num3 > num && num3 <= num2)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        [BehaviorDesigner.Runtime.Tasks.Tooltip("选择的第一种攻击类型")]
        public ATKType firstAtk;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("选择的第二种攻击类型")]
        public ATKType secondAtk;

        private EnemyAIAttribute aiAttr;

        private EnemyAIAttribute.ExpectationParam firstEx;

        private EnemyAIAttribute.ExpectationParam secondEx;

        public enum ATKType
        {
            ATK1,
            ATK2,
            ATK3,
            ATK4,
            ATK5
        }
    }

    [TaskCategory("Enemy/Base条件")]
    [TaskDescription("距离判断")]
    public class DistanceJudge : Conditional
    {
        private Transform player
        {
            get { return R.Player.Transform; }
        }

        public override TaskStatus OnUpdate()
        {
            float num = Mathf.Abs(player.position.x - transform.position.x);
            JudgeState judgeState = judge;
            if (judgeState != JudgeState.Between)
            {
                if (judgeState != JudgeState.Greater)
                {
                    if (judgeState != JudgeState.Less)
                    {
                        return TaskStatus.Failure;
                    }

                    if (num < distanceMax)
                    {
                        return TaskStatus.Success;
                    }

                    return TaskStatus.Failure;
                }

                if (num > distanceMax)
                {
                    return TaskStatus.Success;
                }

                return TaskStatus.Failure;
            }

            if (num >= distanceMin && num <= distanceMax)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        public JudgeState judge;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("第一个距离")]
        public float distanceMax;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("仅判断两个距离之间时使用,表示比较近的距离")]
        public float distanceMin;

        public enum JudgeState
        {
            Greater,
            Between,
            Less
        }
    }

    [TaskDescription("判断是否在玩家视野的范围内")]
    [TaskCategory("Enemy/Base条件")]
    public class IsInPlayerView : Conditional
    {
        private Transform player => R.Player.Transform;

        public override void OnAwake()
        {
            eAttr = GetComponent<EnemyAttribute>();
        }

        public override TaskStatus OnUpdate()
        {
            if (eAttr.playerInView)
                return TaskStatus.Success;

            //距离玩家最远的距离
            float x = transform.localScale.x <= 0f ? transform.position.x - eAttr.viewXBack : transform.position.x - eAttr.viewXFront;
            Rect rect = new Rect(x, transform.position.y - eAttr.viewYDown, eAttr.viewXFront + eAttr.viewXBack, eAttr.viewYTop + eAttr.viewYDown);
            bool flag = rect.Contains(player.position + Vector3.up); //是否在敌人的视距内
            bool flag2 = eAttr.currentHp < eAttr.maxHp;
            if (flag)
            {
                MoveAreaLimitArgs moveAreaLimitArgs = new MoveAreaLimitArgs { battleZoneId = GetComponent<MoveAreaLimit>().battleZoneId };
                GameEvent.EnemyFindPlayer.Trigger(moveAreaLimitArgs);
            }

            if (flag || flag2)
            {
                eAttr.playerInView = true;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        private EnemyAttribute eAttr;
    }

    [TaskDescription("判断AI开启")]
    [TaskCategory("Enemy/Base条件")]
    public class IsAIOpen : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            return GetComponent<EnemyAttribute>().isDead ||
                   !R.SceneData.CanAIRun ||
                   R.Mode.CheckMode(Mode.AllMode.Story)
                ? TaskStatus.Failure
                : TaskStatus.Success;
        }
    }

    [TaskDescription("判断是否面向右边")]
    [TaskCategory("Enemy/Base条件")]
    public class IsTurnRight : Conditional
    {
        public override void OnAwake()
        {
            eAttr = GetComponent<EnemyAttribute>();
        }

        public override TaskStatus OnUpdate()
        {
            return (eAttr.faceDir != 1) ? TaskStatus.Failure : TaskStatus.Success;
        }

        protected EnemyAttribute eAttr;
    }

    [TaskDescription("能否反击")]
    [TaskCategory("Enemy/Base条件")]
    public class Counterattack : Conditional
    {
        public override void OnAwake()
        {
            hurt = GetComponent<EnemyBaseHurt>();
            eAttr = GetComponent<EnemyAttribute>();
        }

        public override TaskStatus OnUpdate()
        {
            if (hurt.canCounterattack)
            {
                hurt.canCounterattack = false;
                eAttr.paBody = true;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        private EnemyBaseHurt hurt;

        private EnemyAttribute eAttr;
    }

    [TaskDescription("判断主角在攻击状态")]
    [TaskCategory("Enemy/Base条件")]
    public class PlayerInAttack : Conditional
    {
        private StateMachine state
        {
            get { return R.Player.StateMachine; }
        }

        public override TaskStatus OnUpdate()
        {
            if (state.currentState.IsInArray(PlayerAction.AirAttackSta) || state.currentState.IsInArray(PlayerAction.AttackSta) ||
                state.currentState.IsInArray(PlayerAction.UpRisingSta) || state.currentState.IsInArray(PlayerAction.HitGroundSta) ||
                state.currentState.IsInArray(PlayerAction.FlashAttackSta) || state.currentState.IsInArray(PlayerAction.ChargeSta))
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }

    [TaskDescription("判断主角在地面")]
    [TaskCategory("Enemy/Base条件")]
    public class playerOnGround : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            if (R.Player.Attribute.isOnGround)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }

    [TaskDescription("判断游戏难度，困难为成功")]
    [TaskCategory("Enemy/Base条件")]
    public class GameDifficulty : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            int difficulty = R.GameData.Difficulty;
            if (difficulty != 1 && difficulty != 3)
            {
                return TaskStatus.Failure;
            }

            return TaskStatus.Success;
        }
    }
}