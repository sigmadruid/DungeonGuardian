using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class AIData : BaseData
    {
        #region Properties

        public int Kid;

        public string Name;

        public Faction Faction;

        public float SeekRadius;

        public float FleeRadius;

        #endregion

        #region Data Controlling Methods

        private static Dictionary<int, AIData> kvDic = new Dictionary<int, AIData>();

        public static void Init()
        {
            kvDic.Clear();
            CSVParser.Init("AI.csv");
            while(!CSVParser.IsEndOfRow())
            {
                AIData data = new AIData();
                data.Kid = CSVParser.ReadInt();
                data.Name = CSVParser.ReadString();
                data.Faction = CSVParser.ReadEnum<Faction>();
                data.SeekRadius = CSVParser.ReadFloat();
                data.FleeRadius = CSVParser.ReadFloat();
                kvDic[data.Kid] = data;
                CSVParser.NextLine();
            }
        }
        public static AIData Get(int kid)
        {
            if (!kvDic.ContainsKey(kid))
            {
                BaseLogger.LogError("AI Data doesn't contain key: " + kid.ToString());
            }
            return kvDic[kid];
        }

        #endregion
    }
}

