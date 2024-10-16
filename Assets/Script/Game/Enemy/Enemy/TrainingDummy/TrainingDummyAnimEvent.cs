using UnityEngine;

public class TrainingDummyAnimEvent : MonoBehaviour
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
    
    private void Awake()
    {
        GetComponent();
        //动画监听
        // _animator.AddAnimatorEvent(EnemyStaEnum.Atk2.ToString(), 0, nameof(SetAtkData), 53);
        // _animator.AddAnimatorEvent(EnemyStaEnum.Atk2.ToString(), 2, nameof(PlaySound), 53);
        // _animator.AddAnimatorEvent(EnemyStaEnum.Atk2.ToString(), 4, nameof(TestOnChangeState), EnemyStaEnum.Idle.ToString());
        // _animator.AddAnimatorEvent(EnemyStaEnum.AtkRemote.ToString(), 5, nameof(PlaySound), 204);
        // _animator.AddAnimatorEvent(EnemyStaEnum.AtkRemote.ToString(), 6, nameof(TestOnChangeState), EnemyStaEnum.Idle.ToString());
        // _animator.AddAnimatorEvent(EnemyStaEnum.Death.ToString(), 2, nameof(DieBlock));
        // _animator.AddAnimatorEvent(EnemyStaEnum.Death.ToString(), 7, nameof(DestroySelf));
        // _animator.AddAnimatorEvent(EnemyStaEnum.Jump.ToString(), 2, nameof(BackToIdle));
        // _animator.AddAnimatorEvent(EnemyStaEnum.Hurt.ToString(), 2, nameof(BackToIdle));
    }
    
    /// <summary>
    /// 最大飞行高度
    /// </summary>
    public float maxFlyHeight;

}