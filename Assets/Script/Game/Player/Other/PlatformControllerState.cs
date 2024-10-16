/// <summary>
/// 平台控制器状态
/// </summary>
public class PlatformControllerState
{
    public bool HasCollisions => IsDetectedFront || IsDetectedBack || IsDetectedHead || IsDetectedGround;

    public bool JustGotGrounded => IsGrounded && !WasGroundedLastFrame;

    public void Update(bool front, bool back, bool head, bool ground)
    {
        WasGroundedLastFrame = IsGrounded;
        IsDetectedFront = front;
        IsDetectedBack = back;
        IsDetectedHead = head;
        IsDetectedGround = ground;
        IsGrounded = ground;
    }

    public override string ToString()
    {
        return $"(控制器: r:{IsDetectedFront} l:{IsDetectedBack} a:{IsDetectedHead} b:{IsDetectedGround} 下坡:{"无"} 上坡:{"无"} 斜角: {"无"}";
    }

    /// <summary>
    /// 是否检测到前面
    /// </summary>
    public bool IsDetectedFront;

    /// <summary>
    /// 是否检测到后面
    /// </summary>
    public bool IsDetectedBack;

    /// <summary>
    /// 是否检测到顶部
    /// </summary>
    public bool IsDetectedHead;

    /// <summary>
    /// 是否检测到地面
    /// </summary>
    public bool IsDetectedGround;

    /// <summary>
    /// 是否接触到地面
    /// </summary>
    public bool IsGrounded;

    /// <summary>
    /// 是否最后一帧接触到地面
    /// </summary>
    public bool WasGroundedLastFrame;
}