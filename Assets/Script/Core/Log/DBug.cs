using System;
using UnityEngine;
/// <summary>
/// 日志
/// </summary>
// public class DBug
// {
//     public static void Log(string strValue) => Debug.Log(strValue);
//     public static void Error(string strValue) => Debug.LogError(strValue);
// }
public static class LogE
{
    public static void Log(this string strValue) => Debug.Log(strValue); // DBug.Log(strValue);
    public static void Log(this object message) => Debug.unityLogger.Log(LogType.Log, message);
    public static void Error(this string strValue) => Debug.LogError(strValue); // DBug.Error(strValue);
    public static void Error(this int strValue) => Debug.LogError(strValue); // DBug.Error(strValue);
    public static void Warning(this string strValue) => Debug.LogWarning(strValue);
    public static void Exception(this string strValue) => throw new Exception(strValue);
}