using UnityEngine;

/// <summary>
/// 进度条
/// barImage的锚点anochors
/// Min     x 0 y 0.5
/// Max     x 0 y 0.5
/// point   x 0 y 0.5
/// 滑动 scale x 就有效果
/// </summary>
public class UIProgressBar2: MonoBehaviour
{
    [SerializeField] private RectTransform barImage;
    
    /// <summary>
    /// 设置进度条
    /// </summary>
    public void SetProgressBar(float x)
    {
        Vector3 v3Temp = barImage.localScale;
        v3Temp.x = x;
        barImage.localScale = v3Temp;
    }
}