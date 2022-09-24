using UnityEngine;
using System.Collections.Generic;

public enum MoveAction
{
    ATTACK,
    MOVE
}

public enum MoveType
{
    SINGLE,
    CONTINUOUS
}


public class MoveDirection
{

    [SerializeField] public Vector2Int direction;
    [SerializeField] public MoveAction moveAction;
    [SerializeField] public MoveType moveType;

    public MoveDirection(Vector2Int direction, MoveAction moveAction, MoveType moveType)
    {
        this.direction = direction;
        this.moveAction = moveAction;
        this.moveType = moveType;
    }
}
public abstract class Character : MonoBehaviour
{
    // should be 1 or - 1 to determine which way this character faces
    public int forward;
    public Field currentPosition;

    protected Character(int forward, Field currentPosition)
    {
        this.forward = forward;
        this.currentPosition = currentPosition;
    }

    // Trigger for being removed from the field.
    public void OnRemovedFromField()
    {
    }
    public abstract List<MoveDirection> GetMoveDirections();
}

public class Pawn : Character
{
    public Pawn(int forward, Field currentPosition) : base(forward, currentPosition)
    {
    }

    override public List<MoveDirection> GetMoveDirections()
    {
        List<MoveDirection> result = new List<MoveDirection>();
        result.Add(
            new MoveDirection(new Vector2Int(forward, 0), MoveAction.MOVE, MoveType.SINGLE)
            );
        result.Add(
            new MoveDirection(new Vector2Int(forward, 1), MoveAction.ATTACK, MoveType.SINGLE)
            );
        result.Add(
            new MoveDirection(new Vector2Int(forward, -1), MoveAction.ATTACK, MoveType.SINGLE)
            );
        return result;
    }
}