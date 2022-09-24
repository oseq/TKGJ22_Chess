using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public enum MoveAction
{
    ATTACK,
    MOVE
}

[System.Serializable]
public enum MoveType
{
    SINGLE,
    CONTINUOUS
}

[System.Serializable]
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

[System.Serializable]
public abstract class Character : MonoBehaviour
{
    // should be 1 or - 1 to determine which way this character faces
    [SerializeField] public int forward;
    [SerializeField] public Field currentPosition;
    [SerializeField] public Player owner;

    protected Character(int forward, Field currentPosition, Player owner)
    {
        this.forward = forward;
        this.currentPosition = currentPosition;
        this.owner = owner;
    }

    // Trigger for being removed from the field.
    public void OnRemovedFromField()
    {
    }
    public abstract List<MoveDirection> GetMoveDirections();
}

[System.Serializable]
public class Pawn : Character
{
    public Pawn(int forward, Field currentPosition, Player owner) : base(forward, currentPosition, owner)
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