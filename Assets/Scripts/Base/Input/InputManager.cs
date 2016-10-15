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

            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 9999f, Layer.Map))
                {
                    MousePosition = hit.point;
                    Game.instance.Router.Notify(Notifications.INPUT_MOUSE_CLICK, MousePosition);
                }

            }
        }
    }
}

