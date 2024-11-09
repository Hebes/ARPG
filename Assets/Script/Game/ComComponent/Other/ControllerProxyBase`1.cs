using System;
using UnityEngine;

public abstract class ControllerProxyBase<T> : MonoBehaviour where T : MonoBehaviour
{
	[HideInInspector]
	private protected T effect {  get; private set; }

	private void Start()
	{
		this.ResetEffect();
		this.ResetToDefault();
		this.ThisStart();
	}

	private void OnEnable()
	{
		ControllerProxyBase<T>.effectLock = this;
		this.ResetEffect();
		this.OnThisEnable();
	}

	private void ResetEffect()
	{
		if (this.effect == null)
		{
			if (Camera.main != null)
			{
				this.effect = this.GetEffect();
				if (this.effect != null && this._autoEnable)
				{
					T effect = this.effect;
					effect.enabled = true;
				}
			}
			else
			{
				UnityEngine.Debug.Log("Can not find Camera");
			}
		}
	}

	private void Update()
	{
		if (object.ReferenceEquals(ControllerProxyBase<T>.effectLock, this) && this.effect != null)
		{
			T effect = this.effect;
			if (effect.gameObject.activeSelf)
			{
				this.ThisUpdate();
			}
		}
	}

	private void OnDisable()
	{
		this.ResetToDefault();
		this.ThisUpdate();
		this.OnThisDisable();
		if (object.ReferenceEquals(ControllerProxyBase<T>.effectLock, this))
		{
			ControllerProxyBase<T>.effectLock = null;
			if (this.effect != null && this._autoEnable)
			{
				T effect = this.effect;
				effect.enabled = false;
				this.effect = (T)((object)null);
			}
		}
	}

	protected virtual T GetEffect()
	{
		if (typeof(T).IsSubclassOf(typeof(SingletonMono<T>)))
		{
			return SingletonMono<T>.I;
		}
		T component = Camera.main.GetComponent<T>();
		if (component != null)
		{
			return component;
		}
		return UnityEngine.Object.FindObjectOfType<T>();
	}

	protected virtual void ThisUpdate()
	{
	}

	protected virtual void ResetToDefault()
	{
	}

	protected virtual void ThisStart()
	{
	}

	protected virtual void OnThisEnable()
	{
	}

	protected virtual void OnThisDisable()
	{
	}

	protected static object effectLock;

	protected bool _autoEnable = true;
}
