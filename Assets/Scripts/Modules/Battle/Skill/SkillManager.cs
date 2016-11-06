using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class SkillManager : BaseManager
    {
        public static SkillManager Instance { get; private set; }
        void Awake()
        {
            Instance = this;
        }
        void OnDestory()
        {
            Instance = null;
        }

        private Dictionary<string, Skill> skillDic = new Dictionary<string, Skill>();

        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach(Skill skill in skillDic.Values)
            {
                if (!skill.HasFinished)
                    skill.Update(deltaTime);
            }
        }

        public override void OnDispose()
        {
            base.OnDispose();
        }

        public Skill GetSkill(string uid)
        {
            if (!skillDic.ContainsKey(uid))
            {
                BaseLogger.LogError("skill manager doesn't contains skill uid: " + uid);
            }
            return skillDic[uid];
        }

        public string AddSkill(int kid)
        {
            Skill skill = Skill.Create(kid);
            skillDic.Add(skill.Uid, skill);
            return skill.Uid;
        }

        public void RemoveSkill(string uid)
        {
            if (!skillDic.ContainsKey(uid))
            {
                BaseLogger.LogError("skill manager doesn't contains skill uid: " + uid);
            }
            skillDic.Remove(uid);
        }
    }
}

