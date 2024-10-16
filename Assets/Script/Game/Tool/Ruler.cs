using UnityEngine;

public class Ruler : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		if (string.IsNullOrEmpty(_property))
		{
			_property = name;
		}
		if (!_show)
		{
			return;
		}
		_color.a = _alpha;
		Gizmos.color = _color;
		DrawCube(transform.position + _offset, _size, _angle);
		DebugX.DrawText(_property + _size, transform.position);
	}

	private void DrawCube(Vector3 center, Vector2 size, float angle)
	{
		float num = size.x / 2f;
		float num2 = size.y / 2f;
		Vector3 vector = VectorRotate(new Vector2(-num, num2), angle);
		Vector3 vector2 = VectorRotate(new Vector2(num, num2), angle);
		Vector3 vector3 = VectorRotate(new Vector2(-num, -num2), angle);
		Vector3 vector4 = VectorRotate(new Vector2(num, -num2), angle);
		switch (_ancor)
		{
		case AncorEnum.TopLeft:
			center -= vector;
			break;
		case AncorEnum.TopRight:
			center -= vector2;
			break;
		case AncorEnum.BottomLeft:
			center -= vector3;
			break;
		case AncorEnum.BottomRight:
			center -= vector4;
			break;
		}
		vector += center;
		vector2 += center;
		vector3 += center;
		vector4 += center;
		Gizmos.DrawLine(vector, vector2);
		Gizmos.DrawLine(vector2, vector4);
		Gizmos.DrawLine(vector4, vector3);
		Gizmos.DrawLine(vector3, vector);
	}

	private Vector3 VectorRotate(Vector2 vector, float angle)
	{
		angle *= 0.0174532924f;
		float x = vector.x;
		float y = vector.y;
		float x2 = x * Mathf.Cos(angle) + y * Mathf.Sin(angle);
		float y2 = -x * Mathf.Sin(angle) + y * Mathf.Cos(angle);
		return new Vector3(x2, y2);
	}

	[SerializeField]
	private bool _show = true;

	[SerializeField]
	private string _property;

	[SerializeField]
	private AncorEnum _ancor = AncorEnum.BottomLeft;

	[SerializeField]
	private Vector2 _size = new Vector2(20f, 10f);

	[SerializeField]
	private Vector3 _offset;

	[SerializeField]
	private float _angle;

	[SerializeField]
	private Color _color = Color.cyan;

	[Range(0f, 1f)]
	[SerializeField]
	private float _alpha = 0.5f;

	private enum AncorEnum
	{
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}
}
