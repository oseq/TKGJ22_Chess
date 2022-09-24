using System;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public event Action<PlayerController> OnPlayerFelt = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if (player)
        {
            OnPlayerFelt?.Invoke(player);
        }
    }
}