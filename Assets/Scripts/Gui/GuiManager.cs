using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
	public Slider fuelSlider;
	public Slider healthSlider;

	public void SetFuelSliderPercentage (float value)
	{
		fuelSlider.value = Mathf.Max (fuelSlider.minValue, Mathf.Min (fuelSlider.maxValue, value));
	}

	public void SetHealthSliderPercentage (float value)
	{
		healthSlider.value = Mathf.Max (healthSlider.minValue, Mathf.Min (healthSlider.maxValue, value));
	}
}
