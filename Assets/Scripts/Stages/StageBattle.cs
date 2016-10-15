using UnityEngine;

using System;
using System.Collections;

using Base;

namespace Logic
{
    public class StageBattle : Stage
    {
        public StageBattle() : base(StageEnum.Battle) {}

        public override void Enter()
        {
            base.Enter();

            InitCoroutine();

            BaseLogger.Log("battle enter");
        }

        public override void Exit()
        {
            base.Exit();
            BaseLogger.Log("battle exit");
        }

        private void InitCoroutine()
        {
            GameObject mapPrefab = Resources.Load<GameObject>("Map/MapTest");
            GameObject.Instantiate(mapPrefab);

            AstarPath.active.Scan();

            DungeonGame.instance.Router.Notify(Notifications.MONSTER_INIT);
        }
    }
}

