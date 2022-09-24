using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pawn : Character
{
    public Pawn(int forward, Field currentPosition, Player owner) : base(forward, currentPosition, owner)
    {
    }

    public override List<MoveDirection> GetMoveDirections()
    {
        var result = new List<MoveDirection>
        {
            new(new Vector2Int(forward, 0), MoveAction.Move, MoveType.Single),
            new(new Vector2Int(forward, 1), MoveAction.Attack, MoveType.Single),
            new(new Vector2Int(forward, -1), MoveAction.Attack, MoveType.Single)
        };
        return result;
    }
}