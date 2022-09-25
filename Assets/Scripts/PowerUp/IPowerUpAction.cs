using DG.Tweening;
using UnityEngine;

public abstract class IPowerUpAction : ScriptableObject
{
    public struct Context
    {
        public GameObject instigator;
        public ParticleSystem effect;
        public TrailRendererEnabler onAttachTrail;
    }

    public abstract void Perform(Context context);
    public abstract void Detached(Context context);
}

public class TrailRendererEnabler
{
    private TrailRenderer _trailRenderer;
    private float _time;

    public TrailRendererEnabler(TrailRenderer trailRenderer, float time)
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
}