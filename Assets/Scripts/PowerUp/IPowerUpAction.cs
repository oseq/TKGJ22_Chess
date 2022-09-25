using DG.Tweening;
using UnityEngine;

public abstract class IPowerUpAction : ScriptableObject
{
    public struct Context
    {
        public GameObject instigator;
        public ParticleSystem effect;
        public TrailRendererToggler onAttachTrail;
    }

    public abstract void Perform(Context context);
    public abstract void Detached(Context context);
}

public class TrailRendererToggler
{
    private TrailRenderer _trailRenderer;
    private float _time;

    public TrailRendererToggler(TrailRenderer trailRenderer, float time)
    {
        _trailRenderer = trailRenderer;
        _time = time;
    }

    public void TurnOnTrail()
    {
        if (_trailRenderer != null)
        {
            _trailRenderer.emitting = true;
            DOVirtual.DelayedCall(_time, () => _trailRenderer.emitting = false);
        }
    }
    
    public void TurnOnTrailPermanently()
    {
        if (_trailRenderer != null)
        {
            _trailRenderer.emitting = true;
        }
    }

    public void TurnOffTrail()
    {
        if (_trailRenderer != null)
        {
            _trailRenderer.emitting = false;
        }
    }

    public bool isToogled()
    {
        if (_trailRenderer != null)
        {
            return _trailRenderer.emitting;
        }

        return false;
    }
}