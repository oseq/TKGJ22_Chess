using System;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public event Action OnActionPerform = delegate { };

    [SerializeField]
    private float _cooldown;

    private float _timeToNextUse;
    private float _cooldownRate;
   

    private float Cooldown => _cooldown;    //get modifiers from stats

    private void TryToPerformAction()
    {
        if (_timeToNextUse > 0)
            return;

        OnActionPerform?.Invoke();
    }

    public void ResetCooldown()
    {
        _nextUseTime = 0f;
    }

    private void ScheduleCooldown()
    {
        _timeToNextUse = Cooldown;
    }

    public void Update()
    {
        _timeToNextUse -= Time.deltaTime * 
    }
}
