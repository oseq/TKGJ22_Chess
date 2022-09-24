using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Move
{
    [SerializeField] public Field from;
    [SerializeField] public Field to;

    public Move(Field from, Field to)
    {
        this.from = from;
        this.to = to;
    }
}

[System.Serializable]
public class BoardManager : MonoBehaviour
{
    [SerializeField] private Board board;

    private IEnumerable<Move> AvailableMoves(Character character)
    {
        var moves = new List<Move>();
        foreach (var moveDirection in character.GetMoveDirections())
        {
            moves.AddRange(ExtendMoveDirection(character, moveDirection));
        }

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
            var validationResult = ValidateMove(
                character,
                position,
                moveDirection.moveAction,
                moveDirection.moveType);
            var validatedMove = new Move(
                character.currentPosition,
                board.CurrentState().GetField(position)
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

    [System.Serializable]
    private enum MoveValidationResult
    {
        ValidStop,
        ValidGo,
        Invalid
    }

    private MoveValidationResult ValidateMove(Character character, Vector2Int position, MoveAction moveAction, MoveType moveType)
    {
        if (!IsPositionInBoard(position))
        {
            return MoveValidationResult.Invalid;
        }

        var toField = board.CurrentState().GetField(position);

        if (toField.IsOccupied())
        {
            return toField.GetCharacter().owner != character.owner
                ? MoveValidationResult.ValidStop
                : MoveValidationResult.Invalid;
        }

        if (moveAction == MoveAction.Attack)
        {
            return MoveValidationResult.Invalid;
        }

        switch(moveType)
        {
            case MoveType.Continuous:
                return MoveValidationResult.ValidGo;
            case MoveType.Single:
                return MoveValidationResult.ValidStop;
            default:
                throw new InvalidProgramException("Unhandled enum MoveValidationResult");
        }
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
        }

        var selected = currentState.GetField(selectedField);
        selected.PossibleMoveOverlay(true);
        var possibleMoves = AvailableMoves(selected.GetCharacter());

        var toMark = possibleMoves.Select(move => move.to);
        foreach (var field in toMark)
        {
            field.PossibleMoveOverlay(true);
        }
    }
}