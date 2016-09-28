using UnityEngine;

using System;
using System.Collections;

public class Mediator 
{
    private Router router;

    public virtual Notifications[] GetInterestedNotifications()
    {
        return null;
    }

    public virtual void OnNotify(Enum notification, object param)
    {
    }
}
