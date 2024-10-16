using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ShowUIWhenStart : MonoBehaviour
{
    private void Start()
    {
        UICharactor.I.ShowUI(true);
        UIStart.I.canvasGroup.FadeTo(0);
    }
}