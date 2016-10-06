using UnityEngine;

using System;

namespace Base
{
    [Serializable]
    public class EntityRecord
    {
		public string Uid;

		public int Kid;

		public Vector3Record WorldPosition;

        public float WorldAngle;
    }
}

