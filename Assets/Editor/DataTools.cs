using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using LitJson;
using UnityEditor;
using UnityEngine;

namespace EditorDataTools
{
    public class DataTools : EditorWindow
    {
        [MenuItem("Tools/编辑Data#E #E")]
        public static void BuildPackageVersions()
        {
            if (!EditorWindow.HasOpenInstances<DataTools>())
                GetWindow(typeof(DataTools), false, "DataTools").Show();
            else
                GetWindow(typeof(DataTools)).Close();
        }


        #region 存读

        private void OnEnable() => Load();
        private void OnDisable() => Save();

        private void Save()
        {
            if (!EditorWindow.HasOpenInstances<DataTools>()) return;
            Type type = GetType();
            var fieldsValue = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var data in fieldsValue)
            {
                if (data.FieldType == typeof(string))
                    PlayerPrefs.SetString($"{Application.productName}{data.Name}Save", (string)data.GetValue(this));
                if (data.FieldType == typeof(int))
                    PlayerPrefs.SetInt($"{Application.productName}{data.Name}Save", (int)data.GetValue(this));
            }

            UnityEngine.Debug.Log("保存成功");
        }

        private void Load()
        {
            Type type = GetType();
            var fieldsValue = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var data in fieldsValue)
            {
                if (data.FieldType == typeof(string))
                    data.SetValue(this, PlayerPrefs.GetString($"{Application.productName}{data.Name}Save"));
                if (data.FieldType == typeof(int))
                    data.SetValue(this, PlayerPrefs.GetInt($"{Application.productName}{data.Name}Save"));
            }
        }

        #endregion

        [MenuItem("Tools/字节测试")]
        public static void TestByte()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("有符号");
            sb.AppendLine("sbyte" + sizeof(sbyte) + "字节");
            sb.AppendLine("int" + sizeof(int) + "字节");
            sb.AppendLine("short" + sizeof(short) + "字节");
            sb.AppendLine("long" + sizeof(long) + "字节");
            sb.AppendLine("无符号");
            sb.AppendLine("byte" + sizeof(byte) + "字节");
            sb.AppendLine("uint" + sizeof(uint) + "字节");
            sb.AppendLine("ushort" + sizeof(ushort) + "字节");
            sb.AppendLine("ulong" + sizeof(ulong) + "字节");
            sb.AppendLine("浮点");
            sb.AppendLine("float" + sizeof(float) + "字节");
            sb.AppendLine("double" + sizeof(double) + "字节");
            sb.AppendLine("decimal" + sizeof(decimal) + "字节");
            sb.AppendLine("特殊");
            sb.AppendLine("bool" + sizeof(bool) + "字节");
            sb.AppendLine("char" + sizeof(char) + "字节");
            UnityEngine.Debug.Log(sb.ToString());
        }


        private void OnGUI()
        {
            GUI.backgroundColor = Color.yellow;
            Title();
            EditorGUILayout.BeginHorizontal();

            switch (_dataOperation)
            {
                case DataOperation.Binary:
                    EditorGUILayout.TextField("1", GUILayout.MinWidth(100f));
                    break;
                case DataOperation.Json:
                    break;
                case DataOperation.PlayerPrefs:
                    break;
                case DataOperation.XML:
                    break;
                case DataOperation.Excel:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("没有这个类型");
            }

            //LeftUI();
            //RightUI();
            EditorGUILayout.EndHorizontal();
        }

        private void SaveData()
        {
        }

        private void LoadData()
        {
        }


        private void Title()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("测试按钮")) Debug(1);
            if (GUILayout.Button("测试按钮")) Debug(1);
            if (GUILayout.Button("测试按钮")) Debug(1);
            EditorGUILayout.EndHorizontal();
        }


        private void BinarySaveData(string path)
        {
            //没有路径创建路径
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //写入文件
            using FileStream fileStream = new FileStream(path, FileMode.Create);
            //去Byte文件写入数据
            using var binaryWriter = new BinaryWriter(fileStream);
        }

        private void BinaryLoadData(string path)
        {
        }

        private void JsonSaveData()
        {
        }

        private void JsonLoadData()
        {
        }

        private void PlayerPrefsData()
        {
        }

        private void Debug(UnityEngine.Object o)
        {
            UnityEngine.Debug.Log(o);
        }

        private void Debug(int o)
        {
            UnityEngine.Debug.Log(o);
        }

        private enum DataOperation
        {
            Binary,
            Json,
            PlayerPrefs,
            XML,
            Excel,
        }

        private DataOperation _dataOperation;
        private string[][] _data;
    }


    public interface IDataHandle
    {
        public abstract void Save(object obj, string fileName);

        public abstract T Load<T>(string fileName) where T : class;
    }

    public class BinaryOperation : IDataHandle
    {
        private static string SAVE_PATH = $"{Application.dataPath}/Excel2Script/Byte/";

        /// <summary>
        /// 读取2进制数据转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T Load<T>(string fileName) where T : class
        {
            string filePath = $"{SAVE_PATH}{fileName}.bytes";
            //如果不存在这个文件 就直接返回泛型对象的默认值
            if (!File.Exists(filePath))
                return default(T);
            using FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            T obj = bf.Deserialize(fs) as T;
            fs.Close();
            return obj;
        }

        /// <summary>
        /// 存储类对象数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        public void Save(object obj, string fileName)
        {
            string filePath = $"{SAVE_PATH}{fileName}.bytes";
            //先判断路径文件夹有没有
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(SAVE_PATH);

            using FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, obj);
            fs.Close();
        }
    }

    public class JsonOperation : IDataHandle
    {
        /// <summary>
        /// 序列化和反序列化Json时  使用的是哪种方案
        /// </summary>
        private enum JsonType
        {
            JsonUtlity,
            LitJson,
        }

        private JsonType _jsonType = JsonType.LitJson;

        public T Load<T>(string path) where T : class
        {
            //确定从哪个路径读取
            //首先先判断 默认数据文件夹中是否有我们想要的数据 如果有 就从中获取
            //string path = $"{Application.streamingAssetsPath}/{fileName}.json";
            //先判断 是否存在这个文件
            //如果不存在默认文件 就从 读写文件夹中去寻找
            // if (!File.Exists(path))
            //     path = $"{Application.persistentDataPath}/{fileName}.json";
            //如果读写文件夹中都还没有 那就返回一个默认对象
            if (!File.Exists(path))
                return default(T);

            //进行反序列化
            string jsonStr = File.ReadAllText(path);
            //数据对象
            T data = default(T);
            switch (_jsonType)
            {
                case JsonType.JsonUtlity:
                    data = JsonUtility.FromJson<T>(jsonStr);
                    break;
                case JsonType.LitJson:
                    data = JsonMapper.ToObject<T>(jsonStr);
                    break;
            }

            //把对象返回出去
            return data;
        }

        public void Save(object data, string path)
        {
            //确定存储路径
            //string path = Application.persistentDataPath + "/" + fileName + ".json";
            //序列化 得到Json字符串
            string jsonStr = "";
            switch (_jsonType)
            {
                case JsonType.JsonUtlity:
                    jsonStr = JsonUtility.ToJson(data);
                    break;
                case JsonType.LitJson:
                    jsonStr = JsonMapper.ToJson(data);
                    break;
            }

            //把序列化的Json字符串 存储到指定路径的文件中
            File.WriteAllText(path, jsonStr);
        }
    }
}