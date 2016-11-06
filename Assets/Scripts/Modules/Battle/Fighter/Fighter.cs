using UnityEngine;

using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class Fighter : Entity
    {
        public new FighterData Data
        {
            get { return data as FighterData;}
            set { data = value; }
        }

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

        public static Fighter Create(int kid, Vector3 position)
        {
            Fighter fighter = new Fighter();

            fighter.Uid = Guid.NewGuid().ToString();
            fighter.Data = FighterData.Get(kid);
            Dictionary<string, AnimatorData> animatorDataDic = AnimatorData.GetSet(fighter.Data.Kid);
            for (int i = 0; i < fighter.Data.SkillList.Count; ++i)
            {
                int skillKid = fighter.Data.SkillList[i];
                string uid = SkillManager.Instance.AddSkill(skillKid); 
                fighter.SkillUidList.Add(uid);
            }

            fighter.Script = ResourceManager.Instance.CreateAsset<FighterScript>(fighter.Data.GetResPath());
            fighter.Script.Init(position, 0, animatorDataDic);
            fighter.Script.CallbackUpdate = fighter.OnUpdate;
            fighter.Script.CallbackMoveStart = fighter.OnMoveStart;
            fighter.Script.CallbackMoveEnd = fighter.OnMoveEnd;

            AIManager.Instance.AddAI(fighter);
            FighterManager.Instance.AddFighter(fighter);

            return fighter;
        }
        public static void Dispose(Fighter fighter)
        {
            AIManager.Instance.RemoveAI(fighter.Uid);
            FighterManager.Instance.RemoveFighter(fighter.Uid);

            for (int i = 0; i < fighter.SkillUidList.Count; ++i)
            {
                string uid = fighter.SkillUidList[i];
                SkillManager.Instance.RemoveSkill(uid);
            }
            fighter.SkillUidList.Clear();
            fighter.Data = null;
            fighter.Script.CallbackUpdate = null;
            //            ResourceManager.Instance.RecycleAsset(monster.Script.gameObject);
            fighter.Script = null;
        }
    }
}

