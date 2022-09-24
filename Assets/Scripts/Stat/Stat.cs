using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

using Modifiers = System.Collections.Generic.List<StatModifier>;
using ModifiersMap = System.Collections.Generic.Dictionary<StatModifier.Type, System.Collections.Generic.List<StatModifier>>;

[Serializable]
public class Stat
{
    private struct Cache
    {
        public bool dirty;
        public float value;
    }

    [SerializeField] private StatType type;
    [SerializeField] private float limitMin = 0.0f;
    [SerializeField] private float limitMax = 0.0f;
    [SerializeField] private Modifiers modifiers = new Modifiers();

    public readonly UnityEvent onStatChanged = new UnityEvent();

    //private readonly ModifiersMap modifiers = new ModifiersMap();

    private Cache cache;

    public StatType Type => type;

    public float GetValue()
    {
        if (cache.dirty)
        {
            Modifiers mods = modifiers.FindAll(mod => mod.type == StatModifier.Type.Additive);
            cache.value = mods.Sum(modifier => modifier.value);

            mods = modifiers.FindAll(mods => mods.type == StatModifier.Type.Multiplier);
            if (mods.Count > 0)
            {
                cache.value *= mods.Sum(modifier => modifier.value);
            }

            cache.dirty = false;
            cache.value = Math.Clamp(cache.value, limitMin, limitMax);
        }

        return cache.value;
    }

    public StatModifier CreateModifier(StatModifier.Type type, float value)
    {
        var mod = new StatModifier(type, value);
        modifiers.Add(mod);

        StatChanged();

        return mod;
    }

    public void RemoveModifier(StatModifier modifier)
    {
        modifiers.Remove(modifier);
        StatChanged();
    }

    private void StatChanged()
    {
        cache.dirty = true;
        onStatChanged.Invoke();
    }
}
