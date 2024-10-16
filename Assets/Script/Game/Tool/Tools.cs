﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using DG.Tweening.Core;
using LitJson;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Internal;

/*
 * https://www.cnblogs.com/sanyejun/p/14845428.html 游戏运行停止编译代码
 */

/*
 * BehaviorDesigner 行为树插件
 * https://opsive.com/support/documentation/behavior-designer/behavior-tree-component/ 官网
 * https://blog.csdn.net/linxinfa/article/details/124483690 BehaviorDesigner插件制作AI行为树
 * https://www.jianshu.com/p/64b5fe01fb1c Behavior Designer 中文版教程
 * https://geekdaxue.co/read/dachengzi-shgxu@kczcy8/exSda0C8lYQ9TDDU BehaviorDesigner 文档翻译与整理
 * https://blog.csdn.net/qq_52324195/article/details/124827915 Unity-Behavior Designer详解
 * https://www.jianshu.com/p/5033daf85fda Unity3D行为树插件BehaviorDesigner（八-复合节点 ）
 * https://www.jianshu.com/p/998f665cb616 Unity3D行为树插件BehaviorDesigner（六-条件节点的终止）
 *
 * task有四种不同的类型：
 * action（行为节点）定义具体行为和任务
 * conditional（条件节点）检测一些游戏的属性进行决定
 * composite（复合节点）控制行为树流程和顺序
 * decorator（装饰节点）对子节点返回值进行修改如取反或者直接控制子节点的表达
 *
 * Sequence 顺序节点：从左到右。为真继续，为假则停
 * Selector 选择节点：从左到右。一真即真，一真即停。全假才假。
 * Parallel 并行节点：并行执行。全真则真，一假则假。一假即停，被停即假。
 * Parallel Selector 并行选择节点：并行执行。一真即真，一真即停，全假才假。
 * Random Selector 随机选择节点：随机执行。真即真，一真即停，全假才假。
 * Random Sequence 随机顺序节点：随机顺序。为真继续，全真才真。一假则假，一假即停。
 */

public static class ComponentTools
{
    /// <summary>
    /// 为EventTrigger的事件类型绑定Action方法
    /// </summary>
    /// <param name="trigger">EventTrigger组件对象</param>
    /// <param name="eventType">事件类型</param>
    /// <param name="listenedAction">要执行的方法</param>
    public static void AddEventTrigger(this EventTrigger trigger, EventTriggerType eventType, Action<PointerEventData> listenedAction)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener(data => listenedAction.Invoke((PointerEventData)data));
        trigger.triggers.Add(entry);
    }

    /// <summary>
    /// 监听携程
    /// IEnumerator
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="eventType"></param>
    /// <param name="routine"></param>
    public static void AddEventTrigger(this EventTrigger trigger, EventTriggerType eventType, IEnumerator routine)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener(data => { trigger.StartCoroutine(routine); });
        trigger.triggers.Add(entry);
    }

    /// <summary>
    /// 监听携程，字符串开启停止 还未测试
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="eventType"></param>
    /// <param name="iEnumeratorRoutineName"></param>
    public static void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, string iEnumeratorRoutineName, Type type)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener(data => { trigger.StartCoroutine($"{type.Name}{iEnumeratorRoutineName}", data); });
        trigger.triggers.Add(entry);
    }

    /// <summary>
    /// UI物体移除EventTrigger
    /// </summary>
    /// <param name="trigger">要移除事件的ui物体</param>
    /// <param name="eventType">事件类型</param>
    /// <param name="action">调用的方法</param>
    public static void RemoveEventTrigger(this EventTrigger trigger, EventTriggerType eventType,
        UnityAction<BaseEventData> action)
    {
        var entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener(action);
        trigger.triggers.Remove(entry);
    }

    /// <summary>
    /// 添加EventTrigger组件
    /// </summary>
    /// <param name="goValue"></param>
    /// <param name="eventType">
    /// PointerEnter：鼠标指针进入目标对象的事件。
    /// PointerExit：鼠标指针离开目标对象的事件。
    /// PointerDown：鼠标按下事件。
    /// PointerUp：鼠标释放事件。
    /// PointerClick：鼠标点击事件。
    /// Drag：拖拽事件。
    /// Drop：释放拖拽事件。
    /// Scroll：滚动事件。</param>
    /// <param name="action"></param>
    public static void AddEventTrigger(this GameObject goValue, EventTriggerType eventType,
        Action<PointerEventData> action)
    {
        var eventTrigger = goValue.GetComponent<EventTrigger>();
        eventTrigger ??= goValue.AddComponent<EventTrigger>();
        eventTrigger.AddEventTrigger(eventType, action);
    }

    /// <summary>
    /// 递归查找
    /// </summary>
    /// <param name="value"></param>
    /// <param name="nameValue"></param>
    /// <returns></returns>
    public static Transform FindChildByName(this Transform value, string nameValue)
    {
        Transform temp = value.Find(nameValue);
        if (temp != null) return temp;
        for (int i = 0; i < value.childCount; i++)
        {
            temp = FindChildByName(value.GetChild(i), nameValue);
            if (temp != null) return temp;
        }

        return null;
    }

    public static Transform FindParentByName(this Transform value, string nameValue)
    {
        return value.name.Equals(nameValue) ? value : FindParentByName(value.parent, nameValue);
    }

    public static T FindChildByType<T>(this Transform value) where T : UnityEngine.Object
    {
        T t = value.GetComponent<T>();
        if (t != null)
        {
            return t;
        }

        for (int i = 0; i < value.childCount; i++)
        {
            t = FindChildByType<T>(value.GetChild(i));
            if (t != null)
            {
                return t;
            }
        }

        return default;
    }
}

