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
                Notifications.INPUT_MOUSE_LEFT_CLICK,
                Notifications.INPUT_MOUSE_RIGHT_CLICK,
            };
        }

        public override void OnNotify(Notifications notification, object param)
        {
            switch(notification)
            {
                case Notifications.MONSTER_INIT:
                {
                    Vector3 startPosition = new Vector3(-1.4f, 0.5f, 7.7f);
                    monster = Monster.Create(100001, startPosition);
                    break;
                }
                case Notifications.INPUT_MOUSE_LEFT_CLICK:
                {
                    Vector3 destPosition = (Vector3)param;
                    monster.Move(destPosition);
                    break;
                }
                case Notifications.INPUT_MOUSE_RIGHT_CLICK:
                {
                    monster.Attack();
                    break;
                }
            }
        }
    }
}

