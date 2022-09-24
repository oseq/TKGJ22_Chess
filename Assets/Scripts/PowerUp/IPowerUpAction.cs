using UnityEngine;

public abstract class IPowerUpAction : ScriptableObject
{
    public struct Context
    {
        public GameObject instigator;
    }

    public abstract void Perform(Context context);
    public abstract void Detached(Context context);
}