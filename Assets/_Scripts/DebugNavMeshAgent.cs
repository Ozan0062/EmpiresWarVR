using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DebugNavMeshAgent : MonoBehaviour
{
    NavMeshAgent agent;

    public bool velocity;
    public bool desiredVelocity;
    public bool path;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void OnDrawGizmos()
    {
        if (velocity)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + agent.velocity);
        }
        if (desiredVelocity)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + agent.velocity);
        }
        if (path)
        {
            Gizmos.color = Color.black; 
            var agentPath = agent.path;
            Vector3 previousCorner = transform.position;
            foreach (var corner in agentPath.corners)
            {
                Gizmos.DrawLine(previousCorner, corner);
                Gizmos.DrawWireSphere(corner, 0.1f);
                previousCorner = corner;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
