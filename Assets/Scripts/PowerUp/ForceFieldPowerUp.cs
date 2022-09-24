using System;
using UnityEngine;


[Serializable]
public class ForceFieldPowerUp : IPowerUpAction
{
    public void Perform(IPowerUpAction.Context context)
    {
        Debug.Log("Boom!");
    }
}
