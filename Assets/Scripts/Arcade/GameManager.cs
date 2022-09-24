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
    [SerializeField]
    private TimeCounter _timeCounter;

    private void Awake()
    {
        _fallDetector.OnPlayerFelt += OnPlayerFelt;
        _timeCounter.OnTimePassedOut += OnTimePassedOut;
    }

    private void OnDestroy()
    {
        _fallDetector.OnPlayerFelt -= OnPlayerFelt;
        _timeCounter.OnTimePassedOut -= OnTimePassedOut;
    }

    private void Update()
    {
        TryToRestart();
        
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

    private void OnPlayerFelt(PlayerController playerFelt)
    {
        RestartScene();
    }

    private void OnTimePassedOut()
    {
        throw new NotImplementedException();
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}