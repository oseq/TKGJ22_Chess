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
    [SerializeField]
    private PlayerController offensivePlayer;
    [SerializeField]
    private SpecialActionController offensivePlayerAction;
    [SerializeField]
    private PlayerController deffensivePlayer;
    [SerializeField]
    private SpecialActionController deffensivePlayerAction;

    private bool isPlayingFinishAnimation = false;

    private void Awake()
    {
        _fallDetector.OnPlayerFelt += OnPlayerFelt;
        _timeCounter.OnTimePassedOut += OnTimePassedOut;
        offensivePlayer.SetPieceType(CrossSceneDataTransfer.OffensivePlayer);
        deffensivePlayer.SetPieceType(CrossSceneDataTransfer.DeffensivePlayer);
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

    public void InitializeGameplay()
    {
        var playerType1 = CrossSceneDataTransfer.OffensivePlayerColor == PieceColor.White ? CrossSceneDataTransfer.OffensivePlayer : CrossSceneDataTransfer.DeffensivePlayer;
        var playerType2 = CrossSceneDataTransfer.OffensivePlayerColor != PieceColor.White ? CrossSceneDataTransfer.OffensivePlayer : CrossSceneDataTransfer.DeffensivePlayer;
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
        FinishAnimation(CrossSceneDataTransfer.DeffensivePlayerColor == PieceColor.White ? 1 : 2);
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

        sequence.SetUpdate(true);
        sequence.AppendInterval(1f);
        sequence.AppendCallback(() => _summaryPanel.ShowText(playerWhoWon.ToString()));
        sequence.AppendInterval(2f);
        sequence.AppendCallback(() => RequestFinish(playerWhoWon));
    }

    private void RequestFinish(int winnerId)
    {
        if (CrossSceneManager.Instance.StrategyManager != null)
        {
            CrossSceneManager.Instance.FightFinished((CrossSceneDataTransfer.OffensivePlayerColor == PieceColor.White && winnerId == 1) || (CrossSceneDataTransfer.OffensivePlayerColor == PieceColor.Black && winnerId == 2));
        }
        else
        {
            //if didn't come from the strategy, just restart the scene
            RestartScene();
        }
    }
}