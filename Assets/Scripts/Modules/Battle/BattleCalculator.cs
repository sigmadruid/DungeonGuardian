using System;

using Base;

namespace Logic
{
    public static class BattleCalculator
    {
        public static void CommonSkill(Skill skill, FighterInfo caster, FighterInfo target)
        {
            float attack = caster.GetValue(BattleAttribute.Attack) * skill.Data.ValueRatio;
            float damage = 0f;
            if(skill.Data.ToEnemy)
            {
                float defense = target.GetValue(BattleAttribute.Defense);
                damage = -(attack - defense);
            }
            else
            {
                damage = attack;
            }
            target.ChangeHP(damage);
//            BaseLogger.LogError(target.Format() + " time:" + UnityEngine.Time.time.ToString());
        }
    }
}

