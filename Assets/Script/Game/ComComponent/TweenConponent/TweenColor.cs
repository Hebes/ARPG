using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 颜色渐变
/// </summary>
public class TweenColor : MonoBehaviour
{
    [SerializeField] private Image _image;
    private float duration = 0.3f;
    [SerializeField] private float ExitEndValue = 0;
    [SerializeField] private float EnterEndValue = 255f;

    private void Awake()
    {
        _image = GetComponent<Image>();
        gameObject.AddEventTrigger(EventTriggerType.PointerEnter, PointerEnter);
        gameObject.AddEventTrigger(EventTriggerType.PointerExit, PointerExit);
        SetColor(0);
    }

    private void PointerExit(PointerEventData obj)
    {
        DOTween.To(SetColor, 255f, ExitEndValue, duration).SetUpdate(true).WaitForCompletion();
    }

    private void PointerEnter(PointerEventData obj)
    {
        DOTween.To(SetColor, 0f, EnterEndValue, duration).SetUpdate(true).WaitForCompletion();
    }

    private void SetColor(float a)
    {
        _image.color = new Color(255f, 255f, 255f, a / 255f);
    }
}