using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 相机控制器
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("是否跟随")] public bool IsFollowPivot = true;
    [Header("摄像机")] private Camera _camera;
    [Header("是否锁定")] [SerializeField] private bool _isLock;
    [Header("平滑时间")] [SerializeField] private float _smoothTime = 0.4f;


    [Header("手动偏移Y")] public float ManualOffsetY;
    [Header("视野")] private float _fieldOfView = 60f;
    [Header("当前速度")] private Vector3 _currentSpeed = Vector3.zero;

    private Transform _pivot;


    /// <summary>
    /// 玩家的位置
    /// </summary>
    public Transform Pivot
    {
        get => _pivot ?? R.Player.Transform;
        set => _pivot = value;
    }

    /// <summary>
    /// 移动摄像机
    /// </summary>
    public Transform MovableCamera => transform.parent;

    /// <summary>
    /// 最大检测精度
    /// </summary>
    private Rect MaxDetectRect => CameraRect(MaxDetectZ);

    /// <summary>
    /// 最小可见矩形
    /// </summary>
    private Rect MinVisibleRect => CameraRect(MinVisibleZ);

    /// <summary>
    /// 最小检测矩形
    /// </summary>
    private Rect MinDetectRect => CameraRect(MinDetectZ);

    /// <summary>
    /// 最大可见Z
    /// </summary>
    private float MaxVisibleZ => -12f + Fv2DeltaZ();

    /// <summary>
    /// 最大检测Z
    /// </summary>
    private float MaxDetectZ => MaxVisibleZ - -2f + Fv2DeltaZ();

    /// <summary>
    /// 最小可见Z
    /// </summary>
    private float MinVisibleZ => -8f + Fv2DeltaZ();

    /// <summary>
    /// 最小检测Z
    /// </summary>
    private float MinDetectZ => MinVisibleZ - -2f + Fv2DeltaZ();

    /// <summary>
    /// Z轴增量
    /// </summary>
    /// <returns></returns>
    private float Fv2DeltaZ()
    {
        float num = Mathf.Abs(-10f);
        return num * Mathf.Tan(0.4537856f) / Mathf.Tan(FieldOfView / 2f * 0.0174532924f) - num;
    }

    public float FieldOfView
    {
        get => _fieldOfView;
        set => _fieldOfView = Mathf.Clamp(value, 0f, 179f);
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void LateUpdate()
    {
        if (IsFollowPivot && Pivot)
        {
            UpdateCamera(Time.deltaTime);
        }
    }

    /// <summary>
    /// 更新摄像机位置
    /// </summary>
    /// <param name="deltaTime"></param>
    private void UpdateCamera(float deltaTime)
    {
        Vector3 target = CalculateFollowCameraPos();
        Vector3 pos = VU.SmoothDamp(MovableCamera.position, target,
            ref _currentSpeed, _smoothTime, float.PositiveInfinity, deltaTime);

        if (!_isLock)
            MovableCamera.position = CameraPositionClamp(pos);
    }

    /// <summary>
    /// 计算摄像机需要到玩家的位置
    /// </summary>
    /// <returns></returns>
    private Vector3 CalculateFollowCameraPos()
    {
        if (Pivot == null)
        {
            return Vector3.zero;
        }

        Vector3 position = Pivot.position; //玩家的位置
        Vector3? farestEnemyPosition = R.Enemy.GetFarestEnemyPosition(position, MaxDetectRect);
        Vector3 vector;
        if (farestEnemyPosition == null)
        {
            vector = position;
            vector.z = -10f;
        }
        else
        {
            vector = (position + farestEnemyPosition.Value) / 2f;
            float num;
            if (Mathf.Abs(((Vector2)position - (Vector2)farestEnemyPosition.Value).Slope()) > new Vector2(16f, 9f).Slope())
                num = Mathf.Abs(position.y - farestEnemyPosition.Value.y);
            else
                num = Mathf.Abs(position.x - farestEnemyPosition.Value.x) * 9f / 16f;
            vector.z = -(num / 2f) / Mathf.Tan(0.4537856f);
            vector.z += -6f;
            vector.z = Mathf.Clamp(vector.z, MaxVisibleZ, MinVisibleZ);
        }

        float distance = Physics2D.Raycast(vector, Vector2.down, 10f, LayerManager.GroundMask).distance;
        float num2 = (ManualOffsetY + 2.4f) * (MovableCamera.position.z / -10f);
        vector += Vector3.up * (distance <= num2 ? num2 - distance : 0f);
        vector.z += Fv2DeltaZ();
        return vector;
    }

    /// <summary>
    /// 摄像机位置卡箍
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private Vector3 CameraPositionClamp(Vector3 pos)
    {
        float num = -pos.z * Mathf.Tan(0.4537856f);
        Vector2 vector = new Vector2(num * _camera.aspect, num);
        Rect cameraRange = GameArea.CameraRange;
        float num2 = cameraRange.width / (-2f * Mathf.Tan(FieldOfView / 2f) * _camera.aspect);
        cameraRange.xMin += vector.x;
        cameraRange.xMax -= vector.x;
        cameraRange.yMin += vector.y;
        cameraRange.yMax -= vector.y;
        Vector3 result;
        result.x = cameraRange.xMin >= cameraRange.xMax ? cameraRange.center.x : Mathf.Clamp(pos.x, cameraRange.xMin, cameraRange.xMax);
        result.y = cameraRange.yMin >= cameraRange.yMax ? cameraRange.center.y : Mathf.Clamp(pos.y, cameraRange.yMin, cameraRange.yMax);
        result.z = pos.z;
        return result;
    }

    /// <summary>
    /// 相机矫正
    /// </summary>
    /// <param name="z"></param>
    /// <returns></returns>
    private Rect CameraRect(float z)
    {
        Vector2 vector;
        vector.y = -z * Mathf.Tan(0.4537856f);
        vector.x = vector.y * _camera.aspect;
        return new Rect
        {
            min = new Vector2(MovableCamera.position.x - vector.x, MovableCamera.position.y - vector.y),
            max = new Vector2(MovableCamera.position.x + vector.x, MovableCamera.position.y + vector.y)
        };
    }

    /// <summary>
    /// 切换场景后相机复位位置
    /// </summary>
    public void CameraResetPostionAfterSwitchScene()
    {
        IsFollowPivot = true;
        Vector3 vector = CameraPositionClamp(Pivot.position);
        MovableCamera.position = new Vector3(vector.x, vector.y, MovableCamera.position.z);
        UpdateCamera(1000f);
    }

    /// <summary>
    /// 相机移动，秒
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="second"></param>
    /// <param name="ease"></param>
    /// <returns></returns>
    public YieldInstruction CameraMoveTo(Vector3 pos, float second, Ease ease = Ease.Linear)
    {
        if (!_isLock)
        {
            IsFollowPivot = false;
            _isLock = true;
            pos.z = MovableCamera.position.z;
            KillTweening();
            return MovableCamera.DOMove(CameraPositionClamp(pos), second).SetEase(ease).OnComplete(delegate { CamereMoveFinished(false); }).WaitForCompletion();
        }

        return null;
    }

    /// <summary>
    /// 相机移动，速度
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="speed"></param>
    /// <param name="canReturn"></param>
    /// <param name="type"></param>
    public void CameraMoveToBySpeed(Vector3 pos, float speed, bool canReturn = false, Ease type = Ease.Linear)
    {
        if (!_isLock)
        {
            IsFollowPivot = false;
            _isLock = true;
            pos.z = MovableCamera.position.z;
            if (canReturn)
            {
                Vector3[] path =
                {
                    CameraPositionClamp(pos),
                    MovableCamera.position
                };
                MovableCamera.DOPath(path, speed).SetSpeedBased(true).SetEase(type).OnComplete(delegate { CamereMoveFinished(true); });
            }
            else
            {
                MovableCamera.DOMove(CameraPositionClamp(pos), speed).SetSpeedBased(true).SetEase(type).OnComplete(delegate { CamereMoveFinished(false); });
            }
        }
    }

    /// <summary>
    /// 摄像机移动完成
    /// </summary>
    /// <param name="follow"></param>
    private void CamereMoveFinished(bool follow)
    {
        _isLock = false;
        IsFollowPivot = follow;
    }

    /// <summary>
    /// 相机变焦
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="second"></param>
    /// <param name="deltaZ"></param>
    public void CameraZoom(Vector3 pos, float second, float deltaZ = 3f)
    {
        if (!_isLock)
        {
            _isLock = true;
            IsFollowPivot = false;
            float num = (1f - Mathf.Tan(0.4537856f) / Mathf.Tan(0.0174532924f * FieldOfView / 2f)) * 7f;
            pos.z = -10f + deltaZ + num;
            MovableCamera.DOMove(CameraPositionClamp(pos), second).SetEase(Ease.Linear).OnComplete(ZoomFinished);
        }
    }

    /// <summary>
    /// 变焦完成
    /// </summary>
    public void ZoomFinished()
    {
        _isLock = false;
    }

    /// <summary>
    /// 相机变焦完成
    /// </summary>
    public void CameraZoomFinished()
    {
        _pivot = R.Player.Transform;
        IsFollowPivot = true;
    }

    /// <summary>
    /// 相机抖动
    /// </summary>
    /// <param name="second">秒，持续时间</param>
    /// <param name="strength">强烈成都</param>
    /// <param name="type">晃动类型</param>
    /// <param name="isLoop">是否循环</param>
    public void CameraShake(float second, ShakeTypeEnum type, float strength = 0.2f, bool isLoop = false)
    {
        "相机抖动".Log();
        if (Math.Abs(second) < 0.01f) return;
        Vector3 strength2 = new Vector3(1f, 1f, 0f) * strength;
        switch (type)
        {
            case ShakeTypeEnum.Vertical:
                strength2.x = 0f;
                break;
            case ShakeTypeEnum.Horizon:
                strength2.y = 0f;
                break;
            case ShakeTypeEnum.Rect: break;
            default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        KillTweening();
        transform.DOShakePosition(second, strength2, 100).SetLoops(isLoop ? -1 : 1).OnKill(OnShakeFinished).OnComplete(OnShakeFinished);
    }

    /// <summary>
    /// 相机晃动完成
    /// </summary>
    private void OnShakeFinished()
    {
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 抖动杀死
    /// </summary>
    public void KillTweening()
    {
        transform.DOKill();
    }

    /// <summary>
    /// 场景切换
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="loadSceneMode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        ManualOffsetY = 0f;
        _fieldOfView = 52f;
        MovableCamera.position = MovableCamera.position.SetZ(-10f);
    }
}

/// <summary>
/// 晃动类型
/// </summary>
public enum ShakeTypeEnum
{
    /// <summary>
    /// 垂直
    /// </summary>
    Vertical = 0,

    /// <summary>
    /// 水平
    /// </summary>
    Horizon,

    /// <summary>
    /// 矩形
    /// </summary>
    Rect
}