using System;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public event Action OnTimePassedOut = delegate { };

    [SerializeField]
    private float timeRemaining = 30f;
    [SerializeField]
    private TMP_Text counterText;

    private void Update()
    {
        if (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            Debug.Log("Time has run out!");
            DisplayTime(0f);

            OnTimePassedOut?.Invoke();
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        counterText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}