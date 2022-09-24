using System;
using UnityEngine;

[CreateAssetMenu(fileName="NewForceFielPowerUp")]
public class ForceFieldPowerUp : IPowerUpAction
{
    public float range;
    public float force;

    public override void Perform(IPowerUpAction.Context context)
    {
        Debug.Log("Boom!");
    }

    public override void Detached(Context context)
    {
        
    }
}
