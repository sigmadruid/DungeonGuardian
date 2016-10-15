using UnityEngine;

using System;

using Base;

namespace Logic
{
    [RequireComponent(typeof(MovementScript))]
    public class CharacterScript : EntityScript
    {
        public Action CallbackUpdate;
        public Action CallbackMoveStart;
        public Action CallbackMoveTurn;
        public Action CallbackMoveEnd;

        private MovementScript movementScript;
        private Animator animator;

        void Awake()
        {
            movementScript = GetComponent<MovementScript>();
            movementScript.CallbackMoveStart = OnMoveStart;
            movementScript.CallbackMoveTurn = OnMoveTurn;
            movementScript.CallbackMoveEnd = OnMoveEnd;
            animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            if (CallbackUpdate != null) CallbackUpdate();
        }

        #region Animator

        private int currentTrigger;
        public void Idle()
        {
            ChangeAnimatorState(AnimatorUtils.TRIGGER_IDLE);
        }
        public void Run()
        {
            ChangeAnimatorState(AnimatorUtils.TRIGGER_RUN);
        }
        public void Attack()
        {
            ChangeAnimatorState(AnimatorUtils.TRIGGER_ATTACK);
        }
        public void Die()
        {
            ChangeAnimatorState(AnimatorUtils.TRIGGER_DIE);
        }

        private void ChangeAnimatorState(int trigger)
        {
            if (currentTrigger != trigger)
            {
                currentTrigger = trigger;
                animator.SetTrigger(trigger);
            }
        }

        #endregion

        #region Movement

        public Vector3 Direction { get { return movementScript.Direction; } }

        public void SetDestination(Vector3 position)
        {
            movementScript.SetDestination(position);
        }

        private void OnMoveStart()
        {
            if(CallbackMoveStart != null) CallbackMoveStart();
        }
        private void OnMoveTurn()
        {
            if(CallbackMoveTurn != null) CallbackMoveTurn();
        }
        private void OnMoveEnd()
        {
            if(CallbackMoveEnd != null) CallbackMoveEnd();
        }

        #endregion
    }
}

