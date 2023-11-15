using System;
using UnityEngine;
using System.Collections;

public sealed class Spawner : MonoBehaviour, IModule<Spawner>
{
    private Location _location;
    private int _maxSpawn;
    private ObjectPool _objectPool;
    private Coroutine _coroutine;

    public Spawner Init(params object[] args)
    {
        if (args.Length < 2)
            throw new ArgumentException(nameof(args));

        _location = (Location)args[0];
        _maxSpawn = (int)args[1];
        _objectPool = (ObjectPool)args[2];

        _coroutine = StartCoroutine(InternalSpawnHandler());

        return this;
    }

    private IEnumerator InternalSpawnHandler()
    {
        int spawnCount = 0;

        while (spawnCount++ < _maxSpawn)
        {
            yield return new WaitForSeconds(5.0f);

            if (_objectPool.TryGetObject(out GameObject obj))
            {
                obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, 0.0f);

                float locationSizeX = (_location.GetGridSize().x - 1) / 2.0f * _location.GetBlockSize().x;
                float locationSizeY = (_location.GetGridSize().y - 1) / 2.0f * _location.GetBlockSize().y;

                float posX = UnityEngine.Random.Range(-locationSizeX, locationSizeX);
                float posY = UnityEngine.Random.Range(-locationSizeY, locationSizeY);

                while (_location.CheckPositionWithinLimitOfLocation(new Vector3(posX, posY, 0.0f)))
                {
                    posX = UnityEngine.Random.Range(-locationSizeX, locationSizeX);
                    posY = UnityEngine.Random.Range(-locationSizeY, locationSizeY);
                }

                obj.transform.localPosition = new Vector3(posX, posY, 0.0f);
                obj.SetActive(true);
            }
        }
    }

    public void StopSpawner()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public Spawner Get()
    {
        return this;
    }
}