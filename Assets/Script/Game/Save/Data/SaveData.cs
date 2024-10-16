using System;
using System.IO;
using System.Text;
using LitJson;
using UnityEngine;

/// <summary>
/// 存档数据
/// https://blog.csdn.net/Czhenya/article/details/88181930 Application各种路径在终端的本地路径
/// </summary>
public class SaveData : SMono<SaveData>
{
    public static event EventHandler<SaveLoadedEventArgs> OnGameLoaded;

    //private static string SaveDataPath => Application.persistentDataPath + "/SaveData/";
    private static string SaveDataPath => $"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}/ARPGSaveData/";

    private static string SaveDataFilePath => SaveDataPath + "save_data.bin";

    /// <summary>
    /// 是否正在存档
    /// </summary>
    public static bool IsBusy => false;

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static bool Save(GameData data)
    {
        $"保存成功,路径：{SaveDataFilePath}".Log();
        byte[] buffer = GetBuffer(data);
        bool result;
        try
        {
            if (!Directory.Exists(SaveDataPath))
                Directory.CreateDirectory(SaveDataPath);

            using FileStream fileStream = File.OpenWrite(SaveDataFilePath);
            using BinaryWriter binaryWriter = new BinaryWriter(fileStream, Encoding.UTF8);
            binaryWriter.Write(buffer);
            result = true;
        }
        catch (FileNotFoundException)
        {
            "保存数据文件不存在".Warning();
            result = false;
        }

        return result;
    }

    /// <summary>
    /// 是否存在自动保存数据
    /// </summary>
    /// <returns></returns>
    public static bool IsAutoSaveDataExists()
    {
        $"存档路径{SaveDataFilePath}".Log();
        return File.Exists(SaveDataFilePath);
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public static bool Load()
    {
        bool result;
        try
        {
            using FileStream fileStream = File.OpenRead(SaveDataFilePath);
            byte[] array = new byte[fileStream.Length];
            fileStream.Read(array, 0, array.Length);
            GameData gameData = GetObject(array);
            OnGameLoaded?.Invoke(null, new SaveLoadedEventArgs(gameData));
            result = true;
        }
        catch (FileNotFoundException)
        {
            "保存数据文件不存在".Warning();
            result = false;
        }

        return result;
    }

    /// <summary>
    /// 删除
    /// </summary>
    public static void Delete()
    {
        if (File.Exists(SaveDataFilePath))
            File.Delete(SaveDataFilePath);
    }

    /// <summary>
    /// 得到缓冲
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static byte[] GetBuffer(GameData obj)
    {
        MemoryStream memoryStream = new MemoryStream();
        BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
        binaryWriter.Write(JsonMapper.ToJson(obj));
        binaryWriter.Close();
        return memoryStream.GetBuffer();
    }

    private static GameData GetObject(byte[] buffer)
    {
        MemoryStream input = new MemoryStream(buffer);
        BinaryReader binaryReader = new BinaryReader(input);
        string str = binaryReader.ReadString();
        GameData result = JsonMapper.ToObject<GameData>(str);
        binaryReader.Close();
        return result;
    }
}