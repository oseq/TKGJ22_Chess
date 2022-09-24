using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pawn : Character
{
    public override List<MoveDirection> GetMoveDirections()
    {
        var result = new List<MoveDirection>
        {
            new(new Vector2Int((int)owner.directionUnit, 0), MoveAction.Move, MoveType.Single),
            new(new Vector2Int((int)owner.directionUnit, 1), MoveAction.Attack, MoveType.Single),
            new(new Vector2Int((int)owner.directionUnit, -1), MoveAction.Attack, MoveType.Single)
        };
        return result;
    }
}