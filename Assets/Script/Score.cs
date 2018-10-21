using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {


	void Update () {
		int a = Mathf.RoundToInt(Generation.AgentsAngleColor - FillGauge.AvatarAngleColor);
		GetComponent<Text>().text = a.ToString();
	}
}
