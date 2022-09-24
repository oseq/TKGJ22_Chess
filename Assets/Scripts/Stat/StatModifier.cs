using System;

[Serializable]
public class StatModifier
{
    public enum Type
    {
        Additive,
        Multiplier
    }

    public Type type;
    public float value;

    public StatModifier(Type type, float value)
    {
        this.type = type;
        this.value = value;
    }
}