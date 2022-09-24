using UnityEngine;

public class PowerUp : ScriptableObject
{
    [SerializeField] private IPowerUpAction[] onAttach;
    [SerializeField] private IPowerUpAction[] onUse;
}
