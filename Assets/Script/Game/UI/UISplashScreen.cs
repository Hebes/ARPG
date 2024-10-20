using System.Collections;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 启动屏幕画面
/// </summary>
public class UISplashScreen : MonoBehaviour
{
    private AsyncOperation _asyncOperation;
    [SerializeField] private CanvasGroup[] _canvasGroupArray;
    [SerializeField] private float _fade = 0.5f;
    [SerializeField] private float _duration = 1.5f;

    private void Start()
    {
        _asyncOperation = LevelManager.LoadScene(CScene.InitScene);
        _asyncOperation.allowSceneActivation = false;
        Sequence0().StartIEnumerator();
    }

    /// <summary>
    /// 暂停应用程序
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        if (!pause && _asyncOperation != null)
        {
            _asyncOperation.allowSceneActivation = true;
        }
    }

    /// <summary>
    /// 按照顺序播放
    /// </summary>
    /// <returns></returns>
    private IEnumerator Sequence0()
    {
        if (false) //UILanguage.IsSimplifiedChinese
        {
            yield return DOTween.To(delegate(float a) { SetColor(0, a); }, 0f, 1f, _fade).WaitForCompletion();
            yield return new WaitForSeconds(3f);
            yield return DOTween.To(delegate(float a) { SetColor(0, a); }, 1f, 0f, _fade).WaitForCompletion();
            yield return new WaitForSeconds(_fade);
        }

        for (var i = 1; i < _canvasGroupArray.Length; i++)
        {
            var iCopy = i;
            yield return DOTween.To(delegate(float a) { SetColor(iCopy, a); }, 0f, 1f, _fade).WaitForCompletion();
            yield return new WaitForSeconds(i == 0 ? 3f : _duration);
            yield return DOTween.To(delegate(float a) { SetColor(iCopy, a); }, 1f, 0f, _fade).WaitForCompletion();
            if (iCopy != _canvasGroupArray.Length - 1)
            {
                yield return new WaitForSeconds(_fade);
            }
        }

        _asyncOperation.allowSceneActivation = true;
        yield break;

        void SetColor(int i, float a) => _canvasGroupArray[i].alpha = a;
    }
}