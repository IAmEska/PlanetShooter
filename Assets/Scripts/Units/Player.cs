using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FuelManager))]

public class Player : Ship
{


	public delegate void FuelChange (float value);
	public event FuelChange FuelChanged;
	protected FuelManager fuelManager;

	public delegate void HealthChange (float value);
	public event HealthChange HealthChanged;

	protected Vector3 startPosition;
	protected float prevHealth;

	protected override void Init ()
	{
		base.Init ();
		startPosition = transform.position;
		fuelManager = GetComponent<FuelManager> ();
		healthManager.Damaged += (amount) => 
		{
			if (HealthChanged != null)
				HealthChanged (amount);
		};
	}

	protected override void RespawnBehaviour ()
	{
		transform.position = startPosition;
		fuelManager.Reset ();
		base.RespawnBehaviour ();
	}

	protected override void AliveBehaviour ()
	{
		if (Input.GetKey (KeyCode.Space)) {
			weaponManager.Fire ();
		}
		
		if (fuelManager.HasFuel ()) {
			movementManager.Move (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
			switch (movementManager.type) {
			case MovementManager.MoveType.Fly:
				fuelManager.SetStatus (FuelManager.FuelStatus.Consume);
				motorParticle.enableEmission = true;
				break;
			case MovementManager.MoveType.LandMove:
				fuelManager.SetStatus (FuelManager.FuelStatus.Refill);
				motorParticle.enableEmission = false;
				break;
			}
		} else {
			healthManager.TakeDamage (healthManager.maxHealth);
		}
		
		if (FuelChanged != null)
			FuelChanged (fuelManager.FuelPercentage ());



		if (Input.GetKeyDown (KeyCode.Q))
			weaponManager.SwitchWeapon (false);
		
		if (Input.GetKeyDown (KeyCode.W))
			weaponManager.SwitchWeapon (true);
		
		if (Input.GetKeyDown (KeyCode.A))
			weaponManager.SwitchSideWeapon (false);
		
		if (Input.GetKeyDown (KeyCode.S))
			weaponManager.SwitchSideWeapon (true);
	}

}
