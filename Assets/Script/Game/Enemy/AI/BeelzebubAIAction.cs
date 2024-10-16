// using System;
// using BehaviorDesigner.Runtime.Tasks;
// using ExtensionMethods;
// using UnityEngine;
//
// public class BeelzebubAIAction
// {
// 	[TaskCategory("Enemy/Beelzebub")]
// 	[TaskDescription("Beelzebub攻击")]
// 	public class BeelzebubAttack : BehaviorDesigner.Runtime.Tasks.Action
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
// 			this._action = base.GetComponent<BeelzebubAction>();
// 		}
//
// 		public override void OnStart()
// 		{
// 			switch (this.atkType)
// 			{
// 			case BeelzebubAIAction.BeelzebubAttack.AtkType.Atk1:
// 				this._action.Attack1(InputSetting.JudgeDir(this.transform.position, this.player.position));
// 				break;
// 			case BeelzebubAIAction.BeelzebubAttack.AtkType.Atk2:
// 				this._action.Attack2(InputSetting.JudgeDir(this.transform.position, this.player.position));
// 				break;
// 			case BeelzebubAIAction.BeelzebubAttack.AtkType.Atk3:
// 				this._action.Attack3(InputSetting.JudgeDir(this.transform.position, this.player.position));
// 				break;
// 			case BeelzebubAIAction.BeelzebubAttack.AtkType.Atk4:
// 				this._action.Attack4(InputSetting.JudgeDir(this.transform.position, this.player.position));
// 				break;
// 			case BeelzebubAIAction.BeelzebubAttack.AtkType.Atk5:
// 				this._action.Attack5(InputSetting.JudgeDir(this.transform.position, this.player.position));
// 				break;
// 			}
// 		}
//
// 		public override TaskStatus OnUpdate()
// 		{
// 			return (!this._action.stateMachine.currentState.IsInArray(BeelzebubAction.AttackSta)) ? TaskStatus.Success : TaskStatus.Running;
// 		}
//
// 		public BeelzebubAIAction.BeelzebubAttack.AtkType atkType;
//
// 		private BeelzebubAction _action;
//
// 		public enum AtkType
// 		{
// 			Atk1,
// 			Atk2,
// 			Atk3,
// 			Atk4,
// 			Atk5
// 		}
// 	}
//
// 	[TaskCategory("Enemy/Beelzebub")]
// 	[TaskDescription("Beelzebub嘲讽")]
// 	public class BeelzebubRidicule : BehaviorDesigner.Runtime.Tasks.Action
// 	{
// 		public override void OnAwake()
// 		{
// 			this._action = base.GetComponent<BeelzebubAction>();
// 		}
//
// 		public override void OnStart()
// 		{
// 			this._action.FaceToPlayer();
// 			this._action.Idle2();
// 		}
//
// 		public override TaskStatus OnUpdate()
// 		{
// 			return (!(this._action.stateMachine.currentState == "Idle2")) ? TaskStatus.Success : TaskStatus.Running;
// 		}
//
// 		private BeelzebubAction _action;
// 	}
// }
