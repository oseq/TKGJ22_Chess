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

    public float timeRemaining = 30;
    public TMP_Text counterText;
    public bool timerIsRunning = false;
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

    private void TryToRestart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        counterText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}