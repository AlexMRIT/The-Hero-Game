using System;
using UnityEngine;

public sealed class AIDirectionHandler : PlayerControlStrategy, IModule<AIDirectionHandler>
{
    private Location _location;
    private float _duration = 1.0f;
    private float _lastPauseDuration;

    public AIDirectionHandler Init(params object[] args)
    {
        if (args.Length < 1)
            throw new ArgumentException(nameof(args));

        _location = (Location)args[0];

        return this;
    }

    public override bool Control(GameObject player)
    {
        player.transform.localPosition = new Vector3(player.transform.localPosition.x, player.transform.localPosition.y, 0.0f);

        if ((Time.time - _lastPauseDuration) < _duration)
            return false;

        _lastPauseDuration = Time.time;

        float horizontal = UnityEngine.Random.Range(-50.0f, 50.0f) + player.transform.position.x;
        float vertical = UnityEngine.Random.Range(-50.0f, 50.0f) + player.transform.position.y;

        if (_location.CheckPositionWithinLimitOfLocation(new Vector3(horizontal, vertical, 0.0f)))
            return false;

        Direction = new Vector3(horizontal, vertical, 0.0f).normalized;

        return true;
    }

    public AIDirectionHandler Get()
    {
        return this;
    }
}