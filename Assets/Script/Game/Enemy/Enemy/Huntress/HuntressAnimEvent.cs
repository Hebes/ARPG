using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 猎手动画
/// </summary>
public class HuntressAnimEvent : MonoBehaviour
{
    #region 组件

    private Transform Player => R.Player.Transform;
    private EnemyAtk _atk;
    private OnionCreator _onion;
    private Rigidbody2D _rigidbody2D;
    private HuntressAction _eAction;
    private EnemyAttribute _eAttr;
    private Animator _animator;

    private void GetComponent()
    {
        _atk = transform.FindChildByType<EnemyAtk>();
        _onion = GetComponent<OnionCreator>();
        _eAction = GetComponent<HuntressAction>();
        _eAttr = GetComponent<EnemyAttribute>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        // _atk = transform.parent.GetComponent<EnemyAtk>();
        // _onion = transform.parent.GetComponent<OnionCreator>();
        // _eAction = transform.parent.GetComponent<HuntressAction>();
        // _eAttr = transform.parent.GetComponent<EnemyAttribute>();
        // _rigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    #endregion


    private Transform gun;
    private Transform gunPos;
    private Transform gunAssistant;
    [SerializeField] private GameObject[] onionObj; //闪用
    private Dictionary<string, Dictionary<PlayerHurtDataType, string>> _atkData;


    private void Awake()
    {
        GetComponent();
        //动画监听
        _animator.UnAnimatorAddEventAll();
        _animator.AddAnimatorEvent(EnemyStaEnum.Atk2.ToString(), 0, nameof(SetAtkData), 53);
        _animator.AddAnimatorEvent(EnemyStaEnum.Atk2.ToString(), 2, nameof(PlaySound), 53);
        _animator.AddAnimatorEvent(EnemyStaEnum.Atk2.ToString(), 4, nameof(TestOnChangeState), EnemyStaEnum.Idle.ToString());
        _animator.AddAnimatorEvent(EnemyStaEnum.AtkRemote.ToString(), 5, nameof(PlaySound), 204);
        _animator.AddAnimatorEvent(EnemyStaEnum.AtkRemote.ToString(), 6, nameof(TestOnChangeState), EnemyStaEnum.Idle.ToString());
        _animator.AddAnimatorEvent(EnemyStaEnum.Death.ToString(), 2, nameof(DieBlock));
        _animator.AddAnimatorEvent(EnemyStaEnum.Death.ToString(), 7, nameof(DestroySelf));
        _animator.AddAnimatorEvent(EnemyStaEnum.Jump.ToString(), 2, nameof(BackToIdle));
        _animator.AddAnimatorEvent(EnemyStaEnum.Hurt.ToString(), 2, nameof(BackToIdle));
    }

    private void Start()
    {
        _rollLoopTimes = 2;
        _atkData = DB.PlayerHurtData[EnemyType.女猎手];
    }

    private void Update()
    {
        if (_eAttr.isDead) return;
        if (_rollAttackFly && _eAttr.isOnGround && _rigidbody2D.velocity.y <= 0f)
        {
            _rollAttackFly = false;
            _eAction.ChangeState(EnemyStaEnum.Idle); //着陆
        }

        if (_eAttr.isFlyingUp)
        {
            bool flag = maxFlyHeight > 0f && _eAttr.height >= maxFlyHeight;
            if (flag)
            {
                Vector2 currentSpeed = _eAttr.timeController.GetCurrentSpeed();
                currentSpeed.y = 0f;
                _eAttr.timeController.SetSpeed(currentSpeed);
            }

            if (_rigidbody2D.velocity.y <= 0f)
            {
                _eAttr.isFlyingUp = false;
                _eAction.ChangeState(EnemyStaEnum.Jump);
            }
        }

        if (_eAttr.checkHitGround && _eAttr.isOnGround)
        {
            _eAttr.checkHitGround = false;
            R.Effect.Generate(6, transform);
            if (_eAction.stateMachine.currentState == "Fall")
            {
                maxFlyHeight = 4f;
                _eAttr.timeController.SetSpeed(Vector2.up * 25f);
            }

            _eAction.ChangeState(EnemyStaEnum.Idle);
        }
    }

    public void TestOnChangeState(string sta)
    {
        _eAction.ChangeState(sta);
    }

    public void OnChangeState(EnemyStaEnum sta)
    {
        _eAction.ChangeState(sta.ToString());
    }

    public void PlaySound(int index)
    {
        R.Audio.PlayEffect(index, transform.position);
    }

    public void PlayMoveSound()
    {
        R.Audio.PlayEffect(moveSound[Random.Range(0, moveSound.Length)], transform.position);
    }

    public void PlayHitGroundSound()
    {
        R.Audio.PlayEffect(hitGroundSound[Random.Range(0, hitGroundSound.Length)], transform.position);
    }

    public void RollStart()
    {
        if (_rollOver)
        {
            _rollOver = false;
            _rollLoopTimes = 2;
        }
    }

    public void Atk2Start()
    {
        _rollOver = true;
        _eAttr.paBody = true;
    }

    /// <summary>
    /// 滚动结束
    /// </summary>
    public void RollOver()
    {
        _rollLoopTimes--;
        if (_rollLoopTimes <= 0)
        {
            if (_eAction.stateMachine.currentState == "DaoAtk6_2" && Random.Range(0, 100) < 50)
            {
                "DaoAtk6_2".Log();
                // _eAction.FaceToPlayer();
                // atk.atkId = Incrementor.GetNextId();
                // _eAction.AnimChangeState(StateEnum.DaoAtk2Ready, 1f);
            }
            else
            {
                _eAction.ChangeState(EnemyStaEnum.Idle);
            }

            _rollOver = true;
        }
    }

    public void Atk6_1Over()
    {
        if (Random.Range(0, 100) < 50)
        {
            "Atk6_1Over".Log();
            // _eAction.FaceToPlayer();
            // atk.atkId = Incrementor.GetNextId();
            // _eAction.AnimChangeState(StateEnum.DaoAtk6_2Ready, 1f);
        }
        else
        {
            _eAction.ChangeState(EnemyStaEnum.Idle);
        }
    }

    /// <summary>
    /// 碰撞到地面
    /// </summary>
    public void HitGround()
    {
        _rollAttackFly = false;
        _eAttr.checkHitGround = true;
    }

    /// <summary>
    /// 飞起
    /// </summary>
    public void FlyUp()
    {
        _rollAttackFly = false;
        _eAttr.isFlyingUp = true;
    }

    
    /// <summary>
    /// 设置攻击数据
    /// </summary>
    public void SetAtkData()
    {
        _atk.atkData = _atkData[_eAction.stateMachine.currentState];
    }

    /// <summary>
    /// 剑斗开始
    /// </summary>
    public void SwordFightStart()
    {
        _rollAttackFly = true;
    }

    /// <summary>
    /// 逃跑结束
    /// </summary>
    public void RunAwayEnd()
    {
        BackToIdle();
        if (!_eAction.IsInNormalState()) return;
        if (Random.Range(0, 2) == 0)
        {
            int dir = InputSetting.JudgeDir(transform.position, Player.position);
            _eAction.Attack3(dir);
        }
    }

    /// <summary>
    /// 起立
    /// </summary>
    public void GetUp()
    {
        if (_eAttr.isDead)
        {
            DieBlock();
            DestroySelf();
        }
        else
        {
            _eAction.ChangeState(EnemyStaEnum.Idle);
        }
    }

    /// <summary>
    /// 回到闲置状态
    /// </summary>
    public void BackToIdle()
    {
        if (_eAction.IsInWeakSta())
        {
            _eAttr.enterWeakMod = false;
        }

        _eAction.ChangeState(EnemyStaEnum.Idle);
    }

    /// <summary>
    /// 弱结束
    /// </summary>
    public void WeakOver()
    {
        if (_eAction.IsInWeakSta()) return;
        _eAction.ChangeState(EnemyStaEnum.Idle);
    }

    /// <summary>
    /// 用于事件回调
    /// </summary>
    public void DestroySelf()
    {
        RealDestroy();
    }

    /// <summary>
    /// 真正的摧毁
    /// </summary>
    private void RealDestroy()
    {
        Destroy(gameObject);
    }

    public void Shoot()
    {
        "Shoot".Log();
        // Transform transform = Instantiate(bullet);
        // EnemyBullet component = transform.GetComponent<EnemyBullet>();
        // component.damage = _eAttr.atk;
        // component.origin = gameObject;
        // transform.position = gunPos.position;
        // Vector2 from = gunPos.position - gunAssistant.position;
        // if (Vector2.Angle(from, Vector2.right) < 3f)
        // {
        //     from = Vector2.right;
        // }
        //
        // if (Vector2.Angle(from, -Vector2.right) < 3f)
        // {
        //     from = -Vector2.right;
        // }
        //
        // transform.GetComponent<Rigidbody2D>().velocity = from.normalized * 12f;
        // component.SetAtkData(_atkData["Bullet"]);
    }

    /// <summary>
    /// 针对玩家
    /// </summary>
    /// <returns></returns>
    public IEnumerator TargetingPlayer()
    {
        float angle = Vector2.Angle(Player.position + Vector3.up - transform.position, Vector2.up);
        if (angle >= 45f)
        {
            //gun.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Override;
            Vector3 startEuler = gun.localEulerAngles;
            float targetAngle = Mathf.Clamp(angle - 8f, 37f, 128f);
            for (int i = 0; i < 40; i++)
            {
                gun.localEulerAngles = Vector3.Lerp(startEuler, new Vector3(0f, 0f, targetAngle), i / 39f);
                yield return null;
            }
        }
    }

    /// <summary>
    /// TargetingRecover
    /// </summary>
    /// <returns></returns>
    public IEnumerator TargetingRecover()
    {
        // if (gun.GetComponent<SkeletonUtilityBone>().mode == SkeletonUtilityBone.Mode.Override)
        // {
        //     Vector3 startEuler = gun.localEulerAngles;
        //     int clips = (int)(startEuler.z / 2f);
        //     if (clips > 1)
        //     {
        //         for (int i = 0; i < clips; i++)
        //         {
        //             gun.localEulerAngles = Vector3.Lerp(startEuler, new Vector3(0f, 0f, 80f), i * 1f / (clips - 1));
        //             yield return null;
        //         }
        //     }
        // }
        //
        // gun.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Follow;
        yield break;
    }

    public void PlayHitGroundEffect()
    {
        R.Effect.Generate(40, transform, Vector3.zero, new Vector3(0f, 90 * _eAttr.faceDir, 0f));
    }

    /// <summary>
    /// 死亡效果
    /// </summary>
    public void DieBlock()
    {
        R.Effect.Generate(163, null, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f), Vector3.zero);
    }

