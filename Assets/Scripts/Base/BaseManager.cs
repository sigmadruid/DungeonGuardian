using UnityEngine;

using System;
using System.Collections;

namespace Base
{
    public class BaseManager : MonoBehaviour
    {
        protected bool HasInitialized { get; set; }

        public virtual void Init()
        {
            HasInitialized = true;
        }

        public virtual void Dispose()
        {
            HasInitialized = false;
        }

        public virtual void Update()
        {
        }

    }
}

