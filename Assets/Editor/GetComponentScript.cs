﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using Object = UnityEngine.Object;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using EditorWindow = UnityEditor.EditorWindow;
using GenericMenu = UnityEditor.GenericMenu;
using GUIContent = UnityEngine.GUIContent;

public class GetComponentScript
{
    #region 获取组件和变量

    [MenuItem("GameObject/获取组件/TweenColor", false, 1)]
    private static void GetAll()
    {
        string str1 = GetComponent<TweenColor>();
        string str2 = GetValue<TweenColor>();
        Debug.Log(str1 + "\n" + str2);
        $"{str1}\n{str2}".Copy();
    }

    #endregion

    #region Other

    [MenuItem("GameObject/清空PlayerPrefs", false, 1)]
    private static void Tool1() => PlayerPrefs.DeleteAll();

    #endregion

    #region 获取变量

    [MenuItem("GameObject/获取变量/Text", false, 1)]
    private static void GetValue1() => GetValue<Text>();

    [MenuItem("GameObject/获取变量/CanvasGroup", false, 1)]
    private static void GetValue2() => GetValue<CanvasGroup>();

    #endregion

    #region 获取组件

    [MenuItem("GameObject/获取组件/TweenColor", false, 1)]
    private static void GetComponent1() => GetComponent<TweenColor>();

    [MenuItem("GameObject/获取组件/Text", false, 1)]
    private static void GetComponent2() => GetComponent<Text>();

    [MenuItem("GameObject/获取组件/CanvasGroup", false, 1)]
    private static void GetComponent3() => GetComponent<CanvasGroup>();

    [MenuItem("GameObject/获取组件/EventTrigger", false, 1)]
    private static void GetComponent4() => GetComponent<EventTrigger>();

    [MenuItem("GameObject/获取组件/Slider", false, 1)]
    private static void GetComponent5() => GetComponent<Slider>();

    [MenuItem("GameObject/获取组件/UIProgressBar2", false, 1)]
    private static void GetComponent6() => GetComponent<UIProgressBar2>();

    [MenuItem("GameObject/获取组件/Image", false, 1)]
    private static void GetComponent7() => GetComponent<Image>();

    #endregion


    /// <summary>
    /// 将isValidateFunction参数设置为true
    /// 并且路径以及按钮名称需要一模一样，表示下面我们声明的方法是对应按钮的验证方法
    /// 方法的返回值一定要是bool类型的，当返回值为true时表示执行下面的方法
    /// 当每次弹出选择栏的时候便会执行一次这个方法
    /// </summary>
    private static string GetComponent<T>() where T : Component
    {
        Object obj = Selection.activeObject;
        if (!obj) throw new Exception("当前物体为获取到，可能是隐藏");
        StringBuilder sb = new StringBuilder(); //字符串
        //获取当前选择的物体组件
        T t1 = obj.GameObject().GetComponent<T>();
        if (t1)
            sb.AppendLine($"{obj.name} = GetComponent<{typeof(T)}>();");
        //获取子物体组件
        List<T> values = new List<T>();
        GetAllChild(obj.GameObject().transform, ref values);
        for (int i = 0; i < values?.Count; i++)
        {
            string str = GetParentPath<T>(values[i], obj.name);
            sb.AppendLine($"{values[i].name} = transform.Find(\"{str}\").GetComponent<{typeof(T)}>();");
        }

        //打印
        Debug.Log(sb.ToString());
        sb.ToString().Copy();
        return sb.ToString();
    }

    /// <summary>
    /// 获取子物体
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="values"></param>
    /// <typeparam name="T"></typeparam>
    private static void GetAllChild<T>(Transform transform, ref List<T> values) where T : Component
    {
        for (int i = 0; i < transform?.childCount; i++)
        {
            T t = transform.GetChild(i).GetComponent<T>();
            if (t)
            {
                values.Add(t);
            }

            GetAllChild(transform.GetChild(i), ref values);
        }
    }

    /// <summary>
    /// 获取到父物体路径
    /// </summary>
    /// <param name="t"></param>
    /// <param name="parentName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static string GetParentPath<T>(T t, string parentName) where T : Component
    {
        List<string> path = new List<string>();
        StringBuilder sb = new StringBuilder();
        Transform flagTf = t.transform;
        while (true)
        {
            path.Add(flagTf.name);
            flagTf = flagTf.parent;
            if (flagTf.name.Equals(parentName))
                break;
        }

        for (int i = path.Count - 1; i >= 0; i--)
        {
            sb.Append(path[i]);
            if (i == 0) continue;
            sb.Append("/");
        }

        return sb.ToString();
    }


