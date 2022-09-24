using UnityEngine;
using System;
using System.Linq;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] objects = FindObjectsOfType<T>();
                if (objects.Length > 1)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    instance = objects.FirstOrDefault();
                }
            }
            return instance;
        }
    }
}