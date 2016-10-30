using System;

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
    }
}

