using System;
using UnityEngine;

public sealed class TouchControlStrategy : PlayerControlStrategy, IModule<TouchControlStrategy>
{
    private Camera _mainCamera;
    private Location _location;

    public TouchControlStrategy Init(params object[] args)
    {
        if (args.Length < 2)
            throw new ArgumentException(nameof(args));

        _mainCamera = (Camera)args[0];
        _location = (Location)args[1];

        return this;
    }

    public override bool Control(GameObject player)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = _mainCamera.ScreenToWorldPoint(touch.position);
            touchPosition.z = player.transform.position.z;

            if (!_location.CheckPositionWithinLimitOfLocation(touchPosition))
            {
                Direction = touchPosition;
                return true;
            }
        }

        return false;
    }

    public TouchControlStrategy Get()
    {
        return this;
    }
}