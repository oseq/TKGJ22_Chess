using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class Queen: Character
{
    public override List<MoveDirection> GetMoveDirections()
    {
        return ParameterizedGetMoveDirections(owner.directionUnit);
    }


    public static List<MoveDirection> ParameterizedGetMoveDirections(CharacterDirectionUnit directionUnit)
    {
        return 
            Rook.ParameterizedGetMoveDirections(directionUnit)
            .Concat(
                Bishop.ParameterizedGetMoveDirections(directionUnit)
            ).ToList();
    }
}