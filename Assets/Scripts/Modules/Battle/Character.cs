using UnityEngine;

using System;

using Base;

namespace Logic
{
    public class Character : Entity
    {
        public new CharacterScript Script
        {
            get { return script as CharacterScript; }
            protected set { script = value; }
        }

        public void OnUpdate()
        {
            if (Script.Direction != Vector3.zero)
                SetRotation(Script.Direction);
        }
        public void OnMoveStart()
        {
            Script.Run();
        }
        public void OnMoveEnd()
        {
            Script.Idle();
        }

        public void Move(Vector3 position)
        {
            Script.SetDestination(position);
        }
    }
}

