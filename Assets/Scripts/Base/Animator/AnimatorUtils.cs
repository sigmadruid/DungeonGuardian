using UnityEngine;

using System;

namespace Base
{
    public static class AnimatorUtils
    {
        public static readonly int TRIGGER_IDLE = Animator.StringToHash("idle");
        public static readonly int TRIGGER_RUN = Animator.StringToHash("run");
        public static readonly int TRIGGER_ATTACK = Animator.StringToHash("attack");
        public static readonly int TRIGGER_DIE = Animator.StringToHash("die");
    }
}

