using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 平台运动
/// </summary>
public class PlatformMovement : MonoBehaviour
{
    [Header("一个方向的层级")] public LayerMask m_oneWayLayer = 0;
    [Header("天花板层级")] public LayerMask m_Ceiling = 0;

    [Header("射线检测到的地面")] public CharacterRay2D[] m_RaysGround;
    [Header("射线检测到的上面")] public CharacterRay2D[] m_RaysHead;
    [Header("射线检测到的正面")] public CharacterRay2D[] m_RaysFront;
    [Header("射线检测到的后面")] public CharacterRay2D[] m_RaysBack;

    [Header("速率")] public Vector2 velocity = Vector2.zero;
    [Header("当前位置")] public Vector2 position;
    [Header("当前位置")] private Vector2 m_curPos;

    [Header("是否是右方")] public bool m_faceRight = true;
    [Header("是否显示射线")] private bool m_DrawRays = true;
    [Header("是否在运行")] public bool isKinematic;
    [Header("重力是否有效")] public bool isGravityActive = true;
    [Header("检测碰撞")] public bool detecteCollided = true;

    [Header("射线检测结果")] private RaycastHit2D[] m_rayResults = new RaycastHit2D[8];
    [Header("最大误差")] private float m_PosErrorMaxVel = 100f;
    [Header("允许渗透")] private float m_allowedPenetration = 0.01f;
    [Header("到地面的距离")] private float m_distToGround;
    [Header("最后一帧的位置")] private Vector2 lastFramePosition;
    [Header("比例偏移")] private Vector2 m_scaleOffset = Vector2.zero;
    [Header("比例")] private Vector2 m_scale = Vector2.one;
    [Header("地面正常位置")] private Vector2 m_groundNormal;
    [Header("地面正常位置LS")] private Vector2 m_groundNormalLs;
    [Header("地面的物体")] private GameObject m_groundGameObject;
    [Header("忽略碰撞的列表")] private List<IgnoreCollider> m_ignoreColliders = new List<IgnoreCollider>(8);
    [Header("Rigidbody2D")] [SerializeField] private Rigidbody2D rigid;


    public readonly PlatformControllerState State = new PlatformControllerState();
    public Vector2 ComputeVelocityOnGround() => ProjectOnGround(velocity);
    public Vector2 GetGroundNormal() => m_groundNormal;
    public Vector2 GetGroundNormalLs() => m_groundNormalLs;
    private float deltaTime => Time.fixedDeltaTime;

    /// <summary>
    /// 获取到地面的距离
    /// </summary>
    /// <returns></returns>
    public float GetDistanceToGround() => m_distToGround;

    /// <summary>
    /// 获取地面物体
    /// </summary>
    /// <returns></returns>
    public GameObject GetGroundGameObject() => m_groundGameObject;

    private LayerMask m_rayLayer
    {
        get
        {
            
            string currentState = R.Player.StateMachine.currentState;
            if (currentState.IsInArray(PlayerAction.FlashAttackSta) && R.Player.Attribute.flashLevel == 3)
                return LayerManager.GroundMask | LayerManager.WallMask; 
            return LayerManager.GroundMask | LayerManager.WallMask | LayerManager.ObstacleMask;
        }
    }

