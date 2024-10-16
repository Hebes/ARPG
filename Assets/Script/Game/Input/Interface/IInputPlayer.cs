using UnityEngine;

/// <summary>
/// 玩家输入接口
/// </summary>
public interface IInputPlayer
{
    /// <summary>
    /// 获取遥感
    /// </summary>
    /// <param name="axis"></param>
    /// <returns></returns>
    public Vector2 GetJoystick(string axis);

    /// <summary>
    /// 获取原始遥感
    /// </summary>
    /// <param name="axis"></param>
    /// <returns></returns>
    public Vector2 GetJoystickRaw(string axis);

    /// <summary>
    /// 获取按钮
    /// </summary>
    /// <param name="buttonName"></param>
    /// <returns></returns>
    public bool GetButton(string buttonName);

    /// <summary>
    /// 设置振动
    /// </summary>
    /// <param name="leftMotorValue"></param>
    /// <param name="rightMotorValue"></param>
    public void SetVibration(float leftMotorValue, float rightMotorValue);
}