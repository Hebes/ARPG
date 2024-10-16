using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraSetting : SingletonMono<CameraSetting>
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = R.Settings.FPS;
        base.GetComponent<Camera>().transparencySortMode = TransparencySortMode.Orthographic;
    }
}