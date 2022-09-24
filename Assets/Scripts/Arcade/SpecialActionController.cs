using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpecialActionController : MonoBehaviour
{
    public float   _velocity;
    public bool    _isJump;
    public Rigidbody _rb;
    public KeyCode _button;
    public SkillData SkillData;
    public float Cooldown;
    public float CooldownRate;
    private float _cooldown;
    private float _timeToNextUse;
    private float _cooldownRate;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckSpecialAction();
        _timeToNextUse -= Time.deltaTime * _cooldownRate;
       
    }

    private void CheckSpecialAction()
    {
        if (Input.GetKeyDown(_button))
        {
            TryToPerformAction();
        }
    }

    public void SpecialAction()
    {
        _rb.AddForce(getActionDirection().normalized * _velocity * Time.deltaTime, ForceMode.Impulse);
        _rb.velocity = getActionDirection().normalized * _velocity;
        ScheduleCooldown();
    }

    private void TryToPerformAction()
    {
        if (_timeToNextUse > 0)
            return;

        SpecialAction();
    }

    public void ResetCooldown()
    {
        _timeToNextUse = 0f;
    }

    private void ScheduleCooldown()
    {
        _timeToNextUse = Cooldown;
    }

    public Vector3 getActionDirection()
    {
        Vector3 CurrentDirection = _rb.velocity;
        float MinDistance = float.MaxValue;
        Vector3 MinVector = new Vector3();
        foreach (Vector3 Vector in SkillData.PossibleMoves)
        {
            float Distance = Vector3.Distance(Vector.normalized, CurrentDirection.normalized);
            if (Distance < MinDistance)
            {
                MinDistance = Distance;
                MinVector = Vector;
            }
        }
        return MinVector;
    }

}
