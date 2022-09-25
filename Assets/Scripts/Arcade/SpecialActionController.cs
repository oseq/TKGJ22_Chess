using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpecialActionController : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;

    private float _velocity;
    private float _jump;
    [SerializeField]
    private Rigidbody _rb;
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

    private TrailRendererToggler _trailRendererToggler;

    [SerializeField] private ParticleSystem forceParticle;

    private StatsContainer _stats;
    private Stat _cooldown;
    private Stat _cooldownRate;
    private float _timeToNextUse;
    private PowerUp _currentPowerUp;

    private InputController inputController => _playerController.InputController;

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
        _trailRendererToggler = new TrailRendererToggler(_trailRenderer, _trailRendererTime);
    }

    private void OnEnable()
    {
        inputController.OnPerformSkillClicked += InputController_OnPerformSkillClicked;
        inputController.OnPerformPowerUpClicked += InputController_OnPerformPowerUpClicked;
    }

    private void OnDisable()
    {
        inputController.OnPerformSkillClicked -= InputController_OnPerformSkillClicked;
        inputController.OnPerformPowerUpClicked -= InputController_OnPerformPowerUpClicked;
    }

    private void Update()
    {
        _timeToNextUse -= Time.deltaTime * _cooldownRate.GetValue();
        _actionButtonView.UpdateTimer(_timeToNextUse, _cooldown.GetValue());
    }

    public void SetPieceType(PieceData type)
    {
        _playerData = type;
    }

    private void InputController_OnPerformSkillClicked(Vector3 moveDir)
    {
        TryToPerformSkill();
    }

    private void InputController_OnPerformPowerUpClicked(Vector3 moveDir)
    {
        TryToPerformPowerUp();
    }

    public void SpecialAction()
    {
        _rb.AddForce(getActionDirection().normalized * (_velocity * Time.deltaTime), ForceMode.Impulse);
        _rb.velocity = getActionDirection().normalized * _velocity;
        ScheduleCooldown();

        if (!_trailRendererToggler.isToogled())
        {
            _trailRendererToggler.TurnOnTrail();
        }
        
    }

    public void TryToPerformPowerUp()
    {
        if(_currentPowerUp)
        {
            if(_currentPowerUp.onUse != null)
            {
                IPowerUpAction.Context context = new()
                {
                    instigator = gameObject,
                    effect = forceParticle
                };
                foreach (var action in _currentPowerUp.onUse)
                {
                    action.Perform(context);
                }
                
                _powerUpButtonView.SetReady(false);
                _currentPowerUp = null;
            }
        }
    }

    private void TryToPerformSkill()
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
        _timeToNextUse = _cooldown.GetValue();
    }

    public Vector3 getActionDirection()
    {
        //Vector3 CurrentDirection = _rb.velocity;
        Vector3 CurrentDirection = inputController.GetLastInputDirection();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            IPowerUpAction.Context context = new()
            {
                instigator = gameObject,
                onAttachTrail = _trailRendererToggler
            };

            if (_currentPowerUp)
            {
                if (_currentPowerUp.onAttach != null)
                {
                    foreach (var action in _currentPowerUp.onAttach)
                    {
                        action.Detached(context);
                    }
                }
                _currentPowerUp = null;
            }

            var container = other.GetComponent<PowerUpContainer>();
            if (container)
            {
                _currentPowerUp = container.Consume();
                if (_currentPowerUp)
                {
                    if (_currentPowerUp.onAttach != null)
                    {
                        foreach (var action in _currentPowerUp.onAttach)
                        {
                            action.Perform(context);
                        }
                    }

                    if(_currentPowerUp.onUse != null && _currentPowerUp.onUse.Length > 0)
                    {
                        _powerUpButtonView.SetReady(true);
                    }
                }
            }
        }
    }

}
