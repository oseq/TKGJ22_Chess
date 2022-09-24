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

    public readonly UnityEvent onStatChanged = new UnityEvent();

    private readonly ModifiersMap modifiers = new ModifiersMap();
    private Cache cache;

    public StatType Type => type;

    public float GetValue()
    {
        if (cache.dirty)
        {
            Modifiers mods;

            if( modifiers.TryGetValue(StatModifier.Type.Additive, out mods) )
            {
                cache.value = mods.Sum(modifier => modifier.value);
            }

            if( modifiers.TryGetValue(StatModifier.Type.Multiplier, out mods) )
            {
                if (mods.Count > 0)
                {
                    cache.value *= mods.Sum(modifier => modifier.value);
                }
            }

            cache.dirty = false;
            cache.value = Math.Clamp(cache.value, limitMin, limitMax);
        }

        return cache.value;
    }

    public StatModifier CreateModifier(StatModifier.Type type, float value)
    {
        if (!modifiers.ContainsKey(type))
        {
            modifiers[type] = new Modifiers();
        }

        var mod = new StatModifier(type, value);
        modifiers[type].Add(mod);

        StatChanged();

        return mod;
    }

    public void RemoveModifier(StatModifier modifier)
    {
        Modifiers mods;
        if(modifiers.TryGetValue(modifier.type, out mods))
        {
            mods.Remove(modifier);

            StatChanged();
        }
    }

    private void StatChanged()
    {
        cache.dirty = true;
        onStatChanged.Invoke();
    }
}
