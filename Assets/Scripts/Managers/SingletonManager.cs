using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SingletonManager<T> : MonoBehaviour where T : SingletonManager<T>
{
    public static T Instance;

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this as T;
    }
}

