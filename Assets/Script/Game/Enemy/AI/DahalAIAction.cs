// using System;
// using BehaviorDesigner.Runtime.Tasks;
// using ExtensionMethods;
// using UnityEngine;
//
// public class DahalAIAction
// {
// 	[TaskDescription("Dahal攻击")]
// 	[TaskCategory("Enemy/Dahal")]
// 	public class DahalAttack : BehaviorDesigner.Runtime.Tasks.Action
// 	{
// 		private Transform player
// 		{
// 			get
// 			{
// 				return R.Player.Transform;
// 			}
// 		}
//
// 		public override void OnAwake()
// 		{
// 			this.action = base.GetComponent<DahalAction>();
// 		}
//
// 		public override void OnStart()
// 		{
// 			int dir = InputSetting.JudgeDir(this.transform.position, this.player.position);
// 			switch (this.atkType)
// 			{
// 			case DahalAIAction.DahalAttack.AtkType.Atk1:
// 				this.action.Attack1(dir);
// 				break;
// 			case DahalAIAction.DahalAttack.AtkType.Atk2:
// 				this.action.Attack2(dir);
// 				break;
// 			case DahalAIAction.DahalAttack.AtkType.Atk3:
// 				this.action.Attack3(dir);
// 				break;
// 			case DahalAIAction.DahalAttack.AtkType.Atk4:
// 				this.action.Attack4(dir);
// 				break;
// 			case DahalAIAction.DahalAttack.AtkType.Atk5:
// 				this.action.Attack5(dir);
// 				break;
// 			case DahalAIAction.DahalAttack.AtkType.Atk6:
// 				this.action.Attack6(dir);
// 				break;
// 			case DahalAIAction.DahalAttack.AtkType.Atk7:
// 				this.action.Attack7(dir);
// 				break;
// 			}
// 		}
//
// 		public override TaskStatus OnUpdate()
// 		{
// 			return TaskStatus.Success;
// 		}
//
// 		public DahalAIAction.DahalAttack.AtkType atkType;
//
// 		private DahalAction action;
//
// 		public enum AtkType
// 		{
// 			Atk1,
// 			Atk2,
// 			Atk3,
// 			Atk4,
// 			Atk5,
// 			Atk6,
// 			Atk7
// 		}
// 	}
//
// 	[TaskCategory("Enemy/Dahal")]
// 	[TaskDescription("Dahal攻击方向为面朝方向,二阶段使用")]
// 	public class DahalAttackFaceDir : BehaviorDesigner.Runtime.Tasks.Action
// 	{
// 		public override void OnAwake()
// 		{
// 			this._action = base.GetComponent<DahalAction>();
// 			this._eAttr = base.GetComponent<EnemyAttribute>();
// 		}
//
// 		public override void OnStart()
// 		{
// 			this._action.ChangeFace(this._eAttr.faceDir * -1);
// 			switch (this.atkType)
// 			{
// 			case DahalAIAction.DahalAttackFaceDir.AtkType.Atk1:
// 				this._action.Attack1(this._eAttr.faceDir);
// 				break;
// 			case DahalAIAction.DahalAttackFaceDir.AtkType.Atk2:
// 				this._action.Attack2(this._eAttr.faceDir);
// 				break;
// 			case DahalAIAction.DahalAttackFaceDir.AtkType.Atk3:
// 				this._action.Attack3(this._eAttr.faceDir);
// 				break;
// 			case DahalAIAction.DahalAttackFaceDir.AtkType.Atk4:
// 				this._action.Attack4(this._eAttr.faceDir);
// 				break;
// 			case DahalAIAction.DahalAttackFaceDir.AtkType.Atk5:
// 				this._action.Attack5(this._eAttr.faceDir);
// 				break;
// 			case DahalAIAction.DahalAttackFaceDir.AtkType.Atk6:
// 				this._action.Attack6(this._eAttr.faceDir);
// 				break;
// 			case DahalAIAction.DahalAttackFaceDir.AtkType.Atk7:
// 				this._action.Attack7(this._eAttr.faceDir);
// 				break;
// 			}
// 		}
//
// 		public override TaskStatus OnUpdate()
// 		{
// 			return TaskStatus.Success;
// 		}
//
// 		public DahalAIAction.DahalAttackFaceDir.AtkType atkType;
//
// 		private DahalAction _action;
//
// 		private EnemyAttribute _eAttr;
//
// 		public enum AtkType
// 		{
// 			Atk1,
// 			Atk2,
// 			Atk3,
// 			Atk4,
// 			Atk5,
// 			Atk6,
// 			Atk7
// 		}
// 	}
//
// 	[TaskCategory("Enemy/Dahal")]
// 	[TaskDescription("Dahal2阶段翻滚")]
// 	public class WaitBackToNormal : BehaviorDesigner.Runtime.Tasks.Action
// 	{
// 		public override void OnAwake()
// 		{
// 			this._action = base.GetComponent<DahalAction>();
// 		}
//
// 		public override TaskStatus OnUpdate()
// 		{
// 			if (this._action.stateMachine.currentState.IsInArray(DahalAction.HurtSta))
// 			{
// 				return TaskStatus.Failure;
// 			}
// 			return (!this._action.stateMachine.currentState.IsInArray(DahalAction.NormalSta)) ? TaskStatus.Running : TaskStatus.Success;
// 		}
//
// 		private DahalAction _action;
// 	}
//
// 	[TaskCategory("Enemy/Dahal")]
// 	[TaskDescription("Dahal闪避")]
// 	public class DahalSideStep : BehaviorDesigner.Runtime.Tasks.Action
// 	{
// 		public override void OnAwake()
// 		{
// 			this._action = base.GetComponent<DahalAction>();
// 		}
//
// 		public override void OnStart()
// 		{
// 			this._action.SideStep();
// 		}
//
// 		public override TaskStatus OnUpdate()
// 		{
// 			return (!(this._action.stateMachine.currentState == "GroundDodge")) ? TaskStatus.Success : TaskStatus.Running;
// 		}
//
// 		private DahalAction _action;
// 	}
//
// 	[TaskDescription("向前移动一段距离,距离参数在属性里设置")]
// 	[TaskCategory("Enemy/Dahal")]
// 	public class DahalMoveSomeDistance : BehaviorDesigner.Runtime.Tasks.Action
// 	{
// 		public override void OnAwake()
// 		{
// 			this.eAttr = base.GetComponent<EnemyAttribute>();
// 			this.enemy = base.GetComponent<DahalAction>();
// 		}
//
// 		public override void OnStart()
// 		{
// 			if (this.randomDis)
// 			{
// 				this.dis = UnityEngine.Random.Range(this.randomMin, this.randomMax);
// 			}
// 			this._aimPosX = this.enemy.transform.position.x + (float)this.eAttr.faceDir * this.dis;
// 			this._aimPosX = Mathf.Clamp(this._aimPosX, GameArea.EnemyRange.min.x + this.eAttr.bounds.size.x, GameArea.EnemyRange.max.x - this.eAttr.bounds.size.x);
// 		}
//
// 		public override TaskStatus OnUpdate()
// 		{
// 			if (!this.enemy.DahyalAutoMove(this.moveType))
// 			{
// 				return TaskStatus.Failure;
// 			}
// 			bool flag = InputSetting.JudgeDir(this.enemy.transform.position.x, this._aimPosX) == this.eAttr.faceDir;
// 			if (flag)
// 			{
// 				return TaskStatus.Running;
// 			}
// 			if (this.isArrivedStopAnim)
// 			{
// 				return (!this.enemy.StopMoveToIdle()) ? TaskStatus.Failure : TaskStatus.Success;
// 			}
// 			return TaskStatus.Success;
// 		}
//
// 		[BehaviorDesigner.Runtime.Tasks.Tooltip("移动距离")]
// 		public float dis;
//
// 		public bool randomDis;
//
// 		public float randomMin;
//
// 		public float randomMax;
//
// 		[BehaviorDesigner.Runtime.Tasks.Tooltip("到达了后是否停止播放移动动画")]
// 		public bool isArrivedStopAnim = true;
//
// 		[BehaviorDesigner.Runtime.Tasks.Tooltip("1普通移动 2快速移动")]
// 		public int moveType;
//
// 		private EnemyAttribute eAttr;
//
// 		private DahalAction enemy;
//
// 		private float _aimPosX;
// 	}
//
// 	[TaskDescription("移动距离主角的某个位置范围")]
// 	[TaskCategory("Enemy/Dahal")]
// 	public class DahalMoveDistanceRange : BehaviorDesigner.Runtime.Tasks.Action
// 	{
// 		private Transform player
// 		{
// 			get
// 			{
// 				return R.Player.Transform;
// 			}
// 		}
//
// 		public override void OnAwake()
// 		{
// 			this.action = base.GetComponent<DahalAction>();
// 			this.eAttr = base.GetComponent<EnemyAttribute>();
// 		}
//
// 		public override void OnStart()
// 		{
// 			this.finalMoveLen = UnityEngine.Random.Range(this.moveRangeMin, this.moveRangeMax);
// 			int num = (UnityEngine.Random.Range(0, 2) != 0) ? -1 : 1;
// 			this.aimPos = this.player.transform.position.x + this.finalMoveLen * (float)num;
// 			int dir = InputSetting.JudgeDir(this.transform.position.x, this.aimPos);
// 			this.action.TurnRound(dir);
// 			this.aimPos = Mathf.Clamp(this.aimPos, GameArea.EnemyRange.min.x + this.eAttr.bounds.size.x, GameArea.EnemyRange.max.x - this.eAttr.bounds.size.x);
// 		}
//
// 		public override TaskStatus OnUpdate()
// 		{
// 			if (!this.action.DahyalAutoMove(this.moveType))
// 			{
// 				return TaskStatus.Failure;
// 			}
// 			if (InputSetting.JudgeDir(this.action.transform.position.x, this.aimPos) != this.eAttr.faceDir)
// 			{
// 				return (!this.action.StopMoveToIdle()) ? TaskStatus.Failure : TaskStatus.Success;
// 			}
// 			float x = Mathf.Abs(this.player.position.x - this.transform.position.x);
// 			if (!MathfX.isInRange(x, this.moveRangeMin, this.moveRangeMax))
// 			{
// 				return TaskStatus.Running;
// 			}
// 			this.action.StopMoveToIdle();
// 			return TaskStatus.Success;
// 		}
//
// 		private DahalAction action;
//
// 		private EnemyAttribute eAttr;
//
// 		public float moveRangeMin;
//
// 		public float moveRangeMax;
//
// 		[BehaviorDesigner.Runtime.Tasks.Tooltip("1普通移动 2快速移动")]
// 		public int moveType;
//
// 		private float aimPos;
//
// 		private float finalMoveLen;
// 	}
// }
