using System;
using System.Reflection;

namespace Base
{
    public class DataManager : BaseManager
    {
        public static DataManager Instance { get; private set; }
        void Awake()
        {
            Instance = this;
        }
        void OnDestory()
        {
            Instance = null;
        }

        public override void OnInit()
        {
            base.OnInit();

            Type baseDataType = typeof(BaseData);
            Type[] types = Assembly.GetAssembly(baseDataType).GetTypes();
            foreach (Type type in types)
            {
                if (type.BaseType == baseDataType || type.BaseType.BaseType == baseDataType)
                {
                    MethodInfo method = type.GetMethod("Init");
                    method.Invoke(null, null);
                }
            }
        }
    }
}

