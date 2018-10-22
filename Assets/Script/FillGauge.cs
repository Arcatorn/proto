using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillGauge : MonoBehaviour {

	float fillSpeed = 0.5f;
	Image fillBarComponent;
	Image chromaCircleComponent;
	bool isFilling = false;
	public GameObject fillBar;
	public GameObject chromaCircle;
	Color color_1 = new Color(0,0.5f,1,1);
	Color color_2 = new Color(0.5f,1,0,1);
	Color color_3 = new Color(1,0,0.5f,1);
	float angleFromLaunch;
	Color actualColor;
	public MeshRenderer meshRenderer;
	public static float AvatarAngleColor = 0;

private void Awake() {
	fillBarComponent = fillBar.GetComponent<Image>();
	chromaCircleComponent = chromaCircle.GetComponent<Image>();
}

	void Update () {
		if (Input.GetButtonDown("Start"))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }

        }

		if (isFilling)
		{
			if (!chromaCircleComponent.enabled)
			{
				chromaCircleComponent.enabled = true;
			}
			if (fillBarComponent.fillAmount < 1){
				fillBarComponent.fillAmount += fillSpeed * Time.deltaTime;
			}
			else
			{
				isFilling = false;
				ResetGauge();
			}

			if (Input.GetKeyDown(KeyCode.Joystick1Button5))
			{
				ColorSelectionCalculation(fillBarComponent.fillAmount);
				meshRenderer.material.color = actualColor;
				ResetGauge();
				isFilling = false;
			}
		}
		else{
			RightStickDetection();
		}

		if (Input.GetKeyDown(KeyCode.Joystick1Button5) && isFilling)
		{
			ResetGauge();
			isFilling = false;
		}

		ActivateChromaCircle();
	}

	void ResetGauge()
	{
		fillBar.transform.Rotate(Vector3.zero);
		fillBarComponent.fillAmount = 0;
	}

	void ActivateChromaCircle()
	{
		if (!chromaCircleComponent.enabled)
			{
				if (isFilling){
				chromaCircleComponent.enabled = true;
				}
				else if (Input.GetAxis("TriggersR") != 0)
				{
					chromaCircleComponent.enabled = true;
				}
			}
			else {
				if (Input.GetAxis("TriggersR") == 0 && !isFilling)
				{
					chromaCircleComponent.enabled = false;
				}
			}
	}

	void RightStickDetection()
	{
		var yAxis = -Input.GetAxis("R_YAxis");
		var xAxis = Input.GetAxis("R_XAxis");
		Vector2 input = new Vector2(xAxis, yAxis);

		if ((yAxis > 0.5f ||
			yAxis < -0.5f ||
			xAxis > 0.5f ||
			xAxis <-0.5f) && !isFilling)
			{
				if (input.sqrMagnitude > 1)
				{
					input = input.normalized;
				}

				float targetAngle = (Mathf.Atan2 (input.y, input.x) * Mathf.Rad2Deg) - 90;
				fillBar.transform.rotation = Quaternion.Euler (0, 0, targetAngle);
				angleFromLaunch = targetAngle;
				isFilling = true;
			}
	}

	void ColorSelectionCalculation(float amountFilled)
	{	
		float x = (angleFromLaunch - 90) - amountFilled * 360;
		if (x < -360)
		{
			x += 360;
		}
		AvatarAngleColor = x;
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
		
	}
}
