using System;

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

        public void Recycle()
        {
        }

        public void Start()
        {
            timer = 0;
            HasFinished = false;
            HasExecuted = false;
        }

        private void Execute()
        {
            BaseLogger.LogError("skill executed!");
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
            skill.Recycle();
            skill.Data = null;
        }
    }
}

