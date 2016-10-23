using UnityEngine;

using System;

namespace Base
{
    public static class AnimatorUtils
    {
        public const string STATE_IDLE = "Base.idle";
        public const string STATE_RUN = "Base.run";
        public const string STATE_ATTACK01 = "Base.attack01";
        public const string STATE_DIE = "Base.die";

        public const string PARAM_IS_MOVING = "isMoving";
        public const string PARAM_ATTACK = "attack";
        public const string PARAM_DIE = "die";

        public static string GetState(string trigger)
        {
            return "Base." + trigger;
        }
    }
}