/// <summary>
/// https://www.bilibili.com/read/cv14521774/ Animator实现动画
/// </summary>
public static class AnimatorTools
{
    /// <summary>
    /// 动画添加事件
    /// 是因为动画切换的时候融合问题，把事件设置到融合时间内会不触发，
    /// 1.用别的办法，
    /// 2.把动画播放完退出的勾去掉
    /// 3把事件触发事件设置在融合时间之前。
    /// </summary>
    /// <param name="animator">动画机</param>
    /// <param name="eventName">事件名称</param>
    /// <param name="animationName"></param>
    /// <param name="parameter">执行方法后要传入的参数</param>
    /// <param name="frame">从左边开始为0 如果是秒0.02 就是第2帧</param>
    public static void AddAnimatorEvent(this Animator animator, string animationName, int frame, string eventName, System.Object parameter = default)
    {
        AnimationEvent evt = new AnimationEvent(); // 创建一个事件
        AnimationClip clip = null;
        foreach (var animationClip in animator.runtimeAnimatorController.animationClips)
        {
            if (!animationClip.name.Equals(animationName)) continue;
            clip = animationClip; // 设置目标动画剪辑
            break;
        }

        if (clip == null)
            throw new Exception($"没有找到动画片段{animationName}");

        evt.functionName = eventName; // 绑定触发事件后要执行的方法名

        if (parameter is String)
        {
            evt.stringParameter = (string)parameter;
        }
        else if (parameter is Int32)
        {
            evt.intParameter = (int)parameter;
        }
        else if (parameter is Single)
        {
            evt.floatParameter = (float)parameter;
        }

        evt.time = 1 / clip.frameRate * frame; // 设置事件关键帧的位置，当事件过了1.3秒后执行
        List<AnimationEvent> animationEvents = new List<AnimationEvent>(clip.events) { evt };
        clip.events = animationEvents.ToArray();
        //clip.AddEvent(evt); // 绑定事件此方法会使事件添加的顺序出现变化
    }

