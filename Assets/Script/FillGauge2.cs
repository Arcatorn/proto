using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillGauge2 : MonoBehaviour {

	float fillSpeed = 0.5f;
	Image fillBarComponent;
	Image chromaCircleComponent;
	bool isFilling = false;
	public GameObject fillBar;
	public GameObject chromaCircle;
	public GameObject chromaBar;
	public GameObject chromaBarFiller;
	Image chromaBarComponent;
	Color color_1 = new Color(0,1,1,1);
	Color color_2 = new Color(1,1,0,1);
	Color color_3 = new Color(1,0,1,1);
	float angleFromLaunch;
	Color actualColor;
	public MeshRenderer meshRenderer;
	public static float AvatarAngleColor = 0;
	int lastDepart = 0;
	Color[] horizBarColors = {new Color(0,1,1,0.5f), new Color(1,1,0,0.5f), new Color(1,0,1,0.5f)};

private void Awake() {
	fillBarComponent = fillBar.GetComponent<Image>();
	chromaCircleComponent = chromaCircle.GetComponent<Image>();
	chromaBarComponent= chromaBarFiller.GetComponent<Image>();
}

	void Update () {
		if (isFilling)
		{
			if (!chromaCircleComponent.enabled)
			{
				chromaCircleComponent.enabled = true;
			}
			if (fillBarComponent.fillAmount < 1){
				fillBarComponent.fillAmount += fillSpeed * Time.deltaTime;
				chromaBarComponent.fillAmount = fillBarComponent.fillAmount;
				HorizBarColorChange();
			}
			else
			{
				isFilling = false;
				ResetGauge();
			}

			if (Input.GetMouseButtonDown(0))
			{
				StopBar();
				meshRenderer.material.color = actualColor;
				ResetGauge();
				isFilling = false;
			}
		}
		else{
			LaunchBar();
		}

		if (Input.GetKeyDown(KeyCode.E) && isFilling)
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
		chromaBarComponent.fillAmount = 0;
	}

	void ActivateChromaCircle()
	{
			if (Input.GetMouseButton(1) || isFilling)
			{
				chromaCircleComponent.enabled = true;
			}
			else{
				chromaCircleComponent.enabled = false;
			}
	}

	void LaunchBar()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (lastDepart == 0)
			{
				fillBar.transform.rotation = Quaternion.Euler (0, 0, 0);
			}
			else if (lastDepart == 1)
			{
				fillBar.transform.rotation = Quaternion.Euler (0, 0, -120);
			}
			else if (lastDepart == 2)
			{
				fillBar.transform.rotation = Quaternion.Euler (0, 0, 120);
			}

			isFilling = true;
		}
	}

	void StopBar()
	{
			var pos = 0;
			if (fillBarComponent.fillAmount <= 0.361f)
			{
				pos += 1;
			}
			else if (fillBarComponent.fillAmount > 0.361f && fillBarComponent.fillAmount <= 0.641f)
			{
				pos += 2;
			}
			else if (fillBarComponent.fillAmount > 0.641f)
			{
				pos += 3;
			}

			pos += lastDepart;

			if (pos == 1 || pos == 4)
			{
				lastDepart = 1;
				actualColor = color_2;
			}
			else if (pos == 2 || pos == 5)
			{
				lastDepart = 2;
				actualColor = color_3;
			}
			else if (pos == 3)
			{
				lastDepart = 0;
				actualColor = color_1;
			}
	}

	void HorizBarColorChange()
	{
		var index = (lastDepart + Mathf.FloorToInt(fillBarComponent.fillAmount * 3) + 1);
		chromaBarComponent.color = horizBarColors[index%3];
	}

}
