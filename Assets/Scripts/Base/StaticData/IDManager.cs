using System;

namespace StaticData
{
	public enum IDType
	{
		Monster = 1,
        Animator = 2,
        Skill = 3,
        AI = 4,
        Buff = 5,
	}

    public class IDManager
    {
		public const int PRE_MULTIPLIER = 10000;

        private static IDManager instance;
        public static IDManager Instance
        {
            get
            {
                if (instance == null) instance = new IDManager();
                return instance;
            }
        }

		public int GetID(IDType type, int subID)
		{
			return ((int)type) * PRE_MULTIPLIER + subID;
		}

		public IDType GetIDType(int id)
		{
			IDType type = (IDType)(id / PRE_MULTIPLIER);
			return type;
		}
    }
}

