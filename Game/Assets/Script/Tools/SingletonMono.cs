using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>
public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private static T instance;

    public static T Instance => instance;
    protected virtual void Awake()
    {
       
        if (instance != null) Destroy(this.gameObject);
        else
        {
            instance = (T)this;
        }
    }
}
