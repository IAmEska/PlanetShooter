using UnityEngine;
using System.Collections;

public class FuelManager : MonoBehaviour
{
	public enum FuelStatus
	{
		Idle,
		Consume,
		Refill
	}

	public float maxFuelValue = 100f;
	public float consumePerSecond = 1f;
	public float refillPerSecond = 1f;

	protected FuelSystem fuelSystem;
	protected FuelStatus status;

	public bool HasFuel ()
	{
		return fuelSystem.HasFuel ();
	}

	public float FuelPercentage ()
	{
		return fuelSystem.FuelPercentage;
	}

	public void SetStatus (FuelStatus status)
	{
		this.status = status;
	}

	// Use this for initialization
	void Start ()
	{
		fuelSystem = new FuelSystem (maxFuelValue);
		status = FuelStatus.Consume;
	}

	public void Reset ()
	{
		fuelSystem.Refill (maxFuelValue);
		status = FuelStatus.Consume;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		switch (status) {
		case FuelStatus.Consume:
			if (fuelSystem.HasFuel ())
				fuelSystem.Consume (Time.deltaTime * consumePerSecond);
			else
				status = FuelStatus.Idle;
			break;

		case FuelStatus.Refill:
			if (!fuelSystem.IsFull ())
				fuelSystem.Refill (Time.deltaTime * refillPerSecond);
			else
				status = FuelStatus.Idle;
			break;
		}
	}
}
