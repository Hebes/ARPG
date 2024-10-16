using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TweenColor2 : MonoBehaviour
{
    [SerializeField] private Image _image1;
    [SerializeField] private Image _image2;
    [Header("过度时间")]public float duration = 0.3f;

    private void Awake()
    {
        gameObject.AddEventTrigger(EventTriggerType.PointerEnter, PointerEnter);
        gameObject.AddEventTrigger(EventTriggerType.PointerExit, PointerExit);
        SetColor(0);
    }

    private void PointerExit(PointerEventData obj)
    {
        DOTween.To(SetColor, 255f, 0f, duration).SetUpdate(true).WaitForCompletion();
    }

    private void PointerEnter(PointerEventData obj)
    {
        DOTween.To(SetColor, 0f, 255f, duration).SetUpdate(true).WaitForCompletion();
    }

    private void SetColor(float a)
    {
        _image1.color = new Color(255f, 255f, 255f, a / 255f);
        _image2.color = new Color(255f, 255f, 255f, a / 255f);
    }
}