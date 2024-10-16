using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 场景进度加载
/// </summary>
public class UILoading : SMono<UILoading>
{
    public CanvasGroup Panel;
    [SerializeField] private UIProgressBar2 LoadImage;
    [SerializeField] private Text loadText;

    private void Awake()
    {
        Panel = transform.Find("Panel").GetComponent<UnityEngine.CanvasGroup>();
        loadText = transform.Find("Panel/loadText").GetComponent<UnityEngine.UI.Text>();
        LoadImage = transform.Find("Panel/Image/LoadImage").GetComponent<UIProgressBar2>();

        Panel.FadeTo(0);
    }

    /// <summary>
    /// 设置进度条
    /// </summary>
    public void SetProgressBar(float x)
    {
        LoadImage.SetProgressBar(x);
        loadText.text = $"{(int)(x * 100)}%";
    }
}