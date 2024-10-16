// using System;
// using BehaviorDesigner.Runtime.Tasks;
// using UnityEngine;
//
// public class DaoPaoMixAIAction
// {
// 	[TaskCategory("Enemy/DaoPaoMix")]
// 	[TaskDescription("跳跃")]
// 	public class Jump : BehaviorDesigner.Runtime.Tasks.Action
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
// 			this.action = base.GetComponent<DaoPaoAction>();
// 		}
//
// 		public override void OnStart()
// 		{
// 			this.action.FaceToPlayer();
// 			if (this.back)
// 			{
// 				this.action.JumpBack();
// 			}
// 			else
// 			{
// 				this.action.Jump();
// 			}
// 		}
//
// 		public override TaskStatus OnUpdate()
// 		{
// 			DaoPaoAction.StateEnum stateEnum = EnumTools.ToEnum<DaoPaoAction.StateEnum>(this.action.stateMachine.currentState, false);
// 			if (stateEnum == DaoPaoAction.StateEnum.Jump || stateEnum == DaoPaoAction.StateEnum.JumpBack)
// 			{
// 				return TaskStatus.Running;
// 			}
// 			return TaskStatus.Success;
// 		}
//
// 		public bool back;
//
// 		private float moveDis;
//
// 		private float aimPos;
//
// 		private DaoPaoAction action;
// 	}
//
// 	[TaskDescription("卖萌")]
// 	[TaskCategory("Enemy/DaoPaoMix")]
// 	public class Moe : BehaviorDesigner.Runtime.Tasks.Action
// 	{
// 		public override void OnAwake()
// 		{
// 			this.action = base.GetComponent<DaoPaoAction>();
// 		}
//
// 		public override void OnStart()
// 		{
// 			this.action.FaceToPlayer();
// 			if (UnityEngine.Random.Range(0, 2) == 0)
// 			{
// 				this.action.Idle2();
// 			}
// 			else
// 			{
// 				this.action.Idle3();
// 			}
// 		}
//
// 		public override TaskStatus OnUpdate()
// 		{
// 			DaoPaoAction.StateEnum stateEnum = EnumTools.ToEnum<DaoPaoAction.StateEnum>(this.action.stateMachine.currentState, false);
// 			if (stateEnum == DaoPaoAction.StateEnum.Idle2 || stateEnum == DaoPaoAction.StateEnum.Idle3)
// 			{
// 				return TaskStatus.Running;
// 			}
// 			return TaskStatus.Success;
// 		}
//
// 		private DaoPaoAction action;
// 	}
//
// 	[TaskCategory("Enemy/DaoPaoMix")]
// 	[TaskDescription("移动到主角对面")]
// 	public class MoveToPlayer : BehaviorDesigner.Runtime.Tasks.Action
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
// 			this.action = base.GetComponent<DaoPaoAction>();
// 			this.eAttr = base.GetComponent<EnemyAttribute>();
// 		}
//
// 		public override void OnStart()
// 		{
// 			this.moveDis = UnityEngine.Random.Range(1f, 5f);
// 			if (this.transform.position.x - this.player.position.x >= 0f)
// 			{
// 				this.aimPos = this.player.position.x - this.moveDis;
// 			}
// 			else
// 			{
// 				this.aimPos = this.player.position.x + this.moveDis;
// 			}
// 			this.aimPos = Mathf.Clamp(this.aimPos, GameArea.EnemyRange.min.x + this.eAttr.bounds.size.x, GameArea.EnemyRange.max.x - this.eAttr.bounds.size.x);
// 			this.action.FaceToPlayer();
// 		}
//
// 		public override TaskStatus OnUpdate()
// 		{
// 			if (!this.action.AutoMove())
// 			{
// 				return TaskStatus.Failure;
// 			}
// 			bool flag = InputSetting.JudgeDir(this.action.transform.position.x, this.aimPos) == this.eAttr.faceDir;
// 			if (flag)
// 			{
// 				return TaskStatus.Running;
// 			}
// 			return (!this.action.StopMoveToIdle()) ? TaskStatus.Failure : TaskStatus.Success;
// 		}
//
// 		private float moveDis;
//
// 		private float aimPos;
//
// 		private DaoPaoAction action;
//
// 		private EnemyAttribute eAttr;
// 	}
// }
