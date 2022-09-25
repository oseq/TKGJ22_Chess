using System;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public event Action OnTimePassedOut = delegate { };

    [SerializeField]
    private float _timeRemaining = 30f;
    [SerializeField]
    private TMP_Text _counterText;
    [SerializeField]
    private StartPanelController startPanelController;

    private bool _counterStopped = false;

    private void Start()
    {
        DisplayTime(_timeRemaining);
    }

    private void Update()
    {
        if (!startPanelController.HasCounterFinished || _counterStopped)
        {
            return;
        }

        if (_timeRemaining > 0f)
        {
            _timeRemaining -= Time.deltaTime;
            DisplayTime(_timeRemaining);
        }
        else
        {
            Debug.Log("Time has run out!");
            _counterStopped = true;
            DisplayTime(0f);

            OnTimePassedOut?.Invoke();
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        _counterText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}