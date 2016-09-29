using UnityEngine;
using System.Collections;

using Pathfinding;

public class PathfindingAI : MonoBehaviour 
{
    public Vector3 targetPosition;

    public float speed;

    public float margin;

    private CharacterController controller;
    private Path path;
    private int pathIndex;

	void Start () 
    {
	    Seeker seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
        pathIndex = 0;
	}

    private void OnPathComplete(Path p)
    {
        if(!p.error)
            path = p;
    }

    void Update()
    {
        if (path == null) return;
        if (pathIndex >= path.vectorPath.Count) return;

        Vector3 destPosition = path.vectorPath[pathIndex];
        Vector3 direction = destPosition - transform.position;
        direction.Normalize();
        Vector3 velocity = direction * speed * Time.deltaTime;
        controller.SimpleMove(velocity);

        if (Vector3.Distance(transform.position, destPosition) < margin)
        {
            pathIndex++;
        }
    }
}
