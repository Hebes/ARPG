// using System;
// using BehaviorDesigner.Runtime.Tasks;
//
// public class BeeAIAction
// {
//     [TaskCategory("Enemy/Bee")]
//     [TaskDescription("蜂鸟闪避")]
//     public class BeeRunAway : BehaviorDesigner.Runtime.Tasks.Action
//     {
//         public override void OnAwake()
//         {
//             this.action = base.GetComponent<BeeAction>();
//         }
//
//         public override void OnStart()
//         {
//             this.action.SideStep();
//         }
//
//         public override TaskStatus OnUpdate()
//         {
//             return (!(this.action.stateMachine.currentState == "MoveAway")) ? TaskStatus.Success : TaskStatus.Running;
//         }
//
//         private BeeAction action;
//     }
//
//     [TaskCategory("Enemy/Bee")]
//     [TaskDescription("蜂鸟卖萌")]
//     public class BeeMoe : BehaviorDesigner.Runtime.Tasks.Action
//     {
//         public override void OnAwake()
//         {
//             this.action = base.GetComponent<BeeAction>();
//         }
//
//         public override void OnStart()
//         {
//             this.action.Idle2();
//         }
//
//         public override TaskStatus OnUpdate()
//         {
//             return (!(this.action.stateMachine.currentState == "Idle2")) ? TaskStatus.Success : TaskStatus.Running;
//         }
//
//         private BeeAction action;
//     }
// }