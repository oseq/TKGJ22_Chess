using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private bool isPlaying = true;
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    private float stepRotation = 90f;
    [SerializeField]
    private float stepTime = .3f;
    [SerializeField]
    private float stepDelay = 1f;

    private float currentRotation = 0.0f;
    private float nextStepTime = 0f;

    private void Update()
    {
        if (isPlaying && nextStepTime <= Time.time)
        {
            currentRotation += stepRotation;
            transform.DORotateQuaternion(Quaternion.Euler(0f, currentRotation, 0f), stepTime);
            nextStepTime = Time.time + stepDelay;
        }
    }
}
