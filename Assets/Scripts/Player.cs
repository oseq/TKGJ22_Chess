using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;

    private Vector2Int _currentField = Vector2Int.zero;

    void Update()
    {
        var input = HandleTestInput();
        if (input != Vector2Int.zero)
        {
            boardManager.OnFieldSelected(_currentField);
            _currentField += input;
        }
    }


    // for testing
    //  w -> (0, 1)
    //  s -> (0, -1)
    //  a -> (-1, 0)
    //  d -> (1, 0)
    private Vector2Int HandleTestInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            return Vector2Int.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            return Vector2Int.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            return Vector2Int.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            return Vector2Int.right;
        }

        return Vector2Int.zero;
    }
}