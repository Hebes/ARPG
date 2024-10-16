using System.Collections.Generic;

/// <summary>
/// 音频数据
/// </summary>
public class AudioClipData
{
    public int id { get; set; }

    public AudioClipData.AudioClipDataType type { get; set; }

    public string name { get; set; }

    public string path { get; set; }

    public string desc { get; set; }

    public float pitchMin { get; set; }

    public float pitchMax { get; set; }

    public float volumeMin { get; set; }

    public float volumeMax { get; set; }

    public static AudioClipData FindById(int id)
    {
        AudioClipData result;
        try
        {
            result = DB.AudioClipDataDic[id];
        }
        catch (KeyNotFoundException)
        {
            ("AudioClipDataID: " + id + " 不存在").Error();
            throw;
        }

        return result;
    }

    public static AudioClipData SetValue(params string[] strings)
    {
        return new AudioClipData
        {
            id = int.Parse(strings[0]),
            type = (AudioClipData.AudioClipDataType)int.Parse(strings[1]),
            name = strings[2],
            path = strings[3],
            desc = strings[4],
            pitchMin = strings[5].ToFloat(),
            pitchMax = strings[6].ToFloat(),
            volumeMin = strings[7].ToFloat(),
            volumeMax = strings[8].ToFloat(),
        };
    }

    public enum AudioClipDataType
    {
        BGM = 1,
        EnemyBoss,
        EnemyElite,
        EnemyNormal,
        PlayerMove,
        PlayerAtk,
        PlayerMaterial,
        UI,
        Group,
        Scene,
        Special,
        Video
    }
}