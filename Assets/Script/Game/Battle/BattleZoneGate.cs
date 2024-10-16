using UnityEngine;

public class BattleZoneGate : BaseBehaviour
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        box = GetComponent<Collider2D>();
        box.enabled = false;
    }

    private void OnEnable()
    {
        GameEvent.HitWall.Register(HitWall);
    }

    private void OnDisable()
    {
        GameEvent.HitWall.UnRegister(HitWall);
    }

    /// <summary>
    /// 撞到墙
    /// </summary>
    /// <param name="name"></param>
    /// <param name="sender"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    private void HitWall(object udata)
    {
        HitWallArgs msg = udata as HitWallArgs;
        float x = msg.who.transform.position.x;
        float diameter = 1f;
        bool flag = MathfX.isInMiddleRange(x, transform.position.x, diameter);
        if (flag && isAppearing)
            animator.Play("Hit");
    }

    /// <summary>
    /// 出现
    /// </summary>
    public void Appear()
    {
        animator.Play("Open");
        isAppearing = true;
    }

    /// <summary>
    /// 消失
    /// </summary>
    public void DisAppear()
    {
        animator.Play("DisAppear");
        isAppearing = false;
    }

    public Transform positionCamera;

    public Transform playerLimitPos;

    public GateType type;

    private Animator animator;

    private float lastCameraX;

    private float lastPlayerX;

    private bool isAppearing;

    private Collider2D box;

    public enum GateType
    {
        Left = 1,
        Right,
        None = 0
    }
}