using UnityEngine;

using System;

using Base;

namespace Logic
{
    public static class AIUtils
    {
        public static Faction GetReverseFaction(Faction faction)
        {
            return faction == Faction.Guardian ? Faction.Invader : Faction.Guardian;
        }

        public static bool NearBy(Vector3 from, Vector3 to, float distance)
        {
            return MathUtils.XZSqrDistance(from, to) < distance * distance;
        }

        public static bool FarFrom(Vector3 from, Vector3 to, float distance)
        {
            return MathUtils.XZSqrDistance(from, to) > distance * distance;
        }
    }
}

