using UnityEngine;

/// <summary>
/// 玩家护盾
/// </summary>
public class PlayerSpHurt
{
	private PlayerAction pac => R.Player.Action;

	private PlayerAttribute pAttr => R.Player.Attribute;

	public bool DaoRoll(Transform enemy)
	{
		return enemy.GetComponent<StateMachine>() != null && enemy.GetComponent<StateMachine>().currentState == "DaoAtk2";
	}

	// public bool CanBeJumperCatach(Transform enemy)
	// {
	// 	bool flag = enemy.GetComponent<JumperAction>() != null && enemy.GetComponent<StateMachine>().currentState == "Atk3";
	// 	bool flag2 = !this.pac.stateMachine.currentState.IsInArray(PlayerAction.SpHurtSta) && !this.pAttr.isDead;
	// 	return flag && flag2;
	// }

	// public bool CanBeJumperFooterCatch(Transform enemy)
	// {
	// 	bool flag = enemy.GetComponent<JumperFooterAction>() != null && enemy.GetComponent<StateMachine>().currentState == "Atk2Ready";
	// 	bool flag2 = !this.pac.stateMachine.currentState.IsInArray(PlayerAction.SpHurtSta) && !this.pAttr.isDead;
	// 	return flag && flag2;
	// }

	// public bool CanBeBeelzebubEat(Transform enemy)
	// {
	// 	bool flag = enemy.GetComponent<BeelzebubAction>() != null && enemy.GetComponent<StateMachine>().currentState == "Atk1";
	// 	bool flag2 = !this.pac.stateMachine.currentState.IsInArray(PlayerAction.SpHurtSta) && !this.pAttr.isDead;
	// 	return flag && flag2;
	// }

	// public bool CanBeBeelzebubSaw(Transform enemy)
	// {
	// 	bool flag = enemy.GetComponent<BeelzebubAction>() != null && enemy.GetComponent<StateMachine>().currentState == "Atk2";
	// 	bool flag2 = !this.pac.stateMachine.currentState.IsInArray(PlayerAction.SpHurtSta) && !this.pAttr.isDead;
	// 	return flag && flag2;
	// }

	// public bool CanBombKillerCatch(Transform enemy)
	// {
	// 	bool flag = enemy.GetComponent<BombKillerAction>() != null && enemy.GetComponent<StateMachine>().currentState == "Atk1Ready";
	// 	bool flag2 = !this.pac.stateMachine.currentState.IsInArray(PlayerAction.SpHurtSta) && !this.pAttr.isDead;
	// 	return flag && flag2;
	// }
	//
	// public bool CanEatingBossEat(Transform enemy)
	// {
	// 	bool flag = enemy.GetComponent<EatingBossAction>() != null && enemy.GetComponent<StateMachine>().currentState == "Atk2";
	// 	bool flag2 = !this.pac.stateMachine.currentState.IsInArray(PlayerAction.SpHurtSta) && !this.pAttr.isDead;
	// 	return flag && flag2;
	// }
	//
	// public bool JumperCatachSuccess(Transform enemy, bool force)
	// {
	// 	return enemy.GetComponent<JumperAction>() != null && force;
	// }
	//
	// public bool JumperFooterCatchSuccess(Transform enemy, bool force)
	// {
	// 	return enemy.GetComponent<JumperFooterAction>() != null && force;
	// }
	//
	// public bool BeelzebubSpAttackSuccess(Transform enemy, bool force)
	// {
	// 	return enemy.GetComponent<BeelzebubAction>() != null && force;
	// }
	//
	// public bool BombKillerAtkSuccess(Transform enemy, bool force)
	// {
	// 	return enemy.GetComponent<BombKillerAction>() != null && force;
	// }
	//
	// public bool EatingBossEatSuccess(Transform enemy, bool force)
	// {
	// 	return enemy.GetComponent<EatingBossAction>() != null && force;
	// }
}
