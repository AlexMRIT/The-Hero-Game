using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIScoreHandler : MonoBehaviour, IModule<UIScoreHandler>
{
    private ColliderHandler _colliderHandler;
    private Text _spawnText;

    private int accumulate = 0;

    public UIScoreHandler Init(params object[] args)
    {
        if (args.Length < 3)
            throw new ArgumentException(nameof(args));

        _colliderHandler = (ColliderHandler)args[0];
        _spawnText = (Text)args[1];
        Transform canvasTransform = (Transform)args[2];
        _spawnText = Instantiate(_spawnText.gameObject, canvasTransform).GetComponent<Text>();

        _colliderHandler.OnValueChange += InternalColliderHandlerOnValueChange;

        return this;
    }

    private void InternalColliderHandlerOnValueChange(int obj)
    {
        accumulate += obj;
        _spawnText.text = $"Score: {accumulate}";
    }

    private void OnDisable()
    {
        if (_colliderHandler != null)
            _colliderHandler.OnValueChange -= InternalColliderHandlerOnValueChange;
    }

    public UIScoreHandler Get()
    {
        return this;
    }
}