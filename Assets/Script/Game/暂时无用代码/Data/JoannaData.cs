using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "乔安娜", menuName = "乔安娜动画数据文件", order = 51)]
public class JoannaData : ScriptableObject
{
    //实际数据
    [Header("攻击")] public Object folder1;
    [Header("等待")] public Object folder2;
    [Header("跑")] public Object folder3;
    [Header("跳跃")] public Object folder4;
    [Header("受伤")] public Object folder5;
    [Header("死亡")] public Object folder6;

    [Header("轻攻击")] public List<Sprite> LightAtk;
    [Header("轻上攻击")] public List<Sprite> UpLightAtk;
    [Header("正面重型攻击")] public List<Sprite> FrontHeavyAtk;

    [Header("等待")] public List<Sprite> Idle;

    [Header("右跑")] public List<Sprite> RunningRight;
    [Header("左跑")] public List<Sprite> RunningLeft;

    [Header("落下开始")] public List<Sprite> Fall;
    [Header("落下正在")] public List<Sprite> Falling;
    [Header("跳开始")] public List<Sprite> Jump;
    [Header("跳正在")] public List<Sprite> Jumping;
    [Header("着陆")] public List<Sprite> LandFX;

    [Header("受伤")] public List<Sprite> Hurt;

    [Header("死亡")] public List<Sprite> Death;
}

// 在自定义的 Editor 类中添加按钮
#if UNITY_EDITOR
[CustomEditor(typeof(JoannaData))]
public class JoannaScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("添加数据")) // 添加一个按钮
        {
            JoannaData data = (JoannaData)target;
            SetData(data, data.folder1, nameof(data.LightAtk));
            SetData(data, data.folder1, nameof(data.UpLightAtk));
            SetData(data, data.folder1, nameof(data.FrontHeavyAtk));
            SetData(data, data.folder2, nameof(data.Idle));
            SetData(data, data.folder3, nameof(data.RunningRight));
            SetData(data, data.folder3, nameof(data.RunningLeft));
            SetData(data, data.folder4, nameof(data.Fall));
            SetData(data, data.folder4, nameof(data.Falling));
            SetData(data, data.folder4, nameof(data.Jump));
            SetData(data, data.folder4, nameof(data.Jumping));
            SetData(data, data.folder4, nameof(data.LandFX));
            SetData(data, data.folder5, nameof(data.Hurt));
            SetData(data, data.folder6, nameof(data.Death));
            ReadData.SortAuto(data);
            UnityEditor.EditorUtility.SetDirty(data);
        }

        GUILayout.Space(10);
        base.OnInspectorGUI(); // 显示默认的 Inspector 面板
    }

    private void SetData(Object o1, Object folderValue, string listName)
    {
        Object[] data = ReadData.SetData(folderValue, listName, ReadData.ReadDataType.Folder);
        ReadData.SetList(o1, listName, data);
    }
}
#endif