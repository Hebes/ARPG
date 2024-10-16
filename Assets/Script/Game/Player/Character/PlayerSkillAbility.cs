// using System;
// using Framework.Core;
//
// /// <summary>
// /// 玩家技能
// /// </summary>
// public class PlayerSkillAbility : CharacterState
// {
// 	public void ShadeAtk()
// 	{
// 		if (R.Player.EnhancementSaveData.ShadeAttack == 0)
// 		{
// 			return;
// 		}
// 		if (this.StateMachine.currentState.IsInArray(this._shadeAtkSta))
// 		{
// 			if (this.PAttr.currentHP > this.PAttr.maxHP / 10)
// 			{
// 				this.PAttr.currentHP -= this.PAttr.maxHP / 10;
// 				this.weapon.HandleShadeAttack();
// 			}
// 			else
// 			{
// 				"R.Ui.Toast".Log();
// 				//R.Ui.Toast.Show(ScriptLocalization.ui.energyMatrixNotEnough, 2f, true);
// 			}
// 		}
// 	}
//
// 	public void BladeStorm()
// 	{
// 		if (R.Player.EnhancementSaveData.BladeStorm == 0)
// 		{
// 			return;
// 		}
// 		if (!this.PAttr.isOnGround)
// 		{
// 			return;
// 		}
// 		if (this.StateMachine.currentState.IsInArray(this._bladeStormSta))
// 		{
// 			if (this.PAttr.currentHP > this.PAttr.maxHP / 10)
// 			{
// 				this.PAttr.currentHP -= this.PAttr.maxHP / 10;
// 				this.weapon.HandleBladeStorm();
// 			}
// 			else
// 			{
// 				"R.Ui.Toast".Log();
// 				//R.Ui.Toast.Show(ScriptLocalization.ui.energyMatrixNotEnough, 2f, true);
// 			}
// 		}
// 	}
//
// 	private readonly string[] _shadeAtkSta = new string[]
// 	{
// 		"EndAtk",
// 		"GetUp",
// 		"Idle",
// 		"Ready",
// 		"Run",
// 		"RunSlow",
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
// 		"AtkHv1Push",
// 		"AtkRollReady",
// 		"AtkRollEnd",
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
// 		"Jump",
// 		"Jump2",
// 		"RollJump"
// 	};
//
// 	private readonly string[] _bladeStormSta = new string[]
// 	{
// 		"EndAtk",
// 		"GetUp",
// 		"Idle",
// 		"Ready",
// 		"Run",
// 		"RunSlow",
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
// 		"AtkHv1Push",
// 		"AtkRollReady",
// 		"AtkRollEnd",
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
// 		"Jump",
// 		"Jump2",
// 		"RollJump"
// 	};
// }
