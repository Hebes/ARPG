using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;
using Random = UnityEngine.Random;

/// <summary>
/// https://opsive.com/support/documentation/behavior-designer/behavior-tree-component/ 官网
/// https://blog.csdn.net/linxinfa/article/details/124483690 BehaviorDesigner插件制作AI行为树
/// https://www.jianshu.com/p/64b5fe01fb1c Behavior Designer 中文版教程
/// https://geekdaxue.co/read/dachengzi-shgxu@kczcy8/exSda0C8lYQ9TDDU BehaviorDesigner 文档翻译与整理
/// https://blog.csdn.net/qq_52324195/article/details/124827915 Unity-Behavior Designer详解
/// https://www.jianshu.com/p/5033daf85fda Unity3D行为树插件BehaviorDesigner（八-复合节点 ）
/// https://www.jianshu.com/p/998f665cb616 Unity3D行为树插件BehaviorDesigner（六-条件节点的终止）
/// 
/// task有四种不同的类型：
/// action（行为节点）定义具体行为和任务
/// conditional（条件节点）检测一些游戏的属性进行决定
/// composite（复合节点）控制行为树流程和顺序
/// decorator（装饰节点）对子节点返回值进行修改如取反或者直接控制子节点的表达
///
/// Sequence 顺序节点：从左到右。为真继续，为假则停
/// Selector 选择节点：从左到右。一真即真，一真即停。全假才假。
/// Parallel 并行节点：并行执行。全真则真，一假则假。一假即停，被停即假。
/// Parallel Selector 并行选择节点：并行执行。一真即真，一真即停，全假才假。
/// Random Selector 随机选择节点：随机执行。真即真，一真即停，全假才假。
/// Random Sequence 随机顺序节点：随机顺序。为真继续，全真才真。一假则假，一假即停。
/// </summary>
public class EnemyBaseAIAction
{
    [TaskDescription("移动到当前选择的攻击可以攻击的位置")]
    [TaskCategory("Enemy/Base基础行动")]
    public class MoveAtkExAim : Action
    {
        private Transform player => R.Player.Transform;

        public override void OnAwake()
        {
            eAttr = GetComponent<EnemyAttribute>();
            enemy = eAttr.GetComponent<EnemyBaseAction>();
            aiAttr = eAttr.GetComponent<EnemyAIAttribute>();
        }

        public override void OnStart()
        {
            if (randomAtkEx)
            {
                atkNum = RandomRange[Random.Range(0, RandomRange.Length)];
                aiAttr.currentEx = aiAttr.GetExpectationParam(atkNum);
            }
            else
            {
                aiAttr.currentEx = aiAttr.GetExpectationParam(atkNum);
            }

            aimPos = MoveAim(); //移动到的点
            aimPos = new Vector2(VU.Clamp(aimPos.x, GameArea.EnemyRange.min.x, GameArea.EnemyRange.max.x), aimPos.y);
        }