    /// <summary>
    /// 2D位置
    /// </summary>
    private Vector2 Position2D
    {
        get => rigid.position;
        set
        {
            Vector3 vector = value;
            vector.z = transform.position.z;
            transform.position = vector;
            rigid.position = vector;
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        m_oneWayLayer = LayerManager.OneWayGroundMask;
        m_Ceiling = LayerManager.CeilingMask;

        //初始化射线检测
        m_RaysGround = new[]//地面
        {
            new CharacterRay2D { m_position = new Vector2(-0.27f, 0.2f), m_penetration = 0.2f, m_extraDistance = 0.1f, },
            new CharacterRay2D { m_position = new Vector2(0f, 0.2f), m_penetration = 0.2f, m_extraDistance = 0.1f, },
            new CharacterRay2D { m_position = new Vector2(0.27f, 0.2f), m_penetration = 0.2f, m_extraDistance = 0.1f, },
        };
        m_RaysHead = new[]//上面
        {
            new CharacterRay2D { m_position = new Vector2(-0.27f, 0.8f), m_penetration = 0.2f, m_extraDistance = 0.1f, },
            new CharacterRay2D { m_position = new Vector2(0f, 0.8f), m_penetration = 0.2f, m_extraDistance = 0.1f, },
            new CharacterRay2D { m_position = new Vector2(0.27f, 0.8f), m_penetration = 0.2f, m_extraDistance = 0.1f, },
        };
        m_RaysFront = new[]//正面
        {
            new CharacterRay2D { m_position = new Vector2(0f, 0.1f), m_penetration = 0.5f, m_extraDistance = 0.1f, },
            new CharacterRay2D { m_position = new Vector2(0f, 0.5f), m_penetration = 0.5f, m_extraDistance = 0.1f, },
            new CharacterRay2D { m_position = new Vector2(0f, 0.9f), m_penetration = 0.5f, m_extraDistance = 0.1f, },
        };
        m_RaysBack = new[]//后面
        {
            new CharacterRay2D { m_position = new Vector2(0f, 0.1f), m_penetration = 0.5f, m_extraDistance = 0.1f, },
            new CharacterRay2D { m_position = new Vector2(0f, 0.5f), m_penetration = 0.5f, m_extraDistance = 0.1f, },
            new CharacterRay2D { m_position = new Vector2(0f, 0.9f), m_penetration = 0.5f, m_extraDistance = 0.1f, },
        };

        ClearRuntimeValues();
    }

    private void FixedUpdate()
    {
        RefreshIgnoreColliders();
        Move();
        position = Position2D;
        lastFramePosition = position;
    }

    /// <summary>
    /// 移动
    /// </summary>
    private void Move()
    {
        //移动
        Vector2 positionOffset = position - lastFramePosition; //偏移
        Vector2 moveOffset;
        if (isKinematic)
        {
            moveOffset = Vector2.zero;
        }
        else
        {
            UpdateMoveGround();
            moveOffset = velocity * deltaTime;
        }

        if (detecteCollided)
        {
            UpdateMovePosition(positionOffset);
            UpdateMoveVelocity(moveOffset);
        }

        UpdateState();
        UpdateMoveGroundFriction();
        if (m_DrawRays)
        {
            Vector3 b = rigid.velocity;
            Debug.DrawLine(transform.position, transform.position + b, Color.green);
        }
    }

    /// <summary>
    /// 更新移动位置
    /// </summary>
    /// <param name="positionOffset"></param>
    private void UpdateMovePosition(Vector2 positionOffset)
    {
        m_curPos = Position2D;
        Vector2 zero = Vector2.zero;

        ClearRaysCollisionInfo(m_RaysGround);
        ClearRaysCollisionInfo(m_RaysHead);
        ClearRaysCollisionInfo(m_RaysFront);
        ClearRaysCollisionInfo(m_RaysBack);

        bool flag = MoveVertical(ref positionOffset, false, true, ref zero);
        bool flag2 = MoveHorizontal(ref positionOffset, true, true, ref zero);
        MoveHorizontal(ref positionOffset, false, !flag2, ref zero);
        MoveVertical(ref positionOffset, true, !flag, ref zero);
        Position2D = m_curPos;
    }

    /// <summary>
    /// 更新垂直移动
    /// </summary>
    /// <param name="moveOffset"></param>
    private void UpdateMoveVelocity(Vector2 moveOffset)
    {
        m_curPos = Position2D;
        Vector2 position2D = Position2D;
        Vector2 zero = Vector2.zero;

        ClearRaysCollisionInfo(m_RaysGround);
        ClearRaysCollisionInfo(m_RaysHead);
        ClearRaysCollisionInfo(m_RaysFront);
        ClearRaysCollisionInfo(m_RaysBack);

        bool flag = MoveVertical(ref moveOffset, false, true, ref zero);
        bool flag2 = MoveHorizontal(ref moveOffset, true, true, ref zero);
        MoveHorizontal(ref moveOffset, false, !flag2, ref zero);
        MoveVertical(ref moveOffset, true, !flag, ref zero);
        if (deltaTime > Mathf.Epsilon) //这个常量表示在浮点数表示中比 0 大的最小正数值
        {
            rigid.velocity = (m_curPos - position2D) / deltaTime;
            velocity = (m_curPos - position2D - zero) / deltaTime;
        }
        else
        {
            rigid.velocity = Vector2.zero;
            velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// 更新地上的移动
    /// </summary>
    private void UpdateMoveGround()
    {
        if (isGravityActive)
        {
            float num = 100f;
            float num2 = State.IsDetectedGround ? GetDistanceToGround() : float.MaxValue; //如果检测到地面,获取到地面的距离
            float num3 = 0.01f;
            if (num2 > num3)
            {
                // Physics2D.gravity 获取当前重力值 rigid.gravityScale 属性设置为0.5，表示 Rigidbody2D 对象受到全局重力的一半影响。
                float num4 = (Physics2D.gravity * (deltaTime * rigid.gravityScale)).y * 1.2f;
                if (velocity.y > -num)
                    velocity.y = Mathf.Max(-num, velocity.y + num4); //获取两个数值之间的最大值
            }
        }
    }

    /// <summary>
    /// 更新状态
    /// </summary>
    private void UpdateState()
    {
        bool front = GetCollidedRay(m_RaysFront) != null;
        bool back = GetCollidedRay(m_RaysBack) != null;
        bool head = GetCollidedRay(m_RaysHead) != null;
        bool ground = GetCollidedRay(m_RaysGround) != null;
        State.Update(front, back, head, ground);
    }

    /// <summary>
    /// 更新移动地面摩擦力
    /// </summary>
    private void UpdateMoveGroundFriction()
    {
        CharacterRay2D collidedRay = GetCollidedRay(m_RaysGround);
        if (State.IsGrounded && collidedRay != null)
        {
            float num = 1;
            float b = Mathf.Abs(velocity.x) - num * deltaTime * 10f;
            velocity.x = Mathf.Max(0f, b) * Mathf.Sign(velocity.x);
        }
    }

    /// <summary>
    /// 获取射线穿透的物体
    /// </summary>
    /// <param name="rays"></param>
    /// <returns></returns>
    private CharacterRay2D GetCollidedRay(CharacterRay2D[] rays)
    {
        foreach (CharacterRay2D characterRay2D in rays)
        {
            if (characterRay2D.m_hitInfo.m_collider == null) continue;
            if (characterRay2D.m_hitInfo.m_penetration > characterRay2D.m_extraDistance) continue;
            return characterRay2D;
        }

        return null;
    }


    /// <summary>
    /// 清除运行时值
    /// </summary>
    private void ClearRuntimeValues()
    {
        velocity = Vector2.zero;
        m_curPos = Vector2.zero;
        m_groundNormal = Vector2.up;
        m_groundNormalLs = Vector2.up;
        m_distToGround = 0f;
        m_scaleOffset = Vector2.zero;
        m_scale = Vector2.one;
        m_groundGameObject = null;
        m_ignoreColliders.Clear();
        position = Position2D; //战斗场景玩家的初始位置设置
        lastFramePosition = position;
        isKinematic = false;
    }

    /// <summary>
    /// 刷新忽略碰撞器
    /// </summary>
    private void RefreshIgnoreColliders()
    {
        for (int i = 0; i < m_ignoreColliders.Count; i++)
            m_ignoreColliders[i].m_curTime += deltaTime;

        for (int j = 0; j < m_ignoreColliders.Count; j++)
        {
            IgnoreCollider ignoreCollider = m_ignoreColliders[j];
            if (ignoreCollider.m_duration > 0f && ignoreCollider.m_curTime > ignoreCollider.m_duration)
                m_ignoreColliders.RemoveAt(j);
        }
    }

    /// <summary>
    /// 计算地面速度
    /// </summary>
    /// <param name="proj"></param>
    /// <returns></returns>
    private Vector2 ProjectOnGround(Vector2 proj)
    {
        Vector2 vector = Vector2.right;
        float f = VU.V2Dot(vector, m_groundNormal);
        if (Mathf.Abs(f) > 1.401298E-45f)
        {
            Vector3 rhs = VU.Cross(vector, m_groundNormal);
            vector = VU.Cross(m_groundNormal, rhs);
            vector.Normalize();
        }

        return vector * VU.V2Dot(proj, vector);
    }


    /// <summary>
    /// 清除指定的碰撞检测
    /// </summary>
    /// <param name="rays"></param>
    private void ClearRaysCollisionInfo(CharacterRay2D[] rays)
    {
        foreach (CharacterRay2D characterRay2D in rays)
            characterRay2D.m_hitInfo.m_collider = null;
    }

    /// <summary>
    /// 构建射线层
    /// </summary>
    /// <returns></returns>
    private LayerMask BuildRayLayer()
    {
        LayerMask rayLayer = m_rayLayer;
        if (m_oneWayLayer != 0)
            rayLayer.value |= m_oneWayLayer;
        if (m_Ceiling != 0)
            rayLayer.value |= m_Ceiling;
        return rayLayer;
    }

    /// <summary>
    /// 获取射线的位置在世界坐标
    /// </summary>
    /// <param name="ray"></param>
    /// <returns></returns>
    private Vector2 GetRayPositionWs(CharacterRay2D ray)
    {
        Vector2 temp = VU.V2Scale(ray.m_position - m_scaleOffset, m_scale) + m_scaleOffset;
        return transform.TransformPoint(temp); //将一个点从局部坐标系转换到世界坐标系或其他坐标系中 
    }


    /// <summary>
    /// 添加忽略碰撞器
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="duration"></param>
    private void AddIgnoreCollider(Collider2D collider, float duration)
    {
        IgnoreCollider ignoreCollider = new IgnoreCollider(collider, duration, 0f);
        m_ignoreColliders.Add(ignoreCollider);
    }

    /// <summary>
    /// 忽略单向地面
    /// </summary>
    /// <returns></returns>
    public bool IgnoreOnOneWayGround()
    {
        bool result = false;
        foreach (CharacterRay2D characterRay2D in m_RaysGround)
        {
            if (characterRay2D.m_hitInfo.m_collider != null &&
                characterRay2D.m_hitInfo.m_penetration < 0.1 &&
                IsOneWayGround(characterRay2D.m_hitInfo.m_collider))
            {
                result = true;
                AddIgnoreCollider(characterRay2D.m_hitInfo.m_collider, 0.5f);
            }
        }

        return result;
    }

    /// <summary>
    /// 单向接地吗?
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    private bool IsOneWayGround(Collider2D collider)
    {
        int num = 1 << collider.gameObject.layer;
        return (m_oneWayLayer.value & num) != 0;
    }

    /// <summary>
    /// 是否是天花板
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    private bool IsCeiling(Collider2D collider)
    {
        int num = 1 << collider.gameObject.layer;
        return (m_Ceiling.value & num) != 0;
    }

    /// <summary>
    /// 是否忽略碰撞器
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    private bool IsIgnoredCollider(Collider2D collider)
    {
        for (int i = 0; i < m_ignoreColliders.Count; i++)
        {
            if (m_ignoreColliders[i].m_collider == collider)
                return true;
        }

        return false;
    }

    /// <summary>
    /// 是否能垂直移动
    /// </summary>
    /// <param name="moveOffset">移动的偏移位置</param>
    /// <param name="bDown">是否朝下</param>
    /// <param name="bCorrectPos">位置是否正确</param>
    /// <param name="m_posError">错误位置</param>
    /// <returns></returns>
    private bool MoveVertical(ref Vector2 moveOffset, bool bDown, bool bCorrectPos, ref Vector2 m_posError)
    {
        bool flag = false;
        float num = 0f;
        float num4 = float.MaxValue;
        Vector2 vector = bDown ? -transform.up : transform.up;
        float num2 = VU.V2Dot(moveOffset, vector);
        float num3 = Mathf.Abs(num2);
        bool flag2 = num2 > 0f;
        Vector2 position2D = Position2D;
        Position2D = m_curPos;
        if (bDown)
        {
            State.IsDetectedGround = false;
            m_groundGameObject = null;
            m_distToGround = 0f;
        }

        CharacterRay2D[] temp = bDown ? m_RaysGround : m_RaysHead;
        foreach (CharacterRay2D characterRay2D in temp)
        {
            //设置检测范围的最大值
            float num5 = characterRay2D.m_penetration * Mathf.Abs(transform.lossyScale.y) * m_scale.y;
            float num6 = num5 + Mathf.Max(characterRay2D.m_extraDistance, 0.5f);
            Vector2 rayPositionWs = GetRayPositionWs(characterRay2D); //获取射线的位置在世界坐标
            if (flag2)
                num6 += Mathf.Abs(num2);
            if (m_DrawRays)
                Debug.DrawLine(rayPositionWs, rayPositionWs + vector * num6);

            //射线的起点 射线的方向  存储射线检测结果的数组 射线的最大长度 指定哪些层可以被射线检测到
            int num7 = Physics2D.RaycastNonAlloc(rayPositionWs, vector, m_rayResults, num6, BuildRayLayer());
            if (num7 > 0)
            {
                float num8 = float.MaxValue;
                for (int j = 0; j < num7; j++)
                {
                    RaycastHit2D raycastHit2D = m_rayResults[j];
                    if (!(raycastHit2D.rigidbody == rigid))
                    {
                        if (raycastHit2D.collider != null)
                        {
                            if (raycastHit2D.collider.isTrigger || IsIgnoredCollider(raycastHit2D.collider))
                                goto IL_3D8;
                            if ((!bDown || raycastHit2D.normal.y < 0f) && IsOneWayGround(raycastHit2D.collider))
                                goto IL_3D8;
                            if ((bDown || raycastHit2D.normal.y > 0f) && IsCeiling(raycastHit2D.collider))
                                goto IL_3D8;
                        }

                        if (raycastHit2D.fraction != 0f)//沿着射线的距离的分数
                        {
                            Vector2 vector2 = transform.InverseTransformDirection(raycastHit2D.normal);
                            if ((!bDown || vector2.y > 0f) && (bDown || vector2.y < 0f))
                            {
                                if (m_DrawRays)
                                    Debug.DrawLine(raycastHit2D.point, raycastHit2D.point + raycastHit2D.normal * 0.1f, Color.red);

                                float num9 = raycastHit2D.fraction * num6;
                                float num10 = num9 - num5;
                                if (num10 + m_allowedPenetration < num)
                                {
                                    flag = true;
                                    num = num10;
                                }

                                if (flag2)
                                    num3 = Mathf.Max(0f, Mathf.Min(num3, num9 - num5));

                                if (bDown)
                                {
                                    if (num10 < num4)
                                    {
                                        m_groundNormal = raycastHit2D.normal;
                                        num4 = num10;
                                        m_groundGameObject = raycastHit2D.collider.gameObject;
                                        m_distToGround = num10;
                                    }
                                }

                                if (raycastHit2D.fraction < num8)
                                {
                                    num8 = raycastHit2D.fraction;
                                    characterRay2D.m_hitInfo.m_collider = raycastHit2D.collider;
                                    characterRay2D.m_hitInfo.m_normal = raycastHit2D.normal;
                                    characterRay2D.m_hitInfo.m_penetration = num10;
                                }
                            }
                        }
                    }

                    IL_3D8: ;
                }
            }
        }

        Vector2 curPos = m_curPos;
        if (flag2)
        {
            moveOffset += vector * (num3 - num2);
            m_curPos += vector * num3;
        }

        if (flag && bCorrectPos)
        {
            Vector2 b = Mathf.Max(num, -m_PosErrorMaxVel * deltaTime) * vector;
            m_curPos += b;
            m_posError += b;
        }

        if (bDown && State.IsDetectedGround)
        {
            m_groundNormalLs = transform.InverseTransformDirection(m_groundNormal);
            m_distToGround += Vector2.Dot(curPos - m_curPos, vector);
        }

        if (bDown)
        {
            for (int k = 0; k < m_RaysGround.Length; k++)
            {
                CharacterRay2D characterRay2D2 = m_RaysGround[k];
                if (characterRay2D2.m_hitInfo.m_collider)
                {
                    float num11 = Vector2.Dot(curPos - m_curPos, vector);
                    CharacterRay2D characterRay2D3 = characterRay2D2;
                    characterRay2D3.m_hitInfo.m_penetration += num11;
                }
            }
        }
        else
        {
            for (int l = 0; l < m_RaysHead.Length; l++)
            {
                CharacterRay2D characterRay2D4 = m_RaysHead[l];
                if (characterRay2D4.m_hitInfo.m_collider)
                {
                    CharacterRay2D characterRay2D5 = characterRay2D4;
                    characterRay2D5.m_hitInfo.m_penetration = characterRay2D5.m_hitInfo.m_penetration + Vector2.Dot(curPos - m_curPos, vector);
                }
            }
        }

        Position2D = position2D;
        return flag;
    }

    /// <summary>
    /// 是否可以水平移动
    /// </summary>
    /// <param name="moveOffset">移动偏移</param>
    /// <param name="bFront">是否是前面</param>
    /// <param name="bCorrectPos">是否可以改正位置</param>
    /// <param name="m_posError">错误位置</param>
    /// <returns></returns>
    private bool MoveHorizontal(ref Vector2 moveOffset, bool bFront, bool bCorrectPos, ref Vector2 m_posError)
    {
        bool flag = false;
        float num = 0f;
        bool flag2 = (m_faceRight && bFront) || (!m_faceRight && !bFront);
        Vector2 vector = flag2 ? transform.right : -transform.right;
        float num2 = VU.V2Dot(moveOffset, vector);
        bool flag3 = num2 > 0f;
        float num3 = Mathf.Abs(num2);
        Vector2 position2D = Position2D;
        Position2D = m_curPos;
        CharacterRay2D[] characterRayTemp = bFront ? m_RaysFront : m_RaysBack;
        foreach (CharacterRay2D characterRay2D in characterRayTemp)
        {
            //设置检测范围的最大值
            float num4 = characterRay2D.m_penetration * Mathf.Abs(transform.lossyScale.x) * m_scale.x;
            float num5 = num4 + characterRay2D.m_extraDistance;
            if (flag3) num5 += Mathf.Abs(num2);
            Vector2 rayPositionWs = GetRayPositionWs(characterRay2D); //获取射线的位置在世界坐标
            if (m_DrawRays) Debug.DrawLine(rayPositionWs, rayPositionWs + vector * num5);
            //射线的起点 射线的方向  存储射线检测结果的数组 射线的最大长度 指定哪些层可以被射线检测到
            int num6 = Physics2D.RaycastNonAlloc(rayPositionWs, vector, m_rayResults, num5, BuildRayLayer());
            if (num6 > 0)
            {
                float num7 = float.MaxValue;
                for (int j = 0; j < num6; j++)
                {
                    RaycastHit2D raycastHit2D = m_rayResults[j];
                    if (!(raycastHit2D.rigidbody == rigid))
                    {
                        if (raycastHit2D.collider == null ||
                            (!raycastHit2D.collider.isTrigger && !IsIgnoredCollider(raycastHit2D.collider)))
                        {
                            float num8 = raycastHit2D.fraction * num5;
                            float num9 = num8 - num4;
                            //APMaterial material = null;
                            if (raycastHit2D.collider != null)
                            {
                                //material = raycastHit2D.collider.GetComponent<APMaterial>();
                                if (IsOneWayGround(raycastHit2D.collider))
                                    goto IL_384;
                            }

                            if (raycastHit2D.fraction != 0f)
                            {
                                Vector2 vector2 = transform.InverseTransformDirection(raycastHit2D.normal);
                                if ((!flag2 || vector2.x <= 0f) && (flag2 || vector2.x >= 0f))
                                {
                                    if (m_DrawRays)
                                        Debug.DrawLine(raycastHit2D.point, raycastHit2D.point + raycastHit2D.normal * 0.1f, Color.red);

                                    if (bFront) //是否是前面
                                        State.IsDetectedFront = true;
                                    else
                                        State.IsDetectedBack = true;

                                    if (num9 + m_allowedPenetration < num)
                                    {
                                        flag = true;
                                        num = num9;
                                    }

                                    if (raycastHit2D.fraction < num7)
                                    {
                                        num7 = raycastHit2D.fraction;
                                        characterRay2D.m_hitInfo.m_collider = raycastHit2D.collider;
                                        characterRay2D.m_hitInfo.m_normal = raycastHit2D.normal;
                                        characterRay2D.m_hitInfo.m_penetration = num9;
                                        //characterRay2D.m_hitInfo.m_material = material;
                                    }

                                    if (flag3)
                                    {
                                        num3 = Mathf.Min(num3, num8 - num4);
                                        num3 = Mathf.Max(0f, num3);
                                    }
                                }
                            }
                        }
                    }

                    IL_384: ;
                }
            }
        }

        Vector2 curPos = m_curPos;
        if (flag3)
        {
            moveOffset += vector * (num3 - num2);
            m_curPos += vector * num3;
        }

        if (flag && bCorrectPos)
        {
            Vector2 b = Mathf.Max(num, -m_PosErrorMaxVel * deltaTime) * vector;
            m_curPos += b;
            m_posError += b;
        }

        if (bFront)
        {
            for (int k = 0; k < m_RaysFront.Length; k++)
            {
                CharacterRay2D characterRay2D2 = m_RaysFront[k];
                if (characterRay2D2.m_hitInfo.m_collider)
                {
                    CharacterRay2D characterRay2D3 = characterRay2D2;
                    characterRay2D3.m_hitInfo.m_penetration += Vector2.Dot(curPos - m_curPos, vector);
                }
            }
        }
        else
        {
            for (int l = 0; l < m_RaysBack.Length; l++)
            {
                CharacterRay2D characterRay2D4 = m_RaysBack[l];
                if (characterRay2D4.m_hitInfo.m_collider)
                {
                    CharacterRay2D characterRay2D5 = characterRay2D4;
                    characterRay2D5.m_hitInfo.m_penetration = characterRay2D5.m_hitInfo.m_penetration + Vector2.Dot(curPos - m_curPos, vector);
                }
            }
        }

        Position2D = position2D;
        return flag;
    }
}