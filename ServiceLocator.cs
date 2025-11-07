using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator:MonoBehaviour
{
    protected static ServiceLocator Instance;
    private static Dictionary<Type, object> dicService = new Dictionary<Type, object>();

    public ServiceLocator()
    {
        dicService.Clear();
        Instance = this;
    }


    public static ServiceLocator GetLocator()
    {
        if (!Instance)
        {
            GameObject go = new GameObject("ServiceLocator");
            Instance = go.AddComponent<ServiceLocator>();
        }
        return Instance;
    }

    public void RegisterService<T>(T instance) where T : class
    {
        if (!ExistService<T>())
        {
            dicService.Add(typeof(T), instance);
        }
        else
            Debug.Log("Class present in dictionary");
    }

    public void InstanceServiceOnScene<T>() where T : class
    {
        if (ExistService<T>())
        {
            GameObject go = new GameObject(dicService[typeof(T)].GetType().Name);
            go.AddComponent(typeof(T));
            go.transform.SetParent(transform);
        }
        else
            Debug.Log("Not Exist Instance");
    }

    public bool ExistService<T>() where T : class
    {
        if (dicService.ContainsKey(typeof(T)))
        {
            if (dicService[typeof(T)] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public T GetServiceInstance<T>() where T : class
    {
        if (!ExistService<T>())
        {
            Debug.Log("Class present in dictionary");
            return null;
        }
        else
            return (T)dicService[typeof(T)];
    }

    public bool RemoveService(Type type)
    {
        return dicService.Remove(type);
    }

}
