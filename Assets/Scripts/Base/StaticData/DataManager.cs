using System;
using System.Reflection;

namespace Base
{
    public class DataManager : BaseManager
    {
        public override void Init()
        {
            base.Init();

            Type baseDataType = typeof(BaseData);
            Type[] types = Assembly.GetAssembly(baseDataType).GetTypes();
            foreach (Type type in types)
            {
                if (type.BaseType == baseDataType)
                {
                    MethodInfo method = type.GetMethod("Init");
                    method.Invoke(null, null);
                }
            }
        }
    }
}

