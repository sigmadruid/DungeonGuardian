using System;
using System.Reflection;

namespace Base
{
    public class DataManager : BaseManager
    {
        public static new DataManager Instance { get { return instance as DataManager; } }

        public override void OnInit()
        {
            base.OnInit();

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

