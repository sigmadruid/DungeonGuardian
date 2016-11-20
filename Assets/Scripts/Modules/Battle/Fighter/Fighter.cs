using UnityEngine;

using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class Fighter : Entity
    {
        #region Components

        public new FighterData Data
        {
            get { return data as FighterData;}
            set { data = value; }
        }

        public FighterInfo Info
        {
            get { return info as FighterInfo; }
            set { info = value; }
        }

        public new FighterScript Script
        {
            get { return script as FighterScript; }
            protected set { script = value; }
        }

        #endregion

        protected Skill currentSkill;
        public List<string> SkillUidList = new List<string>();

        public void OnUpdate()
        {
            if (!Info.IsAlive) return;

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

            if (Script.EnableLog)
            {
                Debug.LogError(Format());
            }
        }

        #region States

        public void Move(Vector3 position)
        {
            if (position != Vector3.zero)
                Script.Run();
            else
                Script.Idle();
            Script.SetDestination(position);
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

        public void CastSkill(int index, LinkedList<Fighter> targetList)
        {
            if (currentSkill == null)
            {
                string uid = SkillUidList[index];
                currentSkill = SkillManager.Instance.GetSkill(uid);
                currentSkill.Start(this, targetList);

                Script.DoSkill(index);
                Script.SetDestination(Vector3.zero);
            }
        }

        protected virtual void OnStateStart(string stateName)
        {
        }
        protected virtual void OnStateEffect(string stateName)
        {
        }
        protected virtual void OnStateEnd(string stateName)
        {
            if (stateName == AnimatorConstDef.STATE_DIE)
            {
                Fighter.Dispose(this);
            }
        }

        private void OnDie()
        {
            Script.SetDestination(Vector3.zero);
            Script.Die();
        }

        #endregion

        public string Format()
        {
            return string.Format("{0}, {1}, {2}", Data.Name, WorldPosition, Info.GetCurrentHP()); 
        }

        #region Factroy

        public static Fighter Create(int kid, Vector3 position)
        {
            Fighter fighter = new Fighter();

            fighter.Uid = Guid.NewGuid().ToString();
            fighter.Data = FighterData.Get(kid);
            fighter.Info = new FighterInfo();
            fighter.Info.Init(kid);
            fighter.Info.CallbackDie = fighter.OnDie;

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
            fighter.Script.CallbackMoveTurn = fighter.OnMoveTurn;
            fighter.Script.CallbackMoveEnd = fighter.OnMoveEnd;
            fighter.Script.CallbackStateStart = fighter.OnStateStart;
            fighter.Script.CallbackStateEffect = fighter.OnStateEffect;
            fighter.Script.CallbackStateEnd = fighter.OnStateEnd;

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
            ResourceManager.Instance.DestroyAsset(fighter.Script.gameObject);
//            ResourceManager.Instance.RecycleAsset(monster.Script.gameObject);
            fighter.Script = null;
        }

        #endregion
    }
}

