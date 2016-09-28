using UnityEngine;

using System;
using System.Collections;

namespace Base
{
    public class Mediator 
    {
        public virtual Notifications[] GetInterestedNotifications()
        {
            return null;
        }

        public virtual void OnNotify(Enum notification, object param)
        {
        }
    }
}