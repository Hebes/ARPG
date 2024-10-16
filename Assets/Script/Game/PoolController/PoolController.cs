using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池控制器
/// </summary>
public static class PoolController
{
    public static readonly Dictionary<string, ObjectPool> EffectDict = new Dictionary<string, ObjectPool>();
}

/// <summary>
/// 对象池
/// </summary>
public class ObjectPool
{
    public IEnumerator Init(GameObject prefab, GameObject parent, int size, int maxSize)
    {
        queue = new Queue<GameObject>();
        this.prefab = prefab;
        this.parent = parent;
        this.maxSize = maxSize;
        if (prefab != null)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject temp = InsertObject();
                temp.SetActive(true);
                temp.transform.position = new Vector3(-1000f, 0f, 0f);
                yield return null;
                temp.SetActive(false);
            }
        }
    }

    public GameObject GetObject()
    {
        if (queue.Count == 0)
        {
            return InsertObject();
        }
        if (queue.Peek() == null)
        {
            queue.Dequeue();
            return InsertObject();
        }
        if (queue.Peek().activeSelf && queue.Count < maxSize)
        {
            return InsertObject();
        }
        GameObject gameObject = queue.Dequeue();
        queue.Enqueue(gameObject);
        if (queue.Count >= maxSize)
        {
            gameObject.SetActive(false);
        }
        return gameObject;
    }

    public GameObject InsertObject()
    {
        GameObject gameObject = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        gameObject.name = prefab.name;
        if (parent != null)
        {
            gameObject.transform.parent = parent.transform;
        }
        gameObject.SetActive(false);
        queue.Enqueue(gameObject);
        return gameObject;
    }

    public Queue<GameObject> queue;

    public GameObject prefab;

    public GameObject parent;

    public int maxSize = 10;
}
