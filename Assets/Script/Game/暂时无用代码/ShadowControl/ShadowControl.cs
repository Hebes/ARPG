using System;
using UnityEngine;

public class ShadowControl : BaseBehaviour
{
	private void Update()
	{
		Vector3 position = _target.position;
		if (!_targetGameObject.activeSelf || EnemyDie())
		{
			Destroy(gameObject);
			return;
		}
		GetGroundHeight(position);
		SetShadowTransform(position);
		SetShadowColor(position);
	}

	public void SetTarget(Transform target)
	{
		_target = target;
		_targetGameObject = target.gameObject;
		BoxCollider2D component = target.GetComponent<BoxCollider2D>();
		_eAttribute = target.GetComponent<EnemyAttribute>();
		_isEnemy = (_eAttribute != null);
		_offset = ((!(component == null)) ? component.offset : Vector2.zero);
		_size = ((!(component == null)) ? component.size : Vector2.one);
		_shadowColor = sprite.color;
		_layerValue = layer.value;
	}

	private void GetGroundHeight(Vector3 targetPos)
	{
		RaycastHit2D raycastHit2D = Physics2D.Raycast(new Vector2(targetPos.x + _offset.x + _size.x / 3f, targetPos.y), Vector2.down, 100f, _layerValue);
		RaycastHit2D raycastHit2D2 = Physics2D.Raycast(new Vector2(targetPos.x + _offset.x - _size.x / 3f, targetPos.y), Vector2.down, 100f, _layerValue);
		_noGround = (raycastHit2D.collider == null || raycastHit2D2.collider == null);
		_groundY = Mathf.Max(raycastHit2D2.point.y, raycastHit2D.point.y);
	}

	private void SetShadowTransform(Vector3 targetPosition)
	{
		transform.position = new Vector3(targetPosition.x, _groundY, targetPosition.z);
		if (_noGround)
		{
			transform.localScale = new Vector3(0f, 0f, 1f);
			return;
		}
		float num = Mathf.Lerp(1f, 0f, (targetPosition.y - _groundY) / 6f);
		transform.localScale = new Vector3(num, num, 1f);
	}

	private void SetShadowColor(Vector3 targetPosition)
	{
		Color color = _shadowColor.SetAlpha(Mathf.Lerp(0.5f, 0f, (targetPosition.y - _groundY) / 8f));
		if (Math.Abs(_shadowColor.a - color.a) > 1.401298E-45f)
		{
			_shadowColor = color;
			sprite.color = color;
		}
	}

	private bool EnemyDie()
	{
		return _isEnemy && _eAttribute.currentHp == 0;
	}

	private float _groundY;

	[SerializeField]
	private SpriteRenderer sprite;

	private Transform _target;

	private GameObject _targetGameObject;

	private EnemyAttribute _eAttribute;

	private bool _isEnemy;

	public LayerMask layer;

	private bool _noGround;

	private Vector2 _offset;

	private Vector2 _size;

	private Color _shadowColor;

	private int _layerValue;
}
