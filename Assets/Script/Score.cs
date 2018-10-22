using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    void Update()
    {
        float x = Mathf.Abs(Generation.AgentsAngleColor);
        float y = Mathf.Abs(FillGauge.AvatarAngleColor);
        int a = Mathf.RoundToInt(x - y);
		a = Mathf.Abs(a);
        if (a > 180)
        {
            if (x > y)
                a = Mathf.RoundToInt(x - (y + 360));
            else
                a = Mathf.RoundToInt((x + 360) - y);
        }
        GetComponent<Text>().text = a.ToString();
    }
}
