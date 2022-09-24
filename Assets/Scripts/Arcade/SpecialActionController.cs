using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpecialActionController : MonoBehaviour
{
    private float   _velocity;
    private float   _jump;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private KeyCode _button;
    [SerializeField]
    private KeyCode _secondButton;
    [SerializeField]
    private PieceData _playerData;
    [SerializeField]
    private ActionButtonView _actionButtonView;
    [SerializeField]
    private ActionButtonView _powerUpButtonView;

    [SerializeField]
    private TrailRenderer _trailRenderer;
    [SerializeField]
    private float _trailRendererTime = .5f;

    private StatsContainer _stats;
    private Stat _cooldown;
    private Stat _cooldownRate;
    private float _timeToNextUse;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _stats = GetComponent<StatsContainer>();
        _jump = _playerData.Jump;
        _velocity = _playerData.SkillVelocity;
        _cooldown = _stats.GetStat(StatType.Cooldown);
        _cooldown.CreateModifier(StatModifier.Type.Additive, _playerData.Cooldown);
        _cooldownRate = _stats.GetStat(StatType.CooldownRate);
        _cooldownRate.CreateModifier(StatModifier.Type.Additive, _playerData.CooldownRate);
        _powerUpButtonView.SetReady(false);
        _trailRenderer.emitting = false;
    }

    private void Update()
    {
        CheckSpecialAction();
        _timeToNextUse -= Time.deltaTime * _cooldownRate.GetValue();
        _actionButtonView.UpdateTimer(_timeToNextUse, _cooldown.GetValue());
    }

    private void CheckSpecialAction()
    {
        if (Input.GetKeyDown(_button))
        {
            TryToPerformAction();
        }
        if (Input.GetKeyDown(_secondButton))
        {
            SecondarySpecialAction();
        }
    }

    public void SpecialAction()
    {
        _rb.AddForce(getActionDirection().normalized * _velocity * Time.deltaTime, ForceMode.Impulse);
        _rb.velocity = getActionDirection().normalized * _velocity;
        ScheduleCooldown();

        _trailRenderer.emitting = true;
        DOVirtual.DelayedCall(_trailRendererTime, () => _trailRenderer.emitting = false);
    }

    public void SecondarySpecialAction()
    {
        
    }

    private void TryToPerformAction()
    {
        if (_timeToNextUse > 0)
            return;

        SpecialAction();
    }

    private void TryToPerformSecondaryAction()
    {
        if (false)
            return;
        SecondarySpecialAction();
    }

    public void ResetCooldown()
    {
        _timeToNextUse = 0f;
    }


    private void ScheduleCooldown()
    {
        _timeToNextUse = _cooldown.GetValue();
    }

    public Vector3 getActionDirection()
    {
        Vector3 CurrentDirection = _rb.velocity;
        float MinDistance = float.MaxValue;
        Vector3 MinVector = new Vector3();
        foreach (Vector3 Vector in _playerData.PossibleMoves)
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
