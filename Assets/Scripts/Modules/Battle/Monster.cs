using UnityEngine;

using System;

namespace Logic
{
    public class Monster : Character
    {
        public static Monster Create(Vector3 position)
        {
            Monster monster = new Monster();
            GameObject characterPrefab = Resources.Load<GameObject>("Monsters/TestMonster");
            monster.Script = GameObject.Instantiate(characterPrefab).GetComponent<CharacterScript>();
            monster.Script.CallbackUpdate = monster.OnUpdate;
            monster.Script.CallbackMoveStart = monster.OnMoveStart;
            monster.Script.CallbackMoveEnd = monster.OnMoveEnd;
            monster.SetPosition(position);
            return monster;
        }
        public static void Dispose(Monster monster)
        {
            monster.Script.CallbackUpdate = null;
            monster.Script = null;
        }
    }
}

