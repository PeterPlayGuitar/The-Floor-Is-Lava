using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualitySliderBehaviour : MonoBehaviour
{
	public Slider slider;
	public Image image;

	void Start()
	{
		slider.value = QualitySettings.GetQualityLevel();

		SliderValueChanged();
	}

	public void SliderValueChanged()
	{
		Color color = Color.green;

		switch (slider.value)
		{
			case 0:
				color = Color.grey;
				break;
			case 1:
				color = Color.red;
				break;
			case 2:
				color = Color.yellow;
				break;
			case 3:
				color = Color.green;
				break;
		}

		image.color = color;
	}

	public void SetQuality()
	{
		print((int)slider.value);
		QualitySettings.SetQualityLevel((int)slider.value);
	}
}
