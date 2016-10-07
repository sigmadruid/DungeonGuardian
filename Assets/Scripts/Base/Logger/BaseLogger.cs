using UnityEngine;

using System;

namespace Base
{
    public static class BaseLogger
    {
        public static void Log(string log, params object[] args)
        {
            Debug.LogFormat(log, args);
        }

        public static void LogWarning(string log, params object[] args)
        {
            Debug.LogWarningFormat(log, args);
        }

        public static void LogError(string log, params object[] args)
        {
            Debug.LogErrorFormat(log, args);
        }
    }
}

