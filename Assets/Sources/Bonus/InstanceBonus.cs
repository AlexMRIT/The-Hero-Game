using System;
using UnityEngine;
using System.Collections.Generic;

public sealed class InstanceBonus : MonoBehaviour, IModule<InstanceBonus>
{
    private List<GameObject> _gameObjects;

    public InstanceBonus Init(params object[] args)
    {
        if (args.Length < 4)
            throw new ArgumentException(nameof(args));

        int poolSize = (int)args[0];
        GameObject objSpawn = (GameObject)args[1];
        Transform transformSpawn = (Transform)args[2];
        Location location = (Location)args[3];

        _gameObjects = new List<GameObject>(capacity: poolSize);

        for (int iterator = 0; iterator < poolSize; iterator++)
        {
            GameObject bonus = Instantiate(objSpawn, transformSpawn);
            bonus.transform.position = Vector3.zero;

            float locationSizeX = (location.GetGridSize().x - 1) / 2.0f * location.GetBlockSize().x;
            float locationSizeY = (location.GetGridSize().y - 1) / 2.0f * location.GetBlockSize().y;

            float posX = UnityEngine.Random.Range(-locationSizeX, locationSizeX);
            float posY = UnityEngine.Random.Range(-locationSizeY, locationSizeY);

            while (location.CheckPositionWithinLimitOfLocation(new Vector3(posX, posY, 0.0f)))
            {
                posX = UnityEngine.Random.Range(-locationSizeX, locationSizeX);
                posY = UnityEngine.Random.Range(-locationSizeY, locationSizeY);
            }

            bonus.transform.position = new Vector3(posX, posY, 0.0f);
            _gameObjects.Add(bonus);

            bonus.SetActive(false);
        }

        return this;
    }

    public IEnumerable<GameObject> GetGameObjects()
    {
        return _gameObjects;
    }

    public InstanceBonus Get()
    {
        return this;
    }
}