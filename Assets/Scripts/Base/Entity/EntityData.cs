using System;

namespace Base
{
    public class EntityData : BaseData
    {
		public int Kid;

		protected string resPath;
        public virtual string GetResPath()
        {
			return resPath;
        }

        public static void Init()
        {
        }
        public new static EntityData Get(int kid)
        {
            return null;
        }
    }
}

