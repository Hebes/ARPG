using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机
/// </summary>
public class StateMachine :MonoBehaviour
{
    public event EventHandler<StateEventArgs> OnEnter; //进入,切换动画
    public event EventHandler<StateEventArgs> OnExit; //退出
    public event EventHandler<StateEventArgs> StateUpdate; //状态更新
    public event EventHandler<TransferEventArgs> OnTransfer; //切换
    private Dictionary<string, State> stateDic = new();
    private Enum _nextState; //当前状态
    
    public string currentState { get; private set; }

    /// <summary>
    /// 是否是某个状态
    /// </summary>
    /// <returns></returns>
    public bool IsSta(string[] array) => currentState.IsInArray(array);

    public void Awake()
    {
        currentState = "__Empty__";
        AddState(currentState);
    }

    public void Update()
    {
        if (!string.IsNullOrEmpty(currentState)) return;
        if (!stateDic.TryGetValue(currentState, out State state)) return;
        StateUpdate?.Invoke(this, new StateEventArgs(state.name));//状态轮询
        state.StateUpdate?.Invoke(this, new StateEventArgs(state.name));//状态轮询
    }

    //------------------------添加状态-----------------------------
    private State AddState(string nameValue)
    {
        if (stateDic.ContainsKey(nameValue))
            $"在状态机中添加了同名方法{nameValue}".Error();
        State state = new State { name = nameValue };
        stateDic[nameValue] = state;
        return state;
    }
    
    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="names"></param>
    public void AddStates(string[] names)
    {
        foreach (var s in names)
            AddState(s);
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="nextState"></param>
    public void SetState(Enum nextState) => SetState(nextState.ToString());

    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="nextState"></param>
    public void SetState(string nextState)
    {
        StateEventArgs nextStateArgs = new StateEventArgs(stateDic[nextState].name);
        StateEventArgs currentStateArgs = new StateEventArgs(stateDic[currentState].name);
        if (currentState == "__Empty__")
        {
            stateDic[nextState].OnEnter?.Invoke(this,nextStateArgs);
            OnEnter?.Invoke(this,nextStateArgs);
            currentState = nextState;
            return;
        }
        stateDic[currentState].OnExit?.Invoke(this, currentStateArgs);//小状态状态退出
        OnExit?.Invoke(this, currentStateArgs);
        OnTransfer?.Invoke(this,new TransferEventArgs(currentState, nextState));
        try
        {
            stateDic[nextState].OnEnter?.Invoke(this,nextStateArgs);
        }
        catch (KeyNotFoundException)
        {
            $"{nextState}未找到".Exception();
        }
        OnEnter?.Invoke(this,new StateEventArgs(stateDic[nextState].name));
        currentState = nextState;
    }

    //------------------------设置状态延迟-----------------------------
    public void SetStateDelay(Enum nextState, float time)
    {
        _nextState = nextState;
        StartCoroutine(SetStateDelayCoroutine(time));
        return;

        IEnumerator SetStateDelayCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            SetState(_nextState.ToString());
        }
    }

    /// <summary>
    /// 小状态中的状态进入
    /// </summary>
    public class State
    {
        public string name;
        public EventHandler<StateEventArgs> OnEnter;
        public EventHandler<StateEventArgs> OnExit;
        public EventHandler<StateEventArgs> StateUpdate;
    }
}