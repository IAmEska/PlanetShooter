using UnityEngine;
using System.Collections;

public class FuelSystem
{

	protected float maxValue;
	protected float actualValue;

	public FuelSystem (float maxValue)
	{
		this.maxValue = maxValue;
		actualValue = maxValue;
	}

	public float FuelPercentage {
		get {
			return actualValue / maxValue;
		}
	}

	public void Refill (float value)
	{
		actualValue = Mathf.Min (maxValue, actualValue + value);
	}

	public void Consume (float value)
	{
		actualValue = Mathf.Max (0, actualValue - value);
	}

	public bool HasFuel ()
	{
		return actualValue > 0;
	}

	public bool IsFull ()
	{
		return actualValue == maxValue;
	}
}