    public static bool CheckClip(this Animator animator, string animationName)
    {
        foreach (var animationClip in animator.runtimeAnimatorController.animationClips)
        {
            if (animationClip.name.Equals(animationName))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="animationName"></param>
    /// <param name="eventName"></param>
    public static void UnAnimatorAddEvent(this Animator animator, string animationName, params string[] eventName)
    {
        //查找AnimationClip
        AnimationClip clip = null;
        foreach (var animationClip in animator.runtimeAnimatorController.animationClips)
        {
            if (animationClip.name.Equals(animationName))
            {
                clip = animationClip;
                break;
            }
        }

        //移除
        List<AnimationEvent> events = new List<AnimationEvent>(clip.events);
        foreach (var str in eventName)
        {
            for (int i = 0; i < events.Count; i++)
            {
                if (str.Equals(events[i].functionName))
                {
                    events.Remove(events[i]);
                }
            }
        }

        //重新监听
        clip.events = events.ToArray();
    }

    /// <summary>
    /// 清空所有动画的事件
    /// </summary>
    /// <param name="animator"></param>
    public static void UnAnimatorAddEventAll(this Animator animator)
    {
        AnimationClip[] animationClips = animator.runtimeAnimatorController.animationClips;
        foreach (var animationClip in animationClips)
            animationClip.events = default;
    }

    /// <summary>
    /// 可以使用下边的方法在程序运行时找到当前播放的动画是否有我们查找的关键帧
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="mathName"></param>
    /// <returns></returns>
    public static bool CurClipHaveChangeHeroEvents(Animator animator, string mathName)
    {
        if (animator.GetCurrentAnimatorClipInfo(0).Length <= 0) return false;
        AnimationClip clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        if (clip == null || clip.events.Length <= 0) return false;
        AnimationEvent[] events = clip.events;
        for (int i = 0; i < events.Length; i++)
        {
            if (events[i].functionName == mathName)
            {
                return true;
            }
        }

        return false;
    }
}

public static class Tween
{
    public static IEnumerator DOUIFade(this CanvasGroup cg, float startValue, float endValue, float duration)
    {
        cg.alpha = startValue;
        cg.interactable = endValue > 0.5;
        cg.blocksRaycasts = endValue > 0.5;
        yield return DOTween.To(delegate(float a) { cg.alpha = a; }, startValue, endValue, duration).SetUpdate(true).WaitForCompletion();
    }

    public static Tweener DOFade(this CanvasGroup cg, float endValue, float duration)
    {
        return DOTween.To(() => cg.alpha, delegate(float a) { cg.alpha = a; }, endValue, duration);
    }

    public static Tweener FadeTo(this CanvasGroup cg, float endValue, float duration, bool isOpen)
    {
        return DOTween.To(() => cg.alpha, delegate(float a) { FadeTo(cg, a, isOpen); }, endValue, duration);
    }

    /// <summary>
    /// CanvasGroupInit
    /// </summary>
    /// <param name="cg"></param>
    /// <param name="startValue"></param>
    /// <param name="isOpen"></param>
    public static void FadeTo(this CanvasGroup cg, float startValue, bool isOpen = true)
    {
        cg.alpha = startValue;
        if (isOpen)
        {
            cg.interactable = startValue > 0.5;
            cg.blocksRaycasts = startValue > 0.5;
        }
    }

    /// <summary>
    /// 界面是否显示
    /// </summary>
    /// <param name="cg"></param>
    /// <returns></returns>
    public static bool CgAlphaOpen(this CanvasGroup cg) => cg.alpha >= 0.95f;
}

public static class IntTools
{
    public static int ToInt(this object obj)
    {
        return Convert.ToInt32(obj);
    }
}

public static class FloatTools
{
    public static float ToFloat(this object obj)
    {
        return Convert.ToSingle(obj);
    }
}

public static class EnumTools
{
    /// <summary>
    /// 是否是枚举值
    /// </summary>
    /// <param name="value"></param>
    /// <param name="ignoreCase">忽略大小写</param>
    /// <typeparam name="TEnum"></typeparam>
    /// <returns></returns>
    public static bool IsInEnum<TEnum>(this string value, bool ignoreCase = false) where TEnum : struct, IConvertible
    {
        if (!ignoreCase)
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }

        string[] names = Enum.GetNames(typeof(TEnum));
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].Equals(value, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    /// <summary>
    /// 尝试转换枚举
    /// </summary>
    /// <param name="value">字符串</param>
    /// <param name="ignoreCase">忽略大小写</param>
    /// <param name="result">新的枚举</param>
    /// <typeparam name="TEnum">枚举</typeparam>
    /// <returns></returns>
    public static bool TryParse<TEnum>(string value, out TEnum result, bool ignoreCase = false) where TEnum : struct
    {
        if (Enum.IsDefined(typeof(TEnum), value))
        {
            result = (TEnum)((object)Enum.Parse(typeof(TEnum), value, true));
            return true;
        }

        if (ignoreCase)
        {
            string[] names = Enum.GetNames(typeof(TEnum));
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    result = (TEnum)((object)Enum.Parse(typeof(TEnum), names[i]));
                    return true;
                }
            }
        }

        result = default(TEnum);
        return false;
    }

