using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int _playerId;
    [SerializeField]
    private float _moveForce = 10f;
    [SerializeField]
    private float _velocityLimit = 3f;

    private Rigidbody _rb;
    private bool _hasControl = true;
    private string _horizontalString;
    private string _verticalString;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _horizontalString = $"Horizontal{_playerId}";
        _verticalString = $"Vertical{_playerId}";
    }

    private void FixedUpdate()
    {
        TryToMove();
    }

    private void TryToMove()
    {
        if (!_hasControl)
            return;

        Vector3 moveDir = transform.forward;
        moveDir = new Vector3(Input.GetAxisRaw(_horizontalString), 0f, Input.GetAxisRaw(_verticalString));

        _rb.AddForce(moveDir * _moveForce * Time.fixedDeltaTime, ForceMode.Force);
        _rb.velocity = ClampVelocity(_rb.velocity);
    }

    private Vector3 ClampVelocity(Vector3 velocity)
    {
        if (Mathf.Abs(velocity.x) > _velocityLimit)
        {
            velocity.x = _velocityLimit * Mathf.Sign(velocity.x);
        }
        if (Mathf.Abs(velocity.y) > _velocityLimit)
        {
            velocity.y = _velocityLimit * Mathf.Sign(velocity.y);
        }
        if (Mathf.Abs(velocity.x) > _velocityLimit)
        {
            velocity.z = _velocityLimit * Mathf.Sign(velocity.z);
        }

        return velocity;
    }
}
