using UnityEngine;

/// <summary>
/// 调试
/// </summary>
public class DebugX : MonoBehaviour
{
	public static void DrawCube(Vector3 pos, Color col, Vector3 scale)
	{
		Vector3 vector = scale * 0.5f;
		Vector3[] array = new Vector3[]
		{
			pos + new Vector3(vector.x, vector.y, vector.z),
			pos + new Vector3(-vector.x, vector.y, vector.z),
			pos + new Vector3(-vector.x, -vector.y, vector.z),
			pos + new Vector3(vector.x, -vector.y, vector.z),
			pos + new Vector3(vector.x, vector.y, -vector.z),
			pos + new Vector3(-vector.x, vector.y, -vector.z),
			pos + new Vector3(-vector.x, -vector.y, -vector.z),
			pos + new Vector3(vector.x, -vector.y, -vector.z)
		};
		UnityEngine.Debug.DrawLine(array[0], array[1], col);
		UnityEngine.Debug.DrawLine(array[1], array[2], col);
		UnityEngine.Debug.DrawLine(array[2], array[3], col);
		UnityEngine.Debug.DrawLine(array[3], array[0], col);
		UnityEngine.Debug.DrawLine(array[4], array[5], col);
		UnityEngine.Debug.DrawLine(array[5], array[6], col);
		UnityEngine.Debug.DrawLine(array[6], array[7], col);
		UnityEngine.Debug.DrawLine(array[7], array[4], col);
		UnityEngine.Debug.DrawLine(array[0], array[4], col);
		UnityEngine.Debug.DrawLine(array[1], array[5], col);
		UnityEngine.Debug.DrawLine(array[2], array[6], col);
		UnityEngine.Debug.DrawLine(array[3], array[7], col);
	}

	public static void DrawRect(Rect rect, Color col, float z = 0f)
	{
		Vector3 pos = new Vector3(rect.x + rect.width / 2f, rect.y + rect.height / 2f, 0f);
		Vector3 scale = new Vector3(rect.width, rect.height, z);
		DebugX.DrawRect(pos, col, scale);
	}

	public static void DrawRect(Vector3 pos, Color col, Vector3 scale)
	{
		Vector3 vector = scale * 0.5f;
		Vector3[] array = new Vector3[]
		{
			pos + new Vector3(vector.x, vector.y, vector.z * 2f),
			pos + new Vector3(-vector.x, vector.y, vector.z * 2f),
			pos + new Vector3(-vector.x, -vector.y, vector.z * 2f),
			pos + new Vector3(vector.x, -vector.y, vector.z * 2f)
		};
		UnityEngine.Debug.DrawLine(array[0], array[1], col);
		UnityEngine.Debug.DrawLine(array[1], array[2], col);
		UnityEngine.Debug.DrawLine(array[2], array[3], col);
		UnityEngine.Debug.DrawLine(array[3], array[0], col);
	}

	public static void DrawPoint(Vector3 pos, Color col, float scale)
	{
		Vector3[] array = new Vector3[]
		{
			pos + Vector3.up * scale,
			pos - Vector3.up * scale,
			pos + Vector3.right * scale,
			pos - Vector3.right * scale,
			pos + Vector3.forward * scale,
			pos - Vector3.forward * scale
		};
		UnityEngine.Debug.DrawLine(array[0], array[1], col);
		UnityEngine.Debug.DrawLine(array[2], array[3], col);
		UnityEngine.Debug.DrawLine(array[4], array[5], col);
		UnityEngine.Debug.DrawLine(array[0], array[2], col);
		UnityEngine.Debug.DrawLine(array[0], array[3], col);
		UnityEngine.Debug.DrawLine(array[0], array[4], col);
		UnityEngine.Debug.DrawLine(array[0], array[5], col);
		UnityEngine.Debug.DrawLine(array[1], array[2], col);
		UnityEngine.Debug.DrawLine(array[1], array[3], col);
		UnityEngine.Debug.DrawLine(array[1], array[4], col);
		UnityEngine.Debug.DrawLine(array[1], array[5], col);
		UnityEngine.Debug.DrawLine(array[4], array[2], col);
		UnityEngine.Debug.DrawLine(array[4], array[3], col);
		UnityEngine.Debug.DrawLine(array[5], array[2], col);
		UnityEngine.Debug.DrawLine(array[5], array[3], col);
	}

	public static void DrawText(string p, Vector2 pos)
	{
	}
}
