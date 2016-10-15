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
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void InitCoroutine()
        {
            GameObject mapPrefab = Resources.Load<GameObject>("Map/MapTest");
            GameObject.Instantiate(mapPrefab);

            AstarPath.active.Scan();

            Router.Instance.Notify(Notifications.MONSTER_INIT);
        }
    }
}