    /// <summary>
    /// 死亡效果
    /// </summary>
    public void ChargingDieEffect()
    {
        // if (!R.Camera.IsInView(gameObject))return;
        // GameObject prefab = CameraEffectProxyPrefabData.GetPrefab(9);
        // Instantiate(prefab, _eAttr.bounds.center, Quaternion.identity);
    }

    public void Die2Spark()
    {
        R.Effect.Generate(144, transform, new Vector3(-(float)_eAttr.faceDir, 1.2f, LayerManager.ZNum.Fx), Vector3.zero);
    }

    public void Die2Explosion()
    {
        // if (!R.Camera.IsInView(gameObject))
        // {
        //     return;
        // }
        //
        // Instantiate<GameObject>(CameraEffectProxyPrefabData.GetPrefab(12));
        // R.Effect.Generate(9, transform, new Vector3(-(float)_eAttr.faceDir, 1.2f, LayerManager.ZNum.Fx), Vector3.zero);
    }

    public void OpenOnion()
    {
        _onion.Open(true, 0.7f, onionObj);
    }

    /// <summary>
    /// 最大飞行高度
    /// </summary>
    public float maxFlyHeight;


    /// <summary>
    /// 翻滚是否结束
    /// </summary>
    private bool _rollOver;

    private bool _rollAttackFly;

    private int _rollLoopTimes;

    [SerializeField] private int[] moveSound;

    [SerializeField] private int[] hitGroundSound;
}