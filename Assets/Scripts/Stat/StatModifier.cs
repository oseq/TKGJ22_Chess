using System;

[Serializable]
public class StatModifier
{
    public enum Type
    {
        Additive,
        Multiplier
    }

    public Type type { get; private set; }
    public float value { get; private set; }

    public StatModifier(Type type, float value)
    {
        this.type = type;
        this.value = value;
    }
}