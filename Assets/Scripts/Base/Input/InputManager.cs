using UnityEngine;

using System;

namespace Base
{
    public class InputManager : BaseManager
    {
        public static InputManager Instance { get; private set; }
        void Awake()
        {
            Instance = this;
        }
        void OnDestory()
        {
            Instance = null;
        }

        public Vector3 MousePosition { get; private set; }

        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnDispose()
        {
            base.OnDispose();
        }

        public override void OnUpdate(float deltaTime)
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

