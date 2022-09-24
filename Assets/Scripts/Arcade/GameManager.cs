using DG.Tweening;
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
    [SerializeField]
    private SummaryPanelController _summaryPanel;

    private bool isPlayingFinishAnimation = false;

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

    private void Start()
    {
        Time.timeScale = 1f;
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
        Debug.Log("Player felt out");
        FinishAnimation(3 - playerFelt.PlayerId);
    }

    private void OnTimePassedOut()
    {
        Debug.Log("Time passed out");
        FinishAnimation(1);
    }

    private void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FinishAnimation(int playerWhoWon)
    {
        if (isPlayingFinishAnimation)
            return;

        isPlayingFinishAnimation = true;

        var sequence = DOTween.Sequence();
        Time.timeScale = .5f;

        //sequence.Append(DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0.25f, 0.5f).SetEase(Ease.InQuad).SetUpdate(true));
        sequence.SetUpdate(true);
        //sequence.AppendInterval(.5f);
        sequence.AppendInterval(1f);
        sequence.AppendCallback(() => _summaryPanel.ShowText(playerWhoWon.ToString()));
        sequence.AppendInterval(2f);
        sequence.AppendCallback(() => RestartScene());
    }
}