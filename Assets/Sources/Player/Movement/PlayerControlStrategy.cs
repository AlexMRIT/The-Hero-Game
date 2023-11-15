using UnityEngine;

public abstract class PlayerControlStrategy
{
    public Vector3 Direction;
    public abstract bool Control(GameObject player);
}