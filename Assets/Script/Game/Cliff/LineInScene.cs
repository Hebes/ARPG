using UnityEngine;

/// <summary>
/// 悬崖死亡专用
/// </summary>
public class LineInScene : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        if (show)
        {
            Vector3 start = offset + transform.position;
            Vector3 end = offset + transform.position + new Vector3(length * Mathf.Cos(0.0174532924f * angle), length * Mathf.Sin(0.0174532924f * angle));
            Debug.DrawLine(start, end, color);
        }
    }

    [SerializeField] public bool show = true;
    [SerializeField] public float length = 1f;
    [SerializeField] public Color color = Color.cyan;
    [SerializeField] public Vector3 offset;
    [SerializeField] public float angle;
}