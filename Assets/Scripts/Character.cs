using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public enum MoveAction
{
    Attack,
    Move
}

[System.Serializable]
public enum MoveType
{
    Single,
    Continuous
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