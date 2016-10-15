using UnityEngine;

using System;

using Base;

namespace Logic
{
    public class MonsterMediator : Mediator
    {
        private Monster monster;

        public override Notifications[] GetInterestedNotifications()
        {
            return new Notifications[]
            {
                Notifications.MONSTER_INIT,
                Notifications.INPUT_MOUSE_CLICK,
            };
        }

        public override void OnNotify(Notifications notification, object param)
        {
            switch(notification)
            {
                case Notifications.MONSTER_INIT:
                {
                    Vector3 startPosition = new Vector3(-1.4f, 0, 7.7f);
                    monster = Monster.Create(startPosition);
                    break;
                }
                case Notifications.INPUT_MOUSE_CLICK:
                {
                    Vector3 destPosition = (Vector3)param;
                    monster.Move(destPosition);
                    break;
                }
            }
        }
    }
}

