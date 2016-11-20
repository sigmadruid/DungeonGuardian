using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class FighterInfo : EntityInfo
    {
        public Action CallbackDie;

        public Faction Faction;

        public new FighterData Data
        {
            get { return data as FighterData; }
            set { data = value; }
        }

        public bool IsAlive { get; private set; }

        private float currentHP;
        private Dictionary<string, Buff> bufferDic = new Dictionary<string, Buff>();

        public void Init(int kid)
        {
            Data = FighterData.Get(kid);
            IsAlive = true;
            currentHP = Data.HP;
        }
        public void Dispose()
        {
            bufferDic.Clear();
            bufferDic = null;
            Data = null;
        }

        public float GetValue(BattleAttribute attr)
        {
            float value = 0;
            switch(attr)
            {
                case BattleAttribute.HP:
                    value = Data.HP;
                    break;
                case BattleAttribute.Attack:
                    value = Data.Attack;
                    break;
                case BattleAttribute.Defense:
                    value = Data.Defense;
                    break;
                case BattleAttribute.CritRate:
                    value = Data.CritRate;
                    break;
                case BattleAttribute.CritDamage:
                    value = Data.CritDamage;
                    break;
                case BattleAttribute.Speed:
                    value = Data.Speed;
                    break;
                case BattleAttribute.Range:
                    value = Data.Range;
                    break;
            }
            float buffValue = GetBuffValue(attr);
            value *= (1 + buffValue);
            return value;
        }
        public float GetCurrentHP()
        {
            return currentHP;
        }
        public void ChangeHP(float delta)
        {
            currentHP += delta;
            if (currentHP <= 0)
            {
                IsAlive = false;
                CallbackDie();
            }
        }

        public void AddBuff(int kid)
        {
            
        }
        public void RemoveBuff(int kid)
        {
        }

        private float GetBuffValue(BattleAttribute attr)
        {
            var enumerator = bufferDic.GetEnumerator();
            float result = 0f;
            while(enumerator.MoveNext())
            {
                Buff buff = enumerator.Current.Value;
                if(buff.Valid)
                {
                    float buffVal = buff.GetValue(attr);
                    result += buffVal;
                }
            }
            return result;
        }
    }
}

