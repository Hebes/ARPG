using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 摄像机的单例类,请必须拖拽上去
/// </summary>
public class UICamera : SMono<UICamera>
{
    private void Awake()
    {
        Camera uiCamera = GetComponent<Camera>();
        var cameraData = Camera.main.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(uiCamera);
    }
}