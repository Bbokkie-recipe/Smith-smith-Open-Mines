using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public IObjectPoolBase pool;
    public void Return()
    {
        pool.ReturnObject(gameObject);
    }
    public void PullObject()
    {
        pool.PullObject(gameObject);
    }
}
