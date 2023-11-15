using UnityEngine;
using System.Collections.Generic;

public sealed class ObjectPool : MonoBehaviour, IModule<ObjectPool>
{
    private Queue<GameObject> _pool;

    public ObjectPool Init(params object[] args)
    {
        return this;
    }

    public void AddEnumerable(IEnumerable<GameObject> gameObjects)
    {
        _pool = new Queue<GameObject>(gameObjects);
    }

    public bool TryGetObject(out GameObject obj)
    {
        if (_pool == null)
        {
            obj = null;
            return false;
        }    

        foreach (GameObject itObj in _pool)
        {
            if (!itObj.activeInHierarchy)
            {
                obj = itObj;
                return true;
            }
        }

        obj = null;
        return false;
    }

    public ObjectPool Get()
    {
        return this;
    }
}