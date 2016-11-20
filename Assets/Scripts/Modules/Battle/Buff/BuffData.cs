using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class BuffData : BaseData
    {
        #region Properties

        public int Kid;

        public float Duration;

        public float HP;
        public float Attack;
        public float Defense;
        public float CritiRate;
        public float CritiDamage;
        public float Speed;
        public float Range;

        #endregion

        #region Data Controlling Methods

        private static Dictionary<int, BuffData> kvDic = new Dictionary<int, BuffData>();

        public static void Init()
        {
            kvDic.Clear();

            CSVParser.Init("Buff.csv");
            while(!CSVParser.IsEndOfRow())
            {
                BuffData data = new BuffData();
                data.Kid = CSVParser.ReadInt();
                data.Duration = CSVParser.ReadFloat();
                data.HP = CSVParser.ReadFloat();
                data.Attack = CSVParser.ReadFloat();
                data.Defense = CSVParser.ReadFloat();
                data.CritiRate = CSVParser.ReadFloat();
                data.CritiDamage = CSVParser.ReadFloat();
                data.Speed = CSVParser.ReadFloat();
                data.Range = CSVParser.ReadFloat();
                kvDic[data.Kid] = data;
                CSVParser.NextLine();
            }
        }

        public static BuffData Get(int kid)
        {
            if (!kvDic.ContainsKey(kid))
            {
                BaseLogger.LogError("Buff Data doesn't contain key: " + kid.ToString());
            }
            return kvDic[kid];
        }

        #endregion
    }
}
