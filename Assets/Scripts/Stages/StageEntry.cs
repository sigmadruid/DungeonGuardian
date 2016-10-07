using System;

using Base;

namespace Logic
{
    public class StageEntry : Stage
    {
        public StageEntry() : base(StageEnum.Entry) {}

        public override void Enter()
        {
            base.Enter();
            BaseLogger.Log("entry enter");
        }

        public override void Exit()
        {
            base.Exit();
            BaseLogger.Log("entry exit");
        }
    }
}

