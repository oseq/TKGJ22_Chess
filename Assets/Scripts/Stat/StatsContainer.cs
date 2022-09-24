using System;
using UnityEngine;

using CallbackMap = System.Collections.Generic.Dictionary<StatType, UnityEngine.Events.UnityEvent>;

public class StatsContainer : MonoBehaviour
{
    [SerializeField] private Stat[] container;

    public readonly CallbackMap callbacks = new CallbackMap();

    private void Start()
    {
        foreach(var stat in container)
        {
            callbacks[stat.Type] = stat.onStatChanged;
        }
    }

    public Stat GetStat(StatType type)
    {
        return Array.Find(container, stat => stat.Type == type);
    }
}
