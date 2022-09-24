using System;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public event Action OnActionPerform = delegate { };

    [SerializeField]
    private float _cooldown;

    private float _nextUseTime;

    private float Cooldown => _cooldown;    //get modifiers from stats

    private void TryToPerformAction()
    {
        if (_nextUseTime >= Time.time)
            return;

        OnActionPerform?.Invoke();
    }

    public void ResetCooldown()
    {
        _nextUseTime = 0f;
    }

    private void ScheduleCooldown()
    {
        _nextUseTime = Time.time + Cooldown;
    }
}
