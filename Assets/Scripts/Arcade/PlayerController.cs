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
    [SerializeField]
    private MeshFilter _pieceType;
  

    private float _inputUnlockedTime;

    private readonly float _inputLockDuration = .25f;

    [SerializeField]
    private Transform indicator;

    public int PlayerId => _playerId;
    public Rigidbody Rigidbody => _rb;
    public bool HasControl => _inputUnlockedTime <= Time.time;
    public InputController InputController => InputManager.Instance.GetInputController(PlayerId);

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _sc = GetComponent<StatsContainer>();
        _speed = _sc.GetStat(StatType.Speed);
        _speedLimit = _sc.GetStat(StatType.SpeedLimit);
    }

    private void Update()
    {
        RefreshIndicator();
    }

    public void SetPieceType(PieceType type)
    {
        switch (type)
        {
            case PieceType.Bishop:
              
                break;
            case PieceType.King:

                break;
            case PieceType.Knight:

                break;
            case PieceType.Pawn:

                break;
            case PieceType.Queen:

                break;
            case PieceType.Rook:

                break;
   
        }
    }

    private void FixedUpdate()
    {
        TryToMove();
    }

    private void TryToMove()
    {
        if (!HasControl)
            return;

        Vector3 moveDir = InputController.GetRealInputDirection();

        _rb.AddForce(moveDir * _speed.GetValue() * Time.fixedDeltaTime, ForceMode.Force);
        _rb.velocity = ClampVelocity(_rb.velocity);
        //InputIndicator(moveDir);

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

    private void RefreshIndicator()
    {
        var dir = InputController.GetLastInputDirection();
        indicator.LookAt(transform.position + dir, Vector3.up);
        indicator.Rotate(90f, 0f, 0f);
    }

    public void BlockInput()
    {
        _inputUnlockedTime = Time.time + _inputLockDuration;
    }
}
