using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravaneManager : MonoBehaviour
{
	Rigidbody rigid;
	float CaravaneSpeed = 50f;
	void Awake()
	{
		rigid = GetComponent<Rigidbody>();
		rigid.velocity = new Vector3(0,0,CaravaneSpeed*Time.deltaTime);
	}
}
