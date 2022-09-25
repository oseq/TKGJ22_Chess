using System;
using UnityEngine;

[CreateAssetMenu(fileName="NewForceFielPowerUp")]
public class ForceFieldPowerUp : IPowerUpAction
{
    public float radius;
    public float force;

    public override void Perform(Context context)
    {
        var position = context.instigator.transform.position;
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        if (context.effect != null)
        {
            context.effect.Play();
        }
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject != context.instigator)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, position, radius, 3.0F);
                }
            }
        }
    }

    public override void Detached(Context context) {}
}
