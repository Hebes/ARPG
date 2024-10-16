using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// string result = Regex.Replace(o.name, @"\d", ""); // 使用正则表达式替换数字为空字符串
/// string result = Regex.Replace(oValue.name, @"\s+", ""); //正则匹配全部空格
/// </summary>
public static class ReadData
{
    public enum ReadDataType
    {
        Folder,
        Image,
    }

#if UNITY_EDITOR
    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="oValue"></param>
    /// <param name="listName"></param>
    /// <param name="readDataType"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static Object[] SetData(Object oValue, string listName, ReadDataType readDataType)
    {
        if (oValue == null) throw new Exception("没有oValue");
        switch (readDataType)
        {
            case ReadDataType.Folder:
                return GetFolderChildSprite(oValue, listName);
            case ReadDataType.Image:
                Object[] temp = GetImageChildSprite(oValue);
                // 去除第一个加载的资源
                Object[] filteredAssets = new Object[temp.Length - 1];
                for (int i = 1; i < temp.Length; i++)
                    filteredAssets[i - 1] = temp[i];
                return filteredAssets;
            default:
                throw new Exception("没有处理方法");
        }
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="o">实力类</param>
    /// <param name="listName"></param>
    /// <param name="objArray">数据</param>
    public static void SetList(Object o, string listName, Object[] objArray)
    {
        Type type = o.GetType();
        FieldInfo temp = type.GetField(listName);
        Type classListType = temp.FieldType;
        object elementData = Activator.CreateInstance(classListType, new object[] { }); //创建一个list返回
        BindingFlags flag = BindingFlags.Instance | BindingFlags.Public;
        MethodInfo methodInfo = classListType.GetMethod("Add", flag);
        foreach (var oValue in objArray)
        {
            methodInfo?.Invoke(elementData, new object[] { oValue }); //相当于List<T>调用Add方法
            temp.SetValue(o, elementData);
        }
    }

    /// <summary>
    /// 自动排序
    /// </summary>
    /// <param name="obj"></param>
    public static void SortAuto(Object obj)
    {
        Type type = obj.GetType();
        FieldInfo[] fields = type.GetFields();
        foreach (FieldInfo field in fields)
        {
            if (!field.FieldType.IsGenericType) continue;
            if (field.FieldType.GetGenericTypeDefinition() != typeof(List<>)) continue;
            var list = (IList)field.GetValue(obj);
            Comparison<Sprite> comparison = Sort;
            MethodInfo sortMethod = field.FieldType.GetMethod("Sort", new[] { typeof(Comparison<Sprite>) });
            sortMethod?.Invoke(list, new object[] { comparison });
        }
    }

    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private static int Sort(Sprite x, Sprite y)
    {
        MatchCollection temp1 = Regex.Matches(x.name, @"\d+");
        MatchCollection temp2 = Regex.Matches(y.name, @"\d+");
        if (int.TryParse(temp1[0].ToString(), out var number1) && int.TryParse(temp2[0].ToString(), out var number2))
            return number1.CompareTo(number2);
        return default;
    }

    /// <summary>
    /// 读取文件夹图片数据
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="listName"></param>
    /// <returns></returns>
    private static Object[] GetFolderChildSprite(Object obj, string listName)
    {
        string rootPath1 = AssetDatabase.GetAssetPath(obj); //获取路径名称  
        string[] pathArray1 = Directory.GetFiles(rootPath1, "*.PNG", SearchOption.AllDirectories);
        List<Object> objects = new List<Object>();
        foreach (string s in pathArray1)
        {
            Object[] spriteArray = AssetDatabase.LoadAllAssetRepresentationsAtPath(s);
            if (spriteArray == null || spriteArray.Length == 0) continue;
            string result = Regex.Replace(spriteArray[0].name, @"\d", ""); // 使用正则表达式替换数字为空字符串
            if (result.Equals(listName))
                objects.AddRange(spriteArray);
        }

        return objects.ToArray();
    }

    /// <summary>
    /// 获取图集中的子图片
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static Object[] GetImageChildSprite(Object obj)
    {
        string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(obj)); //获取路径名称
        string path = rootPath + "/" + obj.name + ".PNG"; //图片路径名称
        Object[] sprites = AssetDatabase.LoadAllAssetsAtPath(path);
        //Object[] sprites = Resources.LoadAll<Sprite>(path);
        // Sprite output_sprite = (Sprite)sprites[0];
        // Debug.Log(output_sprite.name);
        return sprites;
    }
#endif
}