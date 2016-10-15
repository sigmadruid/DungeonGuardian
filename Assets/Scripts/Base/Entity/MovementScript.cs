using UnityEngine;

using System;

using Pathfinding;

namespace Base
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Seeker))]
    public class MovementScript : MonoBehaviour
    {
        public Action CallbackMoveStart;
        public Action CallbackMoveTurn;
        public Action CallbackMoveEnd;

        public float Speed = 3;

        public float AngularSpeed = 5;

        public float StopDistance = 0.1f;

        public bool EnableLog = false;

        private Path path;
        private int nextPathIndex;
        private Vector3 nextPosition;
        private Vector3 currentDirection;
        private Vector3 desiredDirection;

        private Seeker seeker;
        private CharacterController controller;

        void Start () 
        {
            seeker = GetComponent<Seeker>();
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            if (path == null) return;

            if (nextPathIndex < path.vectorPath.Count) 
            {
                nextPosition = path.vectorPath[nextPathIndex];
                desiredDirection = (nextPosition - transform.position).normalized;

                currentDirection = Vector3.Lerp(currentDirection, desiredDirection, Time.deltaTime * AngularSpeed);
                currentDirection.Normalize();
                controller.Move(desiredDirection * Speed * Time.deltaTime);

                if (MathUtils.XZDistance(transform.position, nextPosition) < StopDistance)
                {
                    nextPathIndex++;
                    if (nextPathIndex == path.vectorPath.Count)
                    {
                        transform.position = path.vectorPath[path.vectorPath.Count - 1];
                        if (CallbackMoveEnd != null) CallbackMoveEnd();
                        Log("path end");
                        path = null;
                    }
                    else
                    {
                        if (CallbackMoveTurn != null) CallbackMoveTurn();
                        Log("path turn");
                    }
                }
            }
        }

        public Vector3 Destination { get; private set; }

        public Vector3 Direction { get { return currentDirection; } }

        public void SetDestination(Vector3 destPosition)
        {
            if (destPosition == Vector3.zero)
            {
                path = null;
                if (CallbackMoveEnd != null) CallbackMoveEnd();
                Log("path end");
            }
            else if (destPosition != Destination)
            {
                Destination = destPosition;
                seeker.StartPath(transform.position, destPosition, OnPathComplete);
            }
        }
        private void OnPathComplete(Path p)
        {
            if(!p.error && p.vectorPath.Count > 1)
            {
                path = p;
                transform.position = path.vectorPath[0];
                nextPathIndex = 1;
                if (CallbackMoveStart != null) CallbackMoveStart();
                Log("path start");
            }
        }

        private void Log(string log)
        {
            if (EnableLog) BaseLogger.LogError(log);
        }
    }
}

