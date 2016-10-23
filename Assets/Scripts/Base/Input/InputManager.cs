using UnityEngine;

using System;

namespace Base
{
    public class InputManager : BaseManager
    {
        public Vector3 MousePosition { get; private set; }

        public override void Init()
        {
            base.Init();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override void Update()
        {
            if (!HasInitialized) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 9999f, Layer.Map))
                {
                    MousePosition = hit.point;
                    Router.Instance.Notify(Notifications.INPUT_MOUSE_LEFT_CLICK, MousePosition);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Router.Instance.Notify(Notifications.INPUT_MOUSE_RIGHT_CLICK, MousePosition);
            }
        }
    }
}

