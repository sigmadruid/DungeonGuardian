using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
	public enum AnimatorPriorityEnum
	{
		Idle = 1,
		Run,
		Attack,
		Hit,
		Die,
	}

	public enum AnimatorParamKey
	{
		AreaType = 1,
		Range,
		Angle,
		Width,
	}

    public class AnimatorData : BaseData
	{
		public int Kid;

		public string Name;
		public int NameHash;

		public bool IsLoop;

		public float EventTime;

        public Dictionary<AnimatorParamKey, string> ParamDic = new Dictionary<AnimatorParamKey, string>();
	
        public static void Init()
        {
            BaseLogger.LogError("Animator Data Init");
        }
    }
}

