using System;
using UnityEngine;

/// <summary>
/// 玩家输入控制
/// 输入后行动
/// </summary>
public class PlayerInput : MonoBehaviour
{
    public bool battlePause;
    private PlayerAbilities _abilities;
    private PlayerAttribute _attribute;

    private void Awake()
    {
        _abilities = GetComponent<PlayerAbilities>();
        _attribute = R.Player.Attribute;
        InputDriver temp = InputDriver.I;
    }

    private void Update()
    {
        if (WorldTime.IsPausing) return;
        if (_attribute.isDead) return;
        if (!battlePause) //是否暂停战斗
        {
            if (Setting.GetFlag(InputType.Flash))
            {
                //左闪
                if (Input.Game.Flash.Left.OnPressed)
                {
                    _abilities.flash.FlashLeft();
                    return;
                }

                //右闪
                if (Input.Game.Flash.Right.OnPressed)
                {
                    _abilities.flash.FlashRight();
                    return;
                }

                //按照角色的防线闪
                if (Input.Game.Flash.FaceDirection.OnPressed)
                {
                    _abilities.flash.FlashFace();
                    return;
                }
            }

            //上挑
            if (Input.Game.UpRising.OnPressed && Setting.GetFlag(InputType.UpRising))
            {
                _abilities.upRising.UpJumpAttack();
            }

            //攻击地面
            if (Input.Game.HitGround.OnPressed)
            {
                _abilities.hitGround.HitGround();
            }

            // if (Input.Game.FlashAttack.OnPressed && pab.flashAttack.PressFlashAttack())
            // {
            //     return;
            // }
            // if (Input.Game.Execute.OnPressed && Setting.GetFlag(InputType.Execute))
            // {
            //     //     pab.execute.Execute();
            //     //     return;
            // }

            //跳跃
            if (Input.Game.Jump.OnPressed && Setting.GetFlag(InputType.Jump))
                _abilities.jump.Jump();

            //平常攻击
            if (Input.Game.Atk.OnClick && Setting.GetFlag(InputType.Attack))
            {
                if (Input.Game.MoveLeft.Pressed)
                    _abilities.attack.PlayerAttack(-1, false);
                else if (Input.Game.MoveRight.Pressed)
                    _abilities.attack.PlayerAttack(1, false);
                else
                    _abilities.attack.PlayerAttack(3, false);
            }

            //重攻击
            if (Input.Game.CirtAtk.OnClick && Setting.GetFlag(InputType.Attack))
            {
                if (Input.Game.MoveLeft.Pressed)
                    _abilities.attack.PlayerAttack(-1, true);
                else if (Input.Game.MoveRight.Pressed)
                    _abilities.attack.PlayerAttack(1, true);
                else
                    _abilities.attack.PlayerAttack(3, true);
            }

            //重攻击长按
            if (Input.Game.CirtAtk.LongPressed && Setting.GetFlag(InputType.Attack))
            {
                if (Input.Game.MoveLeft.Pressed)
                    _abilities.attack.PlayerCirtPressAttack(-1);
                else if (Input.Game.MoveRight.Pressed)
                    _abilities.attack.PlayerCirtPressAttack(1);
                else
                    _abilities.attack.PlayerCirtPressAttack(3);
            }

            //重攻击按钮释放
            if (Input.Game.CirtAtk.OnReleased && Setting.GetFlag(InputType.Attack))
                _abilities.attack.PlayerCirtPressAttackReleasd();

            //充能
            // if (Input.Game.Charging.LongPressed && Setting.GetFlag(InputType.Charging))
            // {
            //     //Abilities.charge.Charging();
            // }
        }
        if (Setting.GetFlag(InputType.Move))
        {
            if (Input.Game.MoveLeft.Pressed)
                _abilities.move.Move(-1);
            else if (Input.Game.MoveRight.Pressed)
                _abilities.move.Move(1);
            if (Input.Game.MoveLeft.OnReleased || Input.Game.MoveRight.OnReleased)
                R.Player.Action.tempDir = 3;
        } //能够移动
    }

    /// <summary>
    /// 设置
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// 输入类型
        /// </summary>
        public static InputType AllowInput = InputType.All;

        /// <summary>
        /// 设置标志位
        /// https://www.runoob.com/csharp/csharp-operators.html C#运算符
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="value"></param>
        private static void SetFlag(InputType inputType, bool value)
        {
            if (value)
                AllowInput |= inputType;
            else
                AllowInput &= ~inputType;
        }

        /// <summary>
        /// 判断能否执行
        /// </summary>
        /// <param name="inputType"></param>
        /// <returns></returns>
        public static bool GetFlag(InputType inputType)
        {
            return (inputType & AllowInput) == inputType;
        }
    }
}

/// <summary>
/// 输入的类型
/// </summary>
[Flags] //表示该枚举类型可以用于按位操作，允许枚举值进行组合，并且每个枚举值都是一个单独的位标志，按位或 |、按位与 &、按位取反 ~ 等
public enum InputType
{
    None = 0,
    Move = 1,
    Attack = 2,
    Flash = 4,
    Skill = 8,
    HitGround = 16,
    UpRising = 32,
    Jump = 64,

    /// <summary>
    /// 能否充能
    /// </summary>
    Charging = 128,

    /// <summary>
    /// 是否可以执行
    /// </summary>
    Execute = 256,
    Option = 512,
    Map = 1024,
    All = 2047
}