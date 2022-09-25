using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossSceneManager : MonoBehaviourSingleton<CrossSceneManager>
{
    public ArcadeManager ArcadeManager { get; set; }
    public StrategyManager StrategyManager { get; set; }

    private Player _storedAttacker;
    private Player _storedDefender;
    private Action<Player> _storedFightCallback;

    private void Awake()
    {
        if (CrossSceneManager.Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadStrategyScene();
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            LoadArcadeScene();
        }
    }

    public void RequestFight(Player attacker, Player defender, Action<Player> fightCallback)
    {
        _storedAttacker = attacker;
        _storedDefender = defender;
        _storedFightCallback = fightCallback;
        LoadArcadeScene();
    }

    public void FightFinished(bool hasAttackerWon)
    {
        LoadStrategyScene();
        _storedFightCallback?.Invoke(hasAttackerWon ? _storedAttacker : _storedDefender);
    }

    public void LoadArcadeScene()
    {
        if (ArcadeManager == null)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            StrategyManager?.HideRoot();
        }
        else
        {
            ArcadeManager.ShowRoot();
            StrategyManager?.HideRoot();
        }
    }

    public void LoadStrategyScene()
    {
        if (StrategyManager == null)
        {
            ArcadeManager?.HideRoot();
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        else
        {
            ArcadeManager?.HideRoot();
            SceneManager.UnloadSceneAsync(1);
            StrategyManager.ShowRoot();
        }
        ArcadeManager = null;
    }
}
