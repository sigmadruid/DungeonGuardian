using UnityEngine;
using System.Collections;

namespace Base
{
    public class Stage
    {
        public StageEnum Type { get; protected set; }

        public Stage(StageEnum type)
        {
            Type = type;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }


    }
}