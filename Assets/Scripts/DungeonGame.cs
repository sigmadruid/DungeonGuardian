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

        public StageManager StageManager;

        private BaseManager[] managerList;

        public override void OnInit()
        {
            InitManagers();
            InitMediators();
            InitStages();
        }
        public override void OnUpdate()
        {
            for (int i = 0; i < managerList.Length; ++i)
            {
                managerList[i].OnUpdate();
            }

        }
        public override void OnDispose()
        {
            for (int i = 0; i < managerList.Length; ++i)
            {
                managerList[i].Dispose();
            }
        }

        private void InitManagers()
        {
            managerList = new BaseManager[transform.childCount];
            for (int i = 0; i < transform.childCount; ++i)
            {
                BaseManager manager = transform.GetChild(i).GetComponent<BaseManager>();
                manager.Init();
                managerList[i] = manager;
            }
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
