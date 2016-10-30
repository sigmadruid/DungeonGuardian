using UnityEngine;

using System;

using Base;

namespace Logic
{
    public class Fighter : Entity
    {
        public new FighterScript Script
        {
            get { return script as FighterScript; }
            protected set { script = value; }
        }

        public void OnUpdate()
        {
            if (Script.IsMoving)
            {
                Script.Run();
            }
            else
            {
                Script.Idle();
            }
        }

        public void Move(Vector3 position)
        {
            //For temp use.
            if (!Script.IsAttacking)
                Script.SetDestination(position, OnMoveStart, OnMoveTurn, OnMoveEnd);
        }
        protected virtual void OnMoveStart()
        {
        }
        protected virtual void OnMoveTurn()
        {
        }
        protected virtual void OnMoveEnd()
        {
        }

        public void Attack()
        {
            Script.Attack(OnAttackStart, OnAttackEvent, OnAttackEnd);
            Script.SetDestination(Vector3.zero);
        }
        protected virtual void OnAttackStart()
        {
        }
        protected virtual void OnAttackEvent()
        {
        }
        protected virtual void OnAttackEnd()
        {
        }
    }
}

