using System;
using UnityEngine;

public static class ColorUtils
{
    public static Color SetAlpha(this Color color, float a)
    {
        color.a = a;
        return color;
    }

    public static Color MultiplyRGB(this Color color, float multiplier)
    {
        float a = color.a;
        color *= multiplier;
        color.a = a;
        return color;
    }

    public static Color MultiplyAlpha(this Color color, float multiplier)
    {
        color.a *= multiplier;
        return color;
    }

    public static string ToBBCode(this Color color)
    {
        return $"[{color.ToHexString()}]";
    }

    public static string ToHexString(this Color color)
    {
        return ColorUtility.ToHtmlStringRGB(color);
    }
}