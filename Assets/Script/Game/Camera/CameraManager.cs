using UnityEngine;

/// <summary>
/// 相机管理器
/// </summary>
public class CameraManager
{
    /// <summary>
    /// 在视野中
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public bool IsInView(GameObject go)
    {
        Vector3 vector = Camera.WorldToViewportPoint(go.transform.position);
        return vector.x > 0f && vector.x < 1f;
    }
    
    private Camera _camera;
    private GameObject _gameObject;
    private Transform _transform;
    
    private GameObject GameObject=>_gameObject ??= Camera.gameObject;
    public Transform Transform=>_transform ??= Camera.transform;
    private Camera Camera => _camera ??= Camera.main;
    public CameraController Controller => CameraController.I;
    public T GetComponent<T>() => GameObject.GetComponent<T>();
    public T AddComponent<T>() where T : Component => GameObject.AddComponent<T>();
}