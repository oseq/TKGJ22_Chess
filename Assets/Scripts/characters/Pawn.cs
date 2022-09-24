using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pawn : Character
{
    public override List<MoveDirection> GetMoveDirections()
    {
        return ParameterizedGetMoveDirections(owner.directionUnit);
    }


    public static List<MoveDirection> ParameterizedGetMoveDirections(CharacterDirectionUnit directionUnit)
    {
        return new List<MoveDirection>
        {
            new(new Vector2Int(0, (int)directionUnit), MoveAction.Move, MoveType.Single),
            new(new Vector2Int(1, (int)directionUnit), MoveAction.Attack, MoveType.Single),
            new(new Vector2Int(-1, (int)directionUnit), MoveAction.Attack, MoveType.Single)
        };
    }
}