    /// <summary>
    /// [SerializeField]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static string GetValue<T>() where T : Component
    {
        Object obj = Selection.activeObject;
        if (!obj) throw new Exception("当前物体为获取到，可能是隐藏");
        StringBuilder sb = new StringBuilder(); //字符串
        T t1 = obj.GameObject().GetComponent<T>();
        if (t1)
            sb.AppendLine($"public {typeof(T)} {obj.name};");
        List<T> values = new List<T>();
        GetAllChild(obj.GameObject().transform, ref values);
        for (int i = 0; i < values?.Count; i++)
        {
            string str = GetParentPath<T>(values[i], obj.name);
            sb.AppendLine($"public {typeof(T)} {values[i].name};");
        }

        //打印
        Debug.Log(sb.ToString());
        sb.ToString().Copy();
        return sb.ToString();
    }
}

public class SpriteRenderTools
{
    /// <summary>
    /// https://blog.csdn.net/qq_31042143/article/details/122498295 Editor模式批量修改Sprite导入参数
    /// https://zhuanlan.zhihu.com/p/48921252 修改Sprite的Pivot
    /// https://blog.csdn.net/linxinfa/article/details/114867642 Unity将Slice分割
    /// </summary>
    [MenuItem("Assets/修改图片锚点/GetAll", false, 1)]
    private static void GetAll()
    {
        Object[] obj = Selection.objects;
        foreach (Object texture in obj)
        {
            string selectionPath = AssetDatabase.GetAssetPath(texture);
            TextureImporter textureIm = AssetImporter.GetAtPath(selectionPath) as TextureImporter;
            TextureImporterSettings setting = new TextureImporterSettings();
            //TextureImporterPlatformSettings setting = textureIm.GetDefaultPlatformTextureSettings();
            //textureIm.SetPlatformTextureSettings(setting);
            textureIm.ReadTextureSettings(setting);
            setting.textureType = TextureImporterType.Sprite;
            setting.spritePixelsPerUnit = 40f;
            setting.spriteMode = (int)SpriteImportMode.Single;
            setting.spriteAlignment = (int)SpriteAlignment.Custom;
            setting.spritePivot = new Vector2(0.44f, 0.23f);
            textureIm.SetTextureSettings(setting);
            textureIm.SaveAndReimport();
        }
    }
}

public class SceneTools : UnityEditor.Editor
{
    [MenuItem("GameObject/场景/切换/00Splash", false, 1)]
    public static void Switch00Splash() => SwitchScene($"Assets/Scenes/00Splash.unity");

    [MenuItem("GameObject/场景/切换/01InitScene", false, 1)]
    public static void Switch01InitScene() => SwitchScene($"Assets/Scenes/01InitScene.unity");

    [MenuItem("GameObject/场景/切换/02Game", false, 1)]
    public static void Switch02Game() => SwitchScene($"Assets/Scenes/02Game.unity");

    [MenuItem("GameObject/场景/添加/02Game", false, 1)]
    public static void AddScene1() => SwitchScene($"Assets/Scenes/02Game.unity");

    private static void SwitchScene(string scenePath)
    {
        string currentScenePath = EditorSceneManager.GetActiveScene().path; // 获取当前场景路径
        //string targetScenePath = "Assets/Scenes/TargetScene.unity";// 定义需要切换到的场景路径
        EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        //EditorSceneManager.OpenScene(path); // 切换到目标场景
        //Debug.Log("Switched from " + currentScenePath + " to " + targetScenePath);
    }

    private static void AddScene(string scenePath)
    {
        EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
    }
}

public class GenerateTools
{
    [MenuItem("Tools/生成/Tags", false, 1)]
    public static void Generate1()
    {
        string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
        string str = GetConfig(tags);
        str.Log();
    }

    [MenuItem("Tools/生成/Layer", false, 1)]
    public static void Generate2()
    {
        string[] layers = UnityEditorInternal.InternalEditorUtility.layers;
        string str = GetConfig(layers);
        str.Log();
    }

    [MenuItem("Tools/生成/sortingLayer", false, 1)]
    public static void Generate3()
    {
        System.Reflection.BindingFlags bf = System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic;
        System.Reflection.PropertyInfo sortingLayersProperty = typeof(UnityEditorInternal.InternalEditorUtility).GetProperty("sortingLayerNames", bf);
        string[] sortingLayers = (string[])sortingLayersProperty.GetValue(null, new object[0]);
        string str = GetConfig(sortingLayers);
        str.Log();
    }


    [MenuItem("Tools/生成/GameObject", false, 1)]
    public static void Generate4()
    {
        //GenerateGameObject($"{Application.dataPath}/Resources", "*.prefab");
    }

