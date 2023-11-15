using System;
using UnityEngine;

public sealed class MouseControlStrategy : PlayerControlStrategy, IModule<MouseControlStrategy>
{
    private Camera _mainCamera;
    private Location _location;

    public MouseControlStrategy Init(params object[] args)
    {
        if (args.Length < 2)
            throw new ArgumentException(nameof(args));

        _mainCamera = (Camera)args[0];
        _location = (Location)args[1];

        return this;
    }

    public override bool Control(GameObject player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = player.transform.position.z;

            if (!_location.CheckPositionWithinLimitOfLocation(mousePosition))
            {
                Direction = mousePosition;
                return true;
            }
        }

        return false;
    }

    public MouseControlStrategy Get()
    {
        return this;
    }
}