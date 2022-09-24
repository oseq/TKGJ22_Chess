using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardLoop : MonoBehaviour
{
    private enum State
    {
        Start, // initialization stuff, always go to Player1Move

        Player1SelectingCharacter, // player1 is selecting the character
        Player1SelectingMove, // player1 is selecting the actual move
        Player2SelectingCharacter, // player2 is selecting the move
        Player2SelectingMove, // player2 is selecting the actual move

        Fight, // temporary state, players are fighting
    }

    private enum InputResult
    {
        Previous,
        Next,
        Accept,
        None // default callback
    }

    [SerializeField] private BoardManager boardManager;

    [SerializeField] private Player player1;
    [SerializeField] private Player player2;

    private readonly StateMachine _stateMachine = new();
    private SelectableFieldList _currentSelection;

    private Field _selectedCharacter;
    private Field _selectedMove;

    private void Start()
    {
        _stateMachine.Next();
    }

    private void Update()
    {
        var currentState = _stateMachine.Current();
        if (currentState is State.Start)
        {
            // nothing to do.
            return;
        }

        if (currentState is State.Player1SelectingCharacter or State.Player2SelectingCharacter &&
            _currentSelection == null)
        {
            var possible = boardManager.PossiblePlayerStartingMoves(PlayerFromState());
            _currentSelection = new SelectableFieldList(possible.ToList());
            return;
        }

        if (currentState is State.Player1SelectingCharacter or State.Player2SelectingCharacter)
        {
            var input = HandleTestInput();
            switch (input)
            {
                case InputResult.Previous:
                    _currentSelection.Previous();
                    break;
                case InputResult.Next:
                    _currentSelection.Next();
                    break;
                case InputResult.Accept:
                    _selectedCharacter = _currentSelection.Selection();
                    _currentSelection = null;
                    _stateMachine.Next();
                    break;
                case InputResult.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return;
        }

        if (currentState is State.Player1SelectingMove or State.Player2SelectingMove && _currentSelection == null)
        {
            var possible = boardManager.PossibleMoves(_selectedCharacter);
            _currentSelection = new SelectableFieldList(possible.ToList());

            return;
        }

        if (currentState is State.Player1SelectingMove or State.Player2SelectingMove)
        {
            var input = HandleTestInput();
            switch (input)
            {
                case InputResult.Previous:
                    _currentSelection.Previous();
                    break;
                case InputResult.Next:
                    _currentSelection.Next();
                    break;
                case InputResult.Accept:
                    _selectedMove = _currentSelection.Selection();
                    _currentSelection = null;
                    if (_selectedMove != null)
                    {
                        _stateMachine.Next();
                    }
                    break;
                case InputResult.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return;
        }
        
        if (currentState == State.Fight)
        {
            if (_selectedMove.IsOccupied())
            {
                var attacker = PlayerFromState();
                var defender = attacker == player1 ? player2 : player1;
                if (DoFight(attacker, defender) == attacker)
                {
                    _selectedMove.Occupy(attacker, _selectedCharacter.GetCharacter(), true);
                }
            }
            else
            {
                _selectedMove.Occupy(PlayerFromState(), _selectedCharacter.GetCharacter(), false);
            }
            _selectedCharacter.Deoccupy();
            _stateMachine.Next();
        }
    }

    // TODO: This will be the connection with arcade game, parameters might change
    private static Player DoFight(Player attacker, Player defender)
    {
        return attacker;
    }


    private Player PlayerFromState()
    {
        return _stateMachine.Current() switch
        {
            State.Player1SelectingCharacter or State.Player1SelectingMove => player1,
            State.Player2SelectingCharacter or State.Player2SelectingMove => player2,
            State.Fight when _stateMachine.Previous() == State.Player1SelectingMove => player1,
            State.Fight when _stateMachine.Previous() == State.Player2SelectingMove => player2,
            _ => null
        };
    }

    // KeyCode.Q => previous field
    // KeyCode.W => next field
    // KeyCode.A => accept
    private static InputResult HandleTestInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            return InputResult.Next;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            return InputResult.Previous;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            return InputResult.Accept;
        }

        return InputResult.None;
    }

    private class StateMachine
    {
        private State _previousState;
        private State _currentState;

        public StateMachine()
        {
            _previousState = State.Start;
            _currentState = State.Start;
        }

        public State Current()
        {
            return _currentState;
        }

        public State Previous()
        {
            return _previousState;
        }

        public void Next()
        {
            switch (_currentState)
            {
                case State.Start:
                    _previousState = State.Start;
                    _currentState = State.Player1SelectingCharacter;
                    return;
                case State.Player1SelectingCharacter:
                    _previousState = State.Player1SelectingCharacter;
                    _currentState = State.Player1SelectingMove;
                    return;
                case State.Player1SelectingMove:
                    _previousState = State.Player1SelectingMove;
                    _currentState = State.Fight;
                    return;
                case State.Player2SelectingCharacter:
                    _previousState = State.Player2SelectingCharacter;
                    _currentState = State.Player2SelectingMove;
                    return;
                case State.Player2SelectingMove:
                    _previousState = State.Player2SelectingMove;
                    _currentState = State.Fight;
                    return;
                case State.Fight when _previousState == State.Player1SelectingMove:
                    _previousState = State.Fight;
                    _currentState = State.Player2SelectingCharacter;
                    return;
                case State.Fight when _previousState == State.Player2SelectingMove:
                    _previousState = State.Fight;
                    _currentState = State.Player1SelectingCharacter;
                    return;
            }

            // Debug.Log($"New state: prev: ${_previousState}, new: ${_currentState}");
        }
    }

    private class SelectableFieldList
    {
        private readonly List<Field> _backingField;
        private int _selected = -1;

        public SelectableFieldList(List<Field> backingField)
        {
            _backingField = backingField;
            if (_backingField.Any())
            {
                _selected = 0;
            }
        }

        public Field Selection()
        {
            return _backingField.ElementAtOrDefault(_selected);
        }

        public void Next()
        {
            PreAction();
            _selected += 1;
            if (_selected >= _backingField.Count)
            {
                _selected = 0;
            }

            Apply();
        }

        public void Previous()
        {
            PreAction();
            _selected -= 1;
            if (_selected < 0)
            {
                _selected = _backingField.Count - 1;
            }

            Apply();
        }

        private void PreAction()
        {
            var selection = Selection();
            if (selection != null)
            {
                Selection().Unselect();
            }
        }

        private void Apply()
        {
            Selection().Select();
        }
    }
}