using System;
using UnityEngine;

#pragma warning disable

[RequireComponent(typeof(RectTransform))]
public sealed class MoveDirection : MonoBehaviour, IModule<MoveDirection>
{
    private PlayerControlStrategy _playerControlStrategy;
    private RectTransform _rectTransform;
    private Vector3 _direction;

    private readonly float ROTATION_SPEED = 10.0f;
    private readonly float MOVEMENT_SPEED = 1.0f;

    public MoveDirection Init(params object[] args)
    {
        if (args.Length < 1)
            throw new ArgumentException(nameof(args));

        _playerControlStrategy = args[0] as PlayerControlStrategy;
        _direction = transform.position;

        if (!gameObject.TryGetComponent(out RectTransform rectTransform))
            throw new MissingComponentException(nameof(rectTransform));

        _rectTransform = rectTransform;

        return this;
    }

    private void Update()
    {
        if (_playerControlStrategy.Control(gameObject))
            _direction = _playerControlStrategy.Direction;

        if (Vector2.Distance(transform.position, _direction) > 0.1f)
            transform.position = Vector3.Lerp(transform.position, _direction, MOVEMENT_SPEED * Time.deltaTime);
        else
            transform.position = _direction;
    }

    public MoveDirection Get()
    {
        return this;
    }
}