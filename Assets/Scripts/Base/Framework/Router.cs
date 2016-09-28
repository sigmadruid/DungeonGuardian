using System;
using System.Collections.Generic;

public class Router
{
    private Dictionary<int, List<Mediator>> mediatorDic = new Dictionary<int, List<Mediator>>();

    private bool isNotifying;
    private List<Mediator> toAddList = new List<Mediator>();
    private List<Mediator> toRemoveList = new List<Mediator>();

    public void Notify(Notifications notifyEnum, object param)
    {
        int notifyId = (int)notifyEnum;
        if (mediatorDic.ContainsKey(notifyId))
        {
            isNotifying = true;
            List<Mediator> list = mediatorDic[notifyId];
            for (int i = 0; i < list.Count; ++i)
            {
                Mediator mediator = list[i];
                mediator.OnNotify(notifyEnum, param);
            }
            isNotifying = false;

            if (toRemoveList.Count > 0)
            {
                for (int i = 0; i < toRemoveList.Count; ++i)
                {
                    Mediator mediator = toRemoveList[i];
                    DoRemove(mediator);
                }
            }
            toRemoveList.Clear();
            if (toAddList.Count > 0)
            {
                for (int i = 0; i < toAddList.Count; ++i)
                {
                    Mediator mediator = toAddList[i];
                    DoAdd(mediator);
                }
            }
            toAddList.Clear();
        }
    }

    public void Add(Mediator mediator)
    {
        if (isNotifying)
        {
            toAddList.Add(mediator);
        }
        else
        {
            DoAdd(mediator);
        }
    }
    private void DoAdd(Mediator mediator)
    {
        Notifications[] notifyList = mediator.GetInterestedNotifications();
        for (int i = 0; i < notifyList.Length; ++i)
        {
            int notifyID = (int)notifyList[i];
            List<Mediator> mediatorList = null;
            if (!mediatorDic.ContainsKey(notifyID))
            {
                mediatorList = new List<Mediator>();
                mediatorDic[notifyID] = mediatorList;
            }
            else
            {
                mediatorList = mediatorDic[notifyID];
            }
            mediatorList.Add(mediator);
        }
    }

    public void Remove(Mediator mediator)
    {
        if (isNotifying)
        {
            toRemoveList.Add(mediator);
        }
        else
        {
            DoRemove(mediator);
        }
    }
    private void DoRemove(Mediator mediator)
    {
        Notifications[] notifyList = mediator.GetInterestedNotifications();
        for (int i = 0; i < notifyList.Length; ++i)
        {
            int notifyID = (int)notifyList[i];
            if (mediatorDic.ContainsKey(notifyID))
            {
                List<Mediator> mediatorList = mediatorDic[notifyID];
                mediatorList.Remove(mediator);
            }
        }
    }
}