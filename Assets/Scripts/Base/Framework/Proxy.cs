using UnityEngine;
using System.Collections;

public class Proxy
{
    private Router router;

    public virtual void Init()
    {
    }

    public virtual void Dispose()
    {
    }

    public void Notify(Notifications notifyEnum, object param = null)
    {
        router.Notify(notifyEnum, param);
    }
}
