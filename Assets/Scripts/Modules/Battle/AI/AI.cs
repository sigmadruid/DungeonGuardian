using UnityEngine;

using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class AI
    {
        public string Uid;

        public AIData Data;

        public Fighter ControlledFighter;

        private FighterManager fighterManager;

        private LinkedList<Fighter> targetList = new LinkedList<Fighter>();

        public void Init()
        {
            fighterManager = FighterManager.Instance;
        }
        public void Update()
        {
            if (!ControlledFighter.Info.IsAlive)
            {
                return;
            }
            //TODO: Select an available skill, and then select enemies according to the skill's condition.
            Fighter enemy = fighterManager.GetNearestEnemy(ControlledFighter);
            targetList.Clear();
            targetList.AddLast(enemy);

            if (!IsValid(enemy))
            {
                Idle();
                return;
            }

            if (Data.FleeRadius < 0)
            {
                if (AIUtils.NearBy(enemy.WorldPosition, ControlledFighter.WorldPosition, Data.SeekRadius))
                {
                    Attack();
                }
                else
                {
                    Idle();
                }
            }
            else
            {
                if (AIUtils.NearBy(enemy.WorldPosition, ControlledFighter.WorldPosition, Data.FleeRadius))
                {
                    Flee(enemy);
                }
                else if (AIUtils.NearBy(enemy.WorldPosition, ControlledFighter.WorldPosition, Data.SeekRadius))
                {
                    Attack();
                }
                else
                {
                    Idle();
                }
            }
        }
        public void Dispose()
        {
            ControlledFighter = null;
        }

        private void Idle()
        {
            ControlledFighter.Move(Vector3.zero);
        }
        private void Attack()
        {
            Fighter enemy = targetList.First.Value;
            if (AIUtils.FarFrom(enemy.WorldPosition, ControlledFighter.WorldPosition, ControlledFighter.Data.Range))
            {
                ControlledFighter.Move(enemy.WorldPosition);
            }
            else
            {
                ControlledFighter.Move(Vector3.zero);
                ControlledFighter.CastSkill(0, targetList);
            }
        }
        private void Flee(Fighter enemy)
        {
            Vector3 fleeVec = (ControlledFighter.WorldPosition - enemy.WorldPosition).normalized * Data.FleeRadius;
            ControlledFighter.Move(ControlledFighter.WorldPosition + fleeVec);
        }

        private bool IsValid(Fighter enemy)
        {
            return enemy != null && enemy.Info.IsAlive;
        }

        public static AI Create(Fighter fighter)
        {
            AI ai = new AI();
            ai.Uid = fighter.Uid;
            ai.Data = AIData.Get(fighter.Data.AIKid);
            ai.ControlledFighter = fighter;
            ai.Init();
            return ai;
        }
        public static void Recycle(AI ai)
        {
            ai.Dispose();
            ai.ControlledFighter = null;
            ai.Data = null;
        }
    }
}

