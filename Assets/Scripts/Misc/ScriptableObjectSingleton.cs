using System;
using System.Linq;
using UnityEngine;

public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] objects = Resources.LoadAll<T>(string.Empty);
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