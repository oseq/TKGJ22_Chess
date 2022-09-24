using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private bool isPlaying = true;
    [SerializeField]
    private float speed = 0.0f;

    private float currentRotation = 0.0f;

    private void Update()
    {
        if (isPlaying)
        {
            currentRotation += speed * Time.unscaledDeltaTime;
            transform.rotation = Quaternion.Euler(-90f, 0f, currentRotation);
        }
    }
}
