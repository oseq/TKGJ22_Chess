using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class StrategyInputController : MonoBehaviour
{
    public event Action OnAcceptClickedEvent = delegate { };
    public event Action OnCancelClickedEvent = delegate { };

    [SerializeField]
    private Vector3 _initialInputDirection;

    private Vector3 _lastInputDirection;
    private Vector3 _lastRealInputDirection;
    private bool _wasAcceptTriggered;
    private bool _wasCancelTriggered;

    private readonly float inputDeadzone = 0.1f;

    private void Awake()
    {
        _lastInputDirection = _initialInputDirection;
    }

    /// <summary>
    /// CANNOT be zero.
    /// </summary>
    public Vector3 GetLastInputDirection()
    {
        return _lastInputDirection;
    }

    /// <summary>
    /// Can be zero.
    /// </summary>
    public Vector3 GetRealInputDirection()
    {
        return _lastRealInputDirection;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var movement = context.ReadValue<Vector2>();
        _lastRealInputDirection = new Vector3(movement.x, 0f, movement.y);
        if (Mathf.Abs(movement.x) > inputDeadzone || Mathf.Abs(movement.y) > inputDeadzone)
        {
            _lastInputDirection = _lastRealInputDirection;
        }
        Debug.Log($"OnMove: {movement}, {_lastInputDirection}");
    }

    public void OnAcceptClicked(InputAction.CallbackContext context)
    {
        Debug.Log("OnAccept");
        if (context.action.triggered)
        {
            _wasAcceptTriggered = true;
            OnAcceptClickedEvent?.Invoke();
        }
    }

    public void OnCancelClicked(InputAction.CallbackContext context)
    {
        Debug.Log("OnCancel");
        if (context.action.triggered)
        {
            _wasCancelTriggered = true;
            OnCancelClickedEvent?.Invoke();
        }
    }

    public bool TryToConsumeAccept()
    {
        if (_wasAcceptTriggered)
        {
            _wasAcceptTriggered = false;
            return true;
        }
        return false;
    }

    public bool TryToConsumeCancel()
    {
        if (_wasCancelTriggered)
        {
            _wasCancelTriggered = false;
            return true;
        }
        return false;
    }

    public void ConsumeAll()
    {
        _wasAcceptTriggered = false;
        _wasCancelTriggered = false;
    }
}