using UnityEngine;

[CreateAssetMenu(fileName = "NewApplyStatModifierPowerUp")]
public class ApplyStatModifierPowerUp : IPowerUpAction
{
    public StatType stat;
    public float value;
    public StatModifier.Type modifierType;

    private StatModifier modifier;

    public override void Perform(IPowerUpAction.Context context)
    {
        var statContainer = (StatsContainer)context.instigator.GetComponent<StatsContainer>();
        modifier = statContainer.GetStat(stat).CreateModifier(modifierType, value);
    }

    public override void Detached(Context context)
    {
        var statContainer = (StatsContainer)context.instigator.GetComponent<StatsContainer>();
        statContainer.GetStat(stat).RemoveModifier(modifier);
    }
}
