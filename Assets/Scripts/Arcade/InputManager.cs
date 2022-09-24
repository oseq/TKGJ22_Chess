using System;
using UnityEngine;

public class InputManager : MonoBehaviourSingleton<InputManager>
{
    [SerializeField]
    private InputController _playerOneInputController;
    [SerializeField]
    private InputController _playerTwoInputController;

    public InputController PlayerOneInputController => _playerOneInputController;
    public InputController PlayerTwoInputController => _playerTwoInputController;

    public InputController GetInputController(int playerId)
    {
        if (playerId == 1)
            return PlayerOneInputController;
        else if (playerId == 2)
            return PlayerTwoInputController;
        else
            throw new ArgumentException();
    }
}