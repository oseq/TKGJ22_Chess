using System;
using System.Linq;
using UnityEngine;

public enum AttackType
{
}

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Board board;

    public Tuple<Vector2Int, AttackType>[] AvailableMoves(Field field)
    {
        throw new NotImplementedException();
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
        var possibleMoves = AvailableMoves(selected);

        var toMark = currentState.GetFields(possibleMoves.Select(x => x.Item1));
        foreach (var field in toMark)
        {
            field.PossibleMoveOverlay(true);
        }
    }
}