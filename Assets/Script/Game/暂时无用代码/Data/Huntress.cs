using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "女猎手", menuName = "女猎手动画数据文件", order = 51)]
public class Huntress : ScriptableObject
{
    //实际数据
    [Header("攻击1")] public Object folder1;
    [Header("攻击2")] public Object folder2;
    [Header("攻击3")] public Object folder3;
    [Header("等待")] public Object folder4;
    [Header("跑")] public Object folder5;
    [Header("跳跃")] public Object folder6;
    [Header("受伤")] public Object folder7;
    [Header("死亡")] public Object folder8;
    [Header("落下")] public Object folder9;

    [Header("攻击1")] public List<Sprite> Attack1;
    [Header("攻击2")] public List<Sprite> Attack2;
    [Header("攻击3")] public List<Sprite> Attack3;
    [Header("死亡")] public List<Sprite> Death;
    [Header("落下")] public List<Sprite> Fall;
    [Header("等待")] public List<Sprite> Idle;
    [Header("跳跃")] public List<Sprite> Jump;
    [Header("跑")] public List<Sprite> Run;
    [Header("受伤")] public List<Sprite> Takehit;
}

// 在自定义的 Editor 类中添加按钮
#if UNITY_EDITOR
[CustomEditor(typeof(Huntress))]
public class HuntressScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("添加数据")) // 添加一个按钮
        {
            Huntress data = (Huntress)target;
            SetData(data, data.folder1, nameof(data.Attack1));
            SetData(data, data.folder2, nameof(data.Attack2));
            SetData(data, data.folder3, nameof(data.Attack3));
            SetData(data, data.folder4, nameof(data.Idle));
            SetData(data, data.folder5, nameof(data.Run));
            SetData(data, data.folder6, nameof(data.Jump));
            SetData(data, data.folder7, nameof(data.Takehit));
            SetData(data, data.folder8, nameof(data.Death));
            SetData(data, data.folder9, nameof(data.Fall));
            ReadData.SortAuto(data);
            UnityEditor.EditorUtility.SetDirty(data);
        }

        GUILayout.Space(10);
        base.OnInspectorGUI(); // 显示默认的 Inspector 面板
    }

    private void SetData(Object o1, Object folderValue, string listName)
    {
        Object[] data = ReadData.SetData(folderValue, listName, ReadData.ReadDataType.Image);
        ReadData.SetList(o1, listName, data);
    }
}
#endif