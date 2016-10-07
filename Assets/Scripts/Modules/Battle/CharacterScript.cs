﻿using System;

using Base;

namespace Logic
{
    public class CharacterScript : EntityScript
    {
        private MovementScript movementScript;

        private InputManager inputManager;

        void Awake()
        {
            inputManager = DungeonGame.Instance.InputManager;
            movementScript = GetComponent<MovementScript>();
        }

        void Update()
        {
            movementScript.SetDestination(inputManager.MousePosition);
        }
    }
}

