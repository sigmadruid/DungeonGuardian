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
            ResourceManager.Instance.CreateGameObject("Map/MapTest");

            AstarPath.active.Scan();

            Router.Instance.Notify(Notifications.FIGHTER_INIT);
        }
    }
}

