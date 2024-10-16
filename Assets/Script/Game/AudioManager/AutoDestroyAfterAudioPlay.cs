using UnityEngine;

/// <summary>
/// 音频播放后自动销毁
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AutoDestroyAfterAudioPlay : MonoBehaviour
{
    private AudioSource _source;
    [Header("销毁时间")] [SerializeField] private float delayTime = 0.5f;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_source.isPlaying) return;
        Disable();
    }

    private void OnDisable()
    {
        Disable();
    }

    private void Disable()
    {
        transform.localPosition = Vector3.zero;
        _source.Stop();
        _source.clip = null;
    }


}