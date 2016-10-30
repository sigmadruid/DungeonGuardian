using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class MonsterData : EntityData
    {
        #region Properties

        public int Kid;

        public string Name;

        public string Prefab;

        public string Icon;

        public List<int> SkillList;

        #endregion

        public override string GetResPath()
        {
            if (resPath == null)
                resPath = "Monsters/" + Prefab;
            return resPath;
        }

        #region Data Controlling Methods

        private static Dictionary<int, MonsterData> kvDic = new Dictionary<int, MonsterData>();

        public new static void Init()
        {
            kvDic.Clear();

            CSVParser.Init("Monster.csv");
            while(!CSVParser.IsEndOfRow())
            {
                MonsterData data = new MonsterData();
                data.Kid = CSVParser.ReadInt();
                data.Name = CSVParser.ReadString();
                data.Prefab = CSVParser.ReadString();
                data.Icon = CSVParser.ReadString();
                data.SkillList = CSVParser.ReadIntList();
                kvDic[data.Kid] = data;
                CSVParser.NextLine();
            }
            BaseLogger.LogError("Monster Data Init");
        }

        public new static MonsterData Get(int kid)
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

