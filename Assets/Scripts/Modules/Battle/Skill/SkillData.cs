using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class SkillData : BaseData
    {
        #region Properties

        public int Kid;

        public bool ToEnemy;

        public float ValueRatio;
        public float EffectTime;

        public float CD;

        public float Duration;

        public float Range;

        public int BulletKid;

        public float ScopeRadius;

        public bool CanMove;

        #endregion

        #region Data Controlling Methods

        private static Dictionary<int, SkillData> kvDic = new Dictionary<int, SkillData>();

        public static void Init()
        {
            kvDic.Clear();

            CSVParser.Init("Skill.csv");
            while(!CSVParser.IsEndOfRow())
            {
                SkillData data = new SkillData();
                data.Kid = CSVParser.ReadInt();
                data.ToEnemy = CSVParser.ReadBool();
                data.ValueRatio = CSVParser.ReadFloat();
                data.EffectTime = CSVParser.ReadFloat();
                data.CD = CSVParser.ReadFloat();
                data.Duration = CSVParser.ReadFloat();
                data.Range = CSVParser.ReadFloat();
                data.BulletKid = CSVParser.ReadInt();
                data.ScopeRadius = CSVParser.ReadFloat();
                data.CanMove = CSVParser.ReadBool();
                kvDic[data.Kid] = data;
                CSVParser.NextLine();
            }
        }

        public static SkillData Get(int kid)
        {
            if (!kvDic.ContainsKey(kid))
            {
                BaseLogger.LogError("Skill Data doesn't contain key: " + kid.ToString());
            }
            return kvDic[kid];
        }

        #endregion
    }
}

