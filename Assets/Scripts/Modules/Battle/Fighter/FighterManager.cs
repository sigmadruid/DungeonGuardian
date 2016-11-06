using UnityEngine;

using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class FighterManager : BaseManager
    {
        public static FighterManager Instance { get; private set; }
        void Awake()
        {
            Instance = this;
        }
        void OnDestory()
        {
            Instance = null;
        }

        private Dictionary<string, Fighter> fighterDic = new Dictionary<string, Fighter>();

        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
        }

        public override void OnDispose()
        {
            base.OnDispose();
        }

        public Fighter GetNearestEnemy(Fighter fighter)
        {
            float minSqrDistance = 99999f;
            Fighter result = null;
            foreach(Fighter each in fighterDic.Values)
            {
                float sqrDistance = Vector3.SqrMagnitude(each.WorldPosition - fighter.WorldPosition);
                if (sqrDistance < minSqrDistance 
                    && each.Uid != fighter.Uid 
                    && each.Data.Faction != fighter.Data.Faction)
                {
                    minSqrDistance = sqrDistance;
                    result = each;
                }
            }
            return result;
        }

        public void ForEachFighter(Action<Fighter> operation)
        {
            foreach(Fighter fighter in fighterDic.Values)
            {
                operation(fighter);
            }
        }

        public void AddFighter(Fighter fighter)
        {
            fighterDic.Add(fighter.Uid, fighter);
        }

        public void RemoveFighter(string uid)
        {
            if (!fighterDic.ContainsKey(uid))
            {
                BaseLogger.LogError("fighter manager doesn't contains skill uid: " + uid);
            }
            fighterDic.Remove(uid);
        }
    }
}

