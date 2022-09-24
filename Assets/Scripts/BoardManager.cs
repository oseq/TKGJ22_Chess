using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class Move
{
    public Field from;
    public Field to;

    public Move(Field from, Field to)
    {
        this.from = from;
        this.to = to;
    }
}

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Board board;

    public IEnumerable<Move> AvailableMoves(Character character)
    {
        var result = new List<Move>();
        character.GetMoveDirections().ForEach(delegate(MoveDirection moveDirection)
        {
            result.Add(
                new Move(
                    character.currentPosition,
                    board.CurrentState().GetField(character.currentPosition.position + moveDirection.direction)
                )
            );
        });
        return result;
    }

    // player has selected the field, show possible movements (turn on the proper overlay)
    public void OnFieldSelected(Vector2Int selectedField)
    {
        var currentState = board.CurrentState().ToArray();

        // clear previous state
        foreach (var field in currentState)
        {
            field.PossibleMoveOverlay(false);
            field.SelectedOverlay(false);
        }

        var selected = currentState.GetField(selectedField);
        selected.SelectedOverlay(true);
        var possibleMoves = AvailableMoves(selected.GetCharacter());

        var toMark = possibleMoves.Select(move => move.to);
        foreach (var field in toMark)
        {
            field.PossibleMoveOverlay(true);
        }
    }
}