        public override TaskStatus OnUpdate()
        {
            if (!enemy.AutoMove())
            {
                return TaskStatus.Failure;
            }

            bool flag = InputSetting.JudgeDir(enemy.transform.position.x, aimPos.x) == eAttr.faceDir;
            Vector2 dis = aimPos - (Vector2)R.Player.Transform.position;
            bool flag2 = MoveStop(dis);
            if (flag && !flag2)
            {
                return TaskStatus.Running;
            }

            if (!isArrivedStopAnim)
            {
                return TaskStatus.Success;
            }

            if (enemy.StopMoveToIdle())
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        protected virtual Vector2 MoveAim()
        {
            EnemyAIAttribute.ExpectationParam currentEx = aiAttr.currentEx;
            Vector3 v = player.position - Random.Range(currentEx.distance, currentEx.range + currentEx.distance) * Vector3.right;
            Vector3 v2 = player.position + Random.Range(currentEx.distance, currentEx.range + currentEx.distance) * Vector3.right;
            if (v.x < GameArea.EnemyRange.min.x)
            {
                return v2; //超过敌人可视范围的最小的x
            }

            if (v2.x > GameArea.EnemyRange.max.x)
            {
                return v; //超过敌人可视范围的最大的x
            }

            float num = Mathf.Abs(transform.position.x - v.x);
            float num2 = Mathf.Abs(transform.position.x - v2.x);
            if (num < num2)
            {
                return v;
            }

            return v2;
        }

        protected virtual bool MoveStop(Vector2 dis)
        {
            EnemyAIAttribute.ExpectationParam currentEx = aiAttr.currentEx;
            Vector2 vector = enemy.transform.position - R.Player.Transform.position;
            bool flag = MathfX.isInMiddleRange(R.Player.Transform.position.x,
                (currentEx.distance + currentEx.range / 2f) * aiAttr.facePlayerDir + enemy.transform.position.x, currentEx.range);
            if (flag)
            {
                if (dis.x > 0f && vector.x > 0f && vector.x < dis.x)
                {
                    return true;
                }

                if (dis.x < 0f && vector.x < 0f && vector.x > dis.x)
                {
                    return true;
                }
            }

            return !enemy.IsInNormalState();
        }

        [BehaviorDesigner.Runtime.Tasks.Tooltip("到达了后是否停止播放移动动画")]
        public bool isArrivedStopAnim = true;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("是否随机选择攻击目标")]
        public bool randomAtkEx;

        public EnemyAIAttribute.ActionEnum[] RandomRange;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("选择ATK几")]
        public EnemyAIAttribute.ActionEnum atkNum;

