using System;

namespace Base
{
    public class EntityInfo
    {
        protected EntityData data;
        public EntityData Data
        {
            get { return data; }
            protected set { data = value; }
        }
    }
}

