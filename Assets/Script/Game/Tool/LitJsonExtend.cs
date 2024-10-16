using UnityEngine;
using LitJson;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine.Tilemaps;

public static class LitJsonExtend
{
    public static T Get<T>(this JsonData jsonData, string key, T defaultValue = default(T))
    {
        if (!jsonData.ContainsKey(key))return defaultValue;
        T result;
        try
        {
           string str =  jsonData[key].ToString();
            result = (T)((object)Convert.ChangeType(str, typeof(T), CultureInfo.InvariantCulture));
        }
        catch (Exception message)
        {
            UnityEngine.Debug.LogError(message);
            result = defaultValue;
        }
        return result;
    }
    
# if UNITY_EDITOR
    /// <summary>
    /// 序列化Vector3
    /// </summary>
    [InitializeOnLoadMethod]
    private static void JoinV3Type()
    {
        Action<Vector3, JsonWriter> writeType = (v, w) =>
        {
            w.WriteObjectStart(); //开始写入对象

            w.WritePropertyName("x"); //写入属性名
            w.Write(v.x.ToString()); //写入值

            w.WritePropertyName("y");
            w.Write(v.y.ToString());

            w.WritePropertyName("z");
            w.Write(v.z.ToString());

            w.WriteObjectEnd();
        };

        void Exporter(Vector3 obj, JsonWriter writer)
        {
            writer.WriteObjectStart();

            writer.WritePropertyName("x"); //写入属性名
            writer.Write(obj.x); //写入值
            writer.WritePropertyName("y");
            writer.Write(obj.y);
            writer.WritePropertyName("z");
            writer.Write(obj.z);

            writer.WriteObjectEnd();
        }

        //JsonMapper.RegisterExporter<Vector3>((v, w) => { writeType(v, w); });
        JsonMapper.RegisterExporter((ExporterFunc<Vector3>)Exporter); //序列化
        Debug.Log("Vector3加入成功");
    }


    /// <summary>
    /// 序列化Vector3Int
    /// </summary>
    [InitializeOnLoadMethod]
    static void JoinV3IntType()
    {
        Action<Vector3Int, JsonWriter> writeType = (v, w) =>
        {
            w.WriteObjectStart(); //开始写入对象

            w.WritePropertyName("x"); //写入属性名
            w.Write(v.x.ToString()); //写入值

            w.WritePropertyName("y");
            w.Write(v.y.ToString());

            w.WritePropertyName("z");
            w.Write(v.z.ToString());

            w.WriteObjectEnd();
        };

        JsonMapper.RegisterExporter<Vector3Int>((v, w) => { writeType(v, w); });

        Debug.Log("Vector3Int加入成功");
    }

    /// <summary>
    /// 序列化Vector2
    /// </summary>
    [InitializeOnLoadMethod]
    static void JoinV2Type()
    {
        Action<Vector2, JsonWriter> writeType = (v, w) =>
        {
            w.WriteObjectStart(); //开始写入对象

            w.WritePropertyName("x"); //写入属性名
            w.Write(v.x.ToString()); //写入值

            w.WritePropertyName("y");
            w.Write(v.y.ToString());

            w.WriteObjectEnd();
        };

        JsonMapper.RegisterExporter<Vector2>((v, w) => { writeType(v, w); });

        Debug.Log("Vector2加入成功");
    }

    /// <summary>
    /// 序列化Tile
    /// </summary>
    [InitializeOnLoadMethod]
    private static void joinTileType()
    {
        Action<Tile, JsonWriter> writeType = (v, w) =>
        {
            w.WriteObjectStart(); //开始写入对象

            // w.WritePropertyName("data");//写入属性名
            // w.Write("");//写入值 
            w.WriteObjectEnd();
        };

        JsonMapper.RegisterExporter<Tile>((v, w) => { writeType(v, w); });

        Debug.Log("Tile加入成功");
    }
    
    // /// <summary>
    // /// 序列化Vector2
    // /// </summary>
    // [InitializeOnLoadMethod]
    // static void JoinTool1()
    // {
    //     Action<Dictionary<string, List<string>>, JsonWriter> writeType = (v, w) =>
    //     {
    //         w.WriteObjectStart(); //开始写入对象
    //         foreach (List<string> data in v.Values)
    //         {
    //             foreach (string str in data)
    //             {
    //                 w.Write(str); //写入值
    //             }
    //             
    //         }
    //     };
    //
    //     JsonMapper.RegisterExporter<Dictionary<string, List<string>>>((v, w) => { writeType(v, w); });
    //
    //     Debug.Log("Dictionary<string, List<string>>加入成功");
    // }

    /// <summary>
    /// 反序列化类型
    /// https://blog.csdn.net/baidu_39447417/article/details/115499221
    /// https://blog.csdn.net/big_good_boy/article/details/139140229
    /// </summary>
    [InitializeOnLoadMethod]
    public static void UnJoinV3Type()
    {
        // JsonMapper.RegisterImporter<JsonData, Vector3>(ReadType1);
        //
        // Vector3 ReadType1(JsonData input)
        // {
        //     Debug.Log(11);
        //     throw new NotImplementedException();
        // }
        //JsonMapper.RegisterImporter<JsonData, Vector3>(ReadType);
        //JsonMapper.RegisterImporter<JsonData, Vector3>(ReadType);
        //JsonMapper.RegisterImporter<JsonData, Vector3>(jsonObj => new Vector3((int)jsonObj["x"], (int)jsonObj["y"], (int)jsonObj["z"]));
        // 使用前调用注册方法
        // Util.CustomJsonMapper.Register();
        // string characterInfoData = JsonMapper.ToJson(characterInfo);
        // characterInfo = JsonMapper.ToObject<CommonStruct.NCharacterInfo>(characterInfoData);
    }


    private static Vector3 ReadType(JsonData data)
    {
        float x = (float)data["x"];
        float y = (float)data["y"];
        float z = (float)data["z"];
        Vector3 vector3 = new Vector3(x, y, z);
        Debug.Log($"读取的坐标{vector3}");
        return vector3;
    }
}

public static class CustomJsonMapper
{
    public static void Register()
    {
        // 注册自定义的EnumMapper
        JsonMapper.RegisterExporter<Enum>((obj, writer) => writer.Write(Convert.ToInt32(obj)));

        // 注册自定义的EnumImporter
        JsonMapper.RegisterImporter<int, Enum>((intVal) => (Enum)Enum.ToObject(typeof(Enum), intVal));

        // 注册自定义的Vector3IntMapper
        JsonMapper.RegisterExporter<Vector3Int>((obj, writer) =>
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("x");
            writer.Write(obj.x);
            writer.WritePropertyName("y");
            writer.Write(obj.y);
            writer.WritePropertyName("z");
            writer.Write(obj.z);
            writer.WriteObjectEnd();
        });

        // 注册自定义的Vector3IntImporter
        JsonMapper.RegisterImporter<JsonData, Vector3Int>((jsonObj) => new Vector3Int((int)jsonObj["x"], (int)jsonObj["y"], (int)jsonObj["z"]));
    }
#endif
}