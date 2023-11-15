using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

#pragma warning disable

public sealed class InjectModule : MonoBehaviour
{
    [SerializeField] private BuilderGround _moduleBuildGround;
    [SerializeField] private GameObject _mainPlayer;
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _bonus;
    [SerializeField] private Text _scoreText;
    [SerializeField] private int _countEnemy;
    [SerializeField] private int _countBonuses;

    private void Awake()
    {
        Location location = _moduleBuildGround.Init().CreateLocation();
        PlayerControlStrategy playerControlStrategy = null;

#if UNITY_EDITOR
        playerControlStrategy = new MouseControlStrategy();
        ((MouseControlStrategy)playerControlStrategy).
            Init(new object[] { _mainCamera, location });
#elif UNITY_ANDROID
        playerControlStrategy = new TouchControlStrategy();
        ((TouchControlStrategy)playerControlStrategy).
            Init(new object[] { _mainCamera, location });
#endif

        GameObject player = Instantiate(_mainPlayer, _mainCanvas.transform);
        object[] components = new object[] { playerControlStrategy };

        MoveDirection moveDirection = player.AddComponent<MoveDirection>();
        moveDirection.Init(components);

        ColliderHandler colliderHandler = player.AddComponent<ColliderHandler>();
        colliderHandler.Init();

        PlayerAnimationController playerAnimationController =
            player.AddComponent<PlayerAnimationController>();
        playerAnimationController.Init(components);

        GameObject gameObjectPoolEnemy = new GameObject();
        gameObjectPoolEnemy.name = nameof(gameObjectPoolEnemy);
        ObjectPool objectPoolEnemy = gameObjectPoolEnemy.AddComponent<ObjectPool>();
        InstanceEnemy instanceEnemy = gameObjectPoolEnemy.AddComponent<InstanceEnemy>();

        IEnumerable<GameObject> objectsEnemy = instanceEnemy.Init(new object[]
            { _countEnemy, _enemy, _mainCanvas.gameObject.transform, location }).GetGameObjects();
        objectPoolEnemy.AddEnumerable(objectsEnemy);

        Spawner spawnerEnemy = gameObjectPoolEnemy.AddComponent<Spawner>();
        spawnerEnemy.Init(new object[] { location, _countEnemy, objectPoolEnemy });

        DestroyImmediate(instanceEnemy); // free

        GameObject gameObjectPoolBonus = new GameObject();
        gameObjectPoolBonus.name = nameof(gameObjectPoolBonus);
        ObjectPool objectPoolBonus = gameObjectPoolBonus.AddComponent<ObjectPool>();
        InstanceBonus instanceBonus = gameObjectPoolBonus.AddComponent<InstanceBonus>();

        IEnumerable<GameObject> objectsBonus = instanceBonus.Init(new object[]
            { _countBonuses, _bonus, _mainCanvas.gameObject.transform, location }).GetGameObjects();
        objectPoolBonus.AddEnumerable(objectsBonus);

        Spawner spawnerBonus = gameObjectPoolBonus.AddComponent<Spawner>();
        spawnerBonus.Init(new object[] { location, _countBonuses, objectPoolBonus });

        UIScoreHandler uiScoreHandler = gameObjectPoolBonus.AddComponent<UIScoreHandler>();
        uiScoreHandler.Init(new object[] { colliderHandler, _scoreText, _mainCanvas.gameObject.transform });
    }
}