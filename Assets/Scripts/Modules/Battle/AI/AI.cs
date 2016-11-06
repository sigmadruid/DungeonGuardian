using UnityEngine;

using System;

using Base;

namespace Logic
{
    public class AI
    {
        public string Uid;

        public AIData Data;

        public Fighter ControlledFighter;

        private FighterManager fighterManager;

        public void Init()
        {
            fighterManager = FighterManager.Instance;
        }
        public void Update()
        {
            Fighter enemy = fighterManager.GetNearestEnemy(ControlledFighter);
            if (Data.FleeRadius < 0)
            {
                if (AIUtils.NearBy(enemy.WorldPosition, ControlledFighter.WorldPosition, Data.SeekRadius))
                {
                    Attack(enemy);
                }
            }
            else
            {
                if (AIUtils.NearBy(enemy.WorldPosition, ControlledFighter.WorldPosition, Data.FleeRadius))
                {
                    Flee(enemy);
                }
                else
                {
                    Attack(enemy);
                }
            }
        }
        public void Dispose()
        {
            ControlledFighter = null;
        }

        private void Attack(Fighter enemy)
        {
            if (AIUtils.FarFrom(enemy.WorldPosition, ControlledFighter.WorldPosition, ControlledFighter.Data.Range))
            {
                ControlledFighter.Move(enemy.WorldPosition);
            }
            else
            {
                ControlledFighter.Move(Vector3.zero);
                ControlledFighter.CastSkill(0);
            }
        }
        private void Flee(Fighter enemy)
        {
            Vector3 fleeVec = (ControlledFighter.WorldPosition - enemy.WorldPosition).normalized * Data.FleeRadius;
            ControlledFighter.Move(ControlledFighter.WorldPosition + fleeVec);
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

