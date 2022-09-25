using System;
using UnityEngine;

public class StrategyManager : RootManager<StrategyManager>
{
    protected override void RegisterManager()
    {
        CrossSceneManager.Instance.StrategyManager = this;
    }
}