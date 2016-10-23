using System;
using System.Collections.Generic;

namespace Base
{
	public enum AnimatorParamType
	{
        None,
		HitArea,
	}

    public class AnimatorData : BaseData
	{
        #region Properties

        public int Kid;

        public int CharacterKid;

        public string Name;
        public int NameHash;

        public int Priority;

        public float EventTime;

        public AnimatorParamType ParamType;

        public List<string> ParamList;

        #endregion;

        #region Data Controlling Methods

        private static Dictionary<int, AnimatorData> kvDic = new Dictionary<int, AnimatorData>();

        public static void Init()
        {
            kvDic.Clear();

            CSVParser.Init("Animator.csv");
            while(!CSVParser.IsEndOfRow())
            {
                AnimatorData data = new AnimatorData();
                data.Kid = CSVParser.ReadInt();
                data.CharacterKid = CSVParser.ReadInt();
                data.Name = CSVParser.ReadString();
                data.Priority = CSVParser.ReadInt();
                data.EventTime = CSVParser.ReadFloat();
                data.ParamType = CSVParser.ReadEnum<AnimatorParamType>();
                data.ParamList = CSVParser.ReadStringList();
                kvDic[data.Kid] = data;
                CSVParser.NextLine();
            }
            BaseLogger.LogError("Animator Data Init");
        }

        public static Dictionary<string, AnimatorData> GetSet(int characterKid)
        {
            Dictionary<string, AnimatorData> dataDic = new Dictionary<string, AnimatorData>();
            foreach(AnimatorData data in kvDic.Values)
            {
                if (data.CharacterKid == characterKid)
                    dataDic.Add(data.Name, data);
            }
            return dataDic;
        }

        public static AnimatorData Get(int kid)
        {
            if (!kvDic.ContainsKey(kid))
            {
                BaseLogger.LogError("Animator Data doesn't contain key: " + kid.ToString());
            }
            return kvDic[kid];
        }

        #endregion
    }
}

