using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// 打印程序栈
/// </summary>
public class CallStack
{
    /// <summary>
    /// 基本说明
    /// </summary>
    public string baseInfo;

    /// <summary>
    /// 调用时间
    /// </summary>
    public string callTime;

    /// <summary>
    /// 调用时间
    /// </summary>
    public TimeSpan CallTime;

    /// <summary>
    /// 详细
    /// </summary>
    public List<string> call = new List<string>();
}

public static class ECallStack
{
    public static void CallStack(this string info)
    {
        CallStack callStack = new CallStack();
        StackTrace stackTrace = new StackTrace();
        callStack.CallTime = DateTime.Now.TimeOfDay;
        callStack.callTime = DateTime.Now.TimeOfDay.ToString();
        for (var i = 2; i < stackTrace.FrameCount; i++)
        {
            //堆栈帧
            var stackFrame = stackTrace.GetFrame(i);
            callStack.call.Add($"类:{stackFrame.GetMethod().DeclaringType.Name}, 方法:{stackFrame.GetMethod().Name}");
        }

        StringBuilder sb = new StringBuilder();
        foreach (var s in callStack.call)
        {
            sb.AppendLine(s);
        }

        sb.ToString().Log();
    }

    public static void CallStack(this float info)
    {
        info.ToString().CallStack();
    }
}