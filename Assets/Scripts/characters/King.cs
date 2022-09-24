using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class King : Character
{
    public override List<MoveDirection> GetMoveDirections()
    {
        return ParameterizedGetMoveDirections(owner.directionUnit);
    }


    public static List<MoveDirection> ParameterizedGetMoveDirections(CharacterDirectionUnit directionUnit)
    {
        return new List<MoveDirection>
        {
            new(new Vector2Int(0, (int)directionUnit), MoveAction.AttackMove, MoveType.Single),
            new(new Vector2Int(0, -(int)directionUnit), MoveAction.AttackMove, MoveType.Single),
            new(new Vector2Int(1, 0), MoveAction.AttackMove, MoveType.Single),
            new(new Vector2Int(-1, 0), MoveAction.AttackMove, MoveType.Single),
            new(new Vector2Int(1, (int)directionUnit), MoveAction.AttackMove, MoveType.Single),
            new(new Vector2Int(-1, (int)directionUnit), MoveAction.AttackMove, MoveType.Single),
            new(new Vector2Int(1, -(int)directionUnit), MoveAction.AttackMove, MoveType.Single),
            new(new Vector2Int(-1, -(int)directionUnit), MoveAction.AttackMove, MoveType.Single)
        };
    }
}