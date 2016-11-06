using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class FighterData : EntityData
    {
        #region Properties

        public string Name;
        public string Prefab;
        public string Icon;

        public List<int> SkillList;
        public int HP;
        public int Attack;
        public int Defense;
        public int Speed;

        #endregion

        public override string GetResPath()
        {
            if (resPath == null)
                resPath = "Fighters/" + Prefab;
            return resPath;
        }

        #region Data Controlling Methods

        private static Dictionary<int, FighterData> kvDic = new Dictionary<int, FighterData>();

        public new static void Init()
        {
            kvDic.Clear();

            CSVParser.Init("Fighter.csv");
            while(!CSVParser.IsEndOfRow())
            {
                FighterData data = new FighterData();
                data.Kid = CSVParser.ReadInt();
                data.Name = CSVParser.ReadString();
                data.Prefab = CSVParser.ReadString();
                data.Icon = CSVParser.ReadString();
                data.SkillList = CSVParser.ReadIntList();
                data.HP = CSVParser.ReadInt();
                data.Attack = CSVParser.ReadInt();
                data.Defense = CSVParser.ReadInt();
                data.Speed = CSVParser.ReadInt();
                kvDic[data.Kid] = data;
                CSVParser.NextLine();
            }
            BaseLogger.LogError("Fighter Data Init");
        }

        public new static FighterData Get(int kid)
        {
            if (!kvDic.ContainsKey(kid))
            {
                BaseLogger.LogError("Monster Data doesn't contain key: " + kid.ToString());
            }
            return kvDic[kid];
        }

        #endregion
    }
}

