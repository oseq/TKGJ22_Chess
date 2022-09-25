using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bishop : Character
{
    public override List<MoveDirection> GetMoveDirections()
    {
        return ParameterizedGetMoveDirections(owner.directionUnit);
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Bishop;
    }


    public static List<MoveDirection> ParameterizedGetMoveDirections(CharacterDirectionUnit directionUnit)
    {
        return new List<MoveDirection>
        {
            new(new Vector2Int(1, (int)directionUnit), MoveAction.AttackMove, MoveType.Continuous),
            new(new Vector2Int(-1, (int)directionUnit), MoveAction.AttackMove, MoveType.Continuous),
            new(new Vector2Int(1, -(int)directionUnit ), MoveAction.AttackMove, MoveType.Continuous),
            new(new Vector2Int(-1, -(int)directionUnit), MoveAction.AttackMove, MoveType.Continuous)
        };
    }
}