using System;
using UnityEngine;
using System.Collections.Generic;

public class Move
{
    public Field from;
    public Field to;
    public MoveAction moveAction;

    public Move(Field from, Field to, MoveAction moveAction)
    {
        this.from = from;
        this.to = to;
        this.moveAction = moveAction;
    }
}

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Board board;

    public List<Move> AvailableMoves(Character character)
    {
        List<Move> result = new List<Move>();
        character.GetMoveDirections().ForEach(delegate(MoveDirection moveDirection) {
            result.Add(
                new Move(
                    character.currentPosition,
                    board.GetField(character.currentPosition.position + moveDirection.direction),
                    moveDirection.moveAction
                     )
                );
        });
        return result;
    }
}