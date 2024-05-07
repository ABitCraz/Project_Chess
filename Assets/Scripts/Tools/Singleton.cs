using System;
using UnityEngine;
using System.Collections;
 
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
 
    public static T GetInstance()
    {
        return instance;
    }

    protected virtual void Awake()
    {
        if(instance !=null )
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }
    }

    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    protected void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}