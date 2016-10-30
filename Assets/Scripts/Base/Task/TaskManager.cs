using UnityEngine;

using System;
using System.Collections.Generic;

namespace Base
{
	public enum TaskEnum
	{
		AIUpdate,
		AISlowUpdate,
		ResourceUpdate,
		InputUpdate,
	}

	public class GameTask
	{
		public TaskEnum Type;
		public bool Active;
		public float Delay;
		public float Timer;
        public Action Callback;
	}

    public class TaskManager : BaseManager
	{
        private Dictionary<TaskEnum, GameTask> taskDic = new Dictionary<TaskEnum, GameTask>();
		private	Dictionary<TaskEnum, GameTask>.Enumerator enumerator;
		
        public override void OnUpdate()
		{
			enumerator = taskDic.GetEnumerator();
			while(enumerator.MoveNext())
			{
				GameTask task = enumerator.Current.Value;
				if (task.Active)
				{
					task.Timer += Time.deltaTime;
					if (task.Delay <= 0f)
					{
						task.Callback();
					}
					else if (task.Timer > task.Delay)
					{
						task.Timer = 0f;
						task.Callback();
					}
				}
			}
		}

        public void AddTask(TaskEnum type, float delay, Action callback)
		{
			if (!taskDic.ContainsKey(type))
			{
				GameTask task = new GameTask();
				task.Type = type;
				task.Active = false;
				task.Delay = delay;
				task.Timer = 0f;
				task.Callback = callback;

				taskDic.Add(task.Type, task);
			}
		}

		public void SetAllActive(bool active)
		{
			enumerator = taskDic.GetEnumerator();
			while(enumerator.MoveNext())
			{
				GameTask task = enumerator.Current.Value;
				task.Active = active;
			}
		}
		public void SetActive(TaskEnum type, bool active)
		{
			if (taskDic.ContainsKey(type))
			{
				GameTask task = taskDic[type];
				task.Active = active;
			}
		}
	}
}

