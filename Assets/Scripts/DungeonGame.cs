using UnityEngine;

using System;

using Base;

public class DungeonGame : Game
{
    private static DungeonGame instance;
    public static DungeonGame Instance
    {
        get
        {
            if (instance == null) instance = new DungeonGame();
            return instance;
        }
    }

    public InputManager InputManager;

    public override void Start()
    {
        base.Start();

        InputManager.Init();
    }

    public override void End()
    {
        base.End();
    }
}

