using System;
using UnityEngine;

public class ArcadeManager : RootManager<ArcadeManager>
{
    protected override void RegisterManager()
    {
        CrossSceneManager.Instance.ArcadeManager = this;
    }
}