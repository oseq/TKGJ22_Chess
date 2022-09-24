using System;

[Serializable]
public abstract class IPowerUpAction
{
    public struct Context
    {
        public UnityEngine.GameObject instigator;
    }

    public void Perform(Context context) {}
}