using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
	float timerMax = 5;
	float timer = 0;
	public Color agtColor;
	public GameObject sol;
	Color color_1 = new Color(0,0.5f,1,1);
	Color color_2 = new Color(0.5f,1,0,1);
	Color color_3 = new Color(1,0,0.5f,1);
	public static float AgentsAngleColor = 0;
	public Text t;

	void Start () {
		InitGeneration();
	}

	void Update() {
		ChangeAgentsColor();
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

	 void ChangeAgentsColor()
	{

		timer += Time.deltaTime;
		if (timer >= timerMax)
		{
			Color nextColor =  ColorSelectionCalculation();
			for (int i =0; i <allAgents.Count; i++)
			{
				allAgents[i].go.GetComponent<MeshRenderer>().material.color = nextColor;
			}
			//sol.GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value,Random.value, 0.4f);
			timer = 0;
		}
	}

	Color ColorSelectionCalculation()
	{	
		float x = Random.Range(-360,0);
		AgentsAngleColor = x;
		Color actualColor;
		if (x >= -120)
		{
			var z = Mathf.Abs(x) / 120;
			actualColor = Color.Lerp(color_1, color_2, z);
		}
		else if (x < -120 && x >= -240)
		{
			var y = x + 120;
			var z = Mathf.Abs(y) / 120;
			actualColor = Color.Lerp(color_2, color_3, z);
		}
		else{
			var y = x + 240;
			var z = Mathf.Abs(y) / 120;
			actualColor = Color.Lerp(color_3, color_1, z);
		}
		return actualColor;
	}

}
