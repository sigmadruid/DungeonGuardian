using UnityEngine;

using System;
using System.Collections;

namespace Base
{
    public class BaseManager : MonoBehaviour
    {
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

