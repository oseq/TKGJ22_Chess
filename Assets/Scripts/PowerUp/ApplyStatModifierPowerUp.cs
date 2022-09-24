using System;
using UnityEngine;

public class ApplyStatModifierPowerUp : IPowerUpAction
{
    public StatType stat;
    public float value;
    public StatModifier.Type modifierType;

    private StatModifier modifier;

    public void Perform(IPowerUpAction.Context context)
    {
        var statContainer = (StatsContainer)context.instigator.GetComponent<StatsContainer>();
        modifier = statContainer.GetStat(stat).CreateModifier(modifierType, value);
    }
}
