using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    
    public new GameObject gameObject
    {
        get
        {
            GameObject result;
            if ((result = _gameObject) == null)
            {
                result = (_gameObject = base.gameObject);
            }
            return result;
        }
    }

    public new Transform transform
    {
        get
        {
            Transform result;
            if ((result = _transform) == null)
            {
                result = (_transform = base.transform);
            }
            return result;
        }
    }

    private GameObject _gameObject;

    private Transform _transform;
}