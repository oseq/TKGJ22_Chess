using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] public CharacterDirectionUnit directionUnit;

    private Vector2Int _currentField = Vector2Int.zero;

    void Update()
    {
        // var input = HandleTestInput();
        // if (input != Vector2Int.zero)
        // {
        //     boardManager.OnFieldSelected(_currentField);
        //     _currentField += input;
        // }
    }
}