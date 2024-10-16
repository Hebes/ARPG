using DG.Tweening;
using UnityEngine;

/// <summary>
/// 音乐播放器动作
/// </summary>
public class MusicPlayerAction : BaseBehaviour
{
    /// <summary>
    /// 当前生命值
    /// </summary>
    public int currentHP
    {
        get => _currentHP;
        set => _currentHP = Mathf.Clamp(value, _limited, maxHP);
    }

    private Animator Anim
    {
        get
        {
            Animator result;
            if ((result = _anim) == null)
            {
                result = (_anim = GetComponent<Animator>());
            }
            return result;
        }
    }

    private void Start()
    {
        _limited = 0;
        currentHP = maxHP;
        ChangeState(State.Appear);
    }

    public void ChangeState(State state)
    {
        Anim.Play(state.ToString());
    }

    public void SetLimited(int value)
    {
        _limited = value;
    }

    public void StartShake()
    {
        Vector3 strength = new Vector3(0.2f, 0.2f, 0f);
        transform.DOShakePosition(1f, strength).SetLoops(-1);
    }

    public void StopShake()
    {
        transform.DOKill();
    }

    private Animator _anim;

    private int _currentHP;

    private int _limited;

    public int maxHP;

    public enum State
    {
        Appear,
        Play,
        ChangeMusic,
        Disappear,
        Blast
    }
}