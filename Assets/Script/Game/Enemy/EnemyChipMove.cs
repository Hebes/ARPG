using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyChipMove : BaseBehaviour
{
	private float deltaTime => Time.time - startTime;

	private void OnEnable()
	{
		canPlay = false;
		startTime = Time.time;
		destinationPostion = new Vector3(Random.Range(-3f, 3f), Random.Range(1f, 4f), -0.02f);
		currentPostion = transform.position;
		_sprite = GetComponent<SpriteRenderer>();
		StartCoroutine(PlayEffect());
	}

	private void Update()
	{
		if (canPlay)
		{
			transform.position = Vector3.MoveTowards(transform.position, currentPostion + destinationPostion, 0.45f * speed * deltaTime);
			if (Vector3.Distance(transform.position, destinationPostion) < 0.05f)
			{
				EffectController.TerminateEffect(gameObject);
			}
		}
	}

	public IEnumerator PlayEffect()
	{
		yield return new WaitForSeconds(0.1f);
		float startTime = Time.time;
		if (R.Player != null)
		{
			PlayerAttribute pattr = R.Player.Attribute;
			while (Time.time - startTime < waitTime)
			{
				if (pattr.isInCharging)
				{
					GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
					Transform transform = R.Effect.Generate(125, this.transform);
					transform.GetComponent<ChildChipExplosion>().backToPlayer = true;
					EffectController.TerminateEffect(gameObject);
				}
				yield return new WaitForSeconds(0.1f);
			}
			yield return DOTween.To(() => _sprite.color, delegate(Color x)
			{
				_sprite.color = x;
			}, new Color(1f, 1f, 1f, 0f), 0.3f).WaitForCompletion();
			EffectController.TerminateEffect(gameObject);
		}
	}

	public bool canPlay;

	private float startTime;

	[SerializeField]
	private float speed = 1f;

	private Vector3 destinationPostion;

	private Vector3 currentPostion;

	public float waitTime = 2f;

	private SpriteRenderer _sprite;
}