    /// <summary>
    /// 转枚举
    /// </summary>
    /// <param name="value">字符串</param>
    /// <param name="ignoreCase">忽略大小写</param>
    /// <typeparam name="TEnum">枚举</typeparam>
    /// <returns></returns>
    public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = false) where TEnum : struct, IConvertible
    {
        TryParse<TEnum>(value, out TEnum result, ignoreCase);
        return result;
    }

    public static string[] ToArray<TEnum>(this System.Object obj) where TEnum : struct, IConvertible
    {
        return ToArray(typeof(TEnum));
    }

    public static string[] ToArray(this Type type)
    {
        return Enum.GetNames(type);
    }
}

/// <summary>
/// PlayerPrefs
/// </summary>
public static class PlayerPrefsTools
{
    public static void SetString(this string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public static string GetString(this string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public static bool HasKey(this string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public static void Save()
    {
        PlayerPrefs.Save();
    }
}

/// <summary>
/// 字符串工具
/// </summary>
public static class StringUtils
{
    public static bool IsInArray(this string value, string[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (value != array[i]) continue;
            return true;
        }

        return false;
    }

    public static string Combine(this string name, string path)
    {
        return System.IO.Path.Combine(path, name);
    }

    public static string[] ParseStringArray(string text, char separator = ',')
    {
        string text2 = text.Trim();
        return text2.Substring(1, text2.Length - 2).Split(new char[]
        {
            separator
        });
    }

    public static int?[] ParseIntArray(this string text, char separator = ',')
    {
        string[] array = ParseStringArray(text, separator);
        int?[] array2 = new int?[array.Length];
        for (int i = 0; i < array2.Length; i++)
        {
            int value;
            if (int.TryParse(array[i], out value))
            {
                array2[i] = new int?(value);
            }
            else
            {
                array2[i] = null;
            }
        }

        return array2;
    }

    public static readonly string[] Int2String = new string[]
    {
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9"
    };
}

/// <summary>
/// Vector2 拓展 VectorUtils
/// </summary>
public static class VU
{
    /// <summary>
    /// 两个向量的点乘,大于0则面对，否则则背对着
    /// 点乘得到你当前的面朝向的方向和你到敌人的方向的所成的角度大小
    /// 点积的结果可以用来判断两个向量的方向关系：
    /// 当结果为正数时，说明两个向量之间的夹角小于 90 度；
    /// 当结果为负数时，说明夹角大于 90 度；当结果为 0 时，说明两个向量垂直
    /// https://www.cnblogs.com/Kprogram/p/4135952.html
    /// https://blog.csdn.net/yupu56/article/details/53609028
    /// https://blog.csdn.net/kingsea168/article/details/50395054
    /// https://blog.csdn.net/LearnToStick/article/details/120976515 Unity点乘和叉乘的基本使用
    /// https://blog.csdn.net/yupu56/article/details/53609028 Unity 点乘和叉乘的原理和使用
    /// </summary>
    public static float V2Dot(Vector2 lhs, Vector2 rhs)
    {
        return Vector2.Dot(lhs, rhs);
    }

    /// <summary>
    /// 叉乘
    /// 叉乘可以判断你是往左转还是往右转更好的转向敌人
    /// https://blog.csdn.net/LearnToStick/article/details/120976515 Unity点乘和叉乘的基本使用
    /// https://blog.csdn.net/yupu56/article/details/53609028 Unity 点乘和叉乘的原理和使用
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
    {
        return Vector3.Cross(lhs, rhs);
    }

    /// <summary>
    /// 方法用于对两个 Vector2 向量进行逐元素相乘的操作
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vector2 V2Scale(Vector2 a, Vector2 b)
    {
        return Vector2.Scale(a, b);
    }

    /// <summary>
    /// 坡度
    /// </summary>
    /// <param name="vector2"></param>
    /// <returns></returns>
    public static float Slope(this Vector2 vector2)
    {
        return vector2.y / vector2.x;
    }

    /// <summary>
    /// Mathf.Clamp 是 Unity 引擎中的一个数学函数，用于将一个值限制在指定的范围内。
    /// 该函数接受一个值、最小值和最大值作为参数，然后返回将该值限制在最小值和最大值之间的结果。
    /// </summary>
    /// <returns></returns>
    public static float Clamp(float value, float min, float max)
    {
        return Mathf.Clamp(value, min, max);
    }

    /// <summary>
    /// 平滑地将一个 Vector3 值从当前值向目标值过渡
    /// </summary>
    public static Vector3 SmoothDamp(Vector3 current,
        Vector3 target,
        ref Vector3 currentVelocity,
        float smoothTime,
        [DefaultValue("Mathf.Infinity")] float maxSpeed,
        [DefaultValue("Time.deltaTime")] float deltaTime)
    {
        return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
    }
}

/// <summary>
/// LitJsonTools
/// https://www.cnblogs.com/msxh/p/12541159.html#_label1 魔改LitJson 
/// </summary>
public static class LitJsonTools
{
}

/// <summary>
/// Vector3工具
/// </summary>
public static class Vector3Utils
{
    public static Vector3 AddX(this Vector3 vector3, float delta)
    {
        vector3.x += delta;
        return vector3;
    }

    public static Vector3 AddY(this Vector3 vector3, float delta)
    {
        vector3.y += delta;
        return vector3;
    }

    public static Vector3 SetX(this Vector3 vector3, float x)
    {
        vector3.x = x;
        return vector3;
    }

    public static Vector3 SetY(this Vector3 vector3, float y)
    {
        vector3.y = y;
        return vector3;
    }

    public static Vector3 SetZ(this Vector3 vector3, float z)
    {
        vector3.z = z;
        return vector3;
    }
}

/// <summary>
/// 全称ExpandDoTween
/// https://developer.unity.cn/projects/65201f3eedbc2ae4d76cfb71 OTween教程☀️DOTween的使用教程
/// https://blog.csdn.net/linjf520/article/details/107798477 DOTween库的使用问题
/// https://www.cnblogs.com/Damon-3707/p/11367585.html
/// https://dotween.demigiant.com/documentation.php?api=WaitForCompletion
/// https://blog.csdn.net/qq_51978873/article/details/120738442 DOTween中文详解
/// </summary>
public static class EDoTween
{
    /// <summary>
    /// OnComplete（TweenCallback回调）
    /// 指结束后开始调用
    /// </summary>
    /// <param name="t"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T OnComplete<T>(this T t, TweenCallback action) where T : DG.Tweening.Tween
    {
        return TweenSettingsExtensions.OnComplete(t, action);
    }

    /// <summary>
    /// OnStepComplete（TweenCallback回调）
    /// 完成单个循环开始调用
    /// </summary>
    /// <param name="t"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T OnStepComplete<T>(this T t, TweenCallback action) where T : DG.Tweening.Tween
    {
        // transform.
        // DOMove(new Vector3(0, 3f, 0), 1).
        // SetLoops(3, LoopType.Yoyo).
        // OnStepComplete(() => { Debug.Log("666"); });
        // 最后会输出三次666。
        return TweenSettingsExtensions.OnStepComplete(t, action);
    }

    public static T OnKill<T>(this T t, TweenCallback action) where T : DG.Tweening.Tween
    {
        // Sequence q = DOTween.Sequence();
        // q.Append(transform.DOMove(new Vector3(0, 3f, 0), 2).OnKill(() => { Debug.Log("666"); }));
        // q.Insert(1, transform.DOMove(new Vector3(0, -1f, 0), 1));
        // q.Append(transform.DOMove(new Vector3(0, 2f, 0), 3));
        // 是指所有运动结束时，才触发。最后结果为物体到达（0，2，0），才输出666；
        return TweenSettingsExtensions.OnKill(t, action);
    }

    /// <summary>
    /// 忽略Time.timeScale = 0的影响
    /// </summary>
    /// <param name="t"></param>
    /// <param name="isIndependentUpdate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T SetUpdate<T>(this T t, bool isIndependentUpdate) where T : DG.Tweening.Tween
    {
        return TweenSettingsExtensions.SetUpdate(t, isIndependentUpdate);
    }

    /// <summary>
    /// 设置缓和
    /// </summary>
    /// <param name="t"></param>
    /// <param name="ease"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T SetEase<T>(this T t, Ease ease) where T : DG.Tweening.Tween
    {
        return TweenSettingsExtensions.SetEase(t, ease);
    }

    /// <summary>
    /// 将某点在一定时间内移动到某点
    /// </summary>
    /// <param name="setter">要移动的初始值</param>
    /// <param name="startValue">开始值</param>
    /// <param name="endValue">结束值</param>
    /// <param name="duration">时间</param>
    /// <returns></returns>
    public static Tweener To(DOSetter<float> setter, float startValue, float endValue, float duration)
    {
        return DOTween.To(setter, startValue, endValue, duration);
    }

    /// <summary>
    /// 等待动画执行完
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static YieldInstruction WaitForCompletion(this DG.Tweening.Tween t)
    {
        return TweenExtensions.WaitForCompletion(t);
    }
}

/// <summary>
/// CSV文件读取
/// </summary>
public class CSVHelper
{
    public static IList<T> Csv2List<T>(string fileName, Func<string[], T> setFunc)
    {
        List<T> list = new List<T>();
        TextAsset textAsset = Asset.LoadFromResources<TextAsset>("Conf/DB", fileName);
        using (StringReader stringReader = new StringReader(textAsset.text))
        {
            string text;
            while ((text = stringReader.ReadLine()) != null)
            {
                string[] arg = text.Split(new char[]
                {
                    ','
                });
                list.Add(setFunc(arg));
            }
        }

        return list;
    }

    public static IDictionary<TKey, TValue> Csv2Dictionary<TKey, TValue>(string fileName, Func<string, TKey> setKey, Func<string[], TValue> setValue)
    {
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
        TextAsset textAsset = Asset.LoadFromResources<TextAsset>("Conf/DB", fileName);
        using (StringReader stringReader = new StringReader(textAsset.text))
        {
            string text;
            while ((text = stringReader.ReadLine()) != null)
            {
                string[] array = text.Split(new char[]
                {
                    ','
                });
                dictionary.Add(setKey(array[0]), setValue(array));
            }
        }

        return dictionary;
    }
}

public class Asset
{
    public static bool SerializeToFile(string path, string name, object obj)
    {
        string str = JsonMapper.ToJson(obj);
        return SaveToFile(path, name, str);
    }

    public static T DeserializeFromFile<T>(string path, string name)
    {
        string json = LoadFromFile(path, name);
        return JsonMapper.ToObject<T>(json);
    }

    public static T LoadFromResources<T>(string path, string name) where T : UnityEngine.Object
    {
        return Resources.Load<T>(System.IO.Path.Combine(path, name));
    }

    public struct Path
    {
        public const string Config = "Conf/";
    }

    public static bool SaveToFile(string path, string name, string str)
    {
        UnityEngine.Debug.LogError(path + name + " 不能在非编辑器下保存");
        return false;
    }

    public static string LoadFromFile(string path, string name)
    {
        string result = string.Empty;
        TextAsset textAsset = Resources.Load<TextAsset>(System.IO.Path.Combine(path, name));
        result = textAsset?.text;
        if (string.IsNullOrEmpty(result))
            throw new Exception($"{System.IO.Path.Combine(path, name)}文件不存在");
        return result;
    }

    public static bool IsFileExist(string path, string name)
    {
        UnityEngine.Object x = Resources.Load(System.IO.Path.Combine(path, name));
        return x != null;
    }
}