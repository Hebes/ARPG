using System;
using UnityEngine;

public class RandomActive : BaseBehaviour
{
	private void Start()
	{
		this.objs[UnityEngine.Random.Range(0, this.objs.Length)].SetActive(true);
	}

	private void Update()
	{
	}

	public GameObject[] objs;
}
