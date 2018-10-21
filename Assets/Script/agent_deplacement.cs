using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class agent_deplacement : MonoBehaviour
{
	NavMeshAgent agent;
	Vector3 destination;
    void Start()
    {
		destination = RandomNavmeshLocation(10);
		agent = this.gameObject.GetComponent<NavMeshAgent>();
		agent.SetDestination(destination);
    }

    void Update()
    {
		if((transform.position - destination).magnitude < 0.2f)
		{
			destination = RandomNavmeshLocation(10);
			agent.SetDestination(destination);
		}
    }

    Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += Vector3.zero;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
		finalPosition.y = 0;
        return finalPosition;
    }
}
