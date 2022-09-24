using UnityEngine;

public class PowerUp : ScriptableObject
{
    [SerializeField] public IPowerUpAction[] onAttach;
    [SerializeField] public IPowerUpAction[] onUse;
}
