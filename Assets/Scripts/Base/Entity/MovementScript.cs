using UnityEngine;

using System;

using Pathfinding;

namespace Base
{
    public class MovementScript : MonoBehaviour
    {
        public float Speed;

        public float StopDistance;

        private Path path;
        private int pathIndex;

        private Seeker seeker;
        private CharacterController controller;

        void Start () 
        {
            seeker = GetComponent<Seeker>();
            controller = GetComponent<CharacterController>();
            pathIndex = 0;
        }

        void Update()
        {
            if (path == null) return;
            if (pathIndex >= path.vectorPath.Count) return;

            Vector3 destPosition = path.vectorPath[pathIndex];
            Vector3 direction = destPosition - transform.position;
            direction.Normalize();
            Vector3 velocity = direction * Speed * Time.deltaTime;
            controller.Move(velocity);

            if (Vector3.Distance(transform.position, destPosition) < StopDistance)
            {
                pathIndex++;
            }
        }

        public Vector3 TargetPosition { get; private set; }

        public void SetDestination(Vector3 destPosition)
        {
            if (destPosition != TargetPosition)
            {
                TargetPosition = destPosition;
                pathIndex = 0;
                seeker.StartPath(transform.position, destPosition, OnPathComplete);
            }
        }
        private void OnPathComplete(Path p)
        {
            if(!p.error)
                path = p;
        }
    }
}

