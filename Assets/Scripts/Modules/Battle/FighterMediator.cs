using UnityEngine;

using System;

using Base;

namespace Logic
{
    public class FighterMediator : Mediator
    {
        private Fighter fighter;

        public override Notifications[] GetInterestedNotifications()
        {
            return new Notifications[]
            {
                Notifications.FIGHTER_INIT,
                Notifications.INPUT_MOUSE_LEFT_CLICK,
                Notifications.INPUT_MOUSE_RIGHT_CLICK,
            };
        }

        public override void OnNotify(Notifications notification, object param)
        {
            switch(notification)
            {
                case Notifications.FIGHTER_INIT:
                {
                    Vector3 startPosition = new Vector3(-1.4f, 0.5f, 7.7f);
                    fighter = Fighter.Create(100001, startPosition);
                    Vector3 startPosition1 = new Vector3(7.2f, 0.5f, -9.13f);
                    fighter = Fighter.Create(100000, startPosition1);
                    Vector3 startPosition2 = new Vector3(7.98f, 0.5f, -7.75f);
                    fighter = Fighter.Create(100000, startPosition2);
                    Vector3 startPosition3 = new Vector3(6.19f, 0.5f, -6.74f);
                    fighter = Fighter.Create(100000, startPosition3);
                    break;
                }
//                case Notifications.INPUT_MOUSE_LEFT_CLICK:
//                {
//                    Vector3 destPosition = (Vector3)param;
//                    fighter.Move(destPosition);
//                    break;
//                }
//                case Notifications.INPUT_MOUSE_RIGHT_CLICK:
//                {
//                    int skillIndex = 0;
//                    fighter.CastSkill(skillIndex);
//                    break;
//                }
            }
        }
    }
}

