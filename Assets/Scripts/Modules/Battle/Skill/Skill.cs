using UnityEngine;

using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class Skill
    {
        public string Uid;

        public SkillData Data { get; private set; }

        public bool HasFinished { get; private set; }
        public bool HasExecuted { get; private set; }

        private float timer = 0;
        private Fighter caster;
        private LinkedList<Fighter> targetList;

        public void Init()
        {
            HasFinished = true;
            HasExecuted = true;
        }
        public void Update(float deltaTime)
        {
            timer += deltaTime;
            if (!HasExecuted && timer > Data.EffectTime)
            {
                Execute();
                HasExecuted = true;
            }

            if (timer > Data.Duration)
            {
                timer = 0;
                HasFinished = true;
            }
        }
        public void Dispose()
        {
        }

        public void Start(Fighter caster, LinkedList<Fighter> targetList)
        {
            timer = 0;
            this.caster = caster;
            this.targetList = targetList;

            HasFinished = false;
            HasExecuted = false;

            Vector3 targetDirection = targetList.First.Value.WorldPosition - caster.WorldPosition;
            caster.SetRotation(targetDirection);
        }
        private void Execute()
        {
            var enumerator = targetList.GetEnumerator(); 
            while (enumerator.MoveNext())
            {
                Fighter target = enumerator.Current;
                if (!target.Info.IsAlive)
                    continue;
                BattleCalculator.CommonSkill(this, caster.Info, target.Info);
            }
        }

        public static Skill Create(int kid)
        {
            Skill skill = new Skill();
            skill.Uid = Guid.NewGuid().ToString();
            skill.Data = SkillData.Get(kid);
            skill.Init();
            return skill;
        }
        public static void Recycle(Skill skill)
        {
            skill.Dispose();
            skill.targetList.Clear();
            skill.Data = null;
        }
    }
}

