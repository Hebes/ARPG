using System;
using UnityEngine;

/// <summary>
/// PC的玩家输入
/// </summary>
public class PCInputPlayer : IInputPlayer
{
    public Vector2 GetJoystick(string axis)
    {
        throw new NotImplementedException();
    }

    public Vector2 GetJoystickRaw(string axis)
    {
        throw new NotImplementedException();
    }

    public void SetVibration(float leftMotorValue, float rightMotorValue)
    {
    }

    public bool GetButton(string buttonName)
    {
        switch (buttonName)
        {
            case "Down": return UnityEngine.Input.GetKey(KeyCode.S);
            case "Up": return UnityEngine.Input.GetKey(KeyCode.W);
            case "Left": return UnityEngine.Input.GetKey(KeyCode.A);
            case "Right": return UnityEngine.Input.GetKey(KeyCode.D);
            case "Attack": return UnityEngine.Input.GetKey(KeyCode.J);
            case "Jump": return UnityEngine.Input.GetKey(KeyCode.Space);
            case "Options": return UnityEngine.Input.GetKey(KeyCode.O);
            case "CirtAttack": return UnityEngine.Input.GetKey(KeyCode.K);
            case "Flash": return UnityEngine.Input.GetKey(KeyCode.L);
            default: throw new Exception("按键错误");
        }
    }
}