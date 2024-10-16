using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class DaoBrotherAIAction
{
	[TaskCategory("Enemy/DaoBrother")]
	[TaskDescription("在丢飞刀攻击距离内")]
	public class WithinNormalAtkRange : BehaviorDesigner.Runtime.Tasks.Action
	{
		// private Transform player => R.Player.Transform;
		//
		// private EnemyAIAttribute.ExpectationParam normalAtk => this.ai.attackExpectations[0];
		//
		// public override void OnAwake()
		// {
		// 	this.ai = base.GetComponent<EnemyAIAttribute>();
		// }
		//
		// public override TaskStatus OnUpdate()
		// {
		// 	if (Mathf.Abs(this.transform.position.x - this.player.position.x) < this.normalAtk.distance + this.normalAtk.range)
		// 	{
		// 		return TaskStatus.Success;
		// 	}
		// 	return TaskStatus.Failure;
		// }
		//
		// private EnemyAIAttribute ai;
	}

	[TaskDescription("在两种攻击的攻击力距离内")]
	[TaskCategory("Enemy/DaoBrother")]
	public class BetweenTwoAtkEange : BehaviorDesigner.Runtime.Tasks.Action
	{
		// private Transform player
		// {
		// 	get
		// 	{
		// 		return R.Player.Transform;
		// 	}
		// }
		//
		// private EnemyAIAttribute.ExpectationParam normalAtk
		// {
		// 	get
		// 	{
		// 		return this.ai.attackExpectations[0];
		// 	}
		// }
		//
		// private EnemyAIAttribute.ExpectationParam rollAtk
		// {
		// 	get
		// 	{
		// 		return this.ai.attackExpectations[1];
		// 	}
		// }
		//
		// public override void OnAwake()
		// {
		// 	this.ai = base.GetComponent<EnemyAIAttribute>();
		// }
		//
		// public override TaskStatus OnUpdate()
		// {
		// 	if (Mathf.Abs(this.transform.position.x - this.player.position.x) >= this.normalAtk.distance + this.normalAtk.range && Mathf.Abs(this.transform.position.x - this.player.position.x) < this.rollAtk.distance + this.rollAtk.range)
		// 	{
		// 		return TaskStatus.Success;
		// 	}
		// 	return TaskStatus.Failure;
		// }
		//
		// private EnemyAIAttribute ai;
	}

	[TaskDescription("在滚动攻击距离外")]
	[TaskCategory("Enemy/DaoBrother")]
	public class WithoutRollAtkRange : BehaviorDesigner.Runtime.Tasks.Action
	{
		// private Transform player
		// {
		// 	get
		// 	{
		// 		return R.Player.Transform;
		// 	}
		// }
		//
		// private EnemyAIAttribute.ExpectationParam rollAtk
		// {
		// 	get
		// 	{
		// 		return this.ai.attackExpectations[1];
		// 	}
		// }
		//
		// public override void OnAwake()
		// {
		// 	this.ai = base.GetComponent<EnemyAIAttribute>();
		// }
		//
		// public override TaskStatus OnUpdate()
		// {
		// 	if (Mathf.Abs(this.transform.position.x - this.player.position.x) >= this.rollAtk.distance + this.rollAtk.range)
		// 	{
		// 		return TaskStatus.Success;
		// 	}
		// 	return TaskStatus.Failure;
		// }
		//
		// private EnemyAIAttribute ai;
	}

	[TaskCategory("Enemy/DaoBrother")]
	[TaskDescription("刀哥攻击")]
	public class DaoBrotherAtk : BehaviorDesigner.Runtime.Tasks.Action
	{
		// private Transform player
		// {
		// 	get
		// 	{
		// 		return R.Player.Transform;
		// 	}
		// }
		//
		// public override void OnAwake()
		// {
		// 	this.action = base.GetComponent<DaoAction>();
		// }
		//
		// public override void OnStart()
		// {
		// 	int dir = InputSetting.JudgeDir(this.transform.position, this.player.position);
		// 	DaoBrotherAIAction.DaoBrotherAtk.DaoAtkType daoAtkType = this.atkType;
		// 	if (daoAtkType != DaoBrotherAIAction.DaoBrotherAtk.DaoAtkType.NormalAtk)
		// 	{
		// 		if (daoAtkType == DaoBrotherAIAction.DaoBrotherAtk.DaoAtkType.RollAtk)
		// 		{
		// 			this.action.Attack2(dir);
		// 		}
		// 	}
		// 	else
		// 	{
		// 		this.action.Attack1(dir);
		// 	}
		// }
		//
		// public override TaskStatus OnUpdate()
		// {
		// 	if (this.action.stateMachine.currentState.IsInArray(DaoAction.AttackSta))
		// 	{
		// 		return TaskStatus.Running;
		// 	}
		// 	return TaskStatus.Success;
		// }
		//
		// public DaoBrotherAIAction.DaoBrotherAtk.DaoAtkType atkType;
		//
		// private DaoAction action;
		//
		// public enum DaoAtkType
		// {
		// 	NormalAtk,
		// 	RollAtk
		// }
	}

	[TaskDescription("刀哥卖萌")]
	[TaskCategory("Enemy/DaoBrother")]
	public class Moe : BehaviorDesigner.Runtime.Tasks.Action
	{
		// public override void OnAwake()
		// {
		// 	this.action = base.GetComponent<DaoAction>();
		// }
		//
		// public override void OnStart()
		// {
		// 	this.action.FaceToPlayer();
		// 	if (UnityEngine.Random.Range(0, 2) == 0)
		// 	{
		// 		this.action.Idle2();
		// 	}
		// 	else
		// 	{
		// 		this.action.Idle3();
		// 	}
		// }
		//
		// public override TaskStatus OnUpdate()
		// {
		// 	string currentState = this.action.stateMachine.currentState;
		// 	if (currentState == DaoAction.State.Idle2 || currentState == DaoAction.State.Idle3)
		// 	{
		// 		return TaskStatus.Running;
		// 	}
		// 	return TaskStatus.Success;
		// }
		//
		// private DaoAction action;
	}

	[TaskDescription("移动到攻击1和攻击2距离之间")]
	[TaskCategory("Enemy/DaoBrother")]
	public class MoveToAPosition : BehaviorDesigner.Runtime.Tasks.Action
	{
		// private Transform player
		// {
		// 	get
		// 	{
		// 		return R.Player.Transform;
		// 	}
		// }
		//
		// private float randomLen
		// {
		// 	get
		// 	{
		// 		return UnityEngine.Random.Range(0f, Mathf.Abs(this.ai.attackExpectations[0].distance + this.ai.attackExpectations[0].range - (this.ai.attackExpectations[1].distance + this.ai.attackExpectations[1].range)));
		// 	}
		// }
		//
		// public override void OnAwake()
		// {
		// 	this.ai = base.GetComponent<EnemyAIAttribute>();
		// 	this.action = base.GetComponent<DaoAction>();
		// 	this.eAttr = base.GetComponent<EnemyAttribute>();
		// }
		//
		// public override void OnStart()
		// {
		// 	float num = Mathf.Abs(this.transform.position.x - this.player.position.x) - (this.ai.attackExpectations[1].range + this.ai.attackExpectations[1].distance);
		// 	float num2 = num + this.randomLen;
		// 	this.aimPos = ((this.transform.position.x - this.player.position.x <= 0f) ? (this.transform.position.x + num2) : (this.transform.position.x - num2));
		// 	this.aimPos = Mathf.Clamp(this.aimPos, GameArea.EnemyRange.min.x + this.eAttr.bounds.size.x, GameArea.EnemyRange.max.x - this.eAttr.bounds.size.x);
		// }
		//
		// public override TaskStatus OnUpdate()
		// {
		// 	if (!this.action.AutoMove())
		// 	{
		// 		return TaskStatus.Failure;
		// 	}
		// 	bool flag = InputSetting.JudgeDir(this.action.transform.position.x, this.aimPos) == this.eAttr.faceDir;
		// 	if (flag)
		// 	{
		// 		return TaskStatus.Running;
		// 	}
		// 	return (!this.action.StopMoveToIdle()) ? TaskStatus.Failure : TaskStatus.Success;
		// }
		//
		// private EnemyAIAttribute ai;
		//
		// private DaoAction action;
		//
		// private EnemyAttribute eAttr;
		//
		// private float aimPos;
	}

	[TaskCategory("Enemy/DaoBrother")]
	[TaskDescription("移动到主角对面")]
	public class MoveToPlayer : BehaviorDesigner.Runtime.Tasks.Action
	{
		// private Transform player
		// {
		// 	get
		// 	{
		// 		return R.Player.Transform;
		// 	}
		// }
		//
		// public override void OnAwake()
		// {
		// 	this.action = base.GetComponent<DaoAction>();
		// 	this.eAttr = base.GetComponent<EnemyAttribute>();
		// }
		//
		// public override void OnStart()
		// {
		// 	this.moveDis = UnityEngine.Random.Range(1f, 5f);
		// 	if (this.transform.position.x - this.player.position.x >= 0f)
		// 	{
		// 		this.aimPos = this.player.position.x - this.moveDis;
		// 	}
		// 	else
		// 	{
		// 		this.aimPos = this.player.position.x + this.moveDis;
		// 	}
		// 	this.aimPos = Mathf.Clamp(this.aimPos, GameArea.EnemyRange.min.x + this.eAttr.bounds.size.x, GameArea.EnemyRange.max.x - this.eAttr.bounds.size.x);
		// 	this.action.FaceToPlayer();
		// }
		//
		// public override TaskStatus OnUpdate()
		// {
		// 	if (!this.action.AutoMove())
		// 	{
		// 		return TaskStatus.Failure;
		// 	}
		// 	bool flag = InputSetting.JudgeDir(this.action.transform.position.x, this.aimPos) == this.eAttr.faceDir;
		// 	if (flag)
		// 	{
		// 		return TaskStatus.Running;
		// 	}
		// 	return (!this.action.StopMoveToIdle()) ? TaskStatus.Failure : TaskStatus.Success;
		// }
		//
		// private float moveDis;
		//
		// private float aimPos;
		//
		// private DaoAction action;
		//
		// private EnemyAttribute eAttr;
	}

	[TaskDescription("刀哥跳跃")]
	[TaskCategory("Enemy/DaoBrother")]
	public class DaoJump : BehaviorDesigner.Runtime.Tasks.Action
	{
		// private Transform player
		// {
		// 	get
		// 	{
		// 		return R.Player.Transform;
		// 	}
		// }
		//
		// public override void OnAwake()
		// {
		// 	this.action = base.GetComponent<DaoAction>();
		// }
		//
		// public override void OnStart()
		// {
		// 	this.action.FaceToPlayer();
		// 	if (this.back)
		// 	{
		// 		this.action.JumpBack();
		// 	}
		// 	else
		// 	{
		// 		this.action.Jump();
		// 	}
		// }
		//
		// public override TaskStatus OnUpdate()
		// {
		// 	string currentState = this.action.stateMachine.currentState;
		// 	if (currentState == DaoAction.State.Jump || currentState == DaoAction.State.JumpBack)
		// 	{
		// 		return TaskStatus.Running;
		// 	}
		// 	return TaskStatus.Success;
		// }
		//
		// public bool back;
		//
		// private float moveDis;
		//
		// private float aimPos;
		//
		// private DaoAction action;
	}
}
