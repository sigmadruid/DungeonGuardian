using UnityEngine;

using System;
using System.Collections.Generic;

using Base;

namespace Logic
{
    [RequireComponent(typeof(MovementScript))]
    public class FighterScript : EntityScript
    {
        public bool EnableLog = false;

        public Action CallbackUpdate;
        public Action CallbackMoveStart;
        public Action CallbackMoveTurn;
        public Action CallbackMoveEnd;
        public Action<string> CallbackStateStart;
        public Action<string> CallbackStateEffect;
        public Action<string> CallbackStateEnd;

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
            animator.SetBool(AnimatorConstDef.PARAM_IS_MOVING, false);
        }
        public void Run()
        {
            animator.SetBool(AnimatorConstDef.PARAM_IS_MOVING, true);
        }
        public void DoSkill(int index)
        {
            animator.SetBool(AnimatorConstDef.PARAM_IS_MOVING, false);
            animator.SetTrigger(AnimatorConstDef.PARAM_SKILL + (index + 1).ToString());
        }
        public void Die()
        {
            animator.SetBool(AnimatorConstDef.PARAM_IS_MOVING, false);
            animator.SetTrigger(AnimatorConstDef.PARAM_DIE);
        }

        public void OnStateStart(string stateName)
        {
            if(CallbackStateStart != null) CallbackStateStart(stateName);
        }
        public void OnStateEffect(string stateName)
        {
            if(CallbackStateEffect != null) CallbackStateEffect(stateName);
        }
        public void OnStateEnd(string stateName)
        {
            if(CallbackStateEnd != null) CallbackStateEnd(stateName);
        }

        #endregion

        #region Movement

        public bool IsMoving { get { return movementScript.IsMoving; } }
        public Vector3 Direction { get { return movementScript.Direction; } }
        public Vector3 XZDirection { get { return MathUtils.XZDirection(movementScript.Direction); } }

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

        private void LogTest(string log)
        {
            if (EnableLog) BaseLogger.LogError(log);
        }
    }
}

