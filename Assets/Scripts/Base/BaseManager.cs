using UnityEngine;

using System;
using System.Collections;

namespace Base
{
    public class BaseManager : MonoBehaviour
    {
        protected static BaseManager instance;
        public static BaseManager Instance { get { return instance; } }

        void Awake()
        {
            instance = this;
        }
        void OnDestroy()
        {
            instance = null;
        }

        protected bool HasInitialized { get; set; }

        public virtual void OnInit()
        {
            HasInitialized = true;
        }

        public virtual void OnDispose()
        {
            HasInitialized = false;
        }

        public virtual void OnUpdate(float deltaTime)
        {
        }

    }
}

