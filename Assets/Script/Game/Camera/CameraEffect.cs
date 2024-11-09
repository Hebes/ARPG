using System.Collections;
using UnityEngine;

/// <summary>
/// 相机效果
/// </summary>
public class CameraEffect : MonoBehaviour
{
    // [Header("是动态模糊")] private bool _isMotionBlur;
    // [Header("是动态模糊")] private CameraMotionBlur _blur;
    // private Bloom _globalBloom;
    // private BloomOptimized _bloom;

    public void CameraBloom(float recoveryTime, float waitingTime)
    {
        "相机效果".Log();
        // if (this._bloom == null)
        // {
        //     base.StartCoroutine(this.BloomCoroutine(recoveryTime, waitingTime));
        // }
    }

    private IEnumerator BloomCoroutine(float recoveryTime, float waitingTime)
    {
        // this._bloom = R.Camera.AddComponent<BloomOptimized>();
        // this._bloom.fastBloomShader = (this._bloom.fastBloomShader ?? Shader.Find("Hidden/FastBloom"));
        // if (!this._bloom.enabled)
        // {
        //     this._bloom.enabled = true;
        // }
        // this._bloom.intensity = 2.5f;
        // this._bloom.threshold = 0.3f;
        // yield return new WaitForSeconds(waitingTime);
        // float calTime = 0f;
        // float startTime = Time.time;
        // while (Time.time - startTime < recoveryTime)
        // {
        //     this._bloom.intensity = Mathf.Lerp(2.5f, 0.38f, Mathf.Clamp(calTime, 0f, recoveryTime) / recoveryTime);
        //     this._bloom.threshold = Mathf.Lerp(0.3f, 0.4f, Mathf.Clamp(calTime, 0f, recoveryTime) / recoveryTime);
        //     yield return null;
        //     calTime += Time.deltaTime;
        // }
        // this._bloom.enabled = false;
        // UnityEngine.Object.Destroy(this._bloom);
        // this._bloom = null;
        yield break;
    }

    private IEnumerator CameraMotionBlur(float second, float scale, Vector3 pos)
    {
        // float startTime = Time.time;
        // float calTime = 0f;
        // this._blur.preview = true;
        // do
        // {
        // 	this._blur.velocityScale = scale * calTime / second;
        // 	Vector2 camPos = this._camera.WorldToViewportPoint(pos);
        // 	Vector2 blurPos = camPos * -2f + new Vector2(1f, 1f / this._camera.aspect);
        // 	Vector3 realBlurScale = blurPos;
        // 	realBlurScale.z = 1f;
        // 	realBlurScale *= 13f;
        // 	this._blur.previewScale = realBlurScale;
        // 	calTime += Time.deltaTime;
        // 	yield return null;
        // }
        // while (Time.time - startTime < second);
        // this._blur.enabled = false;
        // this._isMotionBlur = false;
        yield break;
    }

    public void EnableGlobalBloom()
    {
        //this._globalBloom.enabled = true;
    }

    public void DisableGlobalBloom()
    {
        //this._globalBloom.enabled = false;
    }

    public void OpenMotionBlur(float second, float scale, Vector3 pos)
    {
    }

    public void CloseMotionBlur()
    {
    }
}