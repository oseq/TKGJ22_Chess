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
    private string _horizontalString;
    private string _verticalString;
    private float _inputUnlockedTime;

    private readonly float _inputLockDuration = .25f;

    public int PlayerId => _playerId;
    public Rigidbody Rigidbody => _rb;
    public bool HasControl => _inputUnlockedTime <= Time.time;

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
        if (!HasControl)
            return;

        Vector3 moveDir = new Vector3(Input.GetAxisRaw(_horizontalString), 0f, Input.GetAxisRaw(_verticalString));

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

    private void OnCollisionEnter(Collision collision)
    {
        //skip bounce if not attacker to not do it two times
        if (PlayerId != 1 && HasControl)
            return;

        var playerHit = collision.collider.GetComponent<PlayerController>();
        if (playerHit && playerHit.PlayerId != PlayerId)
        {
            //add bounce force
            var ownVelocity = _rb.velocity;
            var otherVelocity = playerHit.Rigidbody.velocity;

            //_rb.AddForce(otherVelocity - ownVelocity, ForceMode.Impulse);
            //playerHit.Rigidbody.AddForce(ownVelocity - otherVelocity, ForceMode.Impulse);

            _rb.velocity = -ownVelocity + otherVelocity;
            playerHit.Rigidbody.velocity = -otherVelocity + ownVelocity;

            BlockInput();
            playerHit.BlockInput();
        }
    }

    public void BlockInput()
    {
        _inputUnlockedTime = Time.time + _inputLockDuration;
    }
}