        private EnemyAttribute eAttr;
        private EnemyBaseAction enemy;
        private EnemyAIAttribute aiAttr;
        private Vector2 aimPos;
    }

    [TaskDescription("远离主角，以攻击范围为准")]
    [TaskCategory("Enemy/Base基础行动")]
    public class FarAwayPlayer : Action
    {
        public override void OnAwake()
        {
            action = GetComponent<EnemyBaseAction>();
            eAttr = GetComponent<EnemyAttribute>();
        }

        private bool nearToPlayer
        {
            get
            {
                EnemyAIAttribute.ExpectationParam expectationParam = GetComponent<EnemyAIAttribute>().attackExpectations[(int)atkNum];
                float num = Mathf.Abs(transform.position.x - R.Player.Transform.position.x);
                return num < expectationParam.range + expectationParam.distance && num > expectationParam.distance;
            }
        }

        public override void OnStart()
        {
            EnemyAIAttribute.ExpectationParam expectationParam = GetComponent<EnemyAIAttribute>().attackExpectations[(int)atkNum];
            atkRange = Random.Range(expectationParam.distance, expectationParam.distance + expectationParam.range);
            int num = JudeMoveDir();
            aimPos = R.Player.Transform.position.x + atkRange * num;
            action.TurnRound(InputSetting.JudgeDir(transform.position.x, aimPos));
            aimPos = Mathf.Clamp(aimPos, GameArea.EnemyRange.min.x + eAttr.bounds.size.x, GameArea.EnemyRange.max.x - eAttr.bounds.size.x);
        }

        public override TaskStatus OnUpdate()
        {
            if (!action.AutoMove())
            {
                return TaskStatus.Failure;
            }

            bool flag = InputSetting.JudgeDir(action.transform.position.x, aimPos) == eAttr.faceDir;
            if (!flag)
            {
                return (!action.StopMoveToIdle()) ? TaskStatus.Failure : TaskStatus.Success;
            }

            if (nearToPlayer)
            {
                return (!action.StopMoveToIdle()) ? TaskStatus.Failure : TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        private int JudeMoveDir()
        {
            float num = atkRange;
            if (Mathf.Abs(transform.position.x - GameArea.EnemyRange.min.x) < num ||
                Mathf.Abs(transform.position.x - GameArea.EnemyRange.max.x) < num)
            {
                return (Mathf.Abs(transform.position.x - GameArea.EnemyRange.min.x) >= num) ? -1 : 1;
            }

            return (transform.position.x - R.Player.Transform.position.x <= 0f) ? -1 : 1;
        }

        private EnemyBaseAction action;

        private EnemyAttribute eAttr;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("选择ATK几")]
        public ATK atkNum;

        private float atkRange;

        private float aimPos;

        public enum ATK
        {
            ATK1,
            ATK2,
            ATK3,
            ATK4,
            ATK5,
            ATK6,
            ATK7,
            ATK8,
            ATK9,
            ATK10
        }
    }

    [TaskDescription("远离主角，以攻击范围为准,方向随机")]
    [TaskCategory("Enemy/Base基础行动")]
    public class RandomMovePlayer : Action
    {
        public override void OnAwake()
        {
            action = GetComponent<EnemyBaseAction>();
            eAttr = GetComponent<EnemyAttribute>();
        }

        private bool nearToPlayer
        {
            get
            {
                EnemyAIAttribute.ExpectationParam expectationParam = GetComponent<EnemyAIAttribute>().attackExpectations[(int)atkNum];
                float num = Mathf.Abs(transform.position.x - R.Player.Transform.position.x);
                return num < expectationParam.range + expectationParam.distance && num > expectationParam.distance;
            }
        }

        public override void OnStart()
        {
            EnemyAIAttribute.ExpectationParam expectationParam = GetComponent<EnemyAIAttribute>().attackExpectations[(int)atkNum];
            atkRange = Random.Range(expectationParam.distance, expectationParam.distance + expectationParam.range);
            int num = JudeMoveDir();
            aimPos = R.Player.Transform.position.x + atkRange * num;
            action.TurnRound(InputSetting.JudgeDir(transform.position.x, aimPos));
            aimPos = Mathf.Clamp(aimPos, GameArea.EnemyRange.min.x + eAttr.bounds.size.x, GameArea.EnemyRange.max.x - eAttr.bounds.size.x);
        }

        public override TaskStatus OnUpdate()
        {
            if (!action.AutoMove())
            {
                return TaskStatus.Failure;
            }

            bool flag = InputSetting.JudgeDir(action.transform.position.x, aimPos) == eAttr.faceDir;
            if (!flag)
            {
                return (!action.StopMoveToIdle()) ? TaskStatus.Failure : TaskStatus.Success;
            }

            if (nearToPlayer)
            {
                return (!action.StopMoveToIdle()) ? TaskStatus.Failure : TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        private int JudeMoveDir()
        {
            float num = atkRange;
            if (Mathf.Abs(transform.position.x - GameArea.EnemyRange.min.x) < num ||
                Mathf.Abs(transform.position.x - GameArea.EnemyRange.max.x) < num)
            {
                return (Mathf.Abs(transform.position.x - GameArea.EnemyRange.min.x) >= num) ? -1 : 1;
            }

            return (Random.Range(0, 2) != 0) ? -1 : 1;
        }

        private EnemyBaseAction action;

        private EnemyAttribute eAttr;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("选择ATK几")]
        public ATK atkNum;

        private float atkRange;

        private float aimPos;

        public enum ATK
        {
            ATK1,
            ATK2,
            ATK3,
            ATK4,
            ATK5,
            ATK6,
            ATK7,
            ATK8,
            ATK9,
            ATK10
        }
    }

    [TaskDescription("移动距离主角的某个位置")]
    [TaskCategory("Enemy/Base基础行动")]
    public class MoveDistance : Action
    {
        public override void OnAwake()
        {
            action = GetComponent<EnemyBaseAction>();
            eAttr = GetComponent<EnemyAttribute>();
        }

        public override void OnStart()
        {
            int dir = JudeMoveDir();
            action.TurnRound(dir);
            aimPos = R.Player.Transform.position.x + moveRange * eAttr.faceDir;
            aimPos = Mathf.Clamp(aimPos, GameArea.EnemyRange.min.x + eAttr.bounds.size.x, GameArea.EnemyRange.max.x - eAttr.bounds.size.x);
        }

        public override TaskStatus OnUpdate()
        {
            if (!action.AutoMove())
            {
                return TaskStatus.Failure;
            }

            bool flag = InputSetting.JudgeDir(action.transform.position.x, aimPos) == eAttr.faceDir;
            if (flag)
            {
                return TaskStatus.Running;
            }

            return (!action.StopMoveToIdle()) ? TaskStatus.Failure : TaskStatus.Success;
        }

        private int JudeMoveDir()
        {
            float num = moveRange;
            if (Mathf.Abs(transform.position.x - GameArea.EnemyRange.min.x) < num ||
                Mathf.Abs(transform.position.x - GameArea.EnemyRange.max.x) < num)
            {
                return (Mathf.Abs(transform.position.x - GameArea.EnemyRange.min.x) >= num) ? -1 : 1;
            }

            return (Random.Range(0, 2) != 0) ? -1 : 1;
        }

        private EnemyBaseAction action;

        private EnemyAttribute eAttr;

        public float moveRange;

        private float aimPos;
    }

    [TaskDescription("向前移动一段距离,距离参数在属性里设置")]
    [TaskCategory("Enemy/Base基础行动")]
    public class MoveSomeDistance : Action
    {
        public override void OnAwake()
        {
            eAttr = GetComponent<EnemyAttribute>();
            if (eAttr == null)
            {
                "EnemyBaseAIAction必须使用在有Enmey的对象上".Error();
            }

            enemy = GetComponent<EnemyBaseAction>();
            if (enemy == null)
            {
                "EnemyBaseAIAction必须使用在有Enmey的对象上".Error();
            }
        }

        public override void OnStart()
        {
            if (randomDis)
            {
                dis = Random.Range(randomMin, randomMax);
            }

            aimPosX = enemy.transform.position.x + eAttr.faceDir * dis;
            aimPosX = Mathf.Clamp(aimPosX, GameArea.EnemyRange.min.x + eAttr.bounds.size.x, GameArea.EnemyRange.max.x - eAttr.bounds.size.x);
        }

        public override TaskStatus OnUpdate()
        {
            if (!enemy.AutoMove())
            {
                return TaskStatus.Failure;
            }

            bool flag = InputSetting.JudgeDir(enemy.transform.position.x, aimPosX) == eAttr.faceDir;
            if (flag)
            {
                return TaskStatus.Running;
            }

            if (!isArrivedStopAnim)
            {
                return TaskStatus.Success;
            }

            if (enemy.StopMoveToIdle())
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        [BehaviorDesigner.Runtime.Tasks.Tooltip("移动距离")]
        public float dis;

        public bool randomDis;

        public float randomMin;

        public float randomMax;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("到达了后是否停止播放移动动画")]
        public bool isArrivedStopAnim = true;

        private EnemyAttribute eAttr;

        private EnemyBaseAction enemy;

        private float aimPosX;
    }

    [TaskDescription("移动距离主角的某个位置范围")]
    [TaskCategory("Enemy/Base基础行动")]
    public class MoveDistanceRange : Action
    {
        public override void OnAwake()
        {
            action = GetComponent<EnemyBaseAction>();
            eAttr = GetComponent<EnemyAttribute>();
        }

        public override void OnStart()
        {
            finalMoveLen = Random.Range(moveRangeMin, moveRangeMax);
            int num = (Random.Range(0, 2) != 0) ? -1 : 1;
            aimPos = R.Player.Transform.position.x + finalMoveLen * num;
            int dir = InputSetting.JudgeDir(transform.position.x, aimPos);
            action.TurnRound(dir);
            aimPos = Mathf.Clamp(aimPos, GameArea.EnemyRange.min.x + eAttr.bounds.size.x, GameArea.EnemyRange.max.x - eAttr.bounds.size.x);
        }

        public override TaskStatus OnUpdate()
        {
            if (!action.AutoMove())
            {
                return TaskStatus.Failure;
            }

            bool flag = InputSetting.JudgeDir(action.transform.position.x, aimPos) == eAttr.faceDir;
            if (!flag)
            {
                return action.StopMoveToIdle() ? TaskStatus.Success : TaskStatus.Failure;
            }

            float x = Mathf.Abs(R.Player.Transform.position.x - transform.position.x);
            if (MathfX.isInRange(x, moveRangeMin, moveRangeMax)) //是否再射程内
            {
                action.StopMoveToIdle();
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        private int JudeMoveDir()
        {
            float num = finalMoveLen;
            if (Mathf.Abs(transform.position.x - GameArea.EnemyRange.min.x) < num ||
                Mathf.Abs(transform.position.x - GameArea.EnemyRange.max.x) < num)
            {
                return (Mathf.Abs(transform.position.x - GameArea.EnemyRange.min.x) >= num) ? -1 : 1;
            }

            return (Random.Range(0, 2) != 0) ? -1 : 1;
        }

        private EnemyBaseAction action;

        private EnemyAttribute eAttr;

        public float moveRangeMin;

        public float moveRangeMax;

        private float aimPos;

        private float finalMoveLen;
    }

    [TaskDescription("移动到主角位置")]
    [TaskCategory("Enemy/Base基础行动")]
    public class MoveToPlayerPos : Action
    {
        public override void OnAwake()
        {
            action = GetComponent<EnemyBaseAction>();
            eAttr = GetComponent<EnemyAttribute>();
        }

        public override void OnStart()
        {
            aimPos = R.Player.Transform.position.x;
            int dir = InputSetting.JudgeDir(transform.position.x, aimPos);
            action.TurnRound(dir);
            aimPos = Mathf.Clamp(aimPos, GameArea.EnemyRange.min.x + eAttr.bounds.size.x, GameArea.EnemyRange.max.x - eAttr.bounds.size.x);
        }

        public override TaskStatus OnUpdate()
        {
            if (!action.AutoMove())
            {
                return TaskStatus.Failure;
            }

            bool flag = InputSetting.JudgeDir(action.transform.position.x, aimPos) == eAttr.faceDir;
            if (flag)
            {
                return TaskStatus.Running;
            }

            return (!action.StopMoveToIdle()) ? TaskStatus.Failure : TaskStatus.Success;
        }

        /// <summary>
        /// 敌人基础行动
        /// </summary>
        private EnemyBaseAction action;

        /// <summary>
        /// 敌人属性
        /// </summary>
        private EnemyAttribute eAttr;

        /// <summary>
        /// 目标位置
        /// </summary>
        private float aimPos;

        /// <summary>
        /// 最后一招
        /// </summary>
        private float finalMoveLen;
    }

    [TaskDescription("转向玩家")]
    [TaskCategory("Enemy/Base基础行动")]
    public class FaceToPlayer : Action
    {
        public override void OnAwake()
        {
            enemy = GetComponent<EnemyBaseAction>();
        }

        public override void OnStart()
        {
            enemy.FaceToPlayer();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

        private EnemyBaseAction enemy;
    }

    [TaskDescription("在一般状态")]
    [TaskCategory("Enemy/Base基础行动")]
    public class InNormalState : Action
    {
        private EnemyBaseAction enemy;

        public override void OnAwake()
        {
            enemy = GetComponent<EnemyBaseAction>();
        }

        public override TaskStatus OnUpdate()
        {
            return enemy.IsInNormalState() ? TaskStatus.Success : TaskStatus.Failure;
        }

    }

    [TaskDescription("AI启动")]
    [TaskCategory("Enemy/Base基础行动")]
    public class StartAI : Action
    {
        public override TaskStatus OnUpdate()
        {
            R.SceneData.CanAIRun = true;
            if (!R.Mode.CheckMode(Mode.AllMode.Story) && R.SceneData.CanAIRun)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }

    [TaskDescription("转身")]
    [TaskCategory("Enemy/Base基础行动")]
    public class TurnRound : Action
    {
        public override void OnAwake()
        {
            eAttr = GetComponent<EnemyAttribute>();
            if (eAttr == null)
            {
                "EnemyBaseAIAction必须使用在有Enmey的对象上".Error();
            }

            enemy = GetComponent<EnemyBaseAction>();
            if (enemy == null)
            {
                "EnemyBaseAIAction必须使用在有EnemyBaseAction的对象上".Error();
            }
        }

        public override TaskStatus OnUpdate()
        {
            int dir = 1;
            switch (turnDir)
            {
                case Dir.Back:
                    if (eAttr.faceDir == 1)
                    {
                        dir = -1;
                    }

                    if (eAttr.faceDir == -1)
                    {
                        dir = 1;
                    }

                    break;
                case Dir.Left:
                    dir = -1;
                    break;
                case Dir.Right:
                    dir = 1;
                    break;
                case Dir.Random:
                    dir = ((Random.Range(0, 2) != 0) ? 1 : -1);
                    break;
                case Dir.None:
                    dir = eAttr.faceDir;
                    break;
            }

            if (enemy.TurnRound(dir))
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        [BehaviorDesigner.Runtime.Tasks.Tooltip("转向方式")]
        public Dir turnDir;

        private Transform playerTransform;

        private EnemyAttribute eAttr;

        private EnemyBaseAction enemy;

        public enum Dir
        {
            Back,
            Left,
            Right,
            Random,
            None
        }
    }

    [TaskDescription("待机")]
    [TaskCategory("Enemy/Base基础行动")]
    public class Idle : Action
    {
        public override void OnAwake()
        {
            enemy = GetComponent<EnemyBaseAction>();
            if (enemy == null)
            {
                "Player没有生成".Error();
            }
        }

        public override void OnStart()
        {
            switch (type)
            {
                case IdleType.Idle1:
                    enemy.Idle1();
                    break;
                case IdleType.Idle2:
                    enemy.Idle2();
                    break;
            }
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

        private EnemyBaseAction enemy;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("待机类型")]
        public IdleType type;

        public enum IdleType
        {
            Idle1,
            Idle2,
        }
    }

    [TaskDescription("等待")]
    [TaskCategory("Enemy/Base基础行动")]
    public class Wait : Action
    {
        public override void OnStart()
        {
            startTime = Time.time;
        }

        public override TaskStatus OnUpdate()
        {
            if (Time.time - startTime > waitTime)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        [BehaviorDesigner.Runtime.Tasks.Tooltip("等待多久")]
        public float waitTime;

        private float startTime;
    }

    [TaskDescription("等待扫描时间")]
    [TaskCategory("Enemy/Base基础行动")]
    public class WaitScanSpeed : Action
    {
        public override void OnStart()
        {
            eAttr = GetComponent<EnemyAttribute>();
            startTime = Time.time;
        }

        public override TaskStatus OnUpdate()
        {
            return Time.time - startTime > eAttr.scanSpeed ? TaskStatus.Success : TaskStatus.Running;
        }

        private EnemyAttribute eAttr;

        private float startTime;
    }

    [TaskDescription("怪物攻击")]
    [TaskCategory("Enemy/Base基础行动")]
    public class EnemyAttack : Action
    {
        public override void OnAwake()
        {
            action = GetComponent<EnemyBaseAction>();
        }

        public override void OnStart()
        {
            int dir = InputSetting.JudgeDir(transform.position, R.Player.Transform.position);
            switch (atkType)
            {
                case EnemyAIAttribute.ActionEnum.Atk1:
                    action.Attack1(dir);
                    break;
                case EnemyAIAttribute.ActionEnum.Atk2:
                    action.Attack2(dir);
                    break;
                case EnemyAIAttribute.ActionEnum.Atk3:
                    action.Attack3(dir);
                    break;
                case EnemyAIAttribute.ActionEnum.AtkRemote:
                    action.AtkRemote(dir);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (action.IsInAttackState())
                return TaskStatus.Running;
            return TaskStatus.Success;
        }

        public EnemyAIAttribute.ActionEnum atkType;

        private EnemyBaseAction action;
    }
}