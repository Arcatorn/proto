using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deplacement : MonoBehaviour
{
	NavMeshAgent agent;
    void Start()
    {
		agent = this.gameObject.GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
    }

    void Update()
    {
		leftStickDetection();
    }

    void leftStickDetection()
    {
        var yAxis = -Input.GetAxis("Vertical");
        var xAxis = Input.GetAxis("Horizontal");
        Vector2 input = new Vector2(xAxis, yAxis);

        if (Mathf.Abs(yAxis) > 0 || Mathf.Abs(xAxis) > 0 )
        {
            if (input.sqrMagnitude > 1)
            {
                input = input.normalized;
            }
			 var destination = transform.position + new Vector3(xAxis, transform.position.y, yAxis);
        	agent.SetDestination(destination);
        } 
    }
}
