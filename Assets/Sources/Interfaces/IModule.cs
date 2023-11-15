using UnityEngine;

public interface IModule<T> where T : class
{
    public T Init(params object[] args);
    public T Get();
}