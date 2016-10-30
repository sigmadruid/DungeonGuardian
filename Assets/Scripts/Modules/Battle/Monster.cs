using UnityEngine;

using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class Monster : Fighter
    {
        public new MonsterData Data
        {
            get { return data as MonsterData;}
            set { data = value; }
        }

        public static Monster Create(int kid, Vector3 position)
        {
            Monster monster = new Monster();

            monster.Data = MonsterData.Get(kid);
            Dictionary<string, AnimatorData> animatorDataDic = AnimatorData.GetSet(monster.Data.Kid);
            for (int i = 0; i < monster.Data.SkillList.Count; ++i)
            {
                int skillKid = monster.Data.SkillList[i];
                string uid = SkillManager.Instance.AddSkill(skillKid); 
                monster.SkillUidList.Add(uid);
            }

            monster.Script = ResourceManager.Instance.CreateAsset<FighterScript>(monster.Data.GetResPath());
            monster.Script.Init(position, 0, animatorDataDic);
            monster.Script.CallbackUpdate = monster.OnUpdate;
            monster.Script.CallbackMoveStart = monster.OnMoveStart;
            monster.Script.CallbackMoveEnd = monster.OnMoveEnd;
            return monster;
        }
        public static void Dispose(Monster monster)
        {
            monster.Script.CallbackUpdate = null;
            monster.Script = null;
        }
    }
}

