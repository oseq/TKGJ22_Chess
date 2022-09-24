using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Rook : Character
{
    public override List<MoveDirection> GetMoveDirections()
    {
        return ParameterizedGetMoveDirections(owner.directionUnit);
    }


    public static List<MoveDirection> ParameterizedGetMoveDirections(CharacterDirectionUnit directionUnit)
    {
        return new List<MoveDirection>
        {
            new(new Vector2Int((int)directionUnit, 0), MoveAction.Move, MoveType.Continuous),
            new(new Vector2Int(-(int)directionUnit, 0), MoveAction.Move, MoveType.Continuous),
            new(new Vector2Int(0, 1), MoveAction.Move, MoveType.Continuous),
            new(new Vector2Int(0, -1), MoveAction.Move, MoveType.Continuous)
        };
    }
}