using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillData")]
public class SkillData : ScriptableObject
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

}
