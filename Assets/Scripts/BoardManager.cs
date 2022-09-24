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

    private IEnumerable<Move> AvailableMoves(Character character)
    {
        var moves = new List<Move>();
        character.GetMoveDirections().ForEach(delegate(MoveDirection moveDirection)
        {
            moves.AddRange(ExtendMoveDirection(character, moveDirection));
        });
        return moves;
    }

    private IEnumerable<Move> ExtendMoveDirection(Character character, MoveDirection moveDirection)
    {
        var moves = new List<Move>();
        var keepExtending = true;
        var position = new Vector2Int(character.currentPosition.position[0], character.currentPosition.position[1]);
        do
        {
            position += moveDirection.direction;
            var positionField = board.CurrentState().GetField(position);
            var validationResult = ValidateMove(
                character,
                positionField,
                moveDirection.moveAction);
            var validatedMove = new Move(
                character.currentPosition,
                positionField
            );
            switch (validationResult)
            {
                case MoveValidationResult.ValidGo:
                    moves.Add(validatedMove);
                    break;
                case MoveValidationResult.ValidStop:
                    moves.Add(validatedMove);
                    keepExtending = false;
                    break;
                case MoveValidationResult.Invalid:
                    keepExtending = false;
                    break;
                default:
                    throw new InvalidProgramException("Unhandled enum MoveValidationResult");
            }
        } while (keepExtending);

        return moves;
    }

    private enum MoveValidationResult
    {
        ValidStop,
        ValidGo,
        Invalid
    }

    private MoveValidationResult ValidateMove(Character character, Field to, MoveAction moveAction)
    {
        if (!IsPositionInBoard(to.position))
        {
            return MoveValidationResult.Invalid;
        }

        if (to.IsOccupied())
        {
            return to.GetCharacter().owner != character.owner
                ? MoveValidationResult.ValidStop
                : MoveValidationResult.Invalid;
        }

        return moveAction == MoveAction.Attack ? MoveValidationResult.Invalid : MoveValidationResult.ValidGo;
    }

    private bool IsPositionInBoard(Vector2Int position)
    {
        return position[0] >= 0
               && position[0] < board.GetNumberOfColumns()
               && position[1] >= 0
               && position[1] < board.GetNumberOfRows();
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