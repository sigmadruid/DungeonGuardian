using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

using Base;

namespace Logic
{
    public class DungeonGame : Game
    {
        public new static DungeonGame Instance
        {
            get
            {
                return instance as DungeonGame;
            }
        }

        #region Manager List

        public InputManager InputManager;
        public StageManager StageManager;

        #endregion

        public override void OnInit()
        {
            InitManagers();
            InitMediators();
            InitStages();
            Debug.Log("game starts");
        }
        public override void OnDispose()
        {
            DisposeManagers();
            Debug.Log("game ends");
        }
        public override void OnUpdate()
        {
        }
        public override void OnHeartBeat()
        {
        }


        private void InitManagers()
        {
            InputManager.Init();
            StageManager.Init();
        }
        private void DisposeManagers()
        {
            InputManager.Dispose();
            StageManager.Dispose();
        }
        private void InitStages()
        {
            StageManager.AddStage(new StageEntry());
            StageManager.AddStage(new StageBattle());
        }

        private void InitMediators()
        {
            Router.Add(new MonsterMediator());
        }
    }
}
