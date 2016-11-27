using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class AIManager : BaseManager
    {
        public static AIManager Instance { get; private set; }
        void Awake()
        {
            Instance = this;
        }
        void OnDestory()
        {
            Instance = null;
        }

        public const float AI_UPDATE_INTERVAL = 0.1f;
        private float timer = 0f;

        private Dictionary<string, AI> aiDic = new Dictionary<string, AI>();

        public override void OnInit()
        {
            base.OnInit();
        }
        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);

            timer += deltaTime;
            if(timer < AI_UPDATE_INTERVAL)
                return;
            timer -= AI_UPDATE_INTERVAL;

            foreach(AI ai in aiDic.Values)
            {
                ai.Update();
            }
        }
        public override void OnDispose()
        {
            base.OnDispose();
            foreach(AI ai in aiDic.Values)
            {
                ai.Dispose();
            }
            aiDic.Clear();
        }

        public void AddAI(Fighter fighter)
        {
            AI ai = AI.Create(fighter);
            aiDic.Add(ai.Uid, ai);
        }
        public void RemoveAI(string uid)
        {
            if (!aiDic.ContainsKey(uid))
            {
                BaseLogger.LogError("ai manager doesn't contains skill uid: " + uid);
            }
            AI ai = aiDic[uid];
            ai.Dispose();
            aiDic.Remove(uid);
        }
    }
}

