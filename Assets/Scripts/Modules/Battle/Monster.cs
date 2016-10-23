﻿using UnityEngine;

using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    public class Monster : Character
    {
        public static Monster Create(int kid, Vector3 position)
        {
            Monster monster = new Monster();

            MonsterData monsterData = MonsterData.Get(kid);
            Dictionary<string, AnimatorData> animatorDataDic = AnimatorData.GetSet(monsterData.Kid);

            GameObject characterPrefab = Resources.Load<GameObject>("Monsters/TestMonster");
            monster.Script = GameObject.Instantiate(characterPrefab).GetComponent<CharacterScript>();
            monster.Script.Init(position, 0, animatorDataDic);
            monster.Script.CallbackUpdate = monster.OnUpdate;
            monster.Script.CallbackMoveStart = monster.OnMoveStart;
            monster.Script.CallbackMoveEnd = monster.OnMoveEnd;
            return monster;
        }
        public static void Dispose(Monster monster)
        {
            monster.Script.CallbackUpdate = null;
            monster.Script = null;
        }
    }
}
