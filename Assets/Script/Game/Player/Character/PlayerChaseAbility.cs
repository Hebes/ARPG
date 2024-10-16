// using UnityEngine;
//
// /// <summary>
// /// 玩家追逐能力
// /// </summary>
// public class PlayerChaseAbility : CharacterState
// {
//     public void Chase()
//     {
//         if (R.Player.EnhancementSaveData.Chase == 0)
//         {
//             return;
//         }
//         if (StateMachine.currentState.IsInArray(PlayerAction.SpHurtSta) || StateMachine.currentState.IsInArray(PlayerAction.HurtSta))
//         {
//             return;
//         }
//         _enemy = GetChaseEnemy();
//         if (_enemy != null)
//         {
//             Vector3 position = _enemy.position;
//             listener.PlayFlashSound();
//             position.y = Mathf.Clamp(position.y, LayerManager.YNum.GetGroundHeight(_enemy.gameObject), float.MaxValue);
//             Transform transform = R.Effect.Generate(182, null, position);
//             Vector3 localScale = transform.localScale;
//             localScale.x = (Random.Range(0, 2) != 0) ? -1 : 1;
//             transform.localScale = localScale;
//             EnemyBaseHurt component = _enemy.GetComponent<EnemyBaseHurt>();
//             if (R.Player.EnhancementSaveData.Chase != 3)
//             {
//                 component.ChaseEnd();
//             }
//             else if (component.currentChaseTime < 2)
//             {
//                 component.ChaseStart();
//             }
//             else
//             {
//                 component.ChaseEnd();
//             }
//         }
//     }
//
//     private Transform GetChaseEnemy()
//     {
//         for (int i = 0; i < R.Enemy.Count; i++)
//         {
//             EnemyAttribute enemyAttribute = R.Enemy.EnemyAttributes[i];
//             if (enemyAttribute.canBeChased)
//             {
//                 return enemyAttribute.transform;
//             }
//         }
//         return null;
//     }
//
//     private Transform _enemy;
// }