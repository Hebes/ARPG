using System.Collections;
using UnityEngine;

public class SupplyBoxAction : BaseBehaviour
{
	//public bool Opened => this.attribute.Opened;

	private void Awake()
	{
		// this.attribute = base.GetComponent<SupplyAttribute>();
		// this.stateMachine = base.GetComponent<StateMachine>();
		// this.spineAnim = base.GetComponent<SpineAnimationController>();
	}

	private void Start()
	{
		// this.stateMachine.AddStates(typeof(SupplyBoxAction.StateEnum));
		// this.stateMachine.OnEnter += this.OnMyStateEnter;
		// this.ChangeState((!this.attribute.Opened) ? SupplyBoxAction.StateEnum.Unknow : SupplyBoxAction.StateEnum.Null);
	}

	private void Update()
	{
		// if (this.stateMachine.currentState == "Unknow" && Vector2.Distance(R.Player.Transform.position, base.transform.position) <= this.attribute.ShowDistance)
		// {
		// 	this.UnknowToKnow();
		// }
	}

	private void OnMyStateEnter(object sender, StateEventArgs args)
	{
		StateEnum stateEnum = args.state.ToEnum<StateEnum>();
		switch (stateEnum)
		{
		case StateEnum.Null:
		case StateEnum.EnLv0ToLv1:
		case StateEnum.EnLv1ToLv2:
		case StateEnum.EnLv2ToLv3:
		case StateEnum.EnLv3ToLv4:
		case StateEnum.EnLv4ToNull:
		case StateEnum.HPLv0ToLv1:
		case StateEnum.HPLv1ToLv2:
		case StateEnum.HPLv2ToLv3:
		case StateEnum.HPLv3ToLv4:
		case StateEnum.HPLv4ToNull:
		case StateEnum.KeyLv0ToLv1:
		case StateEnum.KeyLv1ToLv2:
		case StateEnum.KeyLv2ToLv3:
		case StateEnum.KeyLv3ToLv4:
		case StateEnum.KeyLv4ToNull:
		case StateEnum.MoneyHit:
		case StateEnum.MoneyNull:
		case StateEnum.UnknowToEnergy:
		case StateEnum.UnknowToHP:
		case StateEnum.UnknowToKey:
		case StateEnum.UnknowToMoney:
		case StateEnum.UnknowToWPower:
		case StateEnum.WPLv0ToLv1:
		case StateEnum.WPLv1ToLv2:
		case StateEnum.WPLv2ToLv3:
		case StateEnum.WPLv3ToLv4:
		case StateEnum.WPLv4ToNull:
			//this.spineAnim.Play(stateEnum, false, true, 1f);
			break;
		case StateEnum.Unknow:
			//this.spineAnim.Play(stateEnum, true, false, 1f);
			break;
		}
	}

	public void ChangeState(StateEnum nextSta)
	{
		if (stateMachine.currentState.IsInEnum<DieSta>())return;
		stateMachine.SetState(nextSta);
	}

	private void UnknowToKnow()
	{
		// SupplyAttribute.SupplyBoxType supplyBox = this.attribute.SupplyBox;
		// if (supplyBox != SupplyAttribute.SupplyBoxType.Flash)
		// {
		// 	if (supplyBox != SupplyAttribute.SupplyBoxType.Money)
		// 	{
		// 		if (supplyBox == SupplyAttribute.SupplyBoxType.Cheat)
		// 		{
		// 			this.ChangeState(SupplyBoxAction.StateEnum.UnknowToWPower);
		// 		}
		// 	}
		// 	else
		// 	{
		// 		this.ChangeState(SupplyBoxAction.StateEnum.UnknowToMoney);
		// 	}
		// }
		// else
		// {
		// 	this.ChangeState(SupplyBoxAction.StateEnum.UnknowToEnergy);
		// }
	}

	private void OnDrawGizmosSelected()
	{
		Vector3 from = transform.position + Vector3.up;
		Vector3 vector = transform.position.AddY(-Physics2D.Raycast(transform.position, Vector2.down, 10f).distance);
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(from, vector);
		Gizmos.DrawLine(vector.AddX(-1f), vector.AddX(1f));
	}

	public Coroutine WaitForBreak()
	{
		return StartCoroutine(WaitForBreakCoroutine());
	}

	private IEnumerator WaitForBreakCoroutine()
	{
		// while (!this.attribute.Opened)
		// {
		// 	yield return null;
		// }
		 yield break;
	}

	//private SupplyAttribute attribute;

	//private SpineAnimationController spineAnim;

	private StateMachine stateMachine;

	private enum DieSta
	{
		Null,
		EnLv4ToNull,
		HPLv4ToNull,
		KeyLv4ToNull,
		MoneyNull,
		WPLv4ToNull
	}

	public enum StateEnum
	{
		Null,
		EnLv0ToLv1,
		EnLv1ToLv2,
		EnLv2ToLv3,
		EnLv3ToLv4,
		EnLv4ToNull,
		HPLv0ToLv1,
		HPLv1ToLv2,
		HPLv2ToLv3,
		HPLv3ToLv4,
		HPLv4ToNull,
		KeyLv0ToLv1,
		KeyLv1ToLv2,
		KeyLv2ToLv3,
		KeyLv3ToLv4,
		KeyLv4ToNull,
		MoneyHit,
		MoneyNull,
		Unknow,
		UnknowToEnergy,
		UnknowToHP,
		UnknowToKey,
		UnknowToMoney,
		UnknowToWPower,
		WPLv0ToLv1,
		WPLv1ToLv2,
		WPLv2ToLv3,
		WPLv3ToLv4,
		WPLv4ToNull
	}
}
