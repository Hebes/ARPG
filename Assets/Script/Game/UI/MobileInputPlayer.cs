using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 移动端移动
/// </summary>
public class MobileInputPlayer : SMono<MobileInputPlayer>, IInputPlayer
{
    // [Header("流")] [SerializeField] private NewButton _button1;
    // [Header("闪")] [SerializeField] private NewButton _button2;
    // [Header("重击")] [SerializeField] private NewButton _button3;
    // [Header("跳跃")] [SerializeField] private NewButton _button4;
    // [Header("攻击")] [SerializeField] private NewButton _button5;
    // [Header("斩")] [SerializeField] private NewButton _button6;
    [Header("选项")] [SerializeField] private GameObject _options;
    // [Header("L2")] [SerializeField] private NewButton _l2;
    // [Header("R2")] [SerializeField] private NewButton _r2;
    // [SerializeField] private FBTouchPad _joystick;
    // [SerializeField] private FBSwipe _swipe;
    //[SerializeField] private Localize _button3Sprite;
    [SerializeField] private Text _button3Sprite;
    // [SerializeField] private UISprite _button5SHIMainSprite;
    // [SerializeField] private UISprite _button5SHINumSprite;
    
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _mainController;
    [SerializeField] private GameObject _l2R2Widget;

    private bool _mainControllerVisiable;

    /// <summary>
    /// 是否看的见输入面板
    /// </summary>
    public bool Visible
    {
        get => _panel.gameObject.activeSelf;
        set => _panel.gameObject.SetActive(value);
    }

    /// <summary>
    /// 主面板是否可见
    /// </summary>
    public bool MainControllerVisiable
    {
        get => _mainControllerVisiable;
        set
        {
            _mainControllerVisiable = value;
            // _mainController.alpha = (float)(!value ? 0 : 1);
            _mainController.gameObject.SetActive(value);
        }
    }

    /// <summary>
    /// 是否可见
    /// </summary>
    public bool OptionsVisible
    {
        get => UIOptions.I.IsActive;
        set => UIOptions.I.IsActive = true;
    }

    // /// <summary>
    // /// 按钮6是否可见
    // /// </summary>
    // public bool Button6Visible
    // {
    //     get => _button6.IsActive;
    //     set => _button6.IsActive = value;
    // }

    public bool L2R2Visiable
    {
        get => _l2R2Widget.gameObject.activeSelf;
        private set => _l2R2Widget.gameObject.SetActive(value);
    }

    public void Awake()
    {
        Visible = true;
        MainControllerVisiable = true;
    }

   

    /// <summary>
    /// 可见刀锋风暴
    /// </summary>
    public void VisiableBladeStorm()
    {
        //_button1.gameObject.SetActive(R.Player.EnhancementSaveData.BladeStorm > 0);
    }

    private void OnEnable()
    {
        //EGameEvent.EnhanceLevelup.Register(OnEnhancementLevelUp);
    }


    private void OnDisable()
    {
        //EGameEvent.EnhanceLevelup.UnRegister(OnEnhancementLevelUp);
    }

    private void Update()
    {
        // if (R.Mode.CurrentMode != Mode.AllMode.Battle) return;
        // // _button3Sprite.Term = !R.Enemy.CanBeExecutedEnemyExist() || !R.Player.CanExecute()
        // //     ? "mobile/uisprite/button_02_1"
        // //     : "mobile/uisprite/button_02_2"; 
        // _button3Sprite.text = !R.Enemy.CanBeExecutedEnemyExist() || !R.Player.CanExecute()
        //     ? "mobile/uisprite/button_02_1"
        //     : "mobile/uisprite/button_02_2";
    }

    public void EnterSHI()
    {
        // _button1.gameObject.SetActive(false);
        // _button2.gameObject.SetActive(false);
        // _button3.gameObject.SetActive(false);
        // _button4.gameObject.SetActive(false);
        // _button5SHIMainSprite.spriteName = "Button_03";
        // _button5SHINumSprite.spriteName = "Num03";
    }

    public void ExitSHI()
    {
        VisiableBladeStorm();
        // _button2.gameObject.SetActive(true);
        // _button3.gameObject.SetActive(true);
        // _button4.gameObject.SetActive(true);
        // _button5SHIMainSprite.spriteName = "Button_01";
        // _button5SHINumSprite.spriteName = "Num01";
    }

    public Vector2 GetJoystick(string axis)
    {
        // if (axis != null)
        // {
        //     if (axis == "Joystick")
        //     {
        //         return (!MainControllerVisiable) ? Vector2.zero : _joystick.Axis;
        //     }
        //
        //     if (axis == "Swipe")
        //     {
        //         return (!MainControllerVisiable) ? Vector2.zero : _swipe.Direction;
        //     }
        // }
        //
        // throw new ArgumentOutOfRangeException("axis", axis);
        return Vector2.zero;
    }

    public Vector2 GetJoystickRaw(string axis)
    {
        throw new ArgumentOutOfRangeException(nameof(axis), axis);
    }
    public bool GetButton(string buttonName)
    {
        switch (buttonName)
        {
            case "Options":
                return UIOptions.I.optionsClick;
            
            // case "Button1":
            //     return MainControllerVisiable && _button1.IsPressed;
            // case "Button2":
            //     return MainControllerVisiable && _button2.IsPressed;
            // case "Button3":
            //     return MainControllerVisiable && _button3.IsPressed;
            // case "Button4":
            //     return MainControllerVisiable && _button4.IsPressed;
            // case "Button5":
            //     return MainControllerVisiable && _button5.IsPressed;
            // case "Button6":
            //     return _button6.IsPressed;
            //
            // case "L2":
            //     return _l2.IsPressed;
            // case "R2":
            //     return _r2.IsPressed;
        }

        
        throw new Exception($"按钮 \"{buttonName}\" 不存在.");
    }

    public void SetVibration(float leftMotorValue, float rightMotorValue)
    {
    }

    public void ShowL2R2(string l2, string r2)
    {
        // if (!L2R2Visiable)
        // {
        //     L2R2Visiable = true;
        //     MainControllerVisiable = false;
        // }
        //
        // _l2.Text = l2;
        // _r2.Text = r2;
    }

    public void HideL2R2(bool showMainController = true)
    {
        L2R2Visiable = false;
        MainControllerVisiable = showMainController;
    }

    private void OnEnhancementLevelUp(object udata)
    {
        // (string,int) msg = ((string, int))udata;
        // if (!_button1.gameObject.activeSelf && msg.Item1 == "bladeStorm")
        //     _button1.gameObject.SetActive(true);
    }

    public void UpdateRadiusAndPosition()
    {
        //_joystick.UpdateRadiusAndPosition();
    }
}