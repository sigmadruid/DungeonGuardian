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
                    break;
                }
                case Notifications.INPUT_MOUSE_LEFT_CLICK:
                {
                    Vector3 destPosition = (Vector3)param;
                    fighter.Move(destPosition);
                    break;
                }
                case Notifications.INPUT_MOUSE_RIGHT_CLICK:
                {
                    int skillIndex = 0;
                    fighter.CastSkill(skillIndex);
                    break;
                }
            }
        }
    }
}

