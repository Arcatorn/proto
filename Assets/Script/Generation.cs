using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agents
{
	public GameObject go;
	public NavMeshAgent nma;

	public Agents(GameObject go_)
	{
		go = go_;
	}

	public void NMA(NavMeshAgent nma_)
	{
		nma = nma_;
	}
}

public class Generation : MonoBehaviour {

	List<Agents> allAgents = new List<Agents>();
	public int nbAgentToGenerate;

	void Start () {
		InitGeneration();
	}

	void InitGeneration()
	{
		for (int i = 0; i < nbAgentToGenerate; i++)
		{
			Agents newAgent = new Agents(Instantiate(Resources.Load("Prefab/Agent_") as GameObject));
			newAgent.NMA(newAgent.go.GetComponent<NavMeshAgent>());
			newAgent.go.name = "Agent_" + i.ToString();
			newAgent.go.transform.position = RandomNavmeshLocation(8);
			allAgents.Add(newAgent);
		}
	}
	
	Vector3 RandomNavmeshLocation(float radius) {
         Vector3 randomDirection = Random.insideUnitSphere * radius;
         randomDirection += transform.position;
         NavMeshHit hit;
         Vector3 finalPosition = Vector3.zero;
         if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
             finalPosition = hit.position;            
         }
         return finalPosition;
     }
}
