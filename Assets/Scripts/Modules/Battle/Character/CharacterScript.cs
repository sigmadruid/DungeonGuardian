using UnityEngine;

using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    [RequireComponent(typeof(MovementScript))]
    public class CharacterScript : EntityScript
    {
        public bool EnableLog = false;

        public Action CallbackUpdate;
        public Action CallbackMoveStart;
        public Action CallbackMoveTurn;
        public Action CallbackMoveEnd;

        private MovementScript movementScript;
        private Animator animator;
        private Dictionary<string, AnimatorData> animatorDataDic;

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
            if (XZDirection != Vector3.zero)
                transform.localRotation = Quaternion.LookRotation(XZDirection);

            if (CallbackUpdate != null) 
                CallbackUpdate();
        }

        public void Init(Vector3 position, float angle, Dictionary<string, AnimatorData> animatorDataDic)
        {
            transform.position = position;
            transform.localEulerAngles = Vector3.up * angle;
            this.animatorDataDic = animatorDataDic;
        }

        #region Animator

        public void Idle()
        {
            LogTest("idle");
            animator.SetBool(AnimatorUtils.PARAM_IS_MOVING, false);
        }
        public void Run()
        {
            LogTest("run");
            animator.SetBool(AnimatorUtils.PARAM_IS_MOVING, true);
        }
        public void Attack(Action start, Action effect, Action end)
        {
            LogTest("attack");
            animator.SetTrigger(AnimatorUtils.PARAM_ATTACK);
        }
        public void Die()
        {
            LogTest("die");
            animator.SetTrigger(AnimatorUtils.PARAM_DIE);
        }

        public bool IsAttacking;//For temp use.
        public void OnStateStart(string stateName)
        {
            if (!animatorDataDic.ContainsKey(stateName)) 
                LogTest("Doesn't contain state: " + stateName);

            if (stateName == AnimatorUtils.STATE_ATTACK01)
                IsAttacking = true;
        }
        public void OnStateEffect(string stateName)
        {
            if (!animatorDataDic.ContainsKey(stateName)) 
                LogTest("Doesn't contain state: " + stateName);

            BaseLogger.LogError("Attack !");
        }
        public void OnStateEnd(string stateName)
        {
            if (!animatorDataDic.ContainsKey(stateName)) 
                LogTest("Doesn't contain state: " + stateName);

            if (stateName == AnimatorUtils.STATE_ATTACK01)
                IsAttacking = false;
        }

        #endregion

        #region Movement

        public bool IsMoving { get { return movementScript.IsMoving; } }
        public Vector3 Direction { get { return movementScript.Direction; } }
        public Vector3 XZDirection { get { return MathUtils.XZDirection(movementScript.Direction); } }

        public void SetDestination(Vector3 position, Action start = null, Action turn = null, Action end = null)
        {
            movementScript.SetDestination(position);
            CallbackMoveStart = start;
            CallbackMoveTurn = turn;
            CallbackMoveEnd = end;
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

        private void LogTest(string log)
        {
            if (EnableLog) BaseLogger.LogError(log);
        }
    }
}

