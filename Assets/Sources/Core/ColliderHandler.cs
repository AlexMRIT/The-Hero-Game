using System;
using UnityEngine;

#pragma warning disable

public sealed class ColliderHandler : MonoBehaviour, IModule<ColliderHandler>
{
    private int _addScore = 6;

    public event Action<int> OnValueChange;

    public ColliderHandler Init(params object[] args)
    {
        return this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Collider2D collider) && collider.isTrigger)
        {
            OnValueChange?.Invoke(_addScore);
            other.gameObject.SetActive(false);
        }
    }

    public ColliderHandler Get()
    {
        return this;
    }
}