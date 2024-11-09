using UnityEngine;

/// <summary>
/// 相机管理器
/// </summary>
public class CameraManager
{
    public bool IsInView(GameObject go)
    {
        float x = Camera.WorldToViewportPoint(go.transform.position).x;
        return x is > 0f and < 1f;
    }

    public T AddComponent<T>() where T : Component
    {
        return GameObject.AddComponent<T>();
    }

    private Camera _camera;
    private GameObject _gameObject;
    private Transform _transform;
    private CameraEffect _cameraEffect;
    private CameraController _cameraController;

    private Camera Camera => _camera ??= Camera.main;
    private GameObject GameObject => _gameObject ??= Camera.gameObject;
    public Transform Transform => _transform ??= Camera.transform;
    public CameraController CameraController => _cameraController ?? Camera.transform.FindComponent<CameraController>();
    public CameraEffect CameraEffect => _cameraEffect ?? Camera.transform.FindComponent<CameraEffect>();
}