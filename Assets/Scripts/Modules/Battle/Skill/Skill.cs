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
        }
        private void Execute()
        {
            var enumerator = targetList.GetEnumerator(); 
            while (enumerator.MoveNext())
            {
                Fighter target = enumerator.Current;
                if (!target.Info.IsAlive)
                    continue;
                //TODO: Move these calculation to a battle proxy
                float baseValue = caster.Info.GetValue(BattleAttribute.Attack);
                float finalValue = baseValue * Data.ValueRatio;
                float signal = Data.ToEnemy ? -1 : 1;
                target.Info.ChangeHP(finalValue * signal);
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
            skill.Data = null;
        }
    }
}

