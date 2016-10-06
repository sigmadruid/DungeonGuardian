using System;

namespace Base
{
    public class EntityData
    {
		public int Kid;

		protected string resPath;
        public virtual string GetResPath()
        {
			return resPath;
        }
    }
}

