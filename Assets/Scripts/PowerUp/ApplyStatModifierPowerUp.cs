using UnityEngine;

[CreateAssetMenu(fileName = "NewApplyStatModifierPowerUp")]
public class ApplyStatModifierPowerUp : IPowerUpAction
{
    public StatType stat;
    public float value;
    public StatModifier.Type modifierType;

    private StatModifier modifier;

    public override void Perform(Context context)
    {
        if (context.onAttachTrail != null)
        {
            context.onAttachTrail.TurnOnTrailPermanently();
        }
        var statContainer = context.instigator.GetComponent<StatsContainer>();
        modifier = statContainer.GetStat(stat).CreateModifier(modifierType, value);
    }

    public override void Detached(Context context)
    {
        var statContainer = (StatsContainer)context.instigator.GetComponent<StatsContainer>();
        statContainer.GetStat(stat).RemoveModifier(modifier);
        if (context.onAttachTrail != null)
        {
            context.onAttachTrail.TurnOffTrail();
        }
    }
}
