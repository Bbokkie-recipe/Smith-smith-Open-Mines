using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject<T> where T : MonoBehaviour
{
    public void Return();
}
public interface IObjectPoolBase
{
    public MonoBehaviour ObjectPoolScript
    {
        get;
    }
    public void ReturnObject(GameObject gameObject);
    public void PullObject(GameObject gameObject);
}
public class ObjectPoolBase<T> : IObjectPoolBase where T: MonoBehaviour,IPooledObject<T>
{
    public MonoBehaviour ObjectPoolScript
    {
        get { return objectPoolScript; }
    }
    public MonoBehaviour objectPoolScript;
    T pooledObject;
    int maxPoolSize;
    int initialPoolSize;
    int additionalPoolSize;

    private int currentPoolCount;
    public Queue<GameObject> queue;
    // Start is called before the first frame update
    public ObjectPoolBase(MonoBehaviour _objectPoolScript, T _pooledObject, int _maxPool,int _initialPool,int _additionalPool)
    {
        objectPoolScript = _objectPoolScript;
        pooledObject = _pooledObject;
        maxPoolSize = _maxPool;
        initialPoolSize = _initialPool;
        additionalPoolSize = _additionalPool;
        currentPoolCount = 0;
        queue = new Queue<GameObject>();
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateObject();
        }
    }
    void CreateObject()
    {
        Transform transform = null;
        GameObject gameObject = GameObject.Instantiate(pooledObject.gameObject, transform);
        gameObject.transform.parent = objectPoolScript.transform;
        gameObject.transform.position = objectPoolScript.transform.position + Vector3.up * 0.5f;
        gameObject.SetActive(false);
        gameObject.AddComponent<PooledObject>();
        gameObject.GetComponent<PooledObject>().pool = this;
        queue.Enqueue(gameObject);
        currentPoolCount += 1;
    }
    public GameObject GetObject()
    {
        GameObject gameObject = null;
        if (queue.Count > 0)
        {
            gameObject = queue.Dequeue();
            gameObject.SetActive(true);
        }
        else if (currentPoolCount < maxPoolSize)
        {
            for (int i = 0; i < additionalPoolSize && currentPoolCount < maxPoolSize; i++)
            {
                CreateObject();
            }
            gameObject = queue.Dequeue();
        }
        return gameObject;
    }
    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = null;
        if (queue.Count > 0)
        {
            gameObject = queue.Dequeue();
            gameObject.SetActive(true);
        }
        else if (currentPoolCount < maxPoolSize)
        {
            for (int i = 0; i < additionalPoolSize && currentPoolCount < maxPoolSize; i++)
            {
                CreateObject();
            }
            gameObject = queue.Dequeue();
        }
        if (gameObject != null)
        {
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            gameObject.SetActive(true);
        }
        return gameObject;
    }
    public GameObject GetObject(Transform transform)
    {

        return GetObject(transform.position, transform.rotation);
    }
    public void ReturnObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.parent = objectPoolScript.transform;
        queue.Enqueue(gameObject);
    }
    public void PullObject(GameObject gameObject)
    {
        currentPoolCount -= 1;
        Object.Destroy(gameObject.GetComponent<PooledObject>());
    }
}