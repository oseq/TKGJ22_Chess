﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public event Action<Vector3> OnPerformSkillClicked = delegate { };
    public event Action<Vector3> OnPerformPowerUpClicked = delegate { };

    [SerializeField]
    private Vector3 _initialInputDirection;

    private Vector3 _lastInputDirection;
    private Vector3 _lastRealInputDirection;

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

    public void OnSkill(InputAction.CallbackContext context)
    {
        Debug.Log("OnSkill");
        if (context.action.triggered)
        {
            OnPerformSkillClicked?.Invoke(GetLastInputDirection());
        }
    }

    public void OnPowerUp(InputAction.CallbackContext context)
    {
        Debug.Log("OnPowerUp");
        if (context.action.triggered)
        {
            OnPerformPowerUpClicked?.Invoke(GetLastInputDirection());
        }
    }
}