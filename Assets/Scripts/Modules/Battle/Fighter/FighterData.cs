using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class FighterData : EntityData
    {
        #region Properties

        public Faction Faction;

        public string Name;
        public string Prefab;
        public string Icon;

        public List<int> SkillList;
        public int HP;
        public int Attack;
        public int Defense;
        public float Speed;
        public float Range;

        public int AIKid;

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
                data.Faction = CSVParser.ReadEnum<Faction>();
                data.Name = CSVParser.ReadString();
                data.Prefab = CSVParser.ReadString();
                data.Icon = CSVParser.ReadString();
                data.SkillList = CSVParser.ReadIntList();
                data.HP = CSVParser.ReadInt();
                data.Attack = CSVParser.ReadInt();
                data.Defense = CSVParser.ReadInt();
                data.Speed = CSVParser.ReadFloat();
                data.Range = CSVParser.ReadFloat();
                data.AIKid = CSVParser.ReadInt();
                kvDic[data.Kid] = data;
                CSVParser.NextLine();
            }
        }

        public new static FighterData Get(int kid)
        {
            if (!kvDic.ContainsKey(kid))
            {
                BaseLogger.LogError("Fighter Data doesn't contain key: " + kid.ToString());
            }
            return kvDic[kid];
        }

        #endregion
    }
}