    [MenuItem("Tools/生成/Scene", false, 1)]
    public static void Generate5()
    {
        string[] temp = GenerateGameObject($"{Application.dataPath}", "*.unity");
        string str = GetConfig(temp);
        str.Log();
    }

    private static string[] GenerateGameObject(string path, string suffix)
    {
        string[] strings = Directory.GetFiles(path, suffix, SearchOption.AllDirectories);
        string[] value = new string[strings.Length];
        for (var i = 0; i < strings.Length; i++)
        {
            string fileName = Path.GetFileNameWithoutExtension(strings[i]);
            value[i] = fileName;
        }

        return value;
    }

    private static string GetConfig(params string[] value)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string c in value)
            sb.AppendLine($"\tpublic const string {c} = \"{c}\";");
        sb.ToString().Copy();
        return sb.ToString();
    }

    private string ReplaceStr(string str)
    {
        return str.Replace(" ", String.Empty);
    }
}

public class PrefabTools : UnityEditor.Editor

{
    [MenuItem("GameObject/实例化/Player", false, 1)]
    public static void LoadInstantiatePrefab1() => InstantiatePrefab("Assets/Resources/Prefab/Core/Player.prefab");

    [MenuItem("GameObject/实例化/Camera", false, 1)]
    public static void LoadInstantiatePrefab2() => InstantiatePrefab("Assets/Resources/Prefab/Core/Camera.prefab");

    [MenuItem("GameObject/打开/UI", false, 1)]
    public static void LoadOpenPrefab1() => OpenAsset("Assets/Resources/Prefab/Core/UI.prefab");


    /// <summary>
    /// 实例化资源
    /// </summary>
    /// <param name="path"></param>
    /// <exception cref="NullReferenceException"></exception>
    private static void InstantiatePrefab(string path)
    {
        // 加载Prefab资源（这里假设预制体在"Assets/Prefabs"文件夹下）
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

        // 检查Prefab是否加载成功
        if (prefab == null)
        {
            throw new NullReferenceException();
        }

        // 实例化Prefab到场景中
        GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        // 设置实例化后的Prefab位置
        // if (Selection.activeTransform != null)
        // {
        //     instance.transform.position = Selection.activeTransform.position + Vector3.up; // 举例：将实例化Prefab的位置设置为当前选中对象的位置上方
        // }
    }

    /// <summary>
    /// 打开预制体
    /// </summary>
    /// <param name="path"></param>
    private static void OpenAsset(string path)
    {
        Object prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        if (prefab != null)
        {
            AssetDatabase.OpenAsset(prefab);
        }
        else
        {
            Debug.LogError("没找到: " + path);
        }
    }
}

/// <summary>
/// https://blog.csdn.net/final5788/article/details/129914973 语言国际化工具,生成多语言Excel自
/// </summary>
public static class EditorTool
{
    public static List<GameObject> ScanPrefab()
    {
        string[] str = new[]
        {
            "Assets/Resources/Prefab",
        };
        var assetGUIDs = AssetDatabase.FindAssets("t:Prefab", str);
        List<GameObject> keyList = new List<GameObject>();
        int totalCount = assetGUIDs.Length;
        for (int i = 0; i < totalCount; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(assetGUIDs[i]);
            GameObject pfb = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            keyList.Add(pfb);
        }

        return keyList;
    }

    /// <summary>
    /// 扫描Prefab
    /// </summary>
    public static List<string> ScanLocalizationTextFromPrefab(Action<string, int, int> onProgressUpdate = null)
    {
        string[] str = new[]
        {
            "Assets/Resources/Prefab",
        };
        var assetGUIDs = AssetDatabase.FindAssets("t:Prefab", str);
        List<string> keyList = new List<string>();
        int totalCount = assetGUIDs.Length;
        for (int i = 0; i < totalCount; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(assetGUIDs[i]);
            GameObject pfb = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            // onProgressUpdate?.Invoke(path, totalCount, i);
            // var keyArr = pfb.GetComponentsInChildren<UnityGameFramework.Runtime.UIStringKey>(true);
            // foreach (var newKey in keyArr)
            // {
            //     if (string.IsNullOrWhiteSpace(newKey.Key) || keyList.Contains(newKey.Key)) continue;
            //     keyList.Add(newKey.Key);
            // }
        }

        return keyList;
    }

    /// <summary>
    /// 复制
    /// </summary>
    /// <param name="str"></param>
    public static void Copy(this string str)
    {
        TextEditor te = new TextEditor { text = str };
        te.SelectAll();
        te.Copy();
    }
}