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

        public float HPAdditiveRatio;
        public float AttackAdditiveRatio;
        public float DefenseAdditiveRatio;
        public float SpeedAdditiveRatio;
        public float RangeAdditiveRatio;

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
                data.HPAdditiveRatio = CSVParser.ReadFloat();
                data.AttackAdditiveRatio = CSVParser.ReadFloat();
                data.DefenseAdditiveRatio = CSVParser.ReadFloat();
                data.SpeedAdditiveRatio = CSVParser.ReadFloat();
                data.RangeAdditiveRatio = CSVParser.ReadFloat();
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
