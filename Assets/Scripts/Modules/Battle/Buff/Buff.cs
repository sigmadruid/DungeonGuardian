using System;

namespace Logic
{
    public class Buff
    {
        public BuffData Data { get; private set; }

        private float timer;

        public void Init(int kid)
        {
            Data = FighterData.Get(kid);
            timer = 0;
        }
        public void Dispose()
        {
            Data = null;
        }
        public void Update(float deltaTime)
        {
            if(timer < Data.Duration)
            {
                timer += deltaTime;
            }
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
                    value = Data.CritiRate;
                    break;
                case BattleAttribute.CritDamage:
                    value = Data.CritiDamage;
                    break;
                case BattleAttribute.Speed:
                    value = Data.Speed;
                    break;
                case BattleAttribute.Range:
                    value = Data.Range;
                    break;
            }
            return value;
        }

        public bool Valid
        {
            get { return timer < Data.Duration; }
        }
    }
}

