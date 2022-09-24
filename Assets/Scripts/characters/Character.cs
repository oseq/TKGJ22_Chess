using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public enum MoveAction
{
    Attack,
    AttackMove,
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
public enum CharacterDirectionUnit
{
    Forward = 1,
    Backward = -1
}

[System.Serializable]
public abstract class Character : MonoBehaviour
{
    // should be 1 or - 1 to determine which way this character faces
    [SerializeField] public Field currentPosition;
    [SerializeField] public Player owner;


    public void SetField(Field position)
    {
        currentPosition = position;
    }

    public void SetOwner(Player own)
    {
        owner = own;
    }

    public abstract List<MoveDirection> GetMoveDirections();
}