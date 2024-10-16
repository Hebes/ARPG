// using UnityEngine;
//
// /// <summary>
// /// 玩家充电能力
// /// </summary>
// public class PlayerChargingAbility : CharacterState
// {
// 	public override void Update()
// 	{
// 		if (PAttr.isInCharging && Input.Game.Atk.OnReleased)
// 		{
// 			bool flag = StateMachine.currentState == "AirCharging";
// 			if (weapon.canChargeAttack)
// 			{
// 				PAction.TurnRound(PAction.tempDir);
// 				ReleaseCharge(flag);
// 			}
// 			else
// 			{
// 				//pac.ChangeState((!flag) ? PlayerAction.StateEnum.EndAtk : PlayerAction.StateEnum.Fall1);
// 				CancelCharge();
// 			}
// 		}
// 	}
//
// 	public void Charging()
// 	{
// 		if (StateMachine.currentState.IsInArray(CanChargeSta) && R.Player.EnhancementSaveData.Charging != 0 && ChipManager.HasChipInScene())
// 		{
// 			if (PAttr.isOnGround)
// 				StartChargeGround();
// 			else
// 				StartChargeInAir();
// 		}
// 	}
//
// 	private void StartChargeGround()
// 	{
// 		if (!_chargeReset)
// 		{
// 			return;
// 		}
// 		R.Player.TimeController.SetSpeed(Vector2.zero);
// 		weapon.StartCharge(false);
// 		_chargeReset = false;
// 	}
//
// 	private void StartChargeInAir()
// 	{
// 		if (!_chargeReset)
// 		{
// 			return;
// 		}
// 		R.Player.TimeController.SetSpeed(Vector2.zero);
// 		listener.AirPhysic(0f);
// 		weapon.StartCharge(true);
// 		_chargeReset = false;
// 	}
//
// 	public void CancelCharge()
// 	{
// 		ChargeReset();
// 		weapon.ChargeCancel();
// 	}
//
// 	public void ChargeReset()
// 	{
// 		_chargeReset = true;
// 	}
//
// 	private void ReleaseCharge(bool inAir)
// 	{
// 		weapon.ReleaseCharge(inAir);
// 	}
//
// 	public override void OnStateTransfer(object sender, StateMachine.TransferEventArgs args)
// 	{
// 		if (args.lastState.IsInArray(PlayerAction.ChargeSta) && !args.nextState.IsInArray(PlayerAction.ChargeSta))
// 		{
// 			CancelCharge();
// 			PAction.absorbNum = 0;
// 		}
// 	}
//
// 	public override void Start()
// 	{
// 		ChargeReset();
// 	}
//
// 	private bool _chargeReset;
//
// 	private static readonly string[] CanChargeSta = {
// 		"Atk1",
// 		"Atk2",
// 		"Atk3",
// 		"Atk4",
// 		"Atk5",
// 		"Atk6",
// 		"Atk7",
// 		"Atk8",
// 		"Atk11",
// 		"Atk12",
// 		"Atk13",
// 		"Atk14",
// 		"Atk23",
// 		"Atk15",
// 		"AtkHv1",
// 		"AtkHv2",
// 		"AtkHv3",
// 		"Atk16",
// 		"AirAtk1",
// 		"AirAtk2",
// 		"AirAtk3",
// 		"AirAtk4",
// 		"AirAtk6",
// 		"AirAtkHv1",
// 		"AirAtkHv2",
// 		"AirAtkHv3",
// 		"AirAtkHv4",
// 		"AirAtkHv5",
// 		"AirAtkHv1Push",
// 		"AirAtkRollReady",
// 		"AirAtkRoll",
// 		"Fall1",
// 		"Fall2",
// 		"Flash2",
// 		"FlashDown2",
// 		"FlashUp2",
// 		"AtkHv1Push",
// 		"Flash1",
// 		"FlashDown1",
// 		"FlashUp1",
// 		"HitGround",
// 		"HitGround2",
// 		"RollReady",
// 		"Roll",
// 		"RollEnd",
// 		"IdleToDefense",
// 		"Defense",
// 		"FallToDefenseAir",
// 		"DefenseAir",
// 		"EndAtk",
// 		"GetUp",
// 		"Idle",
// 		"Ready",
// 		"Run",
// 		"RunSlow",
// 		"ExecuteToIdle",
// 		"Execute2ToFall",
// 		"FlashGround"
// 	};
// }
