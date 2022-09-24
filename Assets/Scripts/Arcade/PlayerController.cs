using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int _playerId;

    private Stat _speed;
    private Stat _speedLimit;

    private Rigidbody _rb;
    private StatsContainer _sc;

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

        _sc = GetComponent<StatsContainer>();
        _speed = _sc.GetStat(StatType.Speed);
        _speedLimit = _sc.GetStat(StatType.SpeedLimit);
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

        _rb.AddForce(moveDir * _speed.GetValue() * Time.fixedDeltaTime, ForceMode.Force);
        _rb.velocity = ClampVelocity(_rb.velocity);
    }

    private Vector3 ClampVelocity(Vector3 velocity)
    {
        if (Mathf.Abs(velocity.x) > _speedLimit.GetValue())
        {
            velocity.x = _speedLimit.GetValue() * Mathf.Sign(velocity.x);
        }
        if (Mathf.Abs(velocity.y) > _speedLimit.GetValue())
        {
            velocity.y = _speedLimit.GetValue() * Mathf.Sign(velocity.y);
        }
        if (Mathf.Abs(velocity.x) > _speedLimit.GetValue())
        {
            velocity.z = _speedLimit.GetValue() * Mathf.Sign(velocity.z);
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

            foreach (var item in collision.contacts)
            {
                Debug.DrawRay(item.point, item.normal * 100, Color.red, 10f);
            }
        }
    }

    public void BlockInput()
    {
        _inputUnlockedTime = Time.time + _inputLockDuration;
    }
}
