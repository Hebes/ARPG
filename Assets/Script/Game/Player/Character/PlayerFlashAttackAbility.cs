// using UnityEngine;
//
// /// <summary>
// /// 玩家闪光攻击技能
// /// </summary>
// public class PlayerFlashAttackAbility : CharacterState
// {
// 	public override void Update()
// 	{
// 		if (_target != null)
// 		{
// 			_clearRate += Time.unscaledDeltaTime;
// 			if (_clearRate >= 0.75f)
// 			{
// 				_target = null;
// 			}
// 		}
// 		if (_flashAtkStart)
// 		{
// 			_recoverRate += Time.unscaledDeltaTime;
// 			if (_recoverRate >= 1.5f)
// 			{
// 				PlayerRecover();
// 				_flashAtkStart = false;
// 			}
// 		}
// 		if (_recoverInvincible)
// 		{
// 			PAbilities.hurt.Invincible = true;
// 			_invincibleTime += Time.unscaledDeltaTime;
// 			if (_invincibleTime > 0.6f)
// 			{
// 				_invincibleTime = 0f;
// 				PAbilities.hurt.Invincible = false;
// 				_recoverInvincible = false;
// 			}
// 		}
// 	}
//
// 	public void FlashAttack(GameObject enemy)
// 	{
// 		if (_target != null)
// 		{
// 			return;
// 		}
// 		if (enemy == null)
// 		{
// 			return;
// 		}
// 		_clearRate = 0f;
// 		_target = enemy.gameObject;
// 		StartQTE();
// 		R.Audio.PlayEffect(172, PAction.transform.position);
// 		AttackEnemy(_target);
// 	}
//
// 	private void StartQTE()
// 	{
// 		SingletonMono<WorldTime>.Instance.TimeSlowByFrameOn60Fps(45, 0.15f);
// 	}
//
// 	public bool PressFlashAttack()
// 	{
// 		if (_target == null)
// 		{
// 			return false;
// 		}
// 		if (StateMachine.currentState.IsInArray(PlayerAction.FlashAttackSta))
// 		{
// 			AttackEnemy(_target);
// 			return true;
// 		}
// 		return false;
// 	}
//
// 	public bool CheckEnemy(GameObject enemy)
// 	{
// 		return enemy == _target;
// 	}
//
// 	private void AttackEnemy(GameObject enemy)
// 	{
// 		_recoverRate = 0f;
// 		_flashAtkStart = true;
// 		listener.StopIEnumerator("FlashPositionSet");
// 		//pac.ChangeState(PlayerAction.StateEnum.Disappear);
// 		SingletonMono<WorldTime>.Instance.TimeFrozenByFixedFrame(14, enemy);
// 		R.Audio.PlayEffect(200, PAction.transform.position);
// 		Transform transform = R.Effect.Generate(165, null, enemy.transform.position);
// 		transform.GetComponent<ShadeAttack>().Init(enemy);
// 		transform.localScale = new Vector3(-(float)PAttr.faceDir, 1f, 1f);
// 		Vector3 pos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 2f, Camera.main.transform.parent.position.z + 3f);
// 		SingletonMono<CameraController>.Instance.CameraZoom(pos, 0.2f);
// 	}
//
// 	/// <summary>
// 	/// 玩家重新获得
// 	/// </summary>
// 	private void PlayerRecover()
// 	{
// 		//pac.ChangeState(PlayerAction.StateEnum.EndAtk);
// 		SingletonMono<CameraController>.Instance.CameraZoomFinished();
// 		_recoverInvincible = true;
// 	}
//
// 	private GameObject _target;
//
// 	private float _clearRate;
//
// 	private float _recoverRate;
//
// 	private bool _flashAtkStart;
//
// 	private bool _recoverInvincible;
//
// 	private float _invincibleTime;
// }
