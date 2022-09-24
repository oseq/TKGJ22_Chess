using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PieceData")]
public class PieceData : ScriptableObject
{
    public Vector3[] PossibleMoves =
     {
        new Vector3(1,0,0),
        new Vector3(-1,0,0),
        new Vector3(0,0,-1),
        new Vector3(0,0,1),
        new Vector3(1,0,1),
        new Vector3(1,0,-1),
        new Vector3(-1,0,-1),
        new Vector3(-1,0,1)
    };

    public float Cooldown;
    public float CooldownRate;
    public float SecondaryCooldown;
    public float SecondaryCooldownRate;
    public float SkillVelocity;
    public float Velocity;
    public float Jump;
    public float Mass;

}
