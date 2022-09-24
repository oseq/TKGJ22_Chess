using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    [SerializeField]
    private float forceMultiplier = 100f;
    


    [SerializeField]
    private List<Transform> bouncers = new List<Transform>();
    [SerializeField]
    private float bouncerDefaultScale = 3f;
    [SerializeField]
    private float bouncerMinScale = 1f;
    [SerializeField]
    private float bouncerStreachInTime = 0.15f;
    [SerializeField]
    private float bouncerStreachOutTime = 0.4f;
    [SerializeField]
    private Ease bouncerStreachOutTween = Ease.InOutBack;

    private Sequence sequence;

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<PlayerController>())
        {
            Debug.Log("Player trigger");
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            otherRb.AddForce(otherRb.velocity * -forceMultiplier);
            AnimateBounceDown();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            AnimateBounceUp();
        }
    }

    private void AnimateBounceDown()
    {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        foreach(var bo in bouncers)
        {
            sequence.Insert(0f, bo.DOScaleY(bouncerMinScale, 0.15f));
        }
    }

    private void AnimateBounceUp()
    {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        foreach (var bo in bouncers)
        {
            sequence.Insert(0f, bo.DOScaleY(bouncerDefaultScale, 0.4f).SetEase(bouncerStreachOutTween));
        }
    }
}
