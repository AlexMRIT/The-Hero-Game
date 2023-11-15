using System;
using UnityEngine;
using System.Collections.Generic;

public sealed class InstanceEnemy : MonoBehaviour, IModule<InstanceEnemy>
{
    private List<GameObject> gameObjects;

    public InstanceEnemy Init(params object[] args)
    {
        if (args.Length < 4)
            throw new ArgumentException(nameof(args));

        int poolSize = (int)args[0];
        GameObject objSpawn = (GameObject)args[1];
        Transform transformSpawn = (Transform)args[2];
        Location location = (Location)args[3];

        gameObjects = new List<GameObject>(capacity: poolSize);

        for (int iterator = 0; iterator < poolSize; iterator++)
        {
            GameObject enemy = Instantiate(objSpawn, transformSpawn);
            enemy.transform.position = Vector3.zero;

            PlayerControlStrategy playerControlStrategy = new AIDirectionHandler();
            ((AIDirectionHandler)playerControlStrategy).Init(new object[] { location });

            object[] components = new object[] { playerControlStrategy };

            MoveDirection moveDirection = enemy.AddComponent<MoveDirection>();
            moveDirection.Init(components);

            PlayerAnimationController playerAnimationController = enemy.AddComponent<PlayerAnimationController>();
            playerAnimationController.Init(components);

            gameObjects.Add(enemy);
            enemy.SetActive(false);
        }

        return this;
    }

    public IEnumerable<GameObject> GetGameObjects()
    {
        return gameObjects;
    }

    public InstanceEnemy Get()
    {
        return this;
    }
}