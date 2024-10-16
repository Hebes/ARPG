using UnityEngine;

/// <summary>
/// 播放BGM代理
/// </summary>
public class PlayBGMProxy : BaseBehaviour
{
    private void Start()
    {
        R.Audio.PlayBGM(id);
    }

    [SerializeField] private int id;
}