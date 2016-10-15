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
            get { return instance as DungeonGame; }
        }

        #region Manager List

        public TaskManager TaskManager;
        public InputManager InputManager;
        public StageManager StageManager;

        #endregion

        public override void OnInit()
        {
            InitManagers();
            InitMediators();
            InitStages();
        }
        public override void OnDispose()
        {
            DisposeManagers();
        }
        public override void OnUpdate()
        {
            TaskManager.Update();
        }

        private void InitManagers()
        {
            TaskManager.Init();
            InputManager.Init();
            StageManager.Init();
        }
        private void DisposeManagers()
        {
            TaskManager.Dispose();
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
            Router.Instance.Add(new MonsterMediator());
        }
    }
}
