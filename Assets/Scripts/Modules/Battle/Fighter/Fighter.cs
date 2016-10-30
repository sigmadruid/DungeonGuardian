using UnityEngine;

using System;
using System.Collections.Generic;

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

        protected Skill currentSkill;
        public List<string> SkillUidList = new List<string>();

        public void OnUpdate()
        {
            if (currentSkill == null)
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
            else
            {
                if (currentSkill != null && currentSkill.HasFinished)
                {
                    currentSkill = null;
                }
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

        public void CastSkill(int index)
        {
            if (currentSkill == null)
            {
                string uid = SkillUidList[index];
                currentSkill = SkillManager.Instance.GetSkill(uid);
                currentSkill.Start();

                Script.DoSkill(index);
                Script.SetDestination(Vector3.zero);
            }
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

