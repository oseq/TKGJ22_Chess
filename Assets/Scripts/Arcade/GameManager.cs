using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private FallDetector _fallDetector;

    public float timeRemaining = 30;
    public TMP_Text counterText;
    public bool timerIsRunning = false;

    private void Awake()
    {
        _fallDetector.OnPlayerFelt += OnPlayerFelt;
    }

    private void OnDestroy()
    {
        _fallDetector.OnPlayerFelt -= OnPlayerFelt;
    }

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    private void Update()
    {
        TryToRestart();
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            Debug.Log("Time has run out!");
            timerIsRunning = false;
        }
    }

    private void InitializeGameplay()
    {

    }

    private void TryToRestart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        counterText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnPlayerFelt(PlayerController playerFelt)
    {
        RestartScene();
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}