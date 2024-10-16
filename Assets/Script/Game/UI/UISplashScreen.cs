using System.Collections;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 启动屏幕画面
/// </summary>
public class UISplashScreen : SMono<UISplashScreen>
{
    [SerializeField] private CanvasGroup[] _canvasGroupArray;
    private float _fade = 0.5f;
    private float _duration = 1.5f;

    //private AsyncOperation _asyncOperation;

    private void Awake()
    {
        //_asyncOperation = SceneController.LoadScene(CScene.StartScene);
        Sequence0().StartIEnumerator();
    }

    /// <summary>
    /// 暂停应用程序
    /// </summary>
    /// <param name="pause"></param>
    // private void OnApplicationPause(bool pause)
    // {
    //     if (pause) return;
    //     if (_asyncOperation == null) return;
    //     _asyncOperation.allowSceneActivation = true;
    // }


    /// <summary>
    /// 按照顺序播放
    /// </summary>
    /// <returns></returns>
    private IEnumerator Sequence0()
    {
        yield return DOTween.To(delegate(float a) { SetColor(0, a); }, 0f, 1f, _fade).WaitForCompletion();
        yield return new WaitForSeconds(3f);
        yield return DOTween.To(delegate(float a) { SetColor(0, a); }, 1f, 0f, _fade).WaitForCompletion();
        yield return new WaitForSeconds(_fade);
        for (var i = 1; i < _canvasGroupArray.Length; i++)
        {
            var iCopy = i;
            yield return DOTween.To(delegate(float a) { SetColor(iCopy, a); }, 0f, 1f, _fade).WaitForCompletion();
            yield return new WaitForSeconds(i == 0 ? 3f : _duration);
            yield return DOTween.To(delegate(float a) { SetColor(iCopy, a); }, 1f, 0f, _fade).WaitForCompletion();
            if (iCopy == _canvasGroupArray.Length - 1) continue;
            yield return new WaitForSeconds(_fade);
        }

        yield return UIStart.I.canvasGroup.DOUIFade(0f, 1f, 0.3f);
        //_asyncOperation.allowSceneActivation = true;
        gameObject.SetActive(false);
        yield break;

        void SetColor(int i, float a) => _canvasGroupArray[i].alpha = a;
    }
}