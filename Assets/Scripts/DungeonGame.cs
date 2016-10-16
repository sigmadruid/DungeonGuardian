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
        public DataManager DataManager;
        public InputManager InputManager;
        public StageManager StageManager;

        #endregion

        public override void OnInit()
        {
            InitManagers();
            InitMediators();
            InitStages();
        }
        public override void OnUpdate()
        {
            TaskManager.Update();
        }
        public override void OnDispose()
        {
        }

        private void InitManagers()
        {
            TaskManager.Init();
            DataManager.Init();
            InputManager.Init();
            StageManager.Init();
